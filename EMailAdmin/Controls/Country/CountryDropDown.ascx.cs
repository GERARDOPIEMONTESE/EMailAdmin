using System;
using System.Collections.Generic;
using CapaNegocioDatos.CapaNegocio;

namespace EMailAdmin.Controls.Country
{
    public partial class CountryDropDown : System.Web.UI.UserControl
    {
        #region Delegate

        public delegate void GridLoadComplete(object sender, EventArgs e);
        public delegate void CloseButtonPress(object sender, EventArgs e);

        #endregion

        #region Properties

        public IList<Locacion> GetSelectedItems()
        {
            return ctrCountry.GetSelectedItems();
        }

        public void SelectItems(IList<Locacion> items)
        {
            ctrCountry.SelectItems(items);
            CompleteDropDownList();
        }

        public void SelectItems(IList<int> ids)
        {
            ctrCountry.SelectItems(ids);
            CompleteDropDownList();
        }

        public void UnSelectAll()
        {
            ctrCountry.UnSelectAll();
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

        #endregion

        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlCountry.Attributes.Add("onclick", "javascript:showPopUp();");
            }
        }

        protected void GridLoadedCompleted(object sender, EventArgs e)
        {
            OnGridLoadCompleted(EventArgs.Empty);
        }

        protected void CloseButtonPressed(object sender, EventArgs e)
        {
            mpeCountry.Hide();
            CompleteDropDownList();
            OnCloseButtonPressed(EventArgs.Empty);
        }

        #endregion Methods

        #region Private Methods

        private void CompleteDropDownList()
        {
            ddlCountry.Text = ctrCountry.GetSelectedItemsText();
            //ddlCountry.Items.Clear();
            //if (ctrCountry.GetSelectedItems().Count > 0)
            //{
            //    ddlCountry.Items.Add(ctrCountry.GetSelectedItemsText());
            //    ddlCountry.SelectedIndex = 0;
            //}
        }

        #endregion Private Methods
    }
}