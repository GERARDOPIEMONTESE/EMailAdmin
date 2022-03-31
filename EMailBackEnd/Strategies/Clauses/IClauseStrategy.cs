using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Strategies.TemplateParser;

namespace EMailAdmin.BackEnd.Strategies.Clauses
{
    public interface IClauseStrategy
    {
        string GetCode();

        bool Evaluate(TemplateParserContext context, string value);
    }
}
