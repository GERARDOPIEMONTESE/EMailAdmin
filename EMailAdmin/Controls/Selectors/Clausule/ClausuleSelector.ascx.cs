using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMailAdmin.Controls.Selectors.Clausule
{
    public partial class ClausuleSelector : System.Web.UI.UserControl
    {
        #region Delegate

        public delegate void ClausuleSelectorUploaded(object sender, EventArgs e);
        public delegate void ClausuleSelectorCancel(object sender, EventArgs e);

        #endregion

        #region Properties

        public string ClausuleSelectorText
        {
            get { return txtClausuleCode.Text; }
        }

        #endregion

        #region Events

        public event ClausuleSelectorUploaded ClausuleSelectorUploadedCompleted;
        public event ClausuleSelectorCancel ClausuleSelectorCanceled;

        public void OnClausuleSelectorUploadedCompleted(EventArgs e)
        {
            var handler = ClausuleSelectorUploadedCompleted;
            if (handler != null) handler(this, e);
        }

        public void OnClausuleSelectorCanceled(EventArgs e)
        {
            var handler = ClausuleSelectorCanceled;
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
            OnClausuleSelectorUploadedCompleted(EventArgs.Empty);
        }

        protected void BtnCancelOnClick(object sender, EventArgs e)
        {
            OnClausuleSelectorCanceled(EventArgs.Empty);
        }

        #endregion

        #region Private Methods

        private void LoadList()
        {
            txtClausuleCode.Text = "";
        }

        #endregion Private Methods
    }
}