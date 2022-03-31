using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.Controls.Selectors.TableVariableText
{
    public partial class TableVariableSelector : System.Web.UI.UserControl
    {
        #region Delegate

        public delegate void TableVariableTextUploaded(object sender, EventArgs e);
        public delegate void TableVariableTextCancel(object sender, EventArgs e);

        #endregion

        #region Properties

        public BackEnd.Domain.TableVariableText TableVariableText
        {
            get { return TableVariableTextHome.Get(Convert.ToInt32(ddlType.SelectedValue)); }
        }

        #endregion

        #region Events

        public event TableVariableTextUploaded TableVariableTextUploadedCompleted;
        public event TableVariableTextCancel TableVariableTextCanceled;

        public void OnTableVariableTextUploadedCompleted(EventArgs e)
        {
            var handler = TableVariableTextUploadedCompleted;
            if (handler != null) handler(this, e);
        }

        public void OnTableVariableTextCanceled(EventArgs e)
        {
            var handler = TableVariableTextCanceled;
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
            OnTableVariableTextUploadedCompleted(EventArgs.Empty);
        }

        protected void BtnCancelOnClick(object sender, EventArgs e)
        {
            OnTableVariableTextCanceled(EventArgs.Empty);
        }

        #endregion

        #region Private Methods

        private void LoadList()
        {
            ddlType.DataSource = TableVariableTextHome.FindAll();
            ddlType.DataBind();
        }

        #endregion Private Methods

    }
}