using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.Controls.CountryVisibleTextType
{
    public partial class CountryVisibleTextTypeSelector : UserControl
    {
        #region Delegate

        public delegate void CountryVisibleTextGridLoadComplete(object sender, EventArgs e);
        public delegate void CloseCountryVisibleTextPress(object sender, EventArgs e);

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

        public IList<BackEnd.Domain.CountryVisibleTextType> GetSelectedItems()
        {
            var result = new List<BackEnd.Domain.CountryVisibleTextType>();
            foreach (GridViewRow row in grvCountryVisibleTextType.Rows)
            {
                var checkRow = (DataControlFieldCell)row.Cells[CHECKBOXCELL];
                if (((CheckBox)checkRow.Controls[1]).Checked)
                {
                    var signType = new BackEnd.Domain.CountryVisibleTextType
                                       {
                                           Id = Convert.ToInt32(grvCountryVisibleTextType.DataKeys[row.RowIndex].Value),
                                           Description = row.Cells[DESCRIPCIONCELL].Text
                                       };
                    result.Add(signType);
                }
            }

            return result;
        }

        public void SelectItems(IList<BackEnd.Domain.CountryVisibleTextType> items)
        {
            if (items != null)
            {
                foreach (var item in items)
                {
                    foreach (GridViewRow row in grvCountryVisibleTextType.Rows)
                    {
                        if (item.Id == Convert.ToInt32(grvCountryVisibleTextType.DataKeys[row.RowIndex].Value))
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
            foreach (var CountryVisibleTextType in GetSelectedItems())
            {
                if (sb.ToString() != "")
                {
                    sb.Append(", ");
                }
                sb.Append(CountryVisibleTextType.Description);
            }
            return sb.ToString();
        }

        #endregion

        #region Events

        public event CountryVisibleTextGridLoadComplete CountryVisibleTextGridLoadCompleted;

        public void OnCountryVisibleTextGridLoadCompleted(EventArgs e)
        {
            CountryVisibleTextGridLoadComplete handler = CountryVisibleTextGridLoadCompleted;
            if (handler != null) handler(this, e);
        }

        public event CloseCountryVisibleTextPress CloseCountryVisibleTextPressed;

        public void OnCloseCountryVisibleTextPressed(EventArgs e)
        {
            CloseCountryVisibleTextPress handler = CloseCountryVisibleTextPressed;
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
            OnCloseCountryVisibleTextPressed(EventArgs.Empty);
        }

        #endregion Methods

        #region Private Methods

        private void LoadSelector()
        {
            grvCountryVisibleTextType.DataSource = CountryVisibleTextTypeHome.FindAll();
            grvCountryVisibleTextType.DataBind();
            OnCountryVisibleTextGridLoadCompleted(EventArgs.Empty);
        }

        #endregion Private Methods
    }
}