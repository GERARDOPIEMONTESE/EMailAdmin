using System;
using System.Globalization;
using System.Web.UI;
using AjaxControlToolkit;
using CapaNegocioDatos.CapaHome;
using CapaNegocioDatos.CapaNegocio;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.Utils;
using System.Collections.Generic;
using CapaNegocioDatos.Servicios;

namespace EMailAdmin.Controls.Template
{
    public partial class TemplateControl : UserControl
    {
        #region Delegate

        public delegate void ImageButton(object sender, EventArgs e);
        public delegate void LinkButton(object sender, EventArgs e);
        public delegate void VariableTextButton(object sender, EventArgs e);
        public delegate void SignatureButton(object sender, EventArgs e);
        public delegate void CountryVisibleTextButton(object sender, EventArgs e);
        public delegate void UpgradeVariableTextButton(object sender, EventArgs e);
        public delegate void ContactButton(object sender, EventArgs e);
        public delegate void TableButton(object sender, EventArgs e);
        public delegate void ConditionVariableTextButton(object sender, EventArgs e);
        public delegate void PixelButton(object sender, EventArgs e);
        public delegate void ClausuleButton(object sender, EventArgs e);

        #endregion

        #region Constructor

        protected void Page_Load(object sender, EventArgs e)
        {
            //TODO: COMPLETAR EL TEXTO PARA Q PAREZCA LLENO.
            if(IsPostBack)
            {
            }
            EnableControls();
        }

        #endregion Constructor

        private void EnableControls()
        {
            btnUpgradeVariableText.Visible = ServicioCodigoActivador.
                 Instancia().ValidarHabilitarTextoVariableUpgrade();
            bool enablePdfHeaderFooter = ServicioCodigoActivador.Instancia().
                ValidarHabilitarEMailHeaderFooterPDF();
            
            lblHeaderPDFName.Visible = enablePdfHeaderFooter;
            lblHeaderPDF.Visible = enablePdfHeaderFooter;
            fupHeaderPDF.Visible = enablePdfHeaderFooter;
            btnDeleteHeaderPDF.Visible = enablePdfHeaderFooter;

            lblFooterPDFName.Visible = enablePdfHeaderFooter;
            lblFooterPDF.Visible = enablePdfHeaderFooter;
            fupFooterPDF.Visible = enablePdfHeaderFooter;
            btnDeleteFooterPDF.Visible = enablePdfHeaderFooter;
        }

        #region Properties

        public string HeaderName
        {
            get
            {
                return lblHeaderName.Text;
            }
            set
            {
                lblHeaderName.Text = value;
            }
        }

        public string HeaderPDFName
        {
            get
            {
                return lblHeaderPDFName.Text;
            }
            set
            {
                lblHeaderPDFName.Text = value;
            }
        }

        public string FooterName
        {
            get
            {
                return lblFooterName.Text;
            }
            set
            {
                lblFooterName.Text = value;
            }
        }

        public string FooterPDFName
        {
            get
            {
                return lblFooterPDFName.Text;
            }
            set
            {
                lblFooterPDFName.Text = value;
            }
        }

        public string Subject
        {
            get
            {
                return txtSubject.Text;
            }
            set
            {
                txtSubject.Text = value;
            }
        }

        public string ColorSel
        {
            get
            {
                return txtColor.Text;
            }
            set
            {
                txtColor.Text = value;
                txtColor.ToolTip = SetRGBColor(txtColor.Text);
            }
        }

        private string SetRGBColor(string colorHEX)
        {
            if (colorHEX != "")
            {
                var sysColor = System.Drawing.ColorTranslator.FromHtml("#" + colorHEX);
                return sysColor.ToString();
            }
            else
                return "";
        }

        public string EVoucherName
        {
            get
            {
                return txtEVoucherName.Text;
            }
            set
            {
                txtEVoucherName.Text = value;
            }
        }

        public bool HeaderDeleted 
        {
            get
            {
                IDictionary<int, bool> dic = SessionManager.GetIsDeletedHeader(Session);

                return dic.ContainsKey(IdLanguage) ? dic[IdLanguage] : false;
            }
            set
            {
                IDictionary<int, bool> dic = SessionManager.GetIsDeletedHeader(Session);

                if (dic.ContainsKey(IdLanguage))
                {
                    dic[IdLanguage] = value;
                }
                else
                {
                    dic.Add(IdLanguage, value);
                }
                SessionManager.SetIsDeletedHeader(dic, Session);
            }
        }

        public bool HeaderPDFDeleted
        {
            get
            {
                IDictionary<int, bool> dic = SessionManager.GetIsDeletedHeaderPDF(Session);

                return dic.ContainsKey(IdLanguage) ? dic[IdLanguage] : false;
            }
            set
            {
                IDictionary<int, bool> dic = SessionManager.GetIsDeletedHeaderPDF(Session);

                if (dic.ContainsKey(IdLanguage))
                {
                    dic[IdLanguage] = value;
                }
                else
                {
                    dic.Add(IdLanguage, value);
                }
                SessionManager.SetIsDeletedHeaderPDF(dic, Session);
            }
        }

        public bool FooterDeleted 
        {
            get
            {
                IDictionary<int, bool> dic = SessionManager.GetIsDeletedFooter(Session);

                return dic.ContainsKey(IdLanguage) ? dic[IdLanguage] : false;
            }
            set
            {
                IDictionary<int, bool> dic = SessionManager.GetIsDeletedFooter(Session);

                if (dic.ContainsKey(IdLanguage))
                {
                    dic[IdLanguage] = value;
                }
                else
                {
                    dic.Add(IdLanguage, value);
                }
                SessionManager.SetIsDeletedFooter(dic, Session);
            }
        }

        public bool FooterPDFDeleted
        {
            get
            {
                IDictionary<int, bool> dic = SessionManager.GetIsDeletedFooterPDF(Session);

                return dic.ContainsKey(IdLanguage) ? dic[IdLanguage] : false;
            }
            set
            {
                IDictionary<int, bool> dic = SessionManager.GetIsDeletedFooterPDF(Session);

                if (dic.ContainsKey(IdLanguage))
                {
                    dic[IdLanguage] = value;
                }
                else
                {
                    dic.Add(IdLanguage, value);
                }
                SessionManager.SetIsDeletedFooterPDF(dic, Session);
            }
        }

        public ContentImage Header
        {
            get
            {
                var dic = SessionManager.GetTemplateHeader(Session);
                if(dic.ContainsKey(IdLanguage))
                {
                    return dic[IdLanguage];
                }
                return null;
            }
        }

        public ContentImage HeaderPDF
        {
            get
            {
                var dic = SessionManager.GetTemplateHeaderPDF(Session);
                if (dic.ContainsKey(IdLanguage))
                {
                    return dic[IdLanguage];
                }
                return null;
            }
        }

        public ContentImage Footer
        {
            get
            {
                var dic = SessionManager.GetTemplateFooter(Session);
                if (dic.ContainsKey(IdLanguage))
                {
                    return dic[IdLanguage];
                }
                return null;
            }
        }

        public ContentImage FooterPDF
        {
            get
            {
                var dic = SessionManager.GetTemplateFooterPDF(Session);
                if (dic.ContainsKey(IdLanguage))
                {
                    return dic[IdLanguage];
                }
                return null;
            }
        }

        public string Body
        {
            get
            {
                return txtRichText.Text;
            }
            set
            {
                txtRichText.Text = value;
            }
        }

        public string BodyHtml
        {
            get
            {
                return txtRichText.ViewStateText ?? txtRichText.Text;
            }
            set
            {
                txtRichText.Text = value;
            }
        }

        public Idioma Language
        {
            get { return IdiomaHome.Obtener(Convert.ToInt32(hfdLanguage.Value)); }
        }

        public int IdLanguage
        {
            get { return Convert.ToInt32(hfdLanguage.Value); }
        }

        public bool ValidateRequest{ get; set; }

        #endregion Properties

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

        public event VariableTextButton ContactUploadButton;

        public void OnContactUploadButton(EventArgs e)
        {
            var handler = ContactUploadButton;
            if (handler != null) handler(this, e);
        }

        public event TableButton TableUploadButton;

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

        public void OnClausuleUploadButton(EventArgs e)
        {
            var handler = ClausuleUploadButton;
            if (handler != null) handler(this, e);
        }

        #endregion

        #region Methods

        protected void BtnDeleteHeaderOnClick(object sender, EventArgs e)
        {
            HeaderDeleted = true;
            lblHeaderName.Text = "";
        }

        protected void BtnDeleteHeaderPDFOnClick(object sender, EventArgs e)
        {
            HeaderPDFDeleted = true;
            lblHeaderPDFName.Text = "";
        }

        protected void BtnDeleteFooterOnClick(object sender, EventArgs e)
        {
            FooterDeleted = true;
            lblFooterName.Text = "";
        }

        protected void BtnDeleteFooterPDFOnClick(object sender, EventArgs e)
        {
            FooterPDFDeleted = true;
            lblFooterPDFName.Text = "";
        }

        protected void BtnImageOnClick(object sender, EventArgs e)
        {
            SessionManager.SetSelectedGridLanguage(Convert.ToInt32(hfdLanguage.Value), Session);
            OnImageUploadButton(e);
        }

        protected void BtnLinkOnClick(object sender, EventArgs e)
        {
            SessionManager.SetSelectedGridLanguage(Convert.ToInt32(hfdLanguage.Value), Session);
            OnLinkUploadButton(e);
        }

        protected void BtnVariableTextOnClick(object sender, EventArgs e)
        {
            SessionManager.SetSelectedGridLanguage(Convert.ToInt32(hfdLanguage.Value), Session);
            OnVariableTextUploadButton(e);
        }

        protected void BtnSignatureOnClick(object sender, EventArgs e)
        {
            SessionManager.SetSelectedGridLanguage(Convert.ToInt32(hfdLanguage.Value), Session);
            OnSignatureUploadButton(e);
        }

        protected void BtnCountryVisibleTextOnClick(object sender, EventArgs e)
        {
            SessionManager.SetSelectedGridLanguage(Convert.ToInt32(hfdLanguage.Value), Session);
            OnCountryVisibleTextUploadButton(e);
        }

        protected void BtnUpgradeVariableTextOnClick(object sender, EventArgs e)
        {
            SessionManager.SetSelectedGridLanguage(Convert.ToInt32(hfdLanguage.Value), Session);
            OnUpgradeVariableTextUploadButton(e);
        }

        protected void BtnContactOnClick(object sender, EventArgs e)
        {
            SessionManager.SetSelectedGridLanguage(Convert.ToInt32(hfdLanguage.Value), Session);
            OnContactUploadButton(e);
        }

        protected void BtnTableOnClick(object sender, EventArgs e)
        {
            SessionManager.SetSelectedGridLanguage(Convert.ToInt32(hfdLanguage.Value), Session);
            OnTableUploadButton(e);
        }

        protected void BtnConditionVariableTextOnClick(object sender, EventArgs e)
        {
            SessionManager.SetSelectedGridLanguage(Convert.ToInt32(hfdLanguage.Value), Session);
            OnConditionVariableTextUploadButton(e);
        }

        protected void BtnPixelOnClick(object sender, EventArgs e)
        {
            SessionManager.SetSelectedGridLanguage(Convert.ToInt32(hfdLanguage.Value), Session);
            OnPixelUploadButton(e);
        }

        protected void BtnClausuleOnClick(object sender, EventArgs e)
        {
            SessionManager.SetSelectedGridLanguage(Convert.ToInt32(hfdLanguage.Value), Session);
            OnClausuleUploadButton(e);
        }   

        protected void HeaderLoaded(object sender, AsyncFileUploadEventArgs args)
        {
            var dic = SessionManager.GetTemplateHeader(Session);
            var image = FileUtils.Image(fupHeader);
            lblHeaderName.Text = image.Name;
            HeaderDeleted = false;
            if(dic.ContainsKey(IdLanguage))
            {
                dic[IdLanguage] = image;
            }
            else
            {
                dic.Add(IdLanguage, image);
            }
            SessionManager.SetTemplateHeader(dic, Session);
            
        }

        protected void HeaderPDFLoaded(object sender, AsyncFileUploadEventArgs args)
        {
            var dic = SessionManager.GetTemplateHeaderPDF(Session);
            var image = FileUtils.Image(fupHeaderPDF);
            lblHeaderPDFName.Text = image.Name;
            HeaderPDFDeleted = false;
            if (dic.ContainsKey(IdLanguage))
            {
                dic[IdLanguage] = image;
            }
            else
            {
                dic.Add(IdLanguage, image);
            }
            SessionManager.SetTemplateHeaderPDF(dic, Session);

        }

        protected void FooterLoaded(object sender, AsyncFileUploadEventArgs args)
        {
            var dic = SessionManager.GetTemplateFooter(Session);
            var image = FileUtils.Image(fupFooter);
            lblFooterName.Text = image.Name;
            FooterDeleted = false;
            if (dic.ContainsKey(IdLanguage))
            {
                dic[IdLanguage] = image;
            }
            else
            {
                dic.Add(IdLanguage, image);
            }
            SessionManager.SetTemplateFooter(dic, Session);
        }

        protected void FooterPDFLoaded(object sender, AsyncFileUploadEventArgs args)
        {
            var dic = SessionManager.GetTemplateFooterPDF(Session);
            var image = FileUtils.Image(fupFooterPDF);
            lblFooterPDFName.Text = image.Name;
            FooterPDFDeleted = false;
            if (dic.ContainsKey(IdLanguage))
            {
                dic[IdLanguage] = image;
            }
            else
            {
                dic.Add(IdLanguage, image);
            }
            SessionManager.SetTemplateFooterPDF(dic, Session);
        }


        #endregion Methods

        #region Public Methods

        public void SetInformation(int languageId)
        {
            hfdLanguage.Value = languageId.ToString(CultureInfo.InvariantCulture);
        }

        public void SetInformation(string subject, string header, string body, string footer)
        {
            txtRichText.Text = body;
            txtSubject.Text = subject;
        }

        public void Clean()
        {
            txtRichText.Text = "";
            txtSubject.Text = "";
        }

        public void AppendTag(string tag)
        {
            txtRichText.Text = txtRichText.Text + " " + tag + " ";
        }

        #endregion Public Methods   

    }
}