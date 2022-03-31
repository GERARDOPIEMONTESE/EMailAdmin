using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMailAdmin.Utils
{
    public class RichTextUtils
    {
        public static string CleanWordCharacters(string originalValue)
        {
            string convertedValue = originalValue;
            //convertedValue = convertedValue.Replace("<br>", "*||*");
            //convertedValue = convertedValue.Replace("<b>", "*--*");
            //convertedValue = convertedValue.Replace("</b>", "*!-*");

            //System.Text.RegularExpressions.Regex objRegEx = new System.Text.RegularExpressions.Regex("<[^>]*>");

            //convertedValue = objRegEx.Replace(convertedValue, "");
            convertedValue = convertedValue.Replace("*||*", "<br>");
            convertedValue = convertedValue.Replace("*--*", "<b>");
            convertedValue = convertedValue.Replace("*!-*", "</b>");

            return convertedValue;
        }
    }
}