using System;
using FrameworkDAC.Negocio;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Domain
{
    public class EMailLog : ObjetoNegocio
    {
        private const string NAME = "EMailLog";

        #region Status

        public const int INITIAL = 10;

        public const int INPROGRESS = 20;

        public const int EXTERNALINFOCOMPLETE = 30;

        public const int OK = 100;

        public const int OK_HANDLER = 101;

        public const int ERROR = 200;

        public const int ERRORINMAIL = 250;

        public const int ERRORINHANDLER = 251;

        public const int REPROCESSED = 300;

        public const int AGENCY_EXCLUDE = 400;

        public enum StatusEmailLog
        {
            statusINITIAL = INITIAL,
            statusINPROGRESS = INPROGRESS,
            statusEXTERNALINFOCOMPLETE = EXTERNALINFOCOMPLETE,
            statusOK = OK,
            statusOKHandler = OK_HANDLER,
            statusERROR = ERROR,
            statusERRORINMAIL = ERRORINMAIL,
            statusERRORINHANDLER = ERRORINHANDLER,
            statusREPROCESSED = REPROCESSED,
            statusAGENCYEXCLUDE = AGENCY_EXCLUDE
        }


        public static string ProcessDescription(int ProcessStatus)
        {
            var result = "";
            switch (ProcessStatus)
            {
                case EMailLog.INITIAL:
                    result = "INITIATED";
                    break;
                case EMailLog.INPROGRESS:
                    result = "IN PROGRESS";
                    break;
                case EMailLog.EXTERNALINFOCOMPLETE:
                    result = "EXTERNAL INFO COMPLETE";
                    break;
                case OK:
                    result = "SENT";
                    break;
                case OK_HANDLER:
                    result = "PRINT PDF";
                    break;
                case ERROR:
                    result = "ERROR";
                    break;
                case ERRORINMAIL:
                    result = "ERROR IN MAIL";
                    break;
                case REPROCESSED:
                    result = "REPROCESSED";
                    break;
                case AGENCY_EXCLUDE:
                    result = "AGENCY EXCLUDE";
                    break;
                case ERRORINHANDLER:
                    result = "ERROR PRINT PDF";
                    break;
            }
            return result;
        }

        #endregion

        #region Properties

        public int IdTemplate { get; set; }

        public int IdTemplateType { get; set; }

        public string TemplateName { get; set; }

        public int CountryCode { get; set; }

        public string ModuleCode { get; set; }

        public string VoucherCode { get; set; }

        public string InvokeInformation { get; set; }

        public byte[] ContextInformation { get; set; }

        public string MailFrom { get; set; }

        public string MailTo { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string AttachmentIds { get; set; }

        public DateTime EndDate { get; set; }

        public string ErrorMessage { get; set; }

        public string ErrorMessageForGrid { get { return ErrorMessage.Length > 100 ? ErrorMessage.Substring(0, 100) : ErrorMessage; } }

        public int ProcessStatus { get; set; }

        public bool Receive { get; set; }

        public DateTime ReceiveDate { get; set; }

        public string PaxName { get; set; }

        public string PaxSurname { get; set; }

        public string IssuanceDate { get; set; }

        public string ContextInformationZipPending { get; set; }

        public Nullable<int> IdLote { get; set; }

        public Nullable<int> IdClienteUnico { get; set; }

        public bool InvokeByHandler { get; set; }
        #endregion

        public EMailLog()
        {
            InvokeByHandler = false;
        }

        public override FrameworkDAC.Dato.IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoEMailLog();
        }

        public override string ObtenerNombre()
        {
            return NAME;
        }
    }
}
