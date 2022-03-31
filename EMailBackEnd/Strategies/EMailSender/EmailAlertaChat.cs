using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Service;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Properties;

namespace EMailAdmin.BackEnd.Strategies.EMailSender
{
    public class EmailAlertaChat : EMailSenderDefault
    {
        protected override void CompleteDto(DTO.AbstractEMailDTO dto)
        {
            ServiceLocator.Instance().GetAlertChatService().CompleteInformation(dto);
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
            string xml = SerializeObject((EmailAlertChatsDTO)dto);

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
