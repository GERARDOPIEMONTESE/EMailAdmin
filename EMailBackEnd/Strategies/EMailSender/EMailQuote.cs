using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Properties;
using EMailAdmin.BackEnd.DTO;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using EMailAdmin.BackEnd.Service;
using AssistCard.ServerMSG.Message;
using EMailAdmin.BackEnd.Domain;
using CapaNegocioDatos.CapaNegocio;
using CapaNegocioDatos.CapaHome;

namespace EMailAdmin.BackEnd.Strategies.EMailSender
{
    public class EMailQuote : EMailSenderDefault
    {
        protected override void CompleteDto(AbstractEMailDTO dto)
        {
            dto.TemplateType = TemplateTypeHome.GetEMailQuote();
            dto.ApplicationUrl = ConfigurationValueHome.GetApplicationUrl();
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

        protected override void Proccess(AbstractEMailDTO dto, Template template, EMailLog log)
        {
            EMailAddress fromAddress = EMailAddressHome.Get(template.IdEMailFromAddress);
            EmailQuoteDTO dtoQuote = (EmailQuoteDTO)dto;
            if (dtoQuote.From != null && dtoQuote.From != "")
            {
                fromAddress.Address = dtoQuote.From;
            }
            log.MailFrom = fromAddress.Address != null ? fromAddress.Address : "-";
            log.MailTo = dto.To != null ? dto.To : dto.Bcc;
            log.ErrorMessage = "";

            try
            {
             
                    if (template.Id != 0)
                    {
                        dto.AssociationGroupDto = new AssociationGroupDTO();
                        Pais country = PaisHome.ObtenerPorCodigo(dto.CountryCode.ToString());
                        dto.AssociationGroupDto.IdLocation = country.IdLocacion;

                        dto.EMailBody = ProccessContextData(dto, ProccessData(dto, template));
                        dto.Header = (byte[])template.GetContent(dto.IdLanguage).Header.Content;
                        dto.Footer = (byte[])template.GetContent(dto.IdLanguage).Footer.Content;
                        //dto.HeaderPDF = (byte[])template.GetContent(dto.IdLanguage).HeaderPDF.Content;
                        //dto.FooterPDF = (byte[])template.GetContent(dto.IdLanguage).FooterPDF.Content;
                        log.Subject = template.GetSubject(dto.IdLanguage);
                        log.Body = dto.EMailBody;
                        if (dto.MailFromAppearance == "[FROMADDRESSNAME]")
                            dto.MailFromAppearance = (fromAddress.Address != null ? fromAddress.Name : string.Empty);

                        bool sentMail = Messaging.SendMailThread(fromAddress.Address != null ? fromAddress.Address : "-",
                            dto.To, dto.Cc, dto.Bcc, template.GetSubject(dto.IdLanguage),
                            dto.EMailBody, new List<System.Net.Mail.Attachment>() /*GetMailAttachments(dto, template, log)*/,
                            dto.MailFromAppearance != null ? dto.MailFromAppearance : string.Empty);

                        log.ErrorMessage = sentMail ? "" : "Error in mail - To: " + dto.To;
                    }
                

                ServiceLocator.Instance().GetEMailLogService().FinishLog(template, log);
            }
            catch (Exception ex)
            {
                log.ErrorMessage = ex.Message + " - " + ex.StackTrace;
                ServiceLocator.Instance().GetEMailLogService().FinishLog(template, log);
                throw ex;
            }
        }

        protected override string ProccessContextData(AbstractEMailDTO dto, string body)
        {

            string xml = SerializeObject((EmailQuoteDTO)dto);

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
    }
    }

