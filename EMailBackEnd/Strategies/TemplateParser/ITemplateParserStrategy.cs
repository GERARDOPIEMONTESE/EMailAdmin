using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.DTO;

namespace EMailAdmin.BackEnd.Strategies.TemplateParser
{
    public interface ITemplateParserStrategy
    {
        string Parse(TemplateParserContext context);
    }

    public interface ITemplateClauseStrategy : ITemplateParserStrategy
    {
        string GetEndClause();
    }

    public class TemplateParserContext
    {
        public string Name { get; set; }

        public string ReplaceText { get; set; }

        public string ClauseText { get; set; }

        public AbstractEMailDTO Dto { get; set; }

        public Template Template { get; set; }

        public string Links { get; set; }

        public string Xml { get; set; }
    }
}
