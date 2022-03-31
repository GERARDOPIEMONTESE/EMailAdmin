using System;
using System.Collections.Generic;
using System.Web.UI;

namespace EMailAdmin.Controls.Product
{
    public partial class ProductDropDown : UserControl
    {
        #region Delegate

        public delegate void GridLoadComplete(object sender, EventArgs e);
        public delegate void CloseButtonPress(object sender, EventArgs e);
        public delegate void SearchButtonPress(object sender, EventArgs e);

        #endregion

        #region Properties

        public IList<BackEnd.Domain.Product> GetSelectedItems()
        {
            return proProduct.GetSelectedItems();
        }

        public void SelectItems(IList<BackEnd.Domain.Product> items)
        {
            proProduct.SelectItems(items);
            CompleteDropDownList();
        }

        public void UnSelectAll()
        {
            proProduct.UnSelectAll();
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
                ddlProduct.Attributes.Add("onclick", "javascript:showProductPopUp();");                
            }
        }

        protected void GridLoadedCompleted(object sender, EventArgs e)
        {
            OnGridLoadCompleted(EventArgs.Empty);
        }

        protected void CloseButtonPressed(object sender, EventArgs e)
        {
            mpeProduct.Hide();
            CompleteDropDownList();
            OnCloseButtonPressed(EventArgs.Empty);
        }

        protected void SearchButtonPressed(object sender, EventArgs e)
        {
            mpeProduct.Show();
            OnSearchButtonPressed(EventArgs.Empty);
        }

        #endregion Methods

        #region Private Methods

        private void CompleteDropDownList()
        {
            ddlProduct.Text = proProduct.GetSelectedItemsText();
        }

        #endregion Private Methods
    }
}