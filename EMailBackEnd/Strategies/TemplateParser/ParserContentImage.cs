using EMailAdmin.BackEnd.Properties;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.BackEnd.Strategies.TemplateParser
{
    public class ParserContentImage : ITemplateParserStrategy
    {
        #region ITemplateParserStrategy Members

        public static string GetUrlImage(TemplateParserContext context)
        {
            return ConfigurationValueHome.GetContentImageUrl() + "?IdContent=" +
                   context.Template.GetContent(context.Dto.IdLanguage).Id;
        }

        public string Parse(TemplateParserContext context)
        {
            if (context.Dto.IsPreview)
            {
                return "<img src='" + ConfigurationValueHome.GetContentImageUrl() + "?IdContent=" +
                    context.Template.GetContent(context.Dto.IdLanguage).Id +
                    "&Name=" + context.Name +
                    "&IdLanguage=" + context.Dto.IdLanguage +
                    "&IsPreview=true' alt='" + context.Name + "' />";
            }
            return "<img src='" + ConfigurationValueHome.GetContentImageUrl() + "?IdContent=" +
                context.Template.GetContent(context.Dto.IdLanguage).Id + 
                "&Name=" + context.Name +
                "&IdLanguage=" + context.Dto.IdLanguage + 
                "' alt='" + context.Name + "' />";
        }

        #endregion
    }
}
