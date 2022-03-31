using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Properties;
using EMailAdmin.BackEnd.Strategies.Clauses;

namespace EMailAdmin.BackEnd.Strategies.TemplateParser
{
    public class ParserIfClause : ITemplateClauseStrategy
    {
        #region ITemplateParserStrategy Members

        public string Parse(TemplateParserContext context)
        {
            if (context.Name == null)
            {
                return "";
            }

            char orClause = Settings.Default.OrClause.ToCharArray()[0];
            char andClause = Settings.Default.AndClause.ToCharArray()[0];
            char separator = Settings.Default.ClauseSeparator.ToCharArray()[0];

            string[] orParts = context.Name.Split(orClause);
            
            bool orEvaluation = false;
            foreach (string part in orParts)
            {
                bool andEvaluation = true;
                string[] andParts = part.Split(andClause);
                foreach (string singlePart in andParts)
                {
                    string[] clauseParts = singlePart.Split(separator);
                    string code = clauseParts[0];
                    string value = clauseParts.Length > 1 ? clauseParts[1] : "";

                    andEvaluation &= ClauseStrategyLocator.GetIntance().GetStrategy(code).Evaluate(context, value);
                }
                orEvaluation |= andEvaluation;
            }

            return orEvaluation ? context.ClauseText : "";
        }

        public string GetEndClause()
        {
            string variableInitTag = Settings.Default["VariableInitTag"].ToString();
            string variableEndTag = Settings.Default["VariableEndTag"].ToString();

            return variableInitTag + "ENDIF" + variableEndTag;
        }

        #endregion
    }
}
