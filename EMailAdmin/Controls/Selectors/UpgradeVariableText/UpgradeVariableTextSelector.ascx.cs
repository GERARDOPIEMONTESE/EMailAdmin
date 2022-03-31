using System;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.Controls.Selectors.UpgradeVariableText
{
    public partial class UpgradeVariableTextSelector : System.Web.UI.UserControl
    {
        #region Delegate

        public delegate void UpgradeVariableTextUploaded(object sender, EventArgs e);
        public delegate void UpgradeVariableTextCancel(object sender, EventArgs e);

        #endregion

        #region Properties

        public BackEnd.Domain.UpgradeVariableTextType UpgradeVariableText
        {
            get { return UpgradeVariableTextTypeHome.Get(Convert.ToInt32(ddlType.SelectedValue)); }
        }

        #endregion

        #region Events

        public event UpgradeVariableTextUploaded UpgradeVariableTextUploadedCompleted;
        public event UpgradeVariableTextCancel UpgradeVariableTextCanceled;

        public void OnUpgradeVariableTextUploadedCompleted(EventArgs e)
        {
            var handler = UpgradeVariableTextUploadedCompleted;
            if (handler != null) handler(this, e);
        }

        public void OnUpgradeVariableTextCanceled(EventArgs e)
        {
            var handler = UpgradeVariableTextCanceled;
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
            OnUpgradeVariableTextUploadedCompleted(EventArgs.Empty);
        }

        protected void BtnCancelOnClick(object sender, EventArgs e)
        {
            OnUpgradeVariableTextCanceled(EventArgs.Empty);
        }

        #endregion

        #region Private Methods

        private void LoadList()
        {
            ddlType.DataSource = UpgradeVariableTextTypeHome.FindAll();
            ddlType.DataBind();
        }

        #endregion Private Methods
    }
}