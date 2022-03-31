using System;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class Legal : ObjetoPersistido
    {
        #region Constants

        private const string NAME = "EMailLog";

        #endregion Constants

        //Uso las de EmailLog asi estan en un solo lugar
        //#region Status

        //public const int INITIAL = 10;

        //public const int INPROGRESS = 20;

        //public const int EXTERNALINFOCOMPLETE = 30;

        //public const int OK = 100;

        //public const int ERROR = 200;

        //public const int ERRORINMAIL = 250;

        //public const int REPROCESSED = 300;

        //#endregion

        #region Properties

        public int CountryCode { get; set; }

        public string VoucherCode { get; set; }

        public string PaxName { get; set; }

        public string EmissionDate { get; set; }

        public DateTime SentDate { get; set; }

        public string Email { get; set; }

        public int ProcessStatus { get; set; }

        public DateTime ErrorDate { get; set; }

        public string ErrorMessage { get; set; }

        public string ErrorMessageForGrid { get { return ErrorMessage.Length > 100 ? ErrorMessage.Substring(0, 100) : ErrorMessage; } }

        public string EmissionDateForGrid { get { return EmissionDate; } }

        public string SentDateForGrid { get { return SentDate != new DateTime() ? SentDate.ToString() : ""; } }

        public string ErrorDateForGrid { get { return ErrorMessage != "" ? (ErrorDate != new DateTime() ? ErrorDate.ToString() : "") : ""; } }

        public Nullable<int> IdLote { get; set; }

        public string Process
        {
            get
            {
                return EMailLog.ProcessDescription(ProcessStatus);
            }
        }

        public string TemplateName { get; set; }

        #endregion Properties

        #region Methods

        public override string ObtenerNombre()
        {
            return NAME;
        }

        #endregion Methods
    }
}
