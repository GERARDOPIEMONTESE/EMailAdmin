using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocioDatos.CapaHome;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.Utils;

namespace EMailAdmin.Controls.Rate
{
    public partial class RateSelector : UserControl
    {
        #region Delegate

        public delegate void CloseRatePress(object sender, EventArgs e);

        public delegate void RateGridLoadComplete(object sender, EventArgs e);

        public delegate void SearchPress(object sender, EventArgs e);

        public delegate void CountryChangedComplete(object sender, EventArgs e);

        public delegate void ChkPress(object sender, EventArgs e);

        #endregion

        #region Constants

        private const int CHECKBOXCELL = 0;
        private const int DESCRIPTIONCELL = 1;

        #endregion Constants

        #region Constructor

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFilters();
                LoadProducts();
            }
        }

        #endregion Constructor

        #region Properties

        public IList<BackEnd.Domain.Rate> GetSelectedItems()
        {
            var result = new List<BackEnd.Domain.Rate>();
            foreach (GridViewRow row in grvRate.Rows)
            {
                var checkRow = (DataControlFieldCell) row.Cells[CHECKBOXCELL];
                if (((CheckBox) checkRow.Controls[1]).Checked)
                {
                    var rate = new BackEnd.Domain.Rate
                                      {
                                          Id = Convert.ToInt32(grvRate.DataKeys[row.RowIndex].Value),
                                          Descripcion = row.Cells[DESCRIPTIONCELL].Text
                                      };
                    result.Add(rate);
                }
            }

            return result;
        }

        public void SelectItems(IList<BackEnd.Domain.Rate> items)
        {
            if (items != null)
            {
                foreach (BackEnd.Domain.Rate item in items)
                {
                    foreach (GridViewRow row in grvRate.Rows)
                    {
                        if (item.Id == Convert.ToInt32(grvRate.DataKeys[row.RowIndex].Value))
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
            foreach (BackEnd.Domain.Rate product in GetSelectedItems())
            {
                if (sb.ToString() != "")
                {
                    sb.Append(", ");
                }
                sb.Append(product.Description);
            }
            return sb.ToString();
        }

        public void UnSelectAll()
        {
            foreach (GridViewRow row in grvRate.Rows)
            {
                ((CheckBox)row.Cells[CHECKBOXCELL].Controls[1]).Checked = false;
            }
        }

        #endregion

        #region Events

        public event RateGridLoadComplete RateGridLoadCompleted;

        public void OnRateGridLoadCompleted(EventArgs e)
        {
            RateGridLoadComplete handler = RateGridLoadCompleted;
            if (handler != null) handler(this, e);
        }

        public event CloseRatePress CloseRatePressed;

        public void OnCloseRatePressed(EventArgs e)
        {
            CloseRatePress handler = CloseRatePressed;
            if (handler != null) handler(this, e);
        }

        public event SearchPress SearchPressed;

        public void OnSearchPressed(EventArgs e)
        {
            SearchPress handler = SearchPressed;
            if (handler != null) handler(this, e);
        }

        public event CountryChangedComplete CountryChangedCompleted;

        public void OnCountryChangedCompleted(EventArgs e)
        {
            CountryChangedComplete handler = CountryChangedCompleted;
            if (handler != null) handler(this, e);
        }

        public event ChkPress ChkPressed;

        public void OnChkPressed(EventArgs e)
        {
            ChkPress handler = ChkPressed;
            if (handler != null) handler(this, e);
        }

        #endregion

        #region Methods

        protected void GrvRateRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.background ='#ecf0f9'");
                e.Row.Attributes.Add("onmouseout", "this.style.background ='#ffffff'");
            }
        }

        protected void BtnCloseOnClick(object sender, EventArgs e)
        {
            SessionManager.SetRatesSelected(GetSelectedItems(), Session);
            OnCloseRatePressed(EventArgs.Empty);
        }

        protected void ChkChanged(object sender, EventArgs e)
        {
            if (((CheckBox)grvRate.Rows[0].Cells[CHECKBOXCELL].Controls[1]).Checked)
            {
                UnSelectAll();
                ((CheckBox) grvRate.Rows[0].Cells[CHECKBOXCELL].Controls[1]).Checked = true;
            }
            OnChkPressed(EventArgs.Empty);
        }

        protected void BtnSearchOnClick(object sender, EventArgs e)
        {
            Bind();
            OnSearchPressed(EventArgs.Empty);
        }

        protected void CountryChanged(object sender, EventArgs e)
        {
            LoadProducts();
            OnCountryChangedCompleted(EventArgs.Empty);
        }

        #endregion Methods

        #region Private Methods

        private void LoadFilters()
        {
            ddlCountry.DataSource = PaisHome.BuscarLazy();
            ddlCountry.DataBind();
        }

        private void Bind()
        {
            if (ddlProduct.Items.Count > 0)
            {
                var products = new List<BackEnd.Domain.Rate>
                                   {new BackEnd.Domain.Rate {Code = "-1", Id = -1, Descripcion = "All"}};
                products.AddRange(RateHome.FindAllByCountryAndProduct(ddlCountry.SelectedValue,
                                                                      Convert.ToInt32(ddlProduct.SelectedValue)));
                grvRate.DataSource = products;
                grvRate.DataBind();
            }
            OnRateGridLoadCompleted(EventArgs.Empty);
        }

        private void LoadProducts()
        {
            ddlProduct.DataSource = ProductHome.FindAllByCountry(ddlCountry.SelectedValue);
            ddlProduct.DataBind();
        }

        #endregion Private Methods
    }
}