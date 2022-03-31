using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using CapaNegocioDatos.CapaHome;
using EMailAdmin.Utils;

namespace EMailAdmin.Controls.Template
{
    public partial class TabTemplateControl : System.Web.UI.UserControl
    {
        #region Delegate

        public delegate void ImageButton(object sender, EventArgs e);
        public delegate void LinkButton(object sender, EventArgs e);
        public delegate void VariableTextButton(object sender, EventArgs e);
        public delegate void SignatureButton(object sender, EventArgs e);
        public delegate void ContactButton(object sender, EventArgs e);
        public delegate void CountryVisibleTextButton(object sender, EventArgs e);
        public delegate void UpgradeVariableTextButton(object sender, EventArgs e);
        public delegate void TableVariableTextButton(object sender, EventArgs e);
        public delegate void ConditionVariableTextButton(object sender, EventArgs e);
        public delegate void PixelButton(object sender, EventArgs e);
        public delegate void ClausuleButton(object sender, EventArgs e);

        #endregion

        #region Constructor

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var control = (TemplateControl) tbcTemplate.ActiveTab.FindControl("TemplateControl");
                SessionManager.SetSelectedGridLanguage(control.IdLanguage, Session);
            }
            tbcTemplate.ActiveTab = tbcTemplate.Tabs[SessionManager.GetSelectedGridLanguage(Session) - 1];
        }

        #endregion Constructor

        #region Events

        public event ImageButton ImageUploadButton;

        public void OnImageUploadButton(EventArgs e)
        {
            var handler = ImageUploadButton;
            if (handler != null) handler(this, e);
        }

        public event LinkButton LinkUploadButton;

        public void OnLinkUploadButton(EventArgs e)
        {
            var handler = LinkUploadButton;
            if (handler != null) handler(this, e);
        }

        public event VariableTextButton VariableTextUploadButton;

        public void OnVariableTextUploadButton(EventArgs e)
        {
            var handler = VariableTextUploadButton;
            if (handler != null) handler(this, e);
        }

        public event SignatureButton SignatureUploadButton;

        public void OnSignatureUploadButton(EventArgs e)
        {
            var handler = SignatureUploadButton;
            if (handler != null) handler(this, e);
        }

        public event CountryVisibleTextButton CountryVisibleTextUploadButton;

        public void OnCountryVisibleTextUploadButton(EventArgs e)
        {
            var handler = CountryVisibleTextUploadButton;
            if (handler != null) handler(this, e);
        }

        public event UpgradeVariableTextButton UpgradeVariableTextUploadButton;

        public void OnUpgradeVariableTextUploadButton(EventArgs e)
        {
            var handler = UpgradeVariableTextUploadButton;
            if (handler != null) handler(this, e);
        }

        public event ContactButton ContactUploadButton;

        public void OnContactUploadButton(EventArgs e)
        {
            var handler = ContactUploadButton;
            if (handler != null) handler(this, e);
        }

        public event TableVariableTextButton TableUploadButton;

        public void OnTableUploadButton(EventArgs e)
        {
            var handler = TableUploadButton;
            if (handler != null) handler(this, e);
        }

        public event ConditionVariableTextButton ConditionVariableTextUploadButton;

        public void OnConditionVariableTextUploadButton(EventArgs e)
        {           
            var handler = ConditionVariableTextUploadButton;
            if (handler != null) handler(this, e);
        }

        public event PixelButton PixelUploadButton;

        public void OnPixelUploadButton(EventArgs e)
        {
            var handler = PixelUploadButton;
            if (handler != null) handler(this, e);
        }

        public event ClausuleButton ClausuleUploadButton;

        public void OnClausuleButton(EventArgs e)
        {
            var handler = ClausuleUploadButton;
            if (handler != null) handler(this, e);
        }

        #endregion

        #region Methods

        protected override void OnInit(EventArgs e)
        {
            var language = SessionManager.GetLoguedUser(Session).Ididioma;
            var idiomas = IdiomaHome.Buscar();
            foreach (var idioma in idiomas)
            {
                var tabPanel = new TabPanel();
                switch (InternacionalUtils.GetCulture(language))
                {
                    case "es":
                        tabPanel.HeaderText = idioma.Descripcion;
                        break;
                    case "en":
                        tabPanel.HeaderText = idioma.DescripcionIngles;
                        break;
                    case "pt":
                        tabPanel.HeaderText = idioma.DescripcionPortugues;
                        break;
                }
                tabPanel.ID = Convert.ToString(idioma.Id);

                var tab = (TemplateControl)TemplateControl.LoadControl("~/Controls/Template/TemplateControl.ascx");
                tab.ImageUploadButton += ImageUploadButtonPressed;
                tab.LinkUploadButton += LinkUploadButtonPressed;
                tab.VariableTextUploadButton += VariableTextUploadButtonPressed;
                tab.SignatureUploadButton += SignatureUploadButtonPressed;
                tab.CountryVisibleTextUploadButton += CountryVisibleTextUploadButtonPressed;
                tab.UpgradeVariableTextUploadButton += UpgradeVariableTextUploadButtonPressed;
                tab.ContactUploadButton += ContactUploadButtonPressed;
                tab.TableUploadButton += TableUploadButtonPressed;
                tab.ConditionVariableTextUploadButton += ConditionVariableTextUploadButtonPressed;
                tab.PixelUploadButton += PixelTextUploadButtonPressed;
                tab.ClausuleUploadButton += ClausuleUploadButtonPressed;
                tab.ID = "TemplateControl";
                tab.SetInformation(idioma.Id);

                tabPanel.Controls.Add(tab);

                tbcTemplate.Tabs.Add(tabPanel);
            }

            tbcTemplate.ActiveTabIndex = 0;
            base.OnInit(e);
        }

        protected void ImageUploadButtonPressed(object sender, EventArgs e)
        {
            OnImageUploadButton(e);
        }

        protected void LinkUploadButtonPressed(object sender, EventArgs e)
        {
            OnLinkUploadButton(e);
        }

        protected void VariableTextUploadButtonPressed(object sender, EventArgs e)
        {
            OnVariableTextUploadButton(e);
        }

        protected void SignatureUploadButtonPressed(object sender, EventArgs e)
        {
            OnSignatureUploadButton(e);
        }

        protected void ContactUploadButtonPressed(object sender, EventArgs e)
        {
            OnContactUploadButton(e);
        }

        protected void CountryVisibleTextUploadButtonPressed(object sender, EventArgs e)
        {
            OnCountryVisibleTextUploadButton(e);
        }

        protected void UpgradeVariableTextUploadButtonPressed(object sender, EventArgs e)
        {
            OnUpgradeVariableTextUploadButton(e);
        }

        protected void CtmTabsValidator(object sender, ServerValidateEventArgs validation)
        {
            validation.IsValid = IsValidBody() && IsValidSubject();
            return;
        }

        protected void TableUploadButtonPressed(object sender, EventArgs e)
        {
            OnTableUploadButton(e);
        }

        protected void ConditionVariableTextUploadButtonPressed(object sender, EventArgs e)
        {
            OnConditionVariableTextUploadButton(e);
        }

        protected void PixelTextUploadButtonPressed(object sender, EventArgs e)
        {
            OnPixelUploadButton(e);
        }

        protected void ClausuleUploadButtonPressed(object sender, EventArgs e)
        {
            OnClausuleButton(e);
        }

        #endregion Methods

        #region Public Methods

        public IList<BackEnd.Domain.Content> GetValues()
        {
            IList<BackEnd.Domain.Content> values = new List<BackEnd.Domain.Content>();

            foreach (TabPanel panel in tbcTemplate.Tabs)
            {
                var control = (TemplateControl)panel.FindControl("TemplateControl");
                var content = new BackEnd.Domain.Content
                                  {
                                      Body = control.Body,
                                      Subject = control.Subject,
                                      Language = IdiomaHome.Obtener(control.IdLanguage),
                                      Footer = control.Footer,
                                      FooterPDF = control.FooterPDF,
                                      Header = control.Header,
                                      HeaderPDF = control.HeaderPDF,
                                      Images = ContentUtils.GetContentImages(Session, control.IdLanguage),
                                      IdUsuario = SessionManager.GetLoguedUser(Session).Id,
                                      Color = control.ColorSel, // ContentUtils.GetContentColor(Session, control.IdLanguage)
                                      EVoucherName = control.EVoucherName
                                  };
                values.Add(content);
            }

            return values;
        }

        public IList<BackEnd.Domain.Content> GetValuesHtml()
        {
            IList<BackEnd.Domain.Content> values = new List<BackEnd.Domain.Content>();
            var template = SessionManager.GetTemplate(Session);

            foreach (TabPanel panel in tbcTemplate.Tabs)
            {
                var control = (TemplateControl)panel.FindControl("TemplateControl");

                var content = template.GetContent(control.IdLanguage);
                SetContentData(content, template, control);
                
                values.Add(content);
            }

            return values;
        }
        
        private void SetContentData(BackEnd.Domain.Content content, BackEnd.Domain.Template template, TemplateControl control)
        {
            content.Body = RichTextUtils.CleanWordCharacters(control.BodyHtml);
            content.Subject = control.Subject;
            content.Language = IdiomaHome.Obtener(control.IdLanguage);
            content.Footer = control.FooterDeleted ? null :
                control.Footer == null && template.Id != 0 ?
                template.GetContent(control.IdLanguage).Footer : control.Footer;
            content.FooterPDF = control.FooterPDFDeleted ? null :
                control.FooterPDF == null && template.Id != 0 ?
                template.GetContent(control.IdLanguage).FooterPDF : control.FooterPDF;
            content.Header = control.HeaderDeleted ? null : 
                control.Header == null && template.Id != 0 ? 
                template.GetContent(control.IdLanguage).Header : control.Header;
            content.HeaderPDF = control.HeaderPDFDeleted ? null :
                control.HeaderPDF == null && template.Id != 0 ?
                template.GetContent(control.IdLanguage).HeaderPDF : control.HeaderPDF;
            content.Color = control.ColorSel;
            content.EVoucherName = control.EVoucherName;
            content.Images = ContentUtils.GetContentImages(Session, control.IdLanguage);
            content.Contacts = ContentUtils.GetContentContacts(Session, control.IdLanguage);
            content.Signatures = ContentUtils.GetContentSignatures(Session, control.IdLanguage);
            content.Links = ContentUtils.GetContentLinks(Session, control.IdLanguage);
            content.VarableTexts = ContentUtils.GetContentVariableTexts(Session, control.IdLanguage);
            content.CountryVisibleTexts = ContentUtils.GetContentCountryVisibleTexts(Session, control.IdLanguage);
            content.UpgradeVariableTexts = ContentUtils.GetContentUpgradeVariableTexts(Session, control.IdLanguage);
            content.IdUsuario = SessionManager.GetLoguedUser(Session).Id;
        }

        public void SetValues(IList<BackEnd.Domain.Content> values)
        {
            foreach (TabPanel panel in tbcTemplate.Tabs)
            {
                foreach (var content in values)
                {
                    var control = (TemplateControl)panel.FindControl("TemplateControl");
                    if (control.IdLanguage == content.Language.Id)
                    {
                        control.Subject = content.Subject;
                        control.BodyHtml = RichTextUtils.CleanWordCharacters(content.Body);
                        control.HeaderName = content.Header != null && content.Header.Name != null
                            ? content.Header.Name : "";
                        control.FooterName = content.Footer != null && content.Footer.Name != null
                            ? content.Footer.Name : "";
                        control.HeaderPDFName = content.HeaderPDF != null && content.HeaderPDF.Name != null
                            ? content.HeaderPDF.Name : "";
                        control.FooterPDFName = content.FooterPDF != null && content.FooterPDF.Name != null
                            ? content.FooterPDF.Name : "";
                        control.ColorSel = content.Color != null ? content.Color : "";
                        control.EVoucherName = content.EVoucherName;
                    }
                }
            }
        }

        public void AddTag(string tag)
        {
            var control = (TemplateControl) tbcTemplate.Tabs[ SessionManager.GetSelectedGridLanguage(Session) - 1 ].FindControl("TemplateControl");
            control.AppendTag(tag);
        }

        public bool IsValidBody()
        {
            bool isValid = false;
            foreach (TabPanel panel in tbcTemplate.Tabs)
            {
                var control = (TemplateControl)panel.FindControl("TemplateControl");
                if (control.Body != "")
                {
                    isValid = true;
                    break;
                }
            }
            return isValid;
        }

        public bool IsValidSubject()
        {
            bool isValid = false;
            foreach (TabPanel panel in tbcTemplate.Tabs)
            {
                var control = (TemplateControl)panel.FindControl("TemplateControl");
                if (control.Subject != "")
                {
                    isValid = true;
                    break;
                }
            }
            return isValid;
        }
        #endregion Methods
    }
}