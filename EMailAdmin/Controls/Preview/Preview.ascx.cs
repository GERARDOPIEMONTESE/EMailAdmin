using System;
using System.IO;
using CapaNegocioDatos.CapaHome;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.Utils;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace EMailAdmin.Controls.Preview
{
    public partial class Preview : System.Web.UI.UserControl
    {
        #region Delegate

        public delegate void PreviewClose(object sender, EventArgs e);
        public delegate void PreviewShow(object sender, EventArgs e);

        #endregion

        #region Constructor

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadList();
            }
        }

        #endregion Constructor

        #region Events

        public event PreviewClose PreviewClosed;

        public void OnPreviewClose(EventArgs e)
        {
            var handler = PreviewClosed;
            if (handler != null) handler(this, e);
        }

        public event PreviewShow PreviewShowed;

        public void OnPreviewShow(EventArgs e)
        {
            var handler = PreviewShowed;
            if (handler != null) handler(this, e);
        }

        #endregion

        #region Methods

        protected void BtnViewOnClick(object sender, EventArgs e)
        {
            Show();
            OnPreviewShow(EventArgs.Empty);
        }

        protected void BtnCloseOnClick(object sender, EventArgs e)
        {
            OnPreviewClose(EventArgs.Empty);
        }

        protected void BtnDownloadOnClick(object sender, EventArgs e)
        {
            var temp = SessionManager.GetPreviewTemplate(Session);
            var bodyHtml = ServiceLocator.Instance().GetTemplateService().ParseBody(
                Convert.ToInt32(ddlLanguage.SelectedValue), SessionManager.GetLoguedUser(Session).IdPais, temp, true);
            var memory = GeneratePDF(bodyHtml);
            Context.Response.ContentType = "application/pdf";
            Context.Response.BinaryWrite(memory.ToArray());
        }

        #endregion Methods

        #region Private Methods

        private void LoadList()
        {            
            ddlLanguage.DataSource = IdiomaHome.Buscar();
            ddlLanguage.DataBind();
            ddlLanguage.SelectedValue = SessionManager.GetLoguedUser(Session).Ididioma.ToString();
        }

        private void Show()
        {
            var temp = SessionManager.GetPreviewTemplate(Session);
            var bodyHtml = ServiceLocator.Instance().GetTemplateService().ParseBody(
                Convert.ToInt32(ddlLanguage.SelectedValue), SessionManager.GetLoguedUser(Session).IdPais, temp, true);
            SessionManager.SetBodyHTML(bodyHtml, Session);
            var control = FindControl("frame");
            if(control != null)
            {
                control.Visible = true;
            }

           
        }

        public MemoryStream GeneratePDF(string bodyHtml)
        {
            var msOutput = new MemoryStream();
            var reader = new StringReader(bodyHtml);
            var document = new Document(PageSize.A4, 30, 30, 30, 30);
            var writer = PdfWriter.GetInstance(document, msOutput);
            var worker = new HTMLWorker(document);
            document.Open();
            worker.StartDocument();
            worker.Parse(reader);
            worker.EndDocument();
            worker.Close();
            document.Close();

            return msOutput;
        }

        #endregion Private Methods
    }
}