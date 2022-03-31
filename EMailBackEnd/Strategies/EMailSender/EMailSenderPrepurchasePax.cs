using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Service;
using System.Configuration;
using System.Xml;
using EMailAdmin.BackEnd.Properties;
using System.IO;
using System.Xml.Serialization;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Strategies.EMailSender
{
    public class EMailSenderPrepurchasePax : EMailSenderDefault
    {
        protected override void CompleteDto(DTO.AbstractEMailDTO dto)
        {
            EMailPrepurchasePaxDTO ekitDto = (EMailPrepurchasePaxDTO)dto;
            ServiceLocator.Instance().GetPrepurchasePaxService().CompleteInformation(dto);
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

        private static String UTF8ByteArrayToString(Byte[] characters)
        {
            var encoding = new UTF8Encoding();
            var constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        protected override string ProccessContextData(AbstractEMailDTO dto, string body)
        {
            string xml = SerializeObject((EMailPrepurchasePaxDTO)dto);

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

        private void SaveEmailLogPrepurchasePax(EMailPrepurchasePaxDTO ppdto, Domain.EMailLog log)
        {        
            EmailLog_R_PrepurchasePax logppax = new EmailLog_R_PrepurchasePax()
            {
                EmailLog = new EMailLog() { Id = log.Id },
                CodigoPaxBox = ppdto.BoxPaxCode,
                CodigoVerif = ppdto.BoxPaxCodeVerifier,
                VoucherGroup = ppdto.groupVoucher,
                Pais = CapaNegocioDatos.CapaHome.PaisHome.ObtenerPorCodigo(ppdto.CountryCode.ToString()),
            };
            logppax.Persistir();
        }

        protected override void SendAndProcessEMail(AbstractEMailDTO dto, Domain.EMailLog log)
        {
            SaveEmailLogPrepurchasePax(((EMailPrepurchasePaxDTO)dto), log);
            base.SendAndProcessEMail(dto, log);
        }
               

        public override void SaveErrorLog(Exception ex, AbstractEMailDTO dto, EMailLog log)
        {
            log.TemplateName = dto.TemplateType.Descripcion;
            log.MailTo = ((EMailPrepurchasePaxDTO)dto).PaxEMail;
            SaveEmailLogPrepurchasePax(((EMailPrepurchasePaxDTO)dto), log);
            base.SaveErrorLog(ex, dto, log);
            throw ex;
        }
    }
}
