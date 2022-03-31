using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocioDatos.CapaHome;
using CapaNegocioDatos.CapaNegocio;
using EMailAdmin.Utils;

namespace EMailAdmin.Controls.Account
{
    public partial class AccountSelector : UserControl
    {
        #region Delegate

        public delegate void ClosePress(object sender, EventArgs e);

        public delegate void GridLoadComplete(object sender, EventArgs e);

        public delegate void SearchPress(object sender, EventArgs e);

        #endregion

        #region Constants

        private const int CHECKBOXCELL = 0;
        private const int CODECELL = 1;
        private const int BRANCHNUMBERCELL = 2;
        private const int FIRMNAMECELL = 3;

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

        public IList<Sucursal> GetSelectedItems()
        {
            var result = new List<Sucursal>();
            foreach (GridViewRow row in grvAccounts.Rows)
            {
                var checkRow = (DataControlFieldCell) row.Cells[CHECKBOXCELL];
                if (((CheckBox) checkRow.Controls[1]).Checked)
                {
                    var sucursal = new Sucursal
                                       {
                                           Id = Convert.ToInt32(grvAccounts.DataKeys[row.RowIndex].Value),
                                           CodigoDeCuenta = row.Cells[CODECELL].Text,
                                           NumeroSucursal = Convert.ToInt32(row.Cells[BRANCHNUMBERCELL].Text),
                                           PersonaJuridica =
                                               new PersonaJuridica {RazonSocial = row.Cells[FIRMNAMECELL].Text}
                                       };
                    result.Add(sucursal);
                }
            }
            return result;
        }

        public string GetSelectedItemsText()
        {
            var sb = new StringBuilder();
            foreach (Sucursal sucursal in GetSelectedItems())
            {
                if (sb.ToString() != "")
                {
                    sb.Append(", ");
                }
                sb.Append(sucursal.CodigoDeCuenta + " - " + sucursal.NumeroSucursal);
            }
            return sb.ToString();
        }

        public string GetSelectedItemsId()
        {
            var sb = new StringBuilder();
            foreach (Sucursal sucursal in GetSelectedItems())
            {
                if (sb.ToString() != "")
                {
                    sb.Append(", ");
                }
                sb.Append(sucursal.Id);
            }
            return sb.ToString();
        }

        public void SelectItems(IList<Sucursal> items)
        {
            if (items != null)
            {
                foreach (Sucursal item in items)
                {
                    foreach (GridViewRow row in grvAccounts.Rows)
                    {
                        if (item.Id == Convert.ToInt32(grvAccounts.DataKeys[row.RowIndex].Value))
                        {
                            var checkRow = (DataControlFieldCell) row.Cells[CHECKBOXCELL];
                            ((CheckBox) checkRow.Controls[1]).Checked = true;
                        }
                    }
                }
            }
        }

        public void SelectItems(IList<int> ids)
        {
            foreach (int id in ids)
            {
                foreach (GridViewRow row in grvAccounts.Rows)
                {
                    if (id == Convert.ToInt32(grvAccounts.DataKeys[row.RowIndex].Value))
                    {
                        var checkRow = (DataControlFieldCell) row.Cells[CHECKBOXCELL];
                        ((CheckBox) checkRow.Controls[1]).Checked = true;
                    }
                }
            }
        }

        public void UnSelectAll()
        {
            foreach (GridViewRow row in grvAccounts.Rows)
            {
                ((CheckBox) row.Cells[CHECKBOXCELL].Controls[1]).Checked = false;
            }
        }

        #endregion

        #region Events

        public event GridLoadComplete GridLoadCompleted;

        public void OnGridLoadCompleted(EventArgs e)
        {
            GridLoadComplete handler = GridLoadCompleted;
            if (handler != null) handler(this, e);
        }

        public event ClosePress ClosePressed;

        public void OnClosePressed(EventArgs e)
        {
            ClosePress handler = ClosePressed;
            if (handler != null) handler(this, e);
        }

        public event SearchPress SearchPressed;

        public void OnSearchPressed(EventArgs e)
        {
            SearchPress handler = SearchPressed;
            if (handler != null) handler(this, e);
        }

        #endregion

        #region Methods

        protected void GrvAccountsRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.background ='#ecf0f9'");
                e.Row.Attributes.Add("onmouseout", "this.style.background ='#ffffff'");
            }
        }

        protected void BtnCloseOnClick(object sender, EventArgs e)
        {
            SessionManager.SetAccountsSelected(GetSelectedItems(), Session);
            OnClosePressed(EventArgs.Empty);
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
            var countries = new List<Pais> { new Pais { Codigo = "", IdLocacion = -1, Nombre = "", Id = -1 } };
            countries.AddRange(PaisHome.BuscarLazy());

            ddlCountry.DataSource = countries;
            ddlCountry.DataBind();
        }

        private void Bind()
        {
            grvAccounts.DataSource = SucursalHome.BuscarPorFiltrosDTO(Convert.ToInt32(ddlCountry.SelectedValue),
                                                                   txtCode.Text, txtFirmName.Text, txtName.Text);
            grvAccounts.DataBind();
            OnGridLoadCompleted(EventArgs.Empty);
        }

        #endregion Private Methods
    }
}