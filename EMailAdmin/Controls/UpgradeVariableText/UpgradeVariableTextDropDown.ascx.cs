using System;
using System.Collections.Generic;

namespace EMailAdmin.Controls.UpgradeVariableText
{
    public partial class UpgradeVariableTextDropDown : System.Web.UI.UserControl
    {
        #region Delegate

        public delegate void GridUpgradeVariableTextLoadComplete(object sender, EventArgs e);

        #endregion

        #region Properties

        public IList<BackEnd.Domain.Product> GetSelectedItems()
        {
            return uvtsUpgradeVariableText.GetSelectedItems();
        }

        public void SelectItems(IList<BackEnd.Domain.Product> items)
        {
            uvtsUpgradeVariableText.SelectItems(items);
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
                ddl.Attributes.Add("onclick", "javascript:ShowUpgradeVariableTextUpgradePopUp();");
            }
        }

        protected void GridUpgradeVariableTextLoadedCompleted(object sender, EventArgs e)
        {
            OnGridUpgradeVariableTextLoadCompleted(EventArgs.Empty);
        }

        protected void CloseButtonPressed(object sender, EventArgs e)
        {
            mpeUpgrade.Hide();
            CompleteDropDownList();
        }

        #endregion Methods

        #region Private Methods

        private void CompleteDropDownList()
        {
            ddl.Text = uvtsUpgradeVariableText.GetSelectedItemsText();
            //ddl.Items.Clear();
            //if (sgnUpgradeVariableText.GetSelectedItems().Count > 0)
            //{
            //    ddl.Items.Add(sgnUpgradeVariableText.GetSelectedItemsText());
            //    ddl.SelectedIndex = 0;
            //}
        }

        #endregion Private Methods

        #region Public Methods

        public void CleanAndBind()
        {
            uvtsUpgradeVariableText.Bind();
            CompleteDropDownList();
        }

        #endregion Public Methods
    }
}