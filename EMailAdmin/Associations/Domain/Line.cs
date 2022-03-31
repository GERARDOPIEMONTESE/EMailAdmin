using System.Collections.Generic;
using System.Linq;

namespace EMailAdmin.Associations.Domain
{
    public class Line
    {
        #region Properties

        public bool IsValid { get; set; }

        public IList<string> Errors { get; set; }

        public int LineNumber { get; set; }

        public string ErrorToShow
        {
            get
            {
                string totalLine = "";
                foreach (string error in Errors)
                {
                    if(totalLine == "")
                    {
                        totalLine = error;
                    }
                    else
                    {
                        totalLine = totalLine + " - " + error;
                    }
                }
                return totalLine;
            }
        }

        #endregion

        #region Constructor

        public Line()
        {
            Errors = new List<string>();
        }

        public Line(bool isValid)
        {
            IsValid = isValid;
        }

        public Line(bool isValid, string error, int lineNumber)
        {
            Errors = new List<string>();

            IsValid = isValid;
            LineNumber = lineNumber;

            if (error != "")
                Errors.Add(error);
        }

        #endregion

        #region Methods

        public static Line BuildLine(string[] line, int lineNumber)
        {
            if (line.Any() && (line[0] == "" || line[0].Trim() == ""))
                return null;

            if (line.Count() > 3 && line[0].ToUpper() != "CUENTA" && line[0].ToUpper() != "CONTA" &&
                line[0].ToUpper() != "ACCOUNT" && line[0].ToUpper() != "TAXA" && line[0].ToUpper() != "RATE" &&
                line[0].ToUpper() != "TARIFA")
                return new TemplateLine(line, lineNumber);

            if (line.Count() == 1 && line[0].Equals("-"))
                return new Line(true);

            if (line.Any() && ConditionLine.IsConditionLine(line))
                return new ConditionLine(line, lineNumber);

            return new Line(false, line[0] == null || line[0].Length == 0 ? "Error:EmptyLine" : "Error:InvalidFormatLine", lineNumber);
        }

        protected bool ValidPosition(string[] line, int position)
        {
            if (line.Count() <= position)
            {
                if (Errors.Any(t => t == "Error:ParametersQuantity"))
                {
                    return false;
                }

                Errors.Add("Error:ParametersQuantity");
                return false;
            }

            return true;
        }

        protected bool ValidArgumentsQuantity(string[] line, int parametersQuantity)
        {
            if (line.Count() < parametersQuantity)
            {
                Errors.Add("Error:ParametersQuantity");
                return false;
            }
            return true;
        }

        #endregion        
    }
}