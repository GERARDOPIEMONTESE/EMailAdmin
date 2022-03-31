using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocioDatos.CapaHome;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.Utils;

namespace EMailAdmin.Controls.Product
{
    public partial class ProductSelector : UserControl
    {
        #region Delegate

        public delegate void CloseProductPress(object sender, EventArgs e);

        public delegate void ProductGridLoadComplete(object sender, EventArgs e);

        public delegate void SearchPress(object sender, EventArgs e);

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
            }
        }

        #endregion Constructor

        #region Properties

        public IList<BackEnd.Domain.Product> GetSelectedItems()
        {
            var result = new List<BackEnd.Domain.Product>();
            foreach (GridViewRow row in grvProduct.Rows)
            {
                var checkRow = (DataControlFieldCell) row.Cells[CHECKBOXCELL];
                if (((CheckBox) checkRow.Controls[1]).Checked)
                {
                    var product = new BackEnd.Domain.Product
                                      {
                                          Id = Convert.ToInt32(grvProduct.DataKeys[row.RowIndex].Value),
                                          Descripcion = row.Cells[DESCRIPTIONCELL].Text
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
                foreach (BackEnd.Domain.Product item in items)
                {
                    foreach (GridViewRow row in grvProduct.Rows)
                    {
                        if (item.Id == Convert.ToInt32(grvProduct.DataKeys[row.RowIndex].Value))
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
            foreach (BackEnd.Domain.Product product in GetSelectedItems())
            {
                if (sb.ToString() != "")
                {
                    sb.Append(", ");
                }
                //sb.Append(ddlCountry.SelectedValue);
                //sb.Append("/");
                sb.Append(product.Description);
            }
            return sb.ToString();
        }

        public void UnSelectAll()
        {
            foreach (GridViewRow row in grvProduct.Rows)
            {
                ((CheckBox)row.Cells[CHECKBOXCELL].Controls[1]).Checked = false;
            }
        }

        #endregion

        #region Events

        public event ProductGridLoadComplete ProductGridLoadCompleted;

        public void OnProductGridLoadCompleted(EventArgs e)
        {
            ProductGridLoadComplete handler = ProductGridLoadCompleted;
            if (handler != null) handler(this, e);
        }

        public event CloseProductPress CloseProductPressed;

        public void OnCloseProductPressed(EventArgs e)
        {
            CloseProductPress handler = CloseProductPressed;
            if (handler != null) handler(this, e);
        }

        public event SearchPress SearchPressed;

        public void OnSearchPressed(EventArgs e)
        {
            SearchPress handler = SearchPressed;
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

        protected void GrvProductRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.background ='#ecf0f9'");
                e.Row.Attributes.Add("onmouseout", "this.style.background ='#ffffff'");
            }
        }

        protected void BtnCloseOnClick(object sender, EventArgs e)
        {
            SessionManager.SetProductsSelected(GetSelectedItems(), Session);
            OnCloseProductPressed(EventArgs.Empty);
        }

        protected void ChkChanged(object sender, EventArgs e)
        {
            if (((CheckBox)grvProduct.Rows[0].Cells[CHECKBOXCELL].Controls[1]).Checked)
            {
                UnSelectAll();
                ((CheckBox) grvProduct.Rows[0].Cells[CHECKBOXCELL].Controls[1]).Checked = true;
            }
            OnChkPressed(EventArgs.Empty);
        }

        protected void BtnSearchOnClick(object sender, EventArgs e)
        {
            Bind();
            OnSearchPressed(EventArgs.Empty);
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
            var products = new List<BackEnd.Domain.Product>{new BackEnd.Domain.Product{Code = "-1", Id =-1, Descripcion = "All"}};
            products.AddRange(ProductHome.FindAllByCountry(ddlCountry.SelectedValue));                
            grvProduct.DataSource = products;
            grvProduct.DataBind();
            OnProductGridLoadCompleted(EventArgs.Empty);
        }

        #endregion Private Methods
    }
}