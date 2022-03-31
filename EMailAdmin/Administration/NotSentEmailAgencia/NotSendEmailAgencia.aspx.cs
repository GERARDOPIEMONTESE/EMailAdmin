using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ControlMenu;
using EMailAdmin.Utils;
using MenuPortalLibrary.CapaDTO;
using System.Configuration;
using CapaNegocioDatos.CapaNegocio;
using CapaNegocioDatos.CapaHome;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.Administration.NotSentEmailAgencia
{
    public partial class NotSendEmailAgencia : CustomPage
    {
        #region Constants
        //grvUsuarios
        private const int CHECKBOXCELL = 0;
        private const int IDSUCURSALCELL = 1;

        #endregion

        #region Constructor

        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFilters();
                SessionManager.SetLoguedUser(UsuarioLogueadoDTO(), Session);
            }
        }

        #endregion Constructor

        #region Propiedades

        protected override void ChequearSession()
        {
            UsuarioDTO usuario = base.UsuarioLogueadoDTO();
            if (usuario == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings["ExpireSessionRedirect"]);
            }
        }

        #endregion Propiedades

        #region Methods

        private void LoadFilters()
        {
            ddlPais.Items.Clear();
            var paises = new List<Pais> { new Pais { Id = -1, IdLocacion = -1, Codigo = "-1", Nombre = "Todos" } };
            paises.AddRange(PaisHome.Buscar());
            ddlPais.DataSource = paises;
            ddlPais.DataBind();
        }

        protected void BtnAddOnClick(object sender, EventArgs e)
        {
            mpeAccount.Show();
        }

        protected void ChkChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)(((Control)sender).Parent).Parent);
            int idSucursal = Convert.ToInt32(row.Cells[IDSUCURSALCELL].Text);

            if (((CheckBox)row.Cells[CHECKBOXCELL].Controls[1]).Checked)
                SessionManager.GetIdsUsuariosSelected(Session).Add(idSucursal);
            else
                SessionManager.GetIdsUsuariosSelected(Session).Remove(idSucursal);
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            Bind();
        }

        public void Bind()
        {
            grvCuentas.DataSource = EMailListExcludeHome.FindByFilters(int.Parse(ddlPais.SelectedValue), txtCodigo.Text);
            grvCuentas.DataBind();
        }

        protected void ibnDelete_Command(object sender, CommandEventArgs e)
        {
            int idExclude;
            if (int.TryParse(e.CommandArgument.ToString(), out idExclude))
            {
                EMailListExclude ctaExclude = EMailListExcludeHome.Get(idExclude);
                ctaExclude.Eliminar();

                Bind();
            }
        }

        protected void AccAccountSelectorOnSearch(object sender, EventArgs e)
        {
            mpeAccount.Show();
        }

        protected void AccAccountSelectorOnClose(object sender, EventArgs e)
        {
            Guardar(ValidarSeleccion());
        }

        private List<EMailListExclude> ValidarSeleccion()
        {
            List<EMailListExclude> lst = new List<EMailListExclude>();
             var sucsSelected = accAccount.GetSelectedItemsId();
            string[] sucursales = sucsSelected.Split(',');
            foreach (string item in sucursales)
            {
                int idSuc;
                if (int.TryParse(item, out idSuc))
                {
                    Sucursal suc = SucursalHome.ObtenerLight(idSuc);
                    if (suc != null && suc.Id > 0)
                    {
                        EMailListExclude itemExclude = EMailListExcludeHome.Get(int.Parse(suc.CodigoPais), suc.CodigoDeCuenta, suc.NumeroSucursal);
                        if (itemExclude.Id == 0)
                        {// es nuevo                        
                            lst.Add(new EMailListExclude()
                            {
                                AgencyCode = suc.CodigoDeCuenta,
                                Branch = suc.NumeroSucursal,
                                CountryCode = int.Parse(suc.CodigoPais)
                            });
                        }
                        else if (itemExclude.IdEstado == itemExclude.ObtenerEliminado()) //si esta eliminado lo habilita de nuevo                                              
                            lst.Add(itemExclude);
                    }
                }
            }

            return lst;
        }

        private void Guardar(List<EMailListExclude> lst)
        {
            foreach (EMailListExclude item in lst)
            {
                item.IdUsuario = UsuarioLogueadoDTO().IdUsuario;
                item.Persistir();
            }
            Bind();
        }

        #endregion
       
    }
}