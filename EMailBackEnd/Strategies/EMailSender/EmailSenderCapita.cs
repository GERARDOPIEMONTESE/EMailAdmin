using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Properties;
using EMailAdmin.BackEnd.DTO;
using CapaNegocioDatos.CapaHome;
using CapaNegocioDatos.CapaNegocio;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.BackEnd.Domain;
using CapaNegocioDatos.Utilitarios;
using EMailAdmin.BackEnd.DTO.EkitBenefits;

namespace EMailAdmin.BackEnd.Strategies.EMailSender
{
    public class EMailSenderCapita : EMailSenderDefault
    {
        protected override void CompleteDto(DTO.AbstractEMailDTO dto)
        {
            ServiceLocator.Instance().GetCapitaService().CompleteInformation(dto);
        }

        private static String UTF8ByteArrayToString(Byte[] characters)
        {
            var encoding = new UTF8Encoding();
            var constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        private String SerializeObject(Object pObject)
        {
            try
            {
                var memoryStream = new MemoryStream();
                var xs = new XmlSerializer(pObject.GetType());
                var xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                xs.Serialize(xmlTextWriter, pObject);
                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
                string xmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
                return xmlizedString;
            }
            catch (Exception e) { Console.WriteLine(e); return null; }
        }

        protected override string ProccessContextData(AbstractEMailDTO dto, string body)
        {
            string xml = SerializeObject((EmailCapitaDTO)dto);

            var xDoc = new XmlDocument();
            xDoc.LoadXml(xml.Substring(xml.IndexOf("?>") + 2));

            var variableInitTag = Settings.Default["VariableInitTag"].ToString();
            var variableEndTag = Settings.Default["VariableEndTag"].ToString();

            IList<string> textsToReplace = ServiceLocator.Instance().GetTemplateService().GetVariableTags(body);

            foreach (string textToReplace in textsToReplace)
            {
                string tag = textToReplace.Replace(variableInitTag, "").Replace(variableEndTag, "");
                XmlNodeList nodes = xDoc.GetElementsByTagName(tag.Trim());

                body = body.Replace(textToReplace, nodes.Count == 0 ? textToReplace : nodes[0].InnerText);
            }

            return body;
        }

        private void SaveEmailLogCapita(EmailCapitaDTO capitadto, Domain.EMailLog log)
        {
            EmailLog_R_Capita logcapita = new EmailLog_R_Capita()
            {
                EmailLog = new EMailLog() { Id = log.Id },
                Apellido = capitadto.Apellido,
                Nombre = capitadto.Nombre,
                TipoDocumento = (capitadto.PaxType!=""? CapaNegocioDatos.CapaHome.TipoDocumentoHome.Obtener(capitadto.PaxType): null),
                Documento = capitadto.PaxPassport,
                bEnvioLinks = (capitadto.BenefitsText != ""),
                Pais = CapaNegocioDatos.CapaHome.PaisHome.ObtenerPorCodigo(capitadto.CountryCode.ToString()),
                ProductCode = capitadto.ProductCode,
                ProductName = capitadto.ProductName,
                RateCode = capitadto.RateCode,
                RateName = capitadto.RateName
            };
            logcapita.Persistir();
        }

        protected override void SendAndProcessEMail(AbstractEMailDTO dto, Domain.EMailLog log)
        {
            bool HayLinks = (((EmailCapitaDTO)dto).BenefitsText!="");
            if (HayLinks)
            {
                SaveEmailLogCapita(((EmailCapitaDTO)dto), log);
                base.SendAndProcessEMail(dto, log);
            }
            else
                throw new Exception("NOT_CAPITA_LINKS");
        }

        public override void SaveErrorLog(Exception ex, AbstractEMailDTO dto, EMailLog log)
        {
            log.TemplateName = dto.TemplateType.Descripcion;
            SaveEmailLogCapita(((EmailCapitaDTO)dto), log);
            base.SaveErrorLog(ex, dto, log);
            throw ex;
        }        
    }
}
