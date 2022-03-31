using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Strategies.TemplateParser
{
    public class ParserCountryVisibleText : ITemplateParserStrategy
    {
        #region ITemplateParserStrategy Members

        public string Parse(TemplateParserContext context)
        {
            if (context.Dto.IsPreview)
            {
                return context.Name;
            }

            var type = DAOLocator.Instance().GetDaoCountryVisibleTextType().GetByCode(context.Name);
            var texts = DAOLocator.Instance().GetDaoCountryVisibleText().
                FindByFilters(type.Id, context.Dto.AssociationGroupDto.IdLocation);
            return texts.Count == 0 ? "" : texts[0].GetContent(context.Dto.IdLanguage).Content;
        }

        #endregion
    }
}
