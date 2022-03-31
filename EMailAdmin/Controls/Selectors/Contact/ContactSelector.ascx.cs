using System;
using System.Web.UI;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.Controls.Selectors.Contact
{
    public partial class ContactSelector : UserControl
    {
        #region Delegate

        public delegate void ContactUploaded(object sender, EventArgs e);
        public delegate void ContactCancel(object sender, EventArgs e);

        #endregion

        #region Properties

        public EMailContactType Contact
        {
            get { return EMailContactTypeHome.Get(Convert.ToInt32(ddlType.SelectedValue)); }
        }

        #endregion

        #region Events

        public event ContactUploaded ContactUploadedCompleted;
        public event ContactCancel ContactCanceled;

        public void OnContactUploadedCompleted(EventArgs e)
        {
            var handler = ContactUploadedCompleted;
            if (handler != null) handler(this, e);
        }

        public void OnContactCanceled(EventArgs e)
        {
            var handler = ContactCanceled;
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
            OnContactUploadedCompleted(EventArgs.Empty);
        }

        protected void BtnCancelOnClick(object sender, EventArgs e)
        {
            OnContactCanceled(EventArgs.Empty);
        }

        #endregion

        #region Private Methods

        private void LoadList()
        {
            ddlType.DataSource = EMailContactTypeHome.FindAll();
            ddlType.DataBind();
        }

        #endregion Private Methods
    }
}