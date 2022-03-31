using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CapaNegocioDatos.CapaHome;
using CapaNegocioDatos.CapaNegocio;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.DTO.EkitBenefits;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Properties;
using EMailAdmin.BackEnd.Reports.Objects;
using EMailAdmin.BackEnd.Utils;
using iTextSharp.text;
using iTextSharp.text.pdf;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.BackEnd.Strategies.Attachment
{
    public class MainDownloadAttachStrategy : AttachStrategy, IAttachStrategy
    {
        #region Constants

        private const string ATTACH_NAME = "ASSISTCARD-Info.pdf";

        private const string ATTACH_TYPE = "application/pdf";

        #endregion Constants

        public MainDownloadAttachStrategy()
        {
            ATTACHNAME = ATTACH_NAME;
            ATTACHTYPE = ATTACH_TYPE;
        }

        #region Methods

        private byte[] GetAttachContent(AbstractEMailDTO dto)
        {
            try
            {
                IdLanguage = dto.IdLanguage;
                IdStrategy = dto.IdStrategy;

                string url = GetUrlDownload();                
                url = ReplaceVariables(url, dto);
                byte[] report;
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    report = client.DownloadData(url);

                    if (System.Text.Encoding.UTF8.GetString(report).Contains(GetKeyError()) == true)
                        throw new Exception("ERROR GENERACION PDF");
                }

                return report;
            }
            catch (Exception ex)
            {
                throw ex;
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

                byte[] content = GetAttachContent(dto);

                var item = new AttachmentItem
                {
                    Name = GetAttachName(),
                    Description = GetAttachName(),
                    Type = GetAttachType(),
                    Language = IdiomaHome.Obtener(dto.IdLanguage),
                    Content = content,
                    Dimenssion = content == null ? 0 : content.Length
                };
                items.Add(item);
            }
            catch { }

            return items;
        }

        #endregion
    }

}
