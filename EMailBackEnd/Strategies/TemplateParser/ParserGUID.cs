using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMailAdmin.BackEnd.Strategies.TemplateParser
{
    public class ParserGUID : ITemplateParserStrategy
    {
        public string Parse(TemplateParserContext context)
        {
            if (context.Name.StartsWith("NEWGUID"))
            {
                Guid guid = Guid.NewGuid();
                return guid.ToString();
            }

            if (context.Name.StartsWith("NUMRANDOM"))
            {
                return new Random().Next().ToString();
            }

            return "";
        }
    }
}
