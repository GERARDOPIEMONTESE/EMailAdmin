using Spring.Context;
using Spring.Context.Support;

namespace EMailAdmin.BackEnd.Strategies.EMailSender
{
    public class EMailSenderStrategyLocator
    {
        #region Singleton

        private static EMailSenderStrategyLocator _instance;

        private readonly IApplicationContext _context;

        private EMailSenderStrategyLocator()
        {
            _context = ContextRegistry.GetContext();
        }

        public static EMailSenderStrategyLocator Instance()
        {
            return _instance ?? (_instance = new EMailSenderStrategyLocator());
        }

        #endregion

        public IEMailSenderStrategy GetEMailSenderDummy()
        {
            return (IEMailSenderStrategy) _context.GetObject("EMailSenderDummy");
        }

        public IEMailSenderStrategy GetEMailSenderEKit()
        {
            return (IEMailSenderStrategy) _context.GetObject("EMailSenderEkit");
        }

        public IEMailSenderStrategy GetEMailSenderNiceTrip()
        {
            return (IEMailSenderStrategy) _context.GetObject("EMailSenderNiceTrip");
        }

        public IEMailSenderStrategy GetEMailSenderWelcomeBack()
        {
            return (IEMailSenderStrategy) _context.GetObject("EMailSenderWelcomeBack");
        }

        public IEMailSenderStrategy GetEMailPrepurchase()
        {
            return (IEMailSenderStrategy)_context.GetObject("EMailSenderPrepurchase");
        }

        public IEMailSenderStrategy GetEMailHappyBirthday()
        {
            return (IEMailSenderStrategy)_context.GetObject("EMailSenderHappyBirthday");
        }

        public IEMailSenderStrategy GetEmailACCOMQuote()
        {
            return (IEMailSenderStrategy)_context.GetObject("EMailSenderACCOMQuote");
        }

        public IEMailSenderStrategy GetEmailQuoteExchange()
        {
            return (IEMailSenderStrategy)_context.GetObject("EmailSenderQuoteExchange");
        }

        public IEMailSenderStrategy GetEMailPrepurchasePax()
        {
            return (IEMailSenderStrategy)_context.GetObject("EMailSenderPrepurchasePax");
        }

        public IEMailSenderStrategy GetEMailPoints()
        {
            return (IEMailSenderStrategy)_context.GetObject("EMailSenderPoints");
        }

        public IEMailSenderStrategy GetEMailConditionAlert()
        {
            return (IEMailSenderStrategy)_context.GetObject("EMailSenderConditionAlert");
        }

        public IEMailSenderStrategy GetEMailCapita()
        {
            return (IEMailSenderStrategy)_context.GetObject("EMailSenderCapita");
        }

        public IEMailSenderStrategy GetEmailAlertaChat()
        {
            return (IEMailSenderStrategy)_context.GetObject("EmailAlertaChat");
        }

        public IEMailSenderStrategy GetEmailXAMCases()
        {
            return (IEMailSenderStrategy)_context.GetObject("EmailXAMCases");
        }

        public IEMailSenderStrategy GetEMailSenderPoliza()
        {
            return (IEMailSenderStrategy)_context.GetObject("EMailSenderPoliza");
        }

        public IEMailSenderStrategy GetEMailSenderEndoso()
        {
            return (IEMailSenderStrategy)_context.GetObject("EMailSenderEndoso");
        }

        public IEMailSenderStrategy GetEMailSenderACCOMNotIssue()
        {
            return (IEMailSenderStrategy)_context.GetObject("EMailSenderACCOMNotIssue");
        }

        public IEMailSenderStrategy GetEMailQuote()
        {
            return (IEMailSenderStrategy)_context.GetObject("EMailQuote");
        }

        public IEMailSenderStrategy GetEMailSenderBotonPago()
        {
            return (IEMailSenderStrategy)_context.GetObject("EMailSenderBotonPago");
        }

        public IEMailSenderStrategy GetEMailContinuaCompra()
        {
            return (IEMailSenderStrategy)_context.GetObject("EMailSenderContinuaCompra");
        }

        public IEMailSenderStrategy GetEMailBalanceRequest()
        {
            return (IEMailSenderStrategy)_context.GetObject("EMailSenderBalanceRequest");
        }

        public IEMailSenderStrategy GetEMailSenderExternalPaymentFinish()
        {
            return (IEMailSenderStrategy)_context.GetObject("EMailSenderExternalPaymentFinish");
        }

        public IEMailSenderStrategy GetEMailDynamicSender()
        {
            return (IEMailSenderStrategy)_context.GetObject("EmailSenderDynamic");
        }
    }
}
