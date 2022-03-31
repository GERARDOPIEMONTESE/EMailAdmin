using System;
using System.Collections.Generic;

namespace EMailAdmin.Controls.CountryVisibleTextType
{
    public partial class CountryVisibleTextTypeDropDown : System.Web.UI.UserControl
    {
        #region Delegate

        public delegate void GridCountryVisibleTextLoadComplete(object sender, EventArgs e);

        #endregion

        #region Properties

        public IList<BackEnd.Domain.CountryVisibleTextType> GetSelectedItems()
        {
            return sgnCountryVisibleTextType.GetSelectedItems();
        }

        public void SelectItems(IList<BackEnd.Domain.CountryVisibleTextType> items)
        {
            sgnCountryVisibleTextType.SelectItems(items);
            CompleteDropDownList();
        }

        #endregion Properties

        #region Events

        public event GridCountryVisibleTextLoadComplete GridCountryVisibleTextLoadCompleted;

        public void OnGridCountryVisibleTextLoadCompleted(EventArgs e)
        {
            GridCountryVisibleTextLoadComplete handler = GridCountryVisibleTextLoadCompleted;
            if (handler != null) handler(this, e);
        }

        #endregion

        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlType.Attributes.Add("onclick", "javascript:ShowCountryVisibleTextPopUp();");
            }
        }

        protected void GridCountryVisibleTextLoadedCompleted(object sender, EventArgs e)
        {
            OnGridCountryVisibleTextLoadCompleted(EventArgs.Empty);
        }

        protected void CloseButtonPressed(object sender, EventArgs e)
        {
            mpeType.Hide();
            CompleteDropDownList();
        }

        #endregion Methods

        #region Private Methods

        private void CompleteDropDownList()
        {
            ddlType.Text = sgnCountryVisibleTextType.GetSelectedItemsText();
            //ddlType.Items.Clear();
            //if (sgnCountryVisibleTextType.GetSelectedItems().Count > 0)
            //{
            //    ddlType.Items.Add(sgnCountryVisibleTextType.GetSelectedItemsText());
            //    ddlType.SelectedIndex = 0;
            //}
        }

        #endregion Private Methods
    }
}