using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Domain.External;

namespace EMailAdmin.BackEnd.Home
{
    public class TemplateTypeHome
    {
        #region Constant

        private const string EKIT = "EKit";

        private const string NICE_TRIP = "Trip";

        private const string WELCOME_BACK = "Back";

        private const string PREPURCHASE = "PrePur";

        private const string PREFIJO_PREPURCHASEPAX = "BoxPax";

        private const string VOUCHERS_POINTS = "VPR";

        private const string CONDITION_ALERT = "CAT";

        private const string CAPITA = "CAPITA";

        private const string IMESSENGER = "IMESSENGER";

        private const string POLIZA_VOID = "POLIZAVOID";

        private const string POLIZA_UPDATE = "POLIZAUPDATE";

        private const string EMAIL_QUOTE = "ACNETQUOTE";

        private const string BOTON_PAGO = "BOTONPAGO";

        private const string ENDOSO = "ENDOSO";

        private const string HAPPYBIRTH = "HB";

        private const string CONTINUA_COMPRA = "KEEPBUY";

        private const string BALANCE_REQUEST = "BALANCEREQUEST";

        private const string BOTON_PAGO_FINISH = "BOTONPAGOFINISH";

        private const string DYNAMIC = "Dynamic";

        #endregion Constant

        public static TemplateType Get(int id)
        {
            return DAOLocator.Instance().GetDaoTemplateType().Get(id);
        }

        public static TemplateType Get(string code)
        {
            return DAOLocator.Instance().GetDaoTemplateType().Get(code);
        }

        public static TemplateType GetByDescription(string description)
        {
            return DAOLocator.Instance().GetDaoTemplateType().GetByDescription(description);
        }

        public static TemplateType GetEkit()
        {
            return DAOLocator.Instance().GetDaoTemplateType().Get(EKIT);
        }

        public static TemplateType GetNiceTrip()
        {
            return DAOLocator.Instance().GetDaoTemplateType().Get(NICE_TRIP);
        }

        public static TemplateType GetWelcomeBack()
        {
            return DAOLocator.Instance().GetDaoTemplateType().Get(WELCOME_BACK);
        }

        public static TemplateType GetPrepurchase()
        {
            return DAOLocator.Instance().GetDaoTemplateType().Get(PREPURCHASE);
        }

        public static IList<TemplateType> GetPrepurchasePax()
        {
            return DAOLocator.Instance().GetDaoTemplateType().Find(PREFIJO_PREPURCHASEPAX);
        }

        public static TemplateType GetVoucherPoints()
        {
            return DAOLocator.Instance().GetDaoTemplateType().Get(VOUCHERS_POINTS);
        }

        public static TemplateType GetConditionAlert()
        {
            return DAOLocator.Instance().GetDaoTemplateType().Get(CONDITION_ALERT);
        }

        public static TemplateType GetCapita()
        {
            return DAOLocator.Instance().GetDaoTemplateType().Get(CAPITA);
        }

        public static TemplateType GetAlertChats()
        {
            return DAOLocator.Instance().GetDaoTemplateType().Get(IMESSENGER);
        }

        public static TemplateType GetPolizaCancelacion()
        {
            return DAOLocator.Instance().GetDaoTemplateType().Get(POLIZA_VOID);
        }

        public static TemplateType GetPolizaModificacion()
        {
            return DAOLocator.Instance().GetDaoTemplateType().Get(POLIZA_UPDATE);
        }

        public static TemplateType GetEndoso()
        {
            return DAOLocator.Instance().GetDaoTemplateType().Get(ENDOSO);
        }

        public static TemplateType GetEMailQuote()
        {
            return DAOLocator.Instance().GetDaoTemplateType().Get(EMAIL_QUOTE);
        }

        public static IList<TemplateType> FindAll()
        {
            return DAOLocator.Instance().GetDaoTemplateType().FindAll();
        }

        public static TemplateType GetBotonPago()
        {
            return DAOLocator.Instance().GetDaoTemplateType().Get(BOTON_PAGO);
        }

        public static TemplateType GetHappyBirth()
        {
            return DAOLocator.Instance().GetDaoTemplateType().Get(HAPPYBIRTH);
        }

        public static TemplateType GetContinuaTuCompra()
        {
            return DAOLocator.Instance().GetDaoTemplateType().Get(CONTINUA_COMPRA);
        }

        public static TemplateType GetBalanceRequest()
        {
            return DAOLocator.Instance().GetDaoTemplateType().Get(BALANCE_REQUEST);
        }

        public static TemplateType GetBotonPagoFinish()
        {
            return DAOLocator.Instance().GetDaoTemplateType().Get(BOTON_PAGO_FINISH);
        }

        public static TemplateType GetDynamic()
        {
            return DAOLocator.Instance().GetDaoTemplateType().Get(DYNAMIC);
        }
    }
}

