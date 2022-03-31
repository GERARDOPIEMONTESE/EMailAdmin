using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Service.Interfaces;

namespace EMailAdmin.BackEnd.Strategies.TemplateParser
{
    public class ParserTable : ITemplateParserStrategy
    {
        private const string TEMPLATEBODY = "<templateBody>";
        public string Parse(TemplateParserContext context)
        {
            string rstTable = "";

            if (context.Dto.IsPreview)
            {
                return "Table " + context.Name;
            }

            var type = TableVariableTextHome.Get(context.Name);

            TableVariableTextContent contents = type.GetContent(context.Dto.IdLanguage);            

            string TableConfig = "<table border='1'><tbody>$body$</tbody></table>";
            if (contents != null)
            {
                TableConfig = contents.Content;
                if (!contents.Content.Contains("$body$"))
                {
                    rstTable = TableConfig;
                }
                else
                {
                    if (contents.Content.Contains("templateBody"))
                    {
                        string sTemplateBody = TableConfig.Substring(TableConfig.IndexOf(TEMPLATEBODY) + TEMPLATEBODY.Length).Replace(TEMPLATEBODY, "");

                        string TRs = "";
                        foreach (var item in ((ITableBody)context.Dto).ParseBodyArray(type.Name))
                        {
                            TRs += string.Format(sTemplateBody, item.Split(','));
                        }

                        TableConfig = TableConfig.Replace(TEMPLATEBODY + sTemplateBody + TEMPLATEBODY, "");
                        rstTable = TableConfig.Replace("$body$", TRs);
                    }
                    else 
                    {
                        rstTable = TableConfig.Replace("$body$", ((ITableBody)context.Dto).ParseBody(type.Name));
                        if (contents.Content.Contains("$header$"))
                        {
                            rstTable = rstTable.Replace("$header$", ((ITableBody)context.Dto).ParseHeader(type.Name));
                        }
                    }
                        
                }
            }           
            
            return rstTable;
        }
    }
}
