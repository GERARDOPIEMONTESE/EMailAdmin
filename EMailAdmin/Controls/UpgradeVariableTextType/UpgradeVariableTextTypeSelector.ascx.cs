using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.Controls.UpgradeVariableTextType
{
    public partial class UpgradeVariableTextTypeSelector : UserControl
    {
        #region Delegate

        public delegate void UpgradeVariableTextGridLoadComplete(object sender, EventArgs e);
        public delegate void CloseUpgradeVariableTextPress(object sender, EventArgs e);

        #endregion

        #region Constants

        private const int CHECKBOXCELL = 0;
        private const int DESCRIPCIONCELL = 1;

        #endregion Constants

        #region Constructor

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSelector();
            }
        }

        #endregion Constructor

        #region Properties

        public IList<BackEnd.Domain.UpgradeVariableTextType> GetSelectedItems()
        {
            var result = new List<BackEnd.Domain.UpgradeVariableTextType>();
            foreach (GridViewRow row in grvUpgradeVariableTextType.Rows)
            {
                var checkRow = (DataControlFieldCell)row.Cells[CHECKBOXCELL];
                if (((CheckBox)checkRow.Controls[1]).Checked)
                {
                    var signType = new BackEnd.Domain.UpgradeVariableTextType
                                       {
                                           Id = Convert.ToInt32(grvUpgradeVariableTextType.DataKeys[row.RowIndex].Value),
                                           Description = row.Cells[DESCRIPCIONCELL].Text
                                       };
                    result.Add(signType);
                }
            }

            return result;
        }

        public void SelectItems(IList<BackEnd.Domain.UpgradeVariableTextType> items)
        {
            if (items != null)
            {
                foreach (var item in items)
                {
                    foreach (GridViewRow row in grvUpgradeVariableTextType.Rows)
                    {
                        if (item.Id == Convert.ToInt32(grvUpgradeVariableTextType.DataKeys[row.RowIndex].Value))
                        {
                            var checkRow = (DataControlFieldCell) row.Cells[CHECKBOXCELL];
                            ((CheckBox) checkRow.Controls[1]).Checked = true;
                        }
                    }
                }
            }
        }

        public string GetSelectedItemsText()
        {
            var sb = new StringBuilder();
            foreach (var UpgradeVariableTextType in GetSelectedItems())
            {
                if (sb.ToString() != "")
                {
                    sb.Append(", ");
                }
                sb.Append(UpgradeVariableTextType.Description);
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

        private void LoadSelector()
        {
            grvUpgradeVariableTextType.DataSource = UpgradeVariableTextTypeHome.FindAll();
            grvUpgradeVariableTextType.DataBind();
            OnUpgradeVariableTextGridLoadCompleted(EventArgs.Empty);
        }

        #endregion Private Methods
    }
}