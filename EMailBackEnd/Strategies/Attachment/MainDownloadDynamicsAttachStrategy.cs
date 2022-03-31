using CapaNegocioDatos.CapaHome;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMailAdmin.BackEnd.Strategies.Attachment
{
    public class MainDownloadDynamicsAttachStrategy: AttachStrategy, IAttachStrategy
    {
        #region Constants

        private const string ATTACH_NAME = "ASSISTCARD-Info.pdf";

        private const string ATTACH_TYPE = "application/pdf";

        #endregion Constants

        public MainDownloadDynamicsAttachStrategy()
        {
            ATTACHNAME = ATTACH_NAME;
            ATTACHTYPE = ATTACH_TYPE;
        }

        #region Methods

        private byte[] GetAttachContentByUrl(string url)
        {
            try
            {                
                byte[] report;
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    report = client.DownloadData(url);

                    if (System.Text.Encoding.UTF8.GetString(report).Contains(GetKeyError()) == true)
                        return null;
                }

                return report;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private string ReplaceVariables(string url, AbstractEMailDTO dto)
        {            
            var urlRst = url;
            try
            {
                int idxInit = 0;
                int idxEnd = 0;

                int lastIdx = url.LastIndexOf('{');

                while (idxInit<=lastIdx)
                {
                    idxInit = url.IndexOf('{', idxInit) + 1;

                    idxEnd = url.IndexOf('}', idxInit);

                    string variable = url.Substring(idxInit, (idxEnd - idxInit));
                    object valProp = ((EMailEkitDTO)dto).GetType().GetProperty(variable).GetValue((EMailEkitDTO)dto, null);
                    string valor = valProp.ToString();

                    urlRst = urlRst.Replace("{" + variable + "}", valor);

                    idxInit = idxEnd;
                }
            }
            catch {}

            return urlRst;
        }

        public IList<AttachmentItem> GetAttachmentItems(AbstractEMailDTO dto)
        {
            IList<AttachmentItem> items = new List<AttachmentItem>();

            try
            {

                IdLanguage = dto.IdLanguage;
                IdStrategy = dto.IdStrategy;

                string jsonListName = _estrategy.UrlDownload; //nombre de la lista en el json
                DynamicItemDTO tableData = ((DynamicDTO)dto).GetDicValue(jsonListName);

                dynamic listData = tableData.Value;

                foreach (var itemAttach in listData)
                {
                    DynamicDTO dataTr = new DynamicDTO();
                    dataTr.Convert(itemAttach);

                    object url = dataTr.GetDicValue("URL").Value;
                    object AttachName = dataTr.GetDicValue("Name").Value;


                    if (url != null && AttachName != null)
                    {
                        string attachType = AttachName.ToString().Split('.').LastOrDefault();

                        byte[] content = GetAttachContentByUrl(url.ToString());
                        if (content != null)
                        {
                            var item = new AttachmentItem
                            {
                                Name = AttachName.ToString(),
                                Description = AttachName.ToString(),
                                Type = attachType,
                                Language = IdiomaHome.Obtener(dto.IdLanguage),
                                Content = content,
                                Dimenssion = content == null ? 0 : content.Length
                            };
                            items.Add(item);
                        }
                    }
                }
            }
            catch { }

            return items;
        }

        #endregion
    }

}
