namespace EMailAdmin.BackEnd.Reports.Objects
{
    public class ConditionsReportObject
    {
        #region Properties

        public byte[] Header { get; set; }
        public byte[] Footer { get; set; }
        public string ConditionsReportTitle { get; set; }
        public string Code { get; set; }
        public string Text { get; set; }
        public string Leyend { get; set; }
        
        // Específicos para usar en el nuevo Ekit 2.0
        public bool EsClausulaDeSeguro { get; set; }
        public string Price { get; set; }
        
        // COLUMNA DEL MEDIO DE LAS CLAUSULAS
        public string Detail
        {
            get
            {
                if (string.IsNullOrEmpty(Text))
                    return Leyend;
                else
                    return Text;
            }
        }

        // 3ER COLUMNA DE LAS CLAUSULAS
        public string LeyendExt
        {
            get
            {
                if (string.IsNullOrEmpty(Text))
                    return string.Empty;
                else
                    return Leyend;
            }
        }

        #endregion

        #region Constructor

        public ConditionsReportObject(string codeTypeContentPrint, string code, string text, 
                            string leyend, string reportTitle, byte[] header, byte[] footer,
                            bool esClausulaDeSeguro, string price = "")
        {
            //if (code.ToUpper().Equals("LEGAL"))
            //    code = string.Empty;
            Code = code;
            Text = text;
            Leyend = leyend;
            Header = header;
            Footer = footer;
            ConditionsReportTitle = reportTitle;
            EsClausulaDeSeguro = esClausulaDeSeguro;
            Price = price;
            //switch (codeTypeContentPrint.ToUpper())
            //{
            //    case "CONT":
            //        Text = "";
            //        break;
            //    case "IDCONT":
            //        Text = "";
            //        return;
            //    case "DESCONT":
            //        Code = "";
            //        return;
            //    default:
            //        return;
            //}
        }

        #endregion
    }
}