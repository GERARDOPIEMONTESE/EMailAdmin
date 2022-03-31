using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Strategies.TemplateParser
{
    public class ParserUpgradeVariableText : ITemplateParserStrategy
    {
        #region ITemplateParserStrategy Members

        public string Parse(TemplateParserContext context)
        {
            if (context.Dto.IsPreview)
            {
                return context.Name;
            }

            var type = DAOLocator.Instance().GetDaoUpgradeVariableTextType().GetByCode(context.Name);

            EMailEkitDTO dto = (EMailEkitDTO)context.Dto;

            IList<UpgradeVariableText> texts = new List<UpgradeVariableText>();

            foreach (EMailEKitUpgradeDTO upgrade in dto.Upgrades)
            {                
                texts = DAOLocator.Instance().GetDaoUpgradeVariableText().
                    FindByFilters(type.Id, upgrade.IdUpgrade);
                if (texts.Count > 0)
                {
                    break;
                }
            }

            return texts.Count == 0 ? "" : texts[0].GetContent(context.Dto.IdLanguage).Content;
        }

        #endregion
    }
}