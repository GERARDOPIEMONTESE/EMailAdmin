using EMailAdmin.BackEnd.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMailAdmin.BackEnd.Strategies.TemplateParser
{
    public class ParserClause : ITemplateParserStrategy
    {
        #region ITemplateParserStrategy Members

        public string Parse(TemplateParserContext context)
        {
            if (context.Dto.IsPreview)
            {
                return "Clause " + context.Name;
            }

            var clauseValue = ((EMailEkitDTO)context.Dto).GrupoClausulaDTO.GetBenefitContent(context.Name, context.Dto.IdLanguage);

            return clauseValue;
        }

        #endregion
    }
}