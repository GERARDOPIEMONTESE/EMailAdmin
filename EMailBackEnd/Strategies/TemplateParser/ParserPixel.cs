using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Home;
using System.Reflection;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Strategies.TemplateParser
{
    public class ParserPixel : ITemplateParserStrategy
    {
        string[] config = new string[] { "PixelTID", "PixelURL" };

        string[] excluir = new string[] { "PixelURL" };

        public string Parse(TemplateParserContext context)
        {
            if (context.Dto.IsPreview)
            {
                return "Pixel " + context.Name + GetPixel(context);
            }

            return GetPixel(context);
        }

        private string GetPixel(TemplateParserContext context)
        {
            var Pixel = DAOLocator.Instance().GetDaoPixel().Get(context.Name);
            Pixel.pixel.UID = (context.Dto.IsPreview?"UID": context.Dto.Log.Id.ToString());

            string imgPixel = "<img height='1' width='1' style='border-style:none;visibility:hidden;' alt='' src='{URL}?{PARAMS}'>";

            string rst = imgPixel;
            rst = rst.Replace("{PARAMS}", GetParametros(Pixel.pixel));
            rst = rst.Replace("{URL}", Pixel.pixel.PixelURL);

            context.Dto.AddTrackEmail(Pixel.pixel.ConvertToTrackEmail());

            return rst;
        }

        private string GetParametros(Domain.PixelData Pixel)
        {
            List<string> parametros = new List<string>();
            var typeObj = Pixel.GetType();

            foreach (var prop in typeObj.GetProperties())
            {
                string valor = prop.GetValue(Pixel, null).ToString();

                if (config.Contains(valor))
                {
                    valor = ConfigurationValueHome.Obtener(prop.Name);
                    prop.SetValue(Pixel, valor, null);
                }

                if (!excluir.Contains(prop.Name))
                    parametros.Add(prop.Name.ToLower() + "=" + valor);
            }

            return string.Join("&", parametros);            
        }

    }
}
