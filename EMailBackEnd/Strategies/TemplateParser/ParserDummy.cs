using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMailAdmin.BackEnd.Strategies.TemplateParser
{
    public class ParserDummy : ITemplateParserStrategy
    {
        #region ITemplateParserStrategy Members

        public string Parse(TemplateParserContext context)
        {
            return context.ReplaceText;
        }

        #endregion
    }
}
