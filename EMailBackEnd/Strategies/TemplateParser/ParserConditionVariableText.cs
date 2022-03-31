using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Service.Interfaces;
using System.Xml;
using System.Reflection;

namespace EMailAdmin.BackEnd.Strategies.TemplateParser
{
    public class ParserConditionVariableText : ITemplateParserStrategy
    {
        #region ITemplateParserStrategy Members

        public string Parse(TemplateParserContext context)
        {
            if (context.Dto.IsPreview)
            {
                return "Condition " + context.Name;
            }

            var condition = ConditionVariableTextHome.FindByName(context.Name, true);

            string sCondition = "";
            if (ConditionIsValid(condition, context))
            {
                ConditionVariableTextContent contents = condition.GetContent(context.Dto.IdLanguage);                
                if (contents != null)
                    sCondition = contents.Content;                
            }
            return sCondition;
        }
        
        private bool ConditionIsValid(ConditionVariableText condition, TemplateParserContext context)
        {
            bool isValid = true;
            if (condition.VariablesText.Count == 0) isValid = false;

            foreach (var item in condition.VariablesText)
            {
                if (isValid)
                {
                    isValid = GetVariableValue(item, context);
                }
            }
            return isValid;
        }

        private bool GetVariableValue(ConditionVariableText_R_VariableText item, TemplateParserContext context)
        {
            object valProp = null;
            if (item.VariableText.IsDynamicValue)
                valProp = GetDynamicValue(item, context);
            else
                valProp = ((EMailEkitDTO)context.Dto).GetType().GetProperty(item.VariableText.Name).GetValue((EMailEkitDTO)context.Dto, null);
          
            return ConditionVariableText_R_VariableText.Evaluar(valProp.ToString(), item);
        }

        private object GetDynamicValue(ConditionVariableText_R_VariableText item,TemplateParserContext context)
        {
           var dtoDym = ((DynamicDTO)context.Dto);
           DynamicItemDTO dtovalue = dtoDym.GetDicValue(item.DynamicName);
           if (dtovalue != null)
               return dtovalue.Value;
           else
               return "";
        }
        #endregion
    }
}
