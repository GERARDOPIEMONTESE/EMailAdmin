using System.Xml;

namespace EMailAdmin.BackEnd.Strategies.TemplateParser
{
    public class ParserVariableText : ITemplateParserStrategy
    {
        #region ITemplateParserStrategy Members

        public string Parse(TemplateParserContext context)
        {
            if (context.Dto.IsPreview)
            {
                return context.Name;
            }

            var xDoc = new XmlDocument();
            xDoc.LoadXml(context.Xml);

            var nodes = xDoc.GetElementsByTagName(context.Name);
            return nodes.Count == 0 ? context.ReplaceText : nodes[0].InnerText;
        }

        #endregion
    }
}
