using System;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.Controls.Selectors.Signature
{
    public partial class SignatureSelector : System.Web.UI.UserControl
    {
        #region Delegate

        public delegate void SignatureUploaded(object sender, EventArgs e);
        public delegate void SignatureCancel(object sender, EventArgs e);

        #endregion

        #region Properties

        public BackEnd.Domain.SignatureType Signature
        {
            get { return SignatureTypeHome.Get(Convert.ToInt32(ddlType.SelectedValue)); }
        }

        #endregion

        #region Events

        public event SignatureUploaded SignatureUploadedCompleted;
        public event SignatureCancel SignatureCanceled;

        public void OnSignatureUploadedCompleted(EventArgs e)
        {
            var handler = SignatureUploadedCompleted;
            if (handler != null) handler(this, e);
        }

        public void OnSignatureCanceled(EventArgs e)
        {
            var handler = SignatureCanceled;
            if (handler != null) handler(this, e);
        }

        #endregion

        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadList();
            }
        }

        protected void BtnAcceptOnClick(object sender, EventArgs e)
        {
            OnSignatureUploadedCompleted(EventArgs.Empty);
        }

        protected void BtnCancelOnClick(object sender, EventArgs e)
        {
            OnSignatureCanceled(EventArgs.Empty);
        }

        #endregion

        #region Private Methods

        private void LoadList()
        {
            ddlType.DataSource = SignatureTypeHome.FindAll();
            ddlType.DataBind();
        }

        #endregion Private Methods
    }
}