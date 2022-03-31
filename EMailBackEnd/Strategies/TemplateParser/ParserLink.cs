using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Properties;
using EMailAdmin.BackEnd.Home;
using System.Collections.Generic;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.ExternalServices.Service;
using Newtonsoft.Json;
using EmailAdmin.Dto;
using DTOMapper;
using EMailAdmin.BackEnd.Utils;
using DTOMapper.Helpers;

namespace EMailAdmin.BackEnd.Strategies.TemplateParser
{
    public class ParserLink : ITemplateParserStrategy
    {
        #region ITemplateParserStrategy Members

        public Link link { get; set; }
        
        private string LinkFromContext(TemplateParserContext context)
        {
            return context.Links;
        }

        public string Parse(TemplateParserContext context)
        {
            string sUrlLink = "";
            
            string rstLink = ParseLink(context);

            if (link!=null && link.Id != 0 && !context.Dto.IsPreview && !link.LinkType.Code.Equals(LinkType.CONTEXT))
            {
                var tracklink = ConfigurationValueHome.GetByCode("URLTRACKLINK");
                if (tracklink != null && tracklink.Id > 0)
                {
                    string sTrackLink = tracklink.Value;
                    sTrackLink = sTrackLink.Replace("{URLDESTINO}", link.Url.Replace('&', '|'));
                    sTrackLink = sTrackLink.Replace("{IDLINK}", link.Id.ToString());
                    sTrackLink = sTrackLink.Replace("{IDEMAILLOG}", context.Dto.Log.Id.ToString());
                    sUrlLink = sTrackLink;

                    TrackLink tl = new TrackLink()
                    {
                        IdLink = link.Id,
                        UrlDestino = sUrlLink
                    };

                    context.Dto.AddLinkTrack(tl);
                }
                else
                    sUrlLink = link.Url;


                if (!string.IsNullOrEmpty(link.Style))
                    rstLink = rstLink.Replace("{STYLE}", link.Style);
                else
                    rstLink = rstLink.Replace("style={STYLE}", "");

                rstLink = rstLink.Replace("{IMG_STYLE}", link.ImageStyle);
                rstLink = rstLink.Replace("{URLLINK}", sUrlLink);
            }

            return rstLink;
        }

        public string ParseLink(TemplateParserContext context)
        {
            link = DAOLocator.Instance().GetDaoLink().Get(context.Name);

            if (context.Dto.IsPreview)
            {
                return linkHTMLPreview(link, context);
            }

            string urlLink = EMailBodyUtil.ProccessContextData(context.Dto, link.Url);
            
            link.Url = urlLink;

            string deepLink = "";
            if (IsConvertToDeepLink(link, out deepLink))            
                link.Url = deepLink;

            if (link.Id != 0 && link.LinkType.Code.Equals(LinkType.FIXED))
            {   
                if (!string.IsNullOrEmpty(link.ImageName))
                {
                    if (link.ImageEncode)
                        return "<a style={STYLE} href='{URLLINK}'>" + link.ImageName + "</a>";
                    else
                    {
                        string imgLink = "<img {IMG_STYLE} src='" + link.ImageURL + "'/>";
                        return "<a style={STYLE} href='{URLLINK}'>" + imgLink + "</a>";
                    }
                }
                else
                {
                    return "<a style={STYLE} href='{URLLINK}'>" + link.GetDisplayText(context.Dto.IdLanguage) + "</a>";
                }              
            }

            if (link.Id != 0 && link.LinkType.Code.Equals(LinkType.QR))
            {
                string urlQRLink = ConfigurationValueHome.GetHandlerQR() + "?" + link.Url;

                return "<img {IMG_STYLE} src='" + urlQRLink + "'/>";
            }
            
            return link.Id == 0 ? "" : LinkFromContext(context);
        }

        public string linkHTMLPreview(Link link, TemplateParserContext context)
        {
            string urlImage = "";
            if (link.Id != 0 && link.LinkType.Code.Equals(LinkType.FIXED))
            {
                if (link.ImageEncode)
                    urlImage = ParserContentImage.GetUrlImage(context);
                else
                    urlImage = link.ImageURL;
            }

            if (link.Id != 0 && link.LinkType.Code.Equals(LinkType.QR))
            {
                urlImage = ConfigurationValueHome.GetHandlerQR() + "?"+ link.Url;
            }

            string linkText = link.GetDisplayText(context.Dto.IdLanguage);

            string urlLink = link.Url;

            return linkHTML(urlLink, link.Style, linkText, urlImage, link.ImageStyle);
        }

        public string linkHTML(string urlLink, string styleLink, string linkText, string urlImage, string styleImg)
        {
            string linkContent = "<img {IMG_STYLE} src='" + urlImage + "'/>"; 
            string lnkHtml = "<a style={STYLE} href='{URLLINK}'>{LINK_CONTENT}</a>";

            lnkHtml = lnkHtml.Replace("{STYLE}", styleLink);
            lnkHtml = lnkHtml.Replace("{URLLINK}", urlLink);
            lnkHtml = lnkHtml.Replace("{LINK_CONTENT}", linkContent);
            lnkHtml = lnkHtml.Replace("{IMG_STYLE}", styleImg);
            return lnkHtml;
        }

        public IDictionary<string, string> GetValues(string valor)
        {
            IDictionary<string, string> values = new Dictionary<string, string>();

            string[] s = valor.Split('|');
            foreach (var item in s)
            {
                string key = item.Split('=')[0];
                string itemvalor = item.Substring(key.Length + 1);

                values.Add(key, itemvalor);
            }

            return values;
        }
        
        #endregion

        private bool IsConvertToDeepLink(Link link, out string deepLink)
        {
            deepLink = ""; //deja la url original

            try
            {
                if (!link.EnabledDeepLink)
                    return false;
                else
                {
                    string urlLink = link.UrlDeepLink;
                    if (IsValidLinkByDeepLink(urlLink))
                    {
                        string urlDeepLink = ConvertToDeepLink(urlLink);
                        deepLink = link.Url.Replace(urlLink, urlDeepLink); //para que quede el deeplink pero con los parametros de estilo si los tuviera
                        return true;
                    }
                    else
                        return false;
                }
            }
            catch
            {
                return false;
            }
        }
        
        private static bool IsValidLinkByDeepLink(string link)
        {
            bool bOk = false;
            ConfigurationValue configDeepLinkWhiteList = ConfigurationValueHome.GetByCode("DeepLinkWhiteList");
            if (configDeepLinkWhiteList.Id > 0)
            {
                string DeepLinkWhiteList = configDeepLinkWhiteList.Value;
                if (!string.IsNullOrEmpty(DeepLinkWhiteList))
                {
                    string[] _lstDeepLinkWhiteList = DeepLinkWhiteList.Split(';');
                    foreach (var item in _lstDeepLinkWhiteList)
                    {
                        if (link.Contains(item.Replace("*", "")))
                        {
                            bOk = true;
                            break;
                        }
                    }
                }
                return bOk;
            }
            else
                return true;  //si no esta configurado que pase y lo valide en myac
        }

        private string ConvertToDeepLink(string originalUrl)
        {

            string urlDeepLink = ConfigurationValueHome.Obtener("UrlCreateDeepLink");
            
            DTOMapper.DTOFilter filter = new DTOMapper.DTOFilter();
            filter.Parameters.Add("OriginalUrl", originalUrl);

            string information = ExternalServiceLocator.Instance().
                   GetExternalDynamicInformationService().GetInformation(urlDeepLink, JsonConvert.SerializeObject(filter));

            return ResponseTypesHelper.ResponseScalar<string>(information);
        }
    }
}
