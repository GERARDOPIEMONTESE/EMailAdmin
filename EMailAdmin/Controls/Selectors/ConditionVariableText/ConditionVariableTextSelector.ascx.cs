using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.Controls.Selectors.ConditionVariableText
{
    public partial class ConditionVariableTextSelector : System.Web.UI.UserControl
    {
        #region Delegate

        public delegate void ConditionVariableTextUploaded(object sender, EventArgs e);
        public delegate void ConditionVariableTextCancel(object sender, EventArgs e);

        #endregion

        #region Properties

        public BackEnd.Domain.ConditionVariableText ConditionVariableText
        {
            get { return ConditionVariableTextHome.Get(Convert.ToInt32(ddlConditionVT.SelectedValue)); }            
        }

        #endregion

        #region Events

        public event ConditionVariableTextUploaded ConditionVariableTextUploadedCompleted;
        public event ConditionVariableTextCancel ConditionVariableTextCanceled;

        public void OnConditionVariableTextUploadedCompleted(EventArgs e)
        {
            var handler = ConditionVariableTextUploadedCompleted;
            if (handler != null) handler(this, e);
        }

        public void OnUpgradeVariableTextCanceled(EventArgs e)
        {
            var handler = ConditionVariableTextCanceled;
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
            OnConditionVariableTextUploadedCompleted(EventArgs.Empty);
        }

        protected void BtnCancelOnClick(object sender, EventArgs e)
        {
            OnUpgradeVariableTextCanceled(EventArgs.Empty);
        }

        #endregion

        #region Private Methods

        private void LoadList()
        {
            ddlConditionVT.DataSource = ConditionVariableTextHome.FindAll();
            ddlConditionVT.DataBind();
        }

        #endregion Private Methods
    }
}