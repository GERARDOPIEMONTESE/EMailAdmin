using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Properties;
using EMailAdmin.BackEnd.Service;
using System.Reflection;

namespace EMailAdmin.BackEnd.Utils
{
    public class EMailBodyUtil
    {
        #region Singleton

        private static EMailBodyUtil _Instance;

        private EMailBodyUtil()
        {
        }

        public static EMailBodyUtil Instance()
        {
            if (_Instance == null)
            {
                _Instance = new EMailBodyUtil();
            }

            return _Instance;
        }

        #endregion

        public static string ProccessContextData<T>(T dto, string body) where T : AbstractEMailDTO 
        {
            
            var variableInitTag = Settings.Default["VariableInitTag"].ToString();
            var variableEndTag = Settings.Default["VariableEndTag"].ToString();

            IList<string> textsToReplace = GetVariableTags(body);

            foreach (string textToReplace in textsToReplace)
            {
                string tag = textToReplace.Replace(variableInitTag, "").Replace(variableEndTag, "");
                PropertyInfo propInfo = dto.GetType().GetProperty(tag.Trim());
                if (propInfo != null)
                {
                    object valProp = propInfo.GetValue(dto, null);
                    body = body.Replace(textToReplace, (valProp != null ? valProp.ToString() : ""));
                }
            }

            return body;
        }

        public static IList<string> GetVariableTags(string body)
        {
            IList<string> iText = new List<string>();

            var variableInitTag = Settings.Default["VariableInitTag"].ToString();
            var variableEndTag = Settings.Default["VariableEndTag"].ToString();
            var variableSeparator = Settings.Default["VariableSeparator"].ToString().ToCharArray()[0];

            var indexInit = body.IndexOf(variableInitTag);
            var indexEnd = body.IndexOf(variableEndTag, indexInit > -1 ? indexInit : 0);

            while (indexInit != -1)
            {
                string replaceText = body.Substring(indexInit, indexEnd + variableEndTag.Length - indexInit);
                iText.Add(replaceText);

                indexInit = body.IndexOf(variableInitTag, indexEnd);
                indexEnd = body.IndexOf(variableEndTag, indexInit > -1 ? indexInit : 0);
            }
            return iText;
        }

        public string GetBody(Template template, int idLanguage)
        {
            
            return "";
        }
    }
}
