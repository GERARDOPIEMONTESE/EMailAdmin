using System;
using System.Collections.Generic;

namespace EMailAdmin.Controls.UpgradeVariableTextType
{
    public partial class UpgradeVariableTextTypeDropDown : System.Web.UI.UserControl
    {
        #region Delegate

        public delegate void GridUpgradeVariableTextLoadComplete(object sender, EventArgs e);

        #endregion

        #region Properties

        public IList<BackEnd.Domain.UpgradeVariableTextType> GetSelectedItems()
        {
            return sgnUpgradeVariableTextType.GetSelectedItems();
        }

        public void SelectItems(IList<BackEnd.Domain.UpgradeVariableTextType> items)
        {
            sgnUpgradeVariableTextType.SelectItems(items);
            CompleteDropDownList();
        }

        #endregion Properties

        #region Events

        public event GridUpgradeVariableTextLoadComplete GridUpgradeVariableTextLoadCompleted;

        public void OnGridUpgradeVariableTextLoadCompleted(EventArgs e)
        {
            GridUpgradeVariableTextLoadComplete handler = GridUpgradeVariableTextLoadCompleted;
            if (handler != null) handler(this, e);
        }

        #endregion

        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlType.Attributes.Add("onclick", "javascript:ShowUpgradeVariableTextPopUp();");
            }
        }

        protected void GridUpgradeVariableTextLoadedCompleted(object sender, EventArgs e)
        {
            OnGridUpgradeVariableTextLoadCompleted(EventArgs.Empty);
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
            ddlType.Text = sgnUpgradeVariableTextType.GetSelectedItemsText();
            //ddlType.Items.Clear();
            //if (sgnUpgradeVariableTextType.GetSelectedItems().Count > 0)
            //{
            //    ddlType.Items.Add(sgnUpgradeVariableTextType.GetSelectedItemsText());
            //    ddlType.SelectedIndex = 0;
            //}
        }

        #endregion Private Methods
    }
}