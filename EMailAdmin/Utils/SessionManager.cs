using System.Collections.Generic;
using System.IO;
using System.Web.SessionState;
using CapaNegocioDatos.CapaNegocio;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Domain;
using MenuPortalLibrary.CapaDTO;

namespace EMailAdmin.Utils
{
    public class SessionManager
    {
        #region Constantes

        private const string IDEMAILCONTACT = "IdEMailContact";
        private const string EMAILCONTACT = "EMailContact";
        private const string SIGNATURE = "Signature";
        private const string COUNTRYVISIBLETEXT = "CountryVisibleText";
        private const string UPGRADEVARIABLETEXT = "UpgradeVariableText";
        private const string ATTACHMENT = "Attachment";
        private const string ATTACHMENTITEMS = "AttachmentItems";
        private const string GROUPCONDITIONS = "GroupConditions";
        private const string TEMPLATE = "Template";
        private const string ISCOPY = "IsCopy";
        private const string NEWASSOCIATION = "NewAssociation";
        private const string CONTENTIMAGES = "ContentImages";
        private const string CONTENTLINKS = "ContentLinks";
        private const string CONTENTVARIABLETEXT = "ContentVariableText";
        private const string SELECTEDGRIDLANGUAGE = "SelectedGridLanguage";
        private const string CONTENTSIGNATURES = "ContentSignatures";
        private const string CONTENTCOUNTRYVISIBLETEXT = "ContentCountryVisibleText";
        private const string CONTENTUPGRADEVARIABLETEXT = "ContentUPGRADEVARIABLETEXT";
        private const string CONTENTCONTACTS = "ContentContacts";
        private const string IMAGE = "Image";
        private const string SAVEIMAGE = "SaveImage";
        private const string IMAGEEXTENSION = "ImageExtension";
        private const string PREVIEWTEMPLATE = "PreviewTemplate";
        private const string PDFSTREAM = "PdfStream";
        private const string BODYHTML = "BodyHTML";
        private const string GROUPTYPE = "GroupType";
        private const string IDTODELETE = "IdToDelete";
        private const string LOGUEDUSER = "LoguedUser";
        private const string ISDELETEFROMPOPUP = "IsDeleteFromPopUp";
        private const string TEMPLATEASSOCIATIONS = "TemplateAssociations";
        private const string ATTACHMENTASSOCIATIONS = "AttachmentAssociations";
        private const string ACCOUNTSSELECTED = "AccountsSelected";
        private const string COUNTRIESSELECTED = "CountriesSelected";
        private const string PRODUCTSSELECTED = "ProductsSelected";
        private const string RATESSELECTED = "RatesSelected";
        private const string DYNAMICVALUESELECTED = "DynamicValueSelected";
        private const string ISDELETEDHEADER = "IsDeletedHeader";
        private const string TEMPLATEHEADER = "TemplateHeader";
        private const string ISDELETEDHEADERPDF = "IsDeletedHeaderPDF";
        private const string TEMPLATEHEADERPDF = "TemplateHeaderPDF";
        private const string TEMPLATEFOOTER = "TemplateFooter";
        private const string TEMPLATEFOOTERPDF = "TemplateFooterPDF";
        private const string ISDELETEDFOOTER = "IsDeletedFooter";
        private const string ISDELETEDFOOTERPDF = "IsDeletedFooterPDF";
        private const string TEMPLATEIMAGE = "TemplateImage";
        private const string TEMPLATECOLOR = "TemplateColor";
        private const string ADDEDITEMTOLIST = "AddedItemToList";
        private const string EMAILADDRESS = "EmailAddress";
        private const string GROUPIDTOVIEW = "GroupIdToView";
        private const string UPGRADECOUNTRYCODE = "UpgradeCountryCode";
        private const string EMAILLISTTYPE = "EmailListType";
        private const string EMAILLISTUSUARIOSSELECTED = "EMailListUsuariosSelected";
        private const string CONTENTTABLEVARIABLETEXT = "TableVariableText";
        private const string CONDITIONVARIABLETEXT = "ConditionVariableText";
        private const string CONTENTCONDITIONVARIABLETEXT = "ContentConditionVariableText";
        private const string GROUPIDTOEDIT = "GropuIdToEdit";
        private const string FILTRODESCCONDAGREGADA = "FiltroDescCondAgregada";
        private const string CAPITA = "Capita";
        private const string CONTENTATTACHMENT = "ContentAttachment";
        private const string SELECTED_CONFIGURATION_VALUE = "SelectedConfigurationValue";
        private const string SELECTED_PROCESSTYPE_VALUE = "SelectedProcessTypeValue";
        private const string ATTACHMENTTEMPLATES = "Attachment_Templates";

        #endregion Constantes

        #region Properties

        public static void SetContentSignatures(IDictionary<int, IList<SignatureType>> texts, HttpSessionState session)
        {
            session[CONTENTSIGNATURES] = texts;
        }

        public static IDictionary<int, IList<SignatureType>> GetContentSignatures(HttpSessionState session)
        {
            return (IDictionary<int, IList<SignatureType>>)session[CONTENTSIGNATURES];
        }

        public static void SetContentCountryVisibleText(IDictionary<int, IList<CountryVisibleTextType>> texts, HttpSessionState session)
        {
            session[CONTENTCOUNTRYVISIBLETEXT] = texts;
        }

        public static IDictionary<int, IList<CountryVisibleTextType>> GetContentCountryVisibleText(HttpSessionState session)
        {
            return (IDictionary<int, IList<CountryVisibleTextType>>)session[CONTENTCOUNTRYVISIBLETEXT];
        }

        public static void SetContentUpgradeVariableText(IDictionary<int, IList<UpgradeVariableTextType>> texts, HttpSessionState session)
        {
            session[CONTENTUPGRADEVARIABLETEXT] = texts;
        }

        public static IDictionary<int, IList<UpgradeVariableTextType>> GetContentUpgradeVariableText(HttpSessionState session)
        {
            return (IDictionary<int, IList<UpgradeVariableTextType>>)session[CONTENTUPGRADEVARIABLETEXT];
        }

       
        public static void SetContentContacts(IDictionary<int, IList<EMailContactType>> texts, HttpSessionState session)
        {
            session[CONTENTCONTACTS] = texts;
        }

        public static IDictionary<int, IList<EMailContactType>> GetContentContacts(HttpSessionState session)
        {
            return (IDictionary<int, IList<EMailContactType>>)session[CONTENTCONTACTS];
        }

        public static void SetSelectedGridLanguage(int idLanguage, HttpSessionState session)
        {
            session[SELECTEDGRIDLANGUAGE] = idLanguage;
        }

        public static int GetSelectedGridLanguage(HttpSessionState session)
        {
            return (int) session[SELECTEDGRIDLANGUAGE];
        }

        public static void SetContentVariableTexts(IDictionary<int, IList<VariableText>> texts, HttpSessionState session)
        {
            session[CONTENTVARIABLETEXT] = texts;
        }

        public static IDictionary<int, IList<VariableText>> GetContentVariableTexts(HttpSessionState session)
        {
            return (IDictionary<int, IList<VariableText>>)session[CONTENTVARIABLETEXT];
        }

        public static void SetContentLinks(IDictionary<int, IList<Link>> links, HttpSessionState session)
        {
            session[CONTENTLINKS] = links;
        }

        public static IDictionary<int, IList<Link>> GetContentLinks(HttpSessionState session)
        {
            return (IDictionary<int, IList<Link>>)session[CONTENTLINKS];
        }

        public static void SetContentImages(IDictionary<int, IList<ContentImage>> images, HttpSessionState session)
        {
            session[CONTENTIMAGES] = images;
        }

        public static IDictionary<int, IList<ContentImage>> GetContentImages(HttpSessionState session)
        {
            return (IDictionary<int, IList<ContentImage>>)session[CONTENTIMAGES];
        }

        public static void SetIdEMailContact(int id, HttpSessionState session)
        {
            session[IDEMAILCONTACT] = id;
        }

        public static int GetIdEMailContact(HttpSessionState session)
        {
            return (int)session[IDEMAILCONTACT];
        }

        public static void SetEMailContact(EMailContactDTO contact, HttpSessionState session)
        {
            session[EMAILCONTACT] = contact;
        }

        public static void SetEmailListType(BackEnd.Domain.EMailListType emailListType, HttpSessionState session)
        {
            session[EMAILLISTTYPE] = emailListType;
        }

        public static EMailContactDTO GetEMailContact(HttpSessionState session)
        {
            return (EMailContactDTO)session[EMAILCONTACT];
        }

        public static EMailListType GetEMailListType(HttpSessionState session)
        {
            return (EMailListType)session[EMAILLISTTYPE];
        }
        public static void SetSignature(BackEnd.Domain.Signature signature, HttpSessionState session)
        {
            session[SIGNATURE] = signature;
        }

        public static BackEnd.Domain.Template GetTemplate(HttpSessionState session)
        {
            return (BackEnd.Domain.Template)session[TEMPLATE];
        }

        public static void SetTemplate(BackEnd.Domain.Template template, HttpSessionState session)
        {
            session[TEMPLATE] = template;
        }

        public static bool GetIsCopy(HttpSessionState session)
        {
            return session[ISCOPY] != null ? (bool)session[ISCOPY] : false;
        }

        public static void SetIsCopy(bool isCopy, HttpSessionState session)
        {
            session[ISCOPY] = isCopy;
        }

        public static BackEnd.Domain.Signature GetSignature(HttpSessionState session)
        {
            return (BackEnd.Domain.Signature)session[SIGNATURE];
        }

        public static void SetAttachment(BackEnd.Domain.Attachment attachment, HttpSessionState session)
        {
            session[ATTACHMENT] = attachment;
        }

        public static BackEnd.Domain.Attachment GetAttachment(HttpSessionState session)
        {
            return (BackEnd.Domain.Attachment)session[ATTACHMENT];
        }

        public static void SetImage(byte[] image, HttpSessionState session)
        {
            session[IMAGE] = image;
        }

        public static byte[] GetImage(HttpSessionState session)
        {
            if (session != null)
            {
                return (byte[]) session[IMAGE];
            }
            return null;
        }

        public static void SetImageExtension(string extension, HttpSessionState session)
        {
            session[IMAGEEXTENSION] = extension;
        }

        public static string GetImageExtension(HttpSessionState session)
        {
            return (string)session[IMAGEEXTENSION];
        }

        public static void SetSaveImage(bool save, HttpSessionState session)
        {
            session[SAVEIMAGE] = save;
        }

        public static bool GetSaveImage(HttpSessionState session)
        {
            return (bool)session[SAVEIMAGE];
        }

        public static void SetAttachmentItems(IList<AttachmentItem> attachments, HttpSessionState session)
        {
            session[ATTACHMENTITEMS] = attachments;
        }

        public static IList<AttachmentItem> GetAttachmentItems(HttpSessionState session)
        {
            return (IList<AttachmentItem>)session[ATTACHMENTITEMS];
        }

        public static void RemoveAttachmentItems(HttpSessionState session)
        {
            session.Remove(ATTACHMENTITEMS);
        }

        public static IList<GroupCondition> GetGroupConditions(HttpSessionState session)
        {
            return (IList<GroupCondition>)session[GROUPCONDITIONS];
        }

        public static void SetGroupConditions(IList<GroupCondition> conditions, HttpSessionState session)
        {
            session[GROUPCONDITIONS] = conditions;
        }

        public static void SetPreviewTemplate(BackEnd.Domain.Template template, HttpSessionState session)
        {
            session[PREVIEWTEMPLATE] = template;
        }

        public static BackEnd.Domain.Template GetPreviewTemplate(HttpSessionState session)
        {
            return (BackEnd.Domain.Template)session[PREVIEWTEMPLATE];
        }

        public static void SetPdfStream(MemoryStream stream, HttpSessionState session)
        {
            session[PDFSTREAM] = stream;
        }

        public static MemoryStream GetPdfStream(HttpSessionState session)
        {
            return (MemoryStream)session[PDFSTREAM];
        }

        public static void SetBodyHTML(string bodyHtml, HttpSessionState session)
        {
            session[BODYHTML] = bodyHtml;
        }

        public static string GetBodyHTML(HttpSessionState session)
        {
            return (string)session[BODYHTML];
        }

        public static bool GetNewAssociation(HttpSessionState session)
        {
            return (bool)session[NEWASSOCIATION];
        }

        public static void SetNewAssociation(bool template, HttpSessionState session)
        {
            session[NEWASSOCIATION] = template;
        }

        public static GroupType GetGroupType(HttpSessionState session)
        {
            return (session[GROUPTYPE] != null ? (GroupType)session[GROUPTYPE] : new GroupType { Codigo = GroupType.NONE, Id = -1, Descripcion = "NONE" });
        }

        public static void SetGroupType(GroupType type, HttpSessionState session)
        {
            session[GROUPTYPE] = type;
        }

        public static void SetIdToDelete(int id, HttpSessionState session)
        {
            session[IDTODELETE] = id;
        }

        public static int GetIdToDelete(HttpSessionState session)
        {
            return (int)session[IDTODELETE];
        }

        public static void SetLoguedUser(UsuarioDTO user, HttpSessionState session)
        {
            session[LOGUEDUSER] = user;
        }

        public static UsuarioDTO GetLoguedUser(HttpSessionState session)
        {
            return (UsuarioDTO)session[LOGUEDUSER];
        }

        public static bool GetIsDeleteFromPopUp(HttpSessionState session)
        {
            return (bool)session[ISDELETEFROMPOPUP];
        }

        public static void SetIsDeleteFromPopUp(bool delete, HttpSessionState session)
        {
            session[ISDELETEFROMPOPUP] = delete;
        }

        public static IList<Group_R_Template> GetTemplateAssociations(HttpSessionState session)
        {
            return (IList<Group_R_Template>)session[TEMPLATEASSOCIATIONS] ?? new List<Group_R_Template>();
        }

        public static void SetTemplateAssociations(IList<Group_R_Template> associations, HttpSessionState session)
        {
            session[TEMPLATEASSOCIATIONS] = associations;
        }

        public static IList<Attachment_R_Group> GetAttachmentAssociations(HttpSessionState session)
        {
            return (IList<Attachment_R_Group>)session[ATTACHMENTASSOCIATIONS] ?? new List<Attachment_R_Group>();
        }

        public static void SetAttachmentAssociations(IList<Attachment_R_Group> associations, HttpSessionState session)
        {
            session[ATTACHMENTASSOCIATIONS] = associations;
        }

        public static IList<Sucursal> GetAccountsSelected(HttpSessionState session)
        {
            return (IList<Sucursal>)session[ACCOUNTSSELECTED] ?? new List<Sucursal>();
        }

        public static void SetAccountsSelected(IList<Sucursal> branches, HttpSessionState session)
        {
            session[ACCOUNTSSELECTED] = branches;
        }

        public static IList<Locacion> GetCountriesSelected(HttpSessionState session)
        {
            return (IList<Locacion>)session[COUNTRIESSELECTED] ?? new List<Locacion>();
        }

        public static void SetCountriesSelected(IList<Locacion> countries, HttpSessionState session)
        {
            session[COUNTRIESSELECTED] = countries;
        }

        public static IList<Product> GetProductsSelected(HttpSessionState session)
        {
            return (IList<Product>)session[PRODUCTSSELECTED] ?? new List<Product>();
        }

        public static void SetProductsSelected(IList<Product> products, HttpSessionState session)
        {
            session[PRODUCTSSELECTED] = products;
        }

        public static void SetDynamicValuesSelected(IList<DynamicCondition> DynamicValue, HttpSessionState session)
        {
            session[DYNAMICVALUESELECTED] = DynamicValue;
        }

        public static IList<DynamicCondition> GetDynamicValuesSelected(HttpSessionState session)
        {
            return (IList<DynamicCondition>)session[DYNAMICVALUESELECTED] ?? new List<DynamicCondition>();
        }

        public static IList<Rate> GetRatesSelected(HttpSessionState session)
        {
            return (IList<Rate>)session[RATESSELECTED] ?? new List<Rate>();
        }

        public static void SetRatesSelected(IList<Rate> rates, HttpSessionState session)
        {
            session[RATESSELECTED] = rates;
        }

        public static Dictionary<int, ContentImage> GetTemplateHeader(HttpSessionState session)
        {
            return (Dictionary<int, ContentImage>)session[TEMPLATEHEADER] ?? new Dictionary<int, ContentImage>();
        }

        public static void SetTemplateHeader(Dictionary<int, ContentImage> dic, HttpSessionState session)
        {
            session[TEMPLATEHEADER] = dic;
        }

        public static void RemoveTemplateHeader(HttpSessionState session)
        {
            session.Remove(TEMPLATEHEADER);
        }

        public static IDictionary<int, bool> GetIsDeletedHeader(HttpSessionState session)
        {
            return (IDictionary<int, bool>) session[ISDELETEDHEADER] ?? new Dictionary<int, bool>();
        }

        public static void SetIsDeletedHeader(IDictionary<int, bool> dic, HttpSessionState session)
        {
            session[ISDELETEDHEADER] = dic;
        }

        public static void RemoveIsDeletedHeader(HttpSessionState session)
        {
            session.Remove(ISDELETEDHEADER);
        }

        public static Dictionary<int, ContentImage> GetTemplateHeaderPDF(HttpSessionState session)
        {
            return (Dictionary<int, ContentImage>)session[TEMPLATEHEADERPDF] ?? new Dictionary<int, ContentImage>();
        }

        public static void SetTemplateHeaderPDF(Dictionary<int, ContentImage> dic, HttpSessionState session)
        {
            session[TEMPLATEHEADERPDF] = dic;
        }

        public static void RemoveTemplateHeaderPDF(HttpSessionState session)
        {
            session.Remove(TEMPLATEHEADERPDF);
        }

        public static IDictionary<int, bool> GetIsDeletedHeaderPDF(HttpSessionState session)
        {
            return (IDictionary<int, bool>)session[ISDELETEDHEADERPDF] ?? new Dictionary<int, bool>();
        }

        public static void SetIsDeletedHeaderPDF(IDictionary<int, bool> dic, HttpSessionState session)
        {
            session[ISDELETEDHEADERPDF] = dic;
        }

        public static void RemoveIsDeletedHeaderPDF(HttpSessionState session)
        {
            session.Remove(ISDELETEDHEADERPDF);
        }

        public static IDictionary<int,ContentImage> GetTemplateFooter(HttpSessionState session)
        {
            return (IDictionary<int, ContentImage>)session[TEMPLATEFOOTER] ?? new Dictionary<int, ContentImage>();
        }

        public static void SetTemplateFooter(IDictionary<int, ContentImage> dic, HttpSessionState session)
        {
            session[TEMPLATEFOOTER] = dic;
        }

        public static void RemoveTemplateFooter(HttpSessionState session)
        {
            session.Remove(TEMPLATEFOOTER);
        }

        public static IDictionary<int, bool> GetIsDeletedFooter(HttpSessionState session)
        {
            return (IDictionary<int, bool>)session[ISDELETEDFOOTER] ?? new Dictionary<int, bool>();
        }

        public static void SetIsDeletedFooter(IDictionary<int, bool> dic, HttpSessionState session)
        {
            session[ISDELETEDFOOTER] = dic;
        }

        public static void RemoveIsDeletedFooter(HttpSessionState session)
        {
            session.Remove(ISDELETEDFOOTER);
        }

        public static IDictionary<int, ContentImage> GetTemplateFooterPDF(HttpSessionState session)
        {
            return (IDictionary<int, ContentImage>)session[TEMPLATEFOOTERPDF] ?? new Dictionary<int, ContentImage>();
        }

        public static void SetTemplateFooterPDF(IDictionary<int, ContentImage> dic, HttpSessionState session)
        {
            session[TEMPLATEFOOTERPDF] = dic;
        }

        public static void RemoveTemplateFooterPDF(HttpSessionState session)
        {
            session.Remove(TEMPLATEFOOTERPDF);
        }

        public static IDictionary<int, bool> GetIsDeletedFooterPDF(HttpSessionState session)
        {
            return (IDictionary<int, bool>)session[ISDELETEDFOOTERPDF] ?? new Dictionary<int, bool>();
        }

        public static void SetIsDeletedFooterPDF(IDictionary<int, bool> dic, HttpSessionState session)
        {
            session[ISDELETEDFOOTERPDF] = dic;
        }

        public static void RemoveIsDeletedFooterPDF(HttpSessionState session)
        {
            session.Remove(ISDELETEDFOOTERPDF);
        }

        public static ContentImage GetTemplateImage(HttpSessionState session)
        {
            return (ContentImage)session[TEMPLATEIMAGE] ?? new ContentImage();
        }

        public static void SetTemplateImage(ContentImage image, HttpSessionState session)
        {
            session[TEMPLATEIMAGE] = image;
        }

        public static bool GetAddedItemToList(HttpSessionState session)
        {
            return (bool) session[ADDEDITEMTOLIST];
        }

        public static void SetAddedItemToList(bool added, HttpSessionState session)
        {
            session[ADDEDITEMTOLIST] = added;
        }

        public static EMailAddress GetEmailAddress(HttpSessionState session)
        {
            return (EMailAddress)session[EMAILADDRESS];
        }

        public static void SetEmailAddress(EMailAddress emailAddress, HttpSessionState session)
        {
            session[EMAILADDRESS] = emailAddress;
        }

        public static int GetGroupIdToView(HttpSessionState session)
        {
            return (int)session[GROUPIDTOVIEW];
        }

        public static void SetGroupIdToView(int idGroup, HttpSessionState session)
        {
            session[GROUPIDTOVIEW] = idGroup;
        }

        public static BackEnd.Domain.CountryVisibleText GetCountryVisibleText(HttpSessionState session)
        {
            return session[COUNTRYVISIBLETEXT] != null ? (BackEnd.Domain.CountryVisibleText)session[COUNTRYVISIBLETEXT] : new BackEnd.Domain.CountryVisibleText();
        }

        public static void SetCountryVisibleText(BackEnd.Domain.CountryVisibleText countryVisibleText, HttpSessionState session)
        {
            session[COUNTRYVISIBLETEXT] = countryVisibleText;
        }

        public static BackEnd.Domain.UpgradeVariableText GetUpgradeVariableText(HttpSessionState session)
        {
            return session[UPGRADEVARIABLETEXT] != null ? (BackEnd.Domain.UpgradeVariableText)session[UPGRADEVARIABLETEXT] : new BackEnd.Domain.UpgradeVariableText();
        }

        public static void SetUpgradeVariableText(BackEnd.Domain.UpgradeVariableText upgradeVariableText, HttpSessionState session)
        {
            session[UPGRADEVARIABLETEXT] = upgradeVariableText;
        }

        public static string GetUpgradeCountryCode(HttpSessionState session)
        {
            return session[UPGRADECOUNTRYCODE] != null ? (string) session[UPGRADECOUNTRYCODE] : "";
        }

        public static void SetUpgradeCountryCode(string countryCode, HttpSessionState session)
        {
            session[UPGRADECOUNTRYCODE] = countryCode;
        }

        public static List<int> GetIdsUsuariosSelected(HttpSessionState session)
        {
            return (List<int>)session[EMAILLISTUSUARIOSSELECTED] ?? new List<int>();
        }
        public static void SetIdsUsuariosSelected(List<int> idsUsus, HttpSessionState session)
        {
            session[EMAILLISTUSUARIOSSELECTED] = idsUsus;
        }

        public static IDictionary<int, IList<TableVariableText>> GetContentTableVariableText(HttpSessionState session)
        {
            return (IDictionary<int, IList<TableVariableText>>)session[CONTENTTABLEVARIABLETEXT];
        }

        public static void SetContentTableVariableText(IDictionary<int, IList<TableVariableText>> texts, HttpSessionState session)
        {
            session[CONTENTTABLEVARIABLETEXT] = texts;
        }

        public static void SetConditionVariableText(BackEnd.Domain.ConditionVariableText conditionVariableText, HttpSessionState session)
        {
            session[CONDITIONVARIABLETEXT] = conditionVariableText;
        }

        public static BackEnd.Domain.ConditionVariableText GetConditionVariableText(HttpSessionState session)
        {
            return session[CONDITIONVARIABLETEXT] != null ? (BackEnd.Domain.ConditionVariableText)session[CONDITIONVARIABLETEXT] : new BackEnd.Domain.ConditionVariableText();
        }

        #endregion Properties

        public static IDictionary<int, IList<BackEnd.Domain.ConditionVariableText>> GetContentConditionVariableText(HttpSessionState session)
        {
            return (IDictionary<int, IList<BackEnd.Domain.ConditionVariableText>>)session[CONTENTCONDITIONVARIABLETEXT];
        }

        public static void SetContentConditionVariableText(IDictionary<int, IList<BackEnd.Domain.ConditionVariableText>> ConditionVariableTextDic, HttpSessionState session)
        {
            session[CONTENTCONDITIONVARIABLETEXT] = ConditionVariableTextDic;
        }

        public static void SetGroupIdToEdit(int idGroup, HttpSessionState session)
        {
            session[GROUPIDTOEDIT] = idGroup;
        }
        public static void DeleteGroupIdToEdit(HttpSessionState session)
        {
            session.Remove(GROUPIDTOEDIT);
        }

        public static int GetGroupIdToEdit(HttpSessionState session)
        {
            return session[GROUPIDTOEDIT] != null ? (int)session[GROUPIDTOEDIT] : -1;
        }


        internal static void SetFiltroDescCondAgregada(string sFiltro, HttpSessionState session)
        {
            session[FILTRODESCCONDAGREGADA] = sFiltro;
        }

        internal static string GetFiltroDescCondAgregada(HttpSessionState session)
        {
            return session[FILTRODESCCONDAGREGADA] != null ? (string)session[FILTRODESCCONDAGREGADA] : "";
        }

        internal static void DeleteFiltroDescCondAgregada(HttpSessionState session)
        {
            session.Remove(FILTRODESCCONDAGREGADA);
        }
        
        internal static void SetCapitaSeleccionada(Capita capitaSel, HttpSessionState session)
        {
            session[CAPITA] = capitaSel;
        }

        internal static Capita GetCapitaSeleccionada(HttpSessionState session)
        {
            return session[CAPITA] != null ? (Capita)session[CAPITA] : null;
        }

        public static IDictionary<int, string> GetTemplateColor(HttpSessionState session)
        {
            return (IDictionary<int, string>)session[TEMPLATECOLOR] ?? new Dictionary<int, string>();
        }

        public static void SetTemplateColor(IDictionary<int, string> dic, HttpSessionState session)
        {
            session[TEMPLATECOLOR] = dic;
        }

        public static void RemoveTemplateColor(HttpSessionState session)
        {
            session.Remove(TEMPLATECOLOR);
        }


        public static void SetContentAttachment(IList<BackEnd.Domain.ContentAttachment> contentAttachment, HttpSessionState session)
        {
            session[CONTENTATTACHMENT] = contentAttachment;
        }

        internal static IList<BackEnd.Domain.ContentAttachment> GetContentAttachment(HttpSessionState session)
        {
            return ((IList<BackEnd.Domain.ContentAttachment>)session[CONTENTATTACHMENT]);
        }

        public static void RemoveContentAttachment(HttpSessionState session)
        {
            session.Remove(CONTENTATTACHMENT);
        }

        public static void SetAttachmentTemplates(IList<EstrategyAttachmentTemplate> AttachmentTemplates, HttpSessionState session)
        {
            session[ATTACHMENTTEMPLATES] = AttachmentTemplates;
        }

        internal static IList<EstrategyAttachmentTemplate> GetAttachmentTemplates(HttpSessionState session)
        {   
            return (IList<EstrategyAttachmentTemplate>)session[ATTACHMENTTEMPLATES] ?? new List<EstrategyAttachmentTemplate>();
        }

        public static void RemoveAttachmentTemplates(HttpSessionState session)
        {
            session.Remove(ATTACHMENTTEMPLATES);
        }

        internal static void AttachmentTemplates_AddItem(EstrategyAttachmentTemplate newItem, HttpSessionState session)
        {
            ((IList<EstrategyAttachmentTemplate>)session[ATTACHMENTTEMPLATES]).Add(newItem);
        }
        
        public static void RemoveContentItems(HttpSessionState session)
        {
            session.Remove(CONTENTIMAGES);
            session.Remove(CONTENTLINKS);
            session.Remove(CONTENTSIGNATURES);
            session.Remove(CONTENTTABLEVARIABLETEXT);
            session.Remove(CONTENTUPGRADEVARIABLETEXT);
            session.Remove(CONTENTVARIABLETEXT);    
        }

        #region SELECTED_CONFIGURATION_VALUE

        public static void SetSelectedConfigurationValue(ConfigurationValue configurationValue, HttpSessionState session)
        {
            session[SELECTED_CONFIGURATION_VALUE] = configurationValue;
        }

        public static ConfigurationValue GetSelectedConfigurationValue(HttpSessionState session)
        {
            if (session[SELECTED_CONFIGURATION_VALUE] == null)
                return new ConfigurationValue();

            return (ConfigurationValue)session[SELECTED_CONFIGURATION_VALUE];
        }

        public static void RemoveSelectedConfigurationValue(HttpSessionState session)
        {
            session.Remove(SELECTED_CONFIGURATION_VALUE);
        }

        #endregion

        #region SELECTED_PROCESSTYPE_VALUE

        public static void SetSelectedProcessTypeValue(EMailProcessType processTypeValue, HttpSessionState session)
        {
            session[SELECTED_PROCESSTYPE_VALUE] = processTypeValue;
        }

        public static EMailProcessType GetSelectedProcessTypeValue(HttpSessionState session)
        {
            if (session[SELECTED_PROCESSTYPE_VALUE] == null)
                return new EMailProcessType();

            return (EMailProcessType)session[SELECTED_PROCESSTYPE_VALUE];
        }

        public static void RemoveSelectedProcessTypeValue(HttpSessionState session)
        {
            session.Remove(SELECTED_PROCESSTYPE_VALUE);
        }

        #endregion

       
    }
}
