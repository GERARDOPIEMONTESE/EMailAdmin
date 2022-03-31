using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Home;
using System.Configuration;
using EMailAdmin.BackEnd.Domain;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using EMailAdmin.BackEnd.Properties;
using EMailAdmin.BackEnd.Service;

namespace EMailAdmin.BackEnd.Strategies.EMailSender
{
    public class EmailSenderACCOMQuote : EMailSenderDefault
    {
        protected override void CompleteDto(DTO.AbstractEMailDTO dto)
        {
            ACCOMQuoteDTO AccomQuoteDto = (ACCOMQuoteDTO)dto;

            AccomQuoteDto.MailFromAppearance = ConfigurationManager.AppSettings["ACCOMQuoteMailFromAppearance"].ToString(); //"pruebadecumples@assist-card.com";
            AccomQuoteDto.TemplateType = TemplateTypeHome.Get("AQ");
            //Step Quote
            if (AccomQuoteDto.PurchaseProcessCode.Equals("ST2") || AccomQuoteDto.PurchaseProcessCode.Equals("ST3"))
            {
                AccomQuoteDto.QuoteMailACCOM = QuoteMailACCOMHome.GetPendingQuoteMailById(AccomQuoteDto.IdPendingQuoteMail);
                dto.To = AccomQuoteDto.QuoteMailACCOM.Email;
                AccomQuoteDto.HtmlBody = AccomQuoteDto.QuoteMailACCOM.HTMLBody;
            }
            //Step PrePurchase
            else
            {
                AccomQuoteDto.QuoteMailACCOM = QuoteMailACCOMHome.GetPrePurchaseQuoteMailById(AccomQuoteDto.IdPendingQuoteMail);
                //if (AccomQuoteDto.QuoteMailACCOM.CountryCode == 540) //Argentina
                if (HasMailAssigned(AccomQuoteDto.QuoteMailACCOM.CountryCode.ToString()))
                    dto.To = ConfigurationManager.AppSettings[string.Format("TLMKT{0}", AccomQuoteDto.QuoteMailACCOM.CountryCode)].ToString(); //ConfigurationManager.AppSettings["TelemarketingMailToArg"].ToString();
                else
                    dto.To = ConfigurationManager.AppSettings["TLMKTWorld"].ToString();

                dto.Bcc = ConfigurationManager.AppSettings["TLMKTBCC"].ToString();
                AccomQuoteDto.HtmlBody = AccomQuoteDto.QuoteMailACCOM.HTMLBody;
            }
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
            string xml = SerializeObject((ACCOMQuoteDTO)dto);

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

        private bool HasMailAssigned(string countryCode)
        {
            bool result = false;
            var codes = ConfigurationManager.AppSettings["QuoteMailCountries"].Split(';');
            foreach (var code in codes)
            {
                if (code == countryCode)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
