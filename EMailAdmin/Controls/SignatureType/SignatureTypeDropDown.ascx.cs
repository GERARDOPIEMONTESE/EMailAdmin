using System;
using System.Collections.Generic;

namespace EMailAdmin.Controls.SignatureType
{
    public partial class SignatureTypeDropDown : System.Web.UI.UserControl
    {
        #region Delegate

        public delegate void GridSignatureLoadComplete(object sender, EventArgs e);

        #endregion

        #region Properties

        public IList<BackEnd.Domain.SignatureType> GetSelectedItems()
        {
            return sgnSignatureType.GetSelectedItems();
        }

        public void SelectItems(IList<BackEnd.Domain.SignatureType> items)
        {
            sgnSignatureType.SelectItems(items);
            CompleteDropDownList();
        }

        #endregion Properties

        #region Events

        public event GridSignatureLoadComplete GridSignatureLoadCompleted;

        public void OnGridSignatureLoadCompleted(EventArgs e)
        {
            GridSignatureLoadComplete handler = GridSignatureLoadCompleted;
            if (handler != null) handler(this, e);
        }

        #endregion

        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlType.Attributes.Add("onclick", "javascript:ShowSignaturePopUp();");
            }
        }

        protected void GridSignatureLoadedCompleted(object sender, EventArgs e)
        {
            OnGridSignatureLoadCompleted(EventArgs.Empty);
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
            ddlType.Text = sgnSignatureType.GetSelectedItemsText();
            //ddlType.Items.Clear();
            //if (sgnSignatureType.GetSelectedItems().Count > 0)
            //{
            //    ddlType.Items.Add(sgnSignatureType.GetSelectedItemsText());
            //    ddlType.SelectedIndex = 0;
            //}
        }

        #endregion Private Methods
    }
}