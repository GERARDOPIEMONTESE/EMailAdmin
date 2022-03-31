using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using CapaNegocioDatos.CapaHome;
using CapaNegocioDatos.CapaNegocio;
using EMailAdmin.Utils;

namespace EMailAdmin.Controls.Country
{
    public partial class CountrySelector : System.Web.UI.UserControl
    {
        #region Delegate

        public delegate void GridLoadComplete(object sender, EventArgs e);

        public delegate void ClosePress(object sender, EventArgs e);

        public delegate void ChkPress(object sender, EventArgs e);

        #endregion

        #region Constants

        private const int CHECKBOXCELL = 0;
        private const int NAMECELL = 1;

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

        public IList<Locacion> GetSelectedItems()
        {
            var result = new List<Locacion>();
            foreach (GridViewRow row in grvCountries.Rows)
            {
                var checkRow = (DataControlFieldCell) row.Cells[CHECKBOXCELL];
                if (((CheckBox) checkRow.Controls[1]).Checked)
                {
                    var locacion = new Locacion
                                       {
                                           Id = Convert.ToInt32(grvCountries.DataKeys[row.RowIndex].Value),
                                           Nombre = row.Cells[NAMECELL].Text
                                       };
                    result.Add(locacion);
                }
            }

            return result;
        }

        public string GetSelectedItemsText()
        {
            var sb = new StringBuilder();
            foreach (var locacion in GetSelectedItems())
            {
                if (sb.ToString() != "")
                {
                    sb.Append(", ");
                }
                sb.Append(locacion.Nombre);
            }
            return sb.ToString();
        }

        public void SelectItems(IList<Locacion> items)
        {
            if (items != null)
            {
                foreach (var item in items)
                {
                    foreach (GridViewRow row in grvCountries.Rows)
                    {
                        if (item.Id == Convert.ToInt32(grvCountries.DataKeys[row.RowIndex].Value))
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
            foreach (var id in ids)
            {
                foreach (GridViewRow row in grvCountries.Rows)
                {
                    if (id == Convert.ToInt32(grvCountries.DataKeys[row.RowIndex].Value))
                    {
                        var checkRow = (DataControlFieldCell) row.Cells[CHECKBOXCELL];
                        ((CheckBox) checkRow.Controls[1]).Checked = true;
                    }
                }
            }
        }

        public void UnSelectAll()
        {
            foreach (GridViewRow row in grvCountries.Rows)
            {
                ((CheckBox)row.Cells[CHECKBOXCELL].Controls[1]).Checked = false;
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

        public event ChkPress ChkPressed;

        public void OnChkPressed(EventArgs e)
        {
            ChkPress handler = ChkPressed;
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
            SessionManager.SetCountriesSelected(GetSelectedItems(), Session);
            OnClosePressed(EventArgs.Empty);
        }

        protected void ChkChanged(object sender, EventArgs e)
        {
            if (((CheckBox)grvCountries.Rows[0].Cells[CHECKBOXCELL].Controls[1]).Checked)
            {
                UnSelectAll();
                ((CheckBox)grvCountries.Rows[0].Cells[CHECKBOXCELL].Controls[1]).Checked = true;
            }
            OnChkPressed(EventArgs.Empty);
        }

        #endregion Methods

        #region Private Methods

        private void LoadSelector()
        {
            var paises = new List<Pais> { new Pais { Id = -1, Nombre = "All", IdLocacion = -1 } };
            paises.AddRange(PaisHome.BuscarLazy());
            grvCountries.DataSource = paises;
            grvCountries.DataBind();
            OnGridLoadCompleted(EventArgs.Empty);
        }

        #endregion Private Methods
    }
}