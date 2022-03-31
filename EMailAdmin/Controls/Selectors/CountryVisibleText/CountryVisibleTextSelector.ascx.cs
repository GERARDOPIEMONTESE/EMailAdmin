using System;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.Controls.Selectors.CountryVisibleText
{
    public partial class CountryVisibleTextSelector : System.Web.UI.UserControl
    {
        #region Delegate

        public delegate void CountryVisibleTextUploaded(object sender, EventArgs e);
        public delegate void CountryVisibleTextCancel(object sender, EventArgs e);

        #endregion

        #region Properties

        public BackEnd.Domain.CountryVisibleTextType CountryVisibleText
        {
            get { return CountryVisibleTextTypeHome.Get(Convert.ToInt32(ddlType.SelectedValue)); }
        }

        #endregion

        #region Events

        public event CountryVisibleTextUploaded CountryVisibleTextUploadedCompleted;
        public event CountryVisibleTextCancel CountryVisibleTextCanceled;

        public void OnCountryVisibleTextUploadedCompleted(EventArgs e)
        {
            var handler = CountryVisibleTextUploadedCompleted;
            if (handler != null) handler(this, e);
        }

        public void OnCountryVisibleTextCanceled(EventArgs e)
        {
            var handler = CountryVisibleTextCanceled;
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
            OnCountryVisibleTextUploadedCompleted(EventArgs.Empty);
        }

        protected void BtnCancelOnClick(object sender, EventArgs e)
        {
            OnCountryVisibleTextCanceled(EventArgs.Empty);
        }

        #endregion

        #region Private Methods

        private void LoadList()
        {
            ddlType.DataSource = CountryVisibleTextTypeHome.FindAll();
            ddlType.DataBind();
        }

        #endregion Private Methods
    }
}