using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.Utils;

namespace EMailAdmin.Controls.UpgradeVariableText
{
    public partial class UpgradeVariableTextSelector : UserControl
    {
        #region Delegate

        public delegate void UpgradeVariableTextGridLoadComplete(object sender, EventArgs e);
        public delegate void CloseUpgradeVariableTextPress(object sender, EventArgs e);

        #endregion

        #region Constants

        private const int CHECKBOXCELL = 0;
        private const int CODECELL = 1;
        private const int DESCRIPCIONCELL = 2;

        #endregion Constants

        #region Properties

        public IList<BackEnd.Domain.Product> GetSelectedItems()
        {
            var result = new List<BackEnd.Domain.Product>();
            foreach (GridViewRow row in grvUpgradeVariableText.Rows)
            {
                var checkRow = (DataControlFieldCell)row.Cells[CHECKBOXCELL];
                if (((CheckBox)checkRow.Controls[1]).Checked)
                {
                    var product = new BackEnd.Domain.Product
                                       {
                                           Id = Convert.ToInt32(grvUpgradeVariableText.DataKeys[row.RowIndex].Value),
                                           Code = row.Cells[CODECELL].Text,
                                           Descripcion = row.Cells[DESCRIPCIONCELL].Text,
                                       };
                    result.Add(product);
                }
            }

            return result;
        }

        public void SelectItems(IList<BackEnd.Domain.Product> items)
        {
            if (items != null)
            {
                foreach (var item in items)
                {
                    foreach (GridViewRow row in grvUpgradeVariableText.Rows)
                    {
                        if (item.Id == Convert.ToInt32(grvUpgradeVariableText.DataKeys[row.RowIndex].Value))
                        {
                            var checkRow = (DataControlFieldCell) row.Cells[CHECKBOXCELL];
                            ((CheckBox) checkRow.Controls[1]).Checked = true;
                        }
                    }
                }
                pnlGrid.Update();
            }
        }

        public string GetSelectedItemsText()
        {
            var sb = new StringBuilder();
            foreach (var upgradeVariableText in GetSelectedItems())
            {
                if (sb.ToString() != "")
                {
                    sb.Append(", ");
                }
                sb.Append(upgradeVariableText.FullDescription);
            }
            return sb.ToString();
        }

        #endregion

        #region Events

        public event UpgradeVariableTextGridLoadComplete UpgradeVariableTextGridLoadCompleted;

        public void OnUpgradeVariableTextGridLoadCompleted(EventArgs e)
        {
            UpgradeVariableTextGridLoadComplete handler = UpgradeVariableTextGridLoadCompleted;
            if (handler != null) handler(this, e);
        }

        public event CloseUpgradeVariableTextPress CloseUpgradeVariableTextPressed;

        public void OnCloseUpgradeVariableTextPressed(EventArgs e)
        {
            CloseUpgradeVariableTextPress handler = CloseUpgradeVariableTextPressed;
            if (handler != null) handler(this, e);
        }

        #endregion

        #region Methods

        protected void GrvCountriesRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.background ='#ecf0f9'");
                e.Row.Attributes.Add("onmouseout", "this.style.background ='#ffffff'");
            }
        }

        protected void BtnCloseOnClick(object sender, EventArgs e)
        {
            OnCloseUpgradeVariableTextPressed(EventArgs.Empty);
        }

        #endregion Methods

        #region Private Methods

        #endregion Private Methods

        #region Public Methods

        public void Bind()
        {
            grvUpgradeVariableText.DataSource = ProductHome.FindAllUpgradesByCountry(SessionManager.GetUpgradeCountryCode(Session));
            grvUpgradeVariableText.DataBind();
            pnlGrid.Update();
        }

        #endregion Public Methods
    }
}