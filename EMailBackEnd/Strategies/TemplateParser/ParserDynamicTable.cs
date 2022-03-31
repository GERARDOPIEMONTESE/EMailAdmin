using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMailAdmin.BackEnd.Strategies.TemplateParser
{
    public class ParserDynamicTable : ITemplateParserStrategy
    {
        public string Parse(TemplateParserContext context)
        {
            string rstTable = "";

            if (context.Dto.IsPreview)
            {
                return "DynamicTable " + context.Name;
            }

            string dataName = context.Name;
            string htmlTable = context.ReplaceText;

            string htmlDYNTInicio = "${DYNT," + dataName + ",";
            string htmlDYNTFin = "}$";
            htmlTable = htmlTable.Substring(htmlDYNTInicio.Length, (htmlTable.Length - htmlDYNTInicio.Length - htmlDYNTFin.Length));

            DynamicItemDTO tableData = ((IDynamicDTO)context.Dto).GetDicValue(dataName);

            if (tableData.Value != null)
                rstTable = ProccessContextData(dataName, tableData.Value, htmlTable);

            return rstTable;
        }

        private string ProccessContextData(string dataName, dynamic dto, string body)
        {
            var variableInitTag = "<tr " + dataName.ToLower() + "-tr";
            var variableEndTag = "</tr>";

            var indexInit = body.ToLower().IndexOf(variableInitTag);
            var indexEnd = body.IndexOf(variableEndTag, indexInit > -1 ? indexInit : 0);

            string tr = body.Substring(indexInit, indexEnd + variableEndTag.Length - indexInit);

            string filas = "";

            foreach (var item in dto)
            {
                DynamicDTO dataTr = new DynamicDTO();
                dataTr.Convert(item);

                filas += ProccessContextHtml(dataTr, tr);
            }

            body = body.Replace(tr, filas);

            return body;
        }

        private string ProccessContextHtml(DynamicDTO dto, string body)
        {
            var variableInitTag = ConfigurationValueHome.GetByCode("VarDicInitTag").Value;
            var variableEndTag = ConfigurationValueHome.GetByCode("VarDicEndTag").Value;

            foreach (var kv in dto.dicValues)
            {
                var tag = variableInitTag + kv.Key + variableEndTag;
                body = body.Replace(tag, kv.Value.ToString());
            }

            return body;
        }
    }
}
