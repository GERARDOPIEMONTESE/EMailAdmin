using Spring.Context;
using EMailAdmin.BackEnd.Service.Interfaces;
using Spring.Context.Support;

namespace EMailAdmin.BackEnd.Service
{
    public class ServiceLocator
    {
        #region Singleton

        private static ServiceLocator _instance;

        private IApplicationContext _context;

        private ServiceLocator()
        {
            _context = ContextRegistry.GetContext();
        }

        public static ServiceLocator Instance()
        {
            return _instance ?? (_instance = new ServiceLocator());
        }

        #endregion

        public ISignatureService GetSignatureService()
        {
            return (ISignatureService)_context.GetObject("SignatureService");
        }

        public ICountryVisibleTextService GetCountryVisibleTextService()
        {
            return (ICountryVisibleTextService)_context.GetObject("CountryVisibleTextService");
        }

        public IUpgradeVariableTextService GetUpgradeVariableTextService()
        {
            return (IUpgradeVariableTextService)_context.GetObject("UpgradeVariableTextService");
        }

        public ITemplateService GetTemplateService()
        {
            return (ITemplateService)_context.GetObject("TemplateService");
        }

        public IEMailContactService GetEMailContactService()
        {
            return (IEMailContactService)_context.GetObject("EMailContactService");
        }

        public IAttachmentService GetAttachmentService()
        {
            return (IAttachmentService)_context.GetObject("AttachmentService");
        }

        public IContentAttachmentService GetContentAttachmentService()
        {
            return (IContentAttachmentService)_context.GetObject("ContentAttachmentService");
        }

        public ISendMailService GetSendMailService()
        {
            return (ISendMailService)_context.GetObject("SendMailService");
        }

        public IEMailSenderPrepPaxService GetSendMailPrepurchasePaxService()
        {
            return (IEMailSenderPrepPaxService)_context.GetObject("EMailSenderPrepPaxService");
        }

        public IGroupService GetGroupService()
        {
            return (IGroupService)_context.GetObject("GroupService");
        }

        public IGroup_R_TemplateService GetGroupRTemplateService()
        {
            return (IGroup_R_TemplateService)_context.GetObject("Group_R_TemplateService");
        }

        public IAttachment_R_GroupService GetAttachmentRGroupService()
        {
            return (IAttachment_R_GroupService)_context.GetObject("Attachment_R_GroupService");
        }

        public IInformationService GetInformationService()
        {
            return (IInformationService)_context.GetObject("InformationService");
        }

        public IEMailLogService GetEMailLogService()
        {
            return (IEMailLogService)_context.GetObject("EMailLogService");
        }
        
        public IPaxPassedAwayService GetPaxPassedAwayService()
        {
            return (IPaxPassedAwayService)_context.GetObject("PaxPassedAwayService");
        }

        public IEMailProcessLogService GetDaoEMailProcessLog()
        {
            return (IEMailProcessLogService)_context.GetObject("EMailProcessLogService");
        }

        public IConditionVariableTextService GetConditionVariableTextService()
        {
            return (IConditionVariableTextService)_context.GetObject("ConditionVariableTextService");
        }

        public IPrepurchasePaxService GetPrepurchasePaxService()
        {
            return (IPrepurchasePaxService)_context.GetObject("PrepurchasePaxService");
        }

        public ICapitaService GetCapitaService()
        {
            return (ICapitaService)_context.GetObject("CapitaService");
        }

        public IAlertChatService GetAlertChatService()
        {
            return (IAlertChatService)_context.GetObject("AlertChatService");
        }

        public IExternalXAMServices GetSendMailXAMServices()
        {
            return (IExternalXAMServices)_context.GetObject("ExternalXAMServices");
        }

        public IPolizaService GetSendMailPolizaServices()
        {
            return (IPolizaService)_context.GetObject("EmailSenderPolizaServices");
        }

        public IEndosoService GetSendMailEndosoServices()
        {
            return (IEndosoService)_context.GetObject("EmailSenderEndosoServices");
        }

        public IBotonPagoService GetSendMailBotonPagoServices()
        {
            return (IBotonPagoService)_context.GetObject("EmailSenderBotonPagoServices");
        }

        internal IPaxCumpleanosService GetSendMailPaxCumpleanosServices()
        {
            return (IPaxCumpleanosService)_context.GetObject("EmailSenderPaxCumpleanosServices");
        }

        internal IPaxContinuaCompraService GetSendMailPaxContinuaCompraService()
        {
            return (IPaxContinuaCompraService)_context.GetObject("EmailSenderPaxContinuaCompraServices");
        }

        internal IExternalPaymentFinishService GetSendMailBotonPagoFinishServices()
        {
            return (IExternalPaymentFinishService)_context.GetObject("EmailSenderBotonPagoFinishServices");
        }

        internal IInformationDynamicService GetInformationDynamicServices()
        {
            return (IInformationDynamicService)_context.GetObject("InformationDynamicServices");
        }
    }
}
