using EMailAdmin.BackEnd.Data.Interfaces;
using Spring.Context;
using Spring.Context.Support;
using EMailAdmin.ExternalServices.Data.Interfaces;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOLocator
    {
        #region Singleton

        private IApplicationContext _context;

        private static DAOLocator _instance;

        private DAOLocator()
        {
            _context = ContextRegistry.GetContext();
        }

        public static DAOLocator Instance()
        {
            return _instance ?? (_instance = new DAOLocator());
        }

        #endregion

        public IDAOTemplateDTO GetDaoTemplateDto()
        {
            return (IDAOTemplateDTO)_context.GetObject("DaoTemplateDto");
        }
        
        public IDAOTemplate GetDaoTemplate()
        {
           return (IDAOTemplate)_context.GetObject("DaoTemplate");
        }

        public IDAOTemplate_R_Attachment GetDaoTemplate_R_Attachment()
        {
            return (IDAOTemplate_R_Attachment)_context.GetObject("DaoTemplate_R_Attachment");
        }

        public IDAOTemplateType GetDaoTemplateType()
        {
            return (IDAOTemplateType)_context.GetObject("DaoTemplateType");
        }

        public IDAOContent GetDaoContent()
        {
            return (IDAOContent)_context.GetObject("DaoContent");
        }

        public IDAOEstrategy GetDaoEstrategy()
        {
            return (IDAOEstrategy)_context.GetObject("DaoEstrategy");
        }

        public IDAOAttachment GetDaoAttachment()
        {
            return (IDAOAttachment)_context.GetObject("DaoAttachment");
        }

        public IDAOAttachmentItem GetDaoAttachmentItem()
        {
            return (IDAOAttachmentItem)_context.GetObject("DaoAttachmentItem");
        }

        public IDAOContentAttachment GetDaoContentAttachment()
        {
            return (IDAOContentAttachment)_context.GetObject("DaoContentAttachment");
        }

        public IDAOAttachmentType GetDaoAttachmentType()
        {
            return (IDAOAttachmentType)_context.GetObject("DaoAttachmentType");
        }

        public IDAOVariableTextType GetDaoVariableTextType()
        {
            return (IDAOVariableTextType)_context.GetObject("DaoVariableTextType");
        }

        public IDAOVariableText GetDaoVariableText()
        {
            return (IDAOVariableText)_context.GetObject("DaoVariableText");
        }

        public IDAOLink GetDaoLink()
        {
            return (IDAOLink)_context.GetObject("DaoLink");
        }

        public IDAOLink GetDaoLinkSearch()
        {
            return (IDAOLink)_context.GetObject("DaoLinkSeach");
        }

        public IDAOLinkType GetDaoLinkType()
        {
            return (IDAOLinkType)_context.GetObject("DaoLinkType");
        }

        public IDAOBodyImage GetDaoBodyImage()
        {
            return (IDAOBodyImage)_context.GetObject("DaoBodyImage");
        }

        public IDAOSignature GetDaoSignature()
        {
            return (IDAOSignature)_context.GetObject("DaoSignature");
        }

        public IDAOSignatureType GetDaoSignatureType()
        {
            return (IDAOSignatureType)_context.GetObject("DaoSignatureType");
        }

        public IDAOSignatureContent GetDaoSignatureContent()
        {
            return (IDAOSignatureContent)_context.GetObject("DaoSignatureContent");
        }

        public IDAOSignature_R_Country GetDaoSignature_R_Country()
        {
            return (IDAOSignature_R_Country)_context.GetObject("DaoSignature_R_Country");
        }

        public IDAOSignature_R_SignatureType GetDaoSignature_R_SignatureType()
        {
            return (IDAOSignature_R_SignatureType)_context.GetObject("DaoSignature_R_SignatureType");
        }

        public IDAOContentImage GetDaoContentImage()
        {
            return (IDAOContentImage)_context.GetObject("DaoContentImage");
        }

        public IDAOContent_R_ContentImage GetDaoContent_R_Image()
        {
            return (IDAOContent_R_ContentImage)_context.GetObject("DaoContent_R_ContentImage");
        }

        public IDAOContent_R_ContentLink GetDaoContent_R_ContentLink()
        {
            return (IDAOContent_R_ContentLink)_context.GetObject("DaoContent_R_ContentLink");
        }

        public IDAOContent_R_ContentVariableText GetDaoContent_R_ContentVariableText()
        {
            return (IDAOContent_R_ContentVariableText)_context.GetObject("DaoContent_R_ContentVariableText");
        }

        public IDAOContent_R_ContentSignatureType GetDaoContent_R_ContentSignatureType()
        {
            return (IDAOContent_R_ContentSignatureType)_context.GetObject("DaoContent_R_ContentSignatureType");
        }

        public IDAOContent_R_ContentCountryVisibleTextType GetDaoContent_R_ContentCountryVisibleTextType()
        {
            return (IDAOContent_R_ContentCountryVisibleTextType)_context.GetObject("DaoContent_R_ContentCountryVisibleTextType");
        }

        public IDAOContent_R_ContentUpgradeVariableTextType GetDaoContent_R_ContentUpgradeVariableTextType()
        {
            return (IDAOContent_R_ContentUpgradeVariableTextType)_context.GetObject("DaoContent_R_ContentUpgradeVariableTextType");
        }

        public IDAOContent_R_ContentEmailContactType GetDaoContent_R_ContentEmailContactType()
        {
            return (IDAOContent_R_ContentEmailContactType)_context.GetObject("DaoContent_R_ContentEmailContactType");
        }

        public IDAOEMailContact GetDaoEMailContact()
        {
            return (IDAOEMailContact)_context.GetObject("DaoEMailContact");
        }

        public IDAOEMailContactContent GetDaoEMailContactContent()
        {
            return (IDAOEMailContactContent)_context.GetObject("DaoEMailContactContent");
        }

        public IDAOEMailContactLocation GetDaoEMailContactLocation()
        {
            return (IDAOEMailContactLocation)_context.GetObject("DaoEMailLocation");
        }

        public IDAOEMailContactType GetDaoEMailContactType()
        {
            return (IDAOEMailContactType)_context.GetObject("DaoEMailContactType");
        }

        public IDAOEMailListType GetDaoEMailListType()
        {
            return (IDAOEMailListType)_context.GetObject("DaoEMailListType");
        }

        public IDAOEMailList GetDaoEMailList()
        {
            return (IDAOEMailList)_context.GetObject("DaoEMailList");
        }

        public IDAOEMailListExclude GetDaoEMailListExclude()
        {
            return (IDAOEMailListExclude)_context.GetObject("DaoEMailListExclude");
        }

        public IDAOEMailListDTO GetDaoEMailListDTO()
        {
            return (IDAOEMailListDTO)_context.GetObject("DaoEMailListDTO");
        }
        public IDAOEMailListUsuarioDTO GetDaoEMailListUsuarioDTO()
        {
            return (IDAOEMailListUsuarioDTO)_context.GetObject("DaoEMailListUsuarioDTO");
        }

        public IDAOConditionType GetDaoConditionType()
        {
            return (IDAOConditionType)_context.GetObject("DaoConditionType");
        }

        public IDAOProduct GetDaoProduct()
        {
            return (IDAOProduct)_context.GetObject("DaoProduct");
        }

        public IDAORate GetDaoRate()
        {
            return (IDAORate)_context.GetObject("DaoRate");
        }

        public IDAOGroup GetDaoGroup()
        {
            return (IDAOGroup)_context.GetObject("DaoGroup");
        }

        public IDAOGroupType GetDaoGroupType()
        {
            return (IDAOGroupType)_context.GetObject("DaoGroupType");
        }

        public IDAOGroupCondition GetDaoGroupCondition()
        {
            return (IDAOGroupCondition)_context.GetObject("DaoGroupCondition");
        }

        public IDAOGroup_R_Template GetDaoGroup_R_Template()
        {
            return (IDAOGroup_R_Template)_context.GetObject("DaoGroup_R_Template");
        }

        public IDAOGroup_R_Template_Association GetDaoGroup_R_Template_Association()
        {
            return (IDAOGroup_R_Template_Association)_context.GetObject("DAOGroup_R_Template_Association");
        }
        public IDAOAttachment_R_Group GetDaoAttachment_R_Group()
        {
            return (IDAOAttachment_R_Group)_context.GetObject("DaoAttachment_R_Group");
        }

        public IDAOReportLanguage GetDaoReportLanguage()
        {
            return (IDAOReportLanguage)_context.GetObject("DaoReportLanguage");
        }

        public IDAOEMailLog GetDaoEMailLog()
        {
            return (IDAOEMailLog)_context.GetObject("DaoEMailLog"); 
        }

        public IDAOEmailLog_R_PrepurchasePax GetDaoEmailLog_R_PrepurchasePax()
        {
            return (IDAOEmailLog_R_PrepurchasePax)_context.GetObject("DaoEmailLog_R_PrepurchasePax");
        }

        public IDAOEMailProcessType GetDaoEMailProcessType()
        {
            return (IDAOEMailProcessType)_context.GetObject("DaoEMailProcessType");
        }

        public IDAOEMailProcessLog GetDaoEMailProcessLog()
        {
            return (IDAOEMailProcessLog)_context.GetObject("DaoEMailProcessLog");
        }

        public IDAOPaxPassedAway GetDaoPaxPassedAway()
        {
            return (IDAOPaxPassedAway)_context.GetObject("DaoPaxPassedAway");
        }

        public IDAOWelcomeBackInformation GetDaoWelcomeBackInformation()
        {
            return (IDAOWelcomeBackInformation)_context.GetObject("DaoWelcomeBackInformation");
        }

        public IDAOEMailAddress GetDaoEMailAddress()
        {
            return (IDAOEMailAddress)_context.GetObject("DaoEMailAddress");
        }

        public IDAODistributionType GetDaoDistributionType()
        {
            return (IDAODistributionType)_context.GetObject("DaoDistributionType");
        }

        public IDAOInsuranceCompany GetDaoInsuranceCompany()
        {
            return (IDAOInsuranceCompany)_context.GetObject("DaoInsuranceCompany");
        }

        public IDAOCountryVisibleText GetDaoCountryVisibleText()
        {
            return (IDAOCountryVisibleText)_context.GetObject("DaoCountryVisibleText");
        }

        public IDAOCountryVisibleTextType GetDaoCountryVisibleTextType()
        {
            return (IDAOCountryVisibleTextType)_context.GetObject("DaoCountryVisibleTextType");
        }

        public IDAOCountryVisibleTextContent GetDaoCountryVisibleTextContent()
        {
            return (IDAOCountryVisibleTextContent)_context.GetObject("DaoCountryVisibleTextContent");
        }

        public IDAOCountryVisibleText_R_Country GetDaoCountryVisibleText_R_Country()
        {
            return (IDAOCountryVisibleText_R_Country)_context.GetObject("DaoCountryVisibleText_R_Country");
        }

        public IDAOCountryVisibleText_R_CountryVisibleTextType GetDaoCountryVisibleText_R_CountryVisibleTextType()
        {
            return (IDAOCountryVisibleText_R_CountryVisibleTextType)_context.GetObject("DaoCountryVisibleText_R_CountryVisibleTextType");
        }

        public IDAOLegal GetDaoLegal()
        {
            return (IDAOLegal)_context.GetObject("DaoLegal");
        }

        public IDAOUpgradeVariableText GetDaoUpgradeVariableText()
        {
            return (IDAOUpgradeVariableText)_context.GetObject("DaoUpgradeVariableText");
        }

        public IDAOUpgradeVariableTextType GetDaoUpgradeVariableTextType()
        {
            return (IDAOUpgradeVariableTextType)_context.GetObject("DaoUpgradeVariableTextType");
        }

        public IDAOUpgradeVariableTextContent GetDaoUpgradeVariableTextContent()
        {
            return (IDAOUpgradeVariableTextContent)_context.GetObject("DaoUpgradeVariableTextContent");
        }

        public IDAOUpgradeVariableText_R_Upgrade GetDaoUpgradeVariableText_R_Upgrade()
        {
            return (IDAOUpgradeVariableText_R_Upgrade)_context.GetObject("DaoUpgradeVariableText_R_Upgrade");
        }

        public IDAOUpgradeVariableText_R_UpgradeVariableTextType GetDaoUpgradeVariableText_R_UpgradeVariableTextType()
        {
            return (IDAOUpgradeVariableText_R_UpgradeVariableTextType)_context.GetObject("DaoUpgradeVariableText_R_UpgradeVariableTextType");
        }

        public IDAOHappyBirthBody GetDaoHappyBirthBody()
        {
            return (IDAOHappyBirthBody)_context.GetObject("DaoHappyBirthBody");
        }

        public IDAOQuoteExchangeBody GetDaoQuoteExchangeBody()
        {
            return (IDAOQuoteExchangeBody)_context.GetObject("DaoQuoteExchangeBody");
        }

        public IDAOQuoteMailACCOM GetDaoQuoteMailACCOM()
        {
            return (IDAOQuoteMailACCOM)_context.GetObject("DaoQuoteMailACCOM");
        }

        public IDAOTableVariableText GetDaoTableVariableText()
        {
            return (IDAOTableVariableText)_context.GetObject("DaoTableVariableText");
        }

        public IDAOTableVariableTextContent GetDaoTableVariableTextContent()
        {
            return (IDAOTableVariableTextContent)_context.GetObject("DaoTableVariableTextContent");
        }

        public IDAOConditionVariableText GetDaoConditionVariableText()
        {
            return (IDAOConditionVariableText)_context.GetObject("DaoConditionVariableText");
        }

        public IDAOConditionVariableTextContent GetDaoConditionVariableTextContent()
        {
            return (IDAOConditionVariableTextContent)_context.GetObject("DaoConditionVariableTextContent");
        }

        public IDAOConditionVariableText_R_VariableText GetDaoConditionVariableText_R_VariableText()
        {
            return (IDAOConditionVariableText_R_VariableText)_context.GetObject("DaoConditionVariableText_R_VariableText");
        }
        
        public IDAOGetReceivedConditions GetReceivedConditions()
        {
            return (IDAOGetReceivedConditions)_context.GetObject("DaoGetReceivedConditions");
        }

        public IDAOVoucherACNETDTO GetDAOVoucherACNETDTO()
        {
            return (IDAOVoucherACNETDTO)_context.GetObject("DAOVoucherACNETDTO");
        }

        public IDAOPointsReportHistory GetDAOPointsReportHistory()
        {
            return (IDAOPointsReportHistory)_context.GetObject("DAOPointsReportHistory");
        }

        public IDAOCapita GetDAOCapita()
        {
            return (IDAOCapita)_context.GetObject("DAOCapita");
        }

        public IDAOEmailLog_R_Capita GetDaoEmailLog_R_Capita()
        {
            return (IDAOEmailLog_R_Capita)_context.GetObject("DAOEmailLog_R_Capita");
        }

        public IDAOClausula_R_Estrategy GetDaoClausula_R_Estrategy()
        {
            return (IDAOClausula_R_Estrategy)_context.GetObject("DAOClausula_R_Estrategy");
        }

        public IDAOACCOMNotIssue GetDAOACCOMNotIssue()
        {
            return (IDAOACCOMNotIssue)_context.GetObject("DAOACCOMNotIssue");
        }

        public IDAONiceTrip GetDAONiceTrip()
        {
            return (IDAONiceTrip)_context.GetObject("DaoNiceTrip");
        }
        
        public IDAOBIPaxCumpleanos GetDaoPaxCumpleanos()
        {
            return (IDAOBIPaxCumpleanos)_context.GetObject("DaoPaxCumpleanos");
        }

        public IDAOPixel GetDaoPixel()
        {
            return (IDAOPixel)_context.GetObject("DaoPixel");
        }

        public IDAOBIPaxContinuaCompra GetDaoContinuaCompra()
        {
            return (IDAOBIPaxContinuaCompra)_context.GetObject("DaoBiPaxContinuaCompra");
        }

        internal IDAOTrackEmail GetDaoTrackEmail()
        {
            return (IDAOTrackEmail)_context.GetObject("DaoTrackEmail");
        }

        internal IDAOTrackEmailEvent GetDaoTrackEmailEvent()
        {
            return (IDAOTrackEmailEvent)_context.GetObject("DaoTrackEmailEvent");
        }

        internal IDAOTrackLink GetDaoTrackLink()
        {
            return (IDAOTrackLink)_context.GetObject("DaoTrackLink");
        }

        internal IDAOTrackLinkEvent GetDaoTrackLinkEvent()
        {
            return (IDAOTrackLinkEvent)_context.GetObject("DaoTrackLinkEvent");
        }

        internal IDAOGroupAttachment GetDaoGroupAttachment()
        {
            return (IDAOGroupAttachment)_context.GetObject("DaoGroupAttachment");
        }

        internal IDAOEstrategyAttachmentTemplate GetDaoEstrategyAttachmentTemplate()
        {
            return (IDAOEstrategyAttachmentTemplate)_context.GetObject("DaoEstrategyAttachmentTemplate");
        }
    }
}
