using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using ControlMenu;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.Utils;
using MenuPortalLibrary.CapaDTO;
using EMailAdmin.BackEnd.Home;
using CapaNegocioDatos.CapaHome;
using CapaNegocioDatos.CapaNegocio;
using EMailAdmin.BackEnd.DTO;

namespace EMailAdmin.Administration.EMailLists
{
    public partial class EMailListManager : CustomPage
    {
        #region Constants
        //grvUsuarios
        private const int CHECKBOXCELL = 0;
        private const int IDUSUARIOCELL = 1;        
        
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


        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            Bind();
        }

        protected void btnSearchUsu_OnClick(object sender, EventArgs e)
        {
            BindUsuarios();
        }

        protected void BtnAddOnClick(object sender, EventArgs e)
        {
            try
            {
                if (ddlEmailListTypeValidator.IsValid && ddlPaisValidator.IsValid)
                {
                    CleanUsuarios();            
                    mpeUsuarios.Show();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        protected void grvUsuariosPageIndexChange(object sender, GridViewPageEventArgs e)
        {
            grvUsuarios.PageIndex = e.NewPageIndex;
            BindUsuarios();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AddUsuarios();
            ClosePopUp();
        }

        protected void IbnDelete_Onclick(object sender, EventArgs e)
        {
            var bt = (ImageButton)sender;
            int idEmailList = -1;
            if (int.TryParse(bt.CommandArgument, out idEmailList))
                GetEMailList(idEmailList).Eliminar();
            Bind();
        }

        protected void ChkChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)(((Control)sender).Parent).Parent);
            int idUsuario = Convert.ToInt32(row.Cells[IDUSUARIOCELL].Text);
                
            if (((CheckBox)row.Cells[CHECKBOXCELL].Controls[1]).Checked)            
                SessionManager.GetIdsUsuariosSelected(Session).Add(idUsuario);
            else
                SessionManager.GetIdsUsuariosSelected(Session).Remove(idUsuario);                        
        }


        #endregion

        #region Private Methods

        private void LoadFilters()
        {
            btnSave.Visible = false;            
            
            ddlEmailListType.Items.Clear();
            var types = new List<EMailListType> { new EMailListType { Id = -1, Description = "Todos", Code = "" } };
            types.AddRange(EMailListTypeHome.FindAll());
            ddlEmailListType.DataSource = types;
            ddlEmailListType.DataBind();

            ddlPais.Items.Clear();
            var paises = new List<Pais> { new Pais { Id = -1, IdLocacion = -1, Codigo = "", Nombre = "Todos" } };
            paises.AddRange(PaisHome.Buscar());
            ddlPais.DataSource = paises;
            ddlPais.DataBind();

            ddlPais.SelectedValue =PaisHome.Obtener(UsuarioLogueadoDTO().IdPais).IdLocacion.ToString();
            ddlPais.Enabled = !UsuarioLogueadoDTO().EsCategoriaAssistCardPais;

            SessionManager.SetIdsUsuariosSelected(new List<int>(), Session);
        }

        private void Bind()
        {
            grvEmailListUsus.DataSource = EMailListHome.FindByFilters(Convert.ToInt32(ddlPais.SelectedValue), Convert.ToInt32(ddlEmailListType.SelectedValue), txtCorreoelectronico.Text);
            grvEmailListUsus.DataBind();
        }

        private void BindUsuarios()
        {
            grvUsuarios.DataSource = EMailListHome.Find_Tx_Usuarios(txtNombre.Text, txtApellido.Text, txtEMail.Text, Convert.ToInt32(ddlPais.SelectedValue), Convert.ToInt32(ddlEmailListType.SelectedValue));
            grvUsuarios.DataBind();
            grvUsuarios.Visible = true;
            btnSave.Visible = (grvUsuarios.Rows.Count > 0 ? true : false);
        }
        private void CleanUsuarios()
        {
            SessionManager.SetIdsUsuariosSelected(new List<int>(), Session);
            grvUsuarios.Visible = false;
            btnSave.Visible = false;
            txtApellido.Text = "";
            txtNombre.Text = "";
            txtEMail.Text = "";
        }
        
        private void AddUsuarios()
        {
            foreach (int idUsu in SessionManager.GetIdsUsuariosSelected(Session))
            {
                if (IsValid)
                    GetEMailListUsuario(idUsu).Persistir();
            }
        }

        private EMailList GetEMailListUsuario(int idUsuario)
        {
            EMailList eMailList = new EMailList()
            {
                IdEmailListType = Convert.ToInt32(ddlEmailListType.SelectedValue),
                IdPais = Convert.ToInt32(ddlPais.SelectedValue),
                IdUsuarioEL = idUsuario,
                IdUsuario = UsuarioLogueadoDTO().IdUsuario
            };

            return eMailList;
        }
        private EMailList GetEMailList(int idEmailList)
        {
            EMailList eMailList = new EMailList(idEmailList, UsuarioLogueadoDTO().IdUsuario);
            return eMailList;
        }

        private void ClosePopUp()
        {
            Bind();
            uplItems.Update();
            mpeUsuarios.Hide();
        }

        #endregion

        #region Validator
        protected void ddlPaisValidatorComplete(object sender, ServerValidateEventArgs validation)
        {
            validation.IsValid = Convert.ToInt32(ddlPais.SelectedValue) != -1;
            return;
        }

        protected void ddlEmailListTypeValidatorComplete(object sender, ServerValidateEventArgs validation)
        {
            validation.IsValid = Convert.ToInt32(ddlEmailListType.SelectedValue) != -1;
            return;
        }
        #endregion
    }
}