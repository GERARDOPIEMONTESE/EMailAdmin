using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMailAdmin.BackEnd.Strategies.Clauses
{
    public class DummyClauseStrategy : IClauseStrategy
    {
        public const string CODE = "DUMMY";

        #region IClauseStrategy Members

        public string GetCode()
        {
            return CODE;
        }

        public bool Evaluate(TemplateParser.TemplateParserContext context, string value)
        {
            return false;
        }

        #endregion
    }
}
