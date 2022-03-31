using System;
using System.Collections.Generic;
using CapaNegocioDatos.CapaNegocio;

namespace EMailAdmin.Controls.Account
{
    public partial class AccountDropDown : System.Web.UI.UserControl
    {
        #region Delegate

        public delegate void GridLoadComplete(object sender, EventArgs e);
        public delegate void CloseButtonPress(object sender, EventArgs e);
        public delegate void SearchButtonPress(object sender, EventArgs e);

        #endregion

        #region Properties

        public IList<Sucursal> GetSelectedItems()
        {
            return accAccount.GetSelectedItems();
        }

        public void SelectItems(IList<Sucursal> items)
        {
            accAccount.SelectItems(items);
            CompleteDropDownList();
        }

        public void SelectItems(IList<int> ids)
        {
            accAccount.SelectItems(ids);
            CompleteDropDownList();
        }

        public void UnSelectAll()
        {
            accAccount.UnSelectAll();
            CompleteDropDownList();
        }

        #endregion Properties

        #region Events

        public event GridLoadComplete GridLoadCompleted;

        public void OnGridLoadCompleted(EventArgs e)
        {
            GridLoadComplete handler = GridLoadCompleted;
            if (handler != null) handler(this, e);
        }

        public event CloseButtonPress ClosePressed;

        public void OnCloseButtonPressed(EventArgs e)
        {
            CloseButtonPress handler = ClosePressed;
            if (handler != null) handler(this, e);
        }

        public event SearchButtonPress SearchPressed;

        public void OnSearchButtonPressed(EventArgs e)
        {
            SearchButtonPress handler = SearchPressed;
            if (handler != null) handler(this, e);
        }

        #endregion

        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlAccount.Attributes.Add("onclick", "javascript:showAccountPopUp();");
            }
        }

        protected void GridLoadedCompleted(object sender, EventArgs e)
        {
            OnGridLoadCompleted(EventArgs.Empty);
        }

        protected void CloseButtonPressed(object sender, EventArgs e)
        {
            mpeAccount.Hide();
            CompleteDropDownList();
            OnCloseButtonPressed(EventArgs.Empty);
        }

        protected void SearchButtonPressed(object sender, EventArgs e)
        {
            mpeAccount.Show();
            OnSearchButtonPressed(EventArgs.Empty);
        }

        #endregion Methods

        #region Private Methods

        private void CompleteDropDownList()
        {
            ddlAccount.Text = accAccount.GetSelectedItemsText();
        }

        #endregion Private Methods
    }
}