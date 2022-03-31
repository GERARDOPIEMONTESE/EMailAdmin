using System;
using CapaNegocioDatos.CapaNegocio;
using CapaNegocioDatos.CapaHome;
using EMailAdmin.Utils;

namespace EMailAdmin.Controls.TabRich
{
    public partial class RichTextControl : System.Web.UI.UserControl
    {
        #region Constructor

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #endregion Constructor

        #region Properties

        public string Text
        {
            get 
            {
                return RichTextUtils.CleanWordCharacters(txtRichText.Text); 
            }
            set
            {
                txtRichText.Text = RichTextUtils.CleanWordCharacters(value);
            }
        }

        public string HtmlText
        {
            get
            {
                return txtRichText.ViewStateText ?? RichTextUtils.CleanWordCharacters(txtRichText.Text);
            }
            set
            {
                txtRichText.Text = RichTextUtils.CleanWordCharacters(value);
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

        public bool ValidateRequest
        { get; set; }

        #endregion Properties

        #region Public Methods

        public void SetInformation(int languageId)
        {
            hfdLanguage.Value = languageId.ToString();
        }

        public void SetInformation(string richText)
        {
            txtRichText.Text = richText;
        }

        public void Clean()
        {
            txtRichText.Text = "";
        }

        #endregion Public Methods
    }
}