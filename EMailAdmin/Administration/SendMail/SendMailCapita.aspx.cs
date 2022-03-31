using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ControlMenu;
using CapaNegocioDatos.CapaHome;
using EMailAdmin.Utils;
using MenuPortalLibrary.CapaDTO;
using System.Configuration;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.BackEnd.Domain;
using CapaNegocioDatos.CapaNegocio;
using EMailAdmin.Controls.Selectors.Capita;

namespace EMailAdmin.Administration.SendMail
{
    public partial class SendMailCapita : CustomPage
    {

        private const string ModuleCode = "ACNET";
        private const string UserACNETService = "mailservice@assist-card.com";
        private const string PassACNETService = "123456";
        
        protected override void CustomPageLoad(object sender, EventArgs e)
        {
             if (!IsPostBack)
            {
                LoadDropDown();
                SessionManager.SetLoguedUser(UsuarioLogueadoDTO(), Session);
            }
        }

        protected override void ChequearSession()
        {
            UsuarioDTO usuario = base.UsuarioLogueadoDTO();
            if (usuario == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings["ExpireSessionRedirect"]);
            }
        }

        private void LoadDropDown()
        {
            ddlTipoDocumento.Items.Clear();
            ddlTipoDocumento.DataSource = TipoDocumentoHome.Buscar();
            ddlTipoDocumento.DataBind();
            ddlTipoDocumento.Items.Insert(0, new ListItem() { Value = "", Text = "" });

            ddlIdioma.Items.Clear();
            ddlIdioma.DataSource = IdiomaHome.Buscar();
            ddlIdioma.DataTextField = Idioma.GetIdiomaDescripcion(UsuarioLogueadoDTO().Ididioma);
            ddlIdioma.DataBind();
        }

        protected void BtnAcceptOnClick(object sender, EventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    Capita capita = SessionManager.GetCapitaSeleccionada(Session);
                    if (capita != null)
                    {
                        EmailCapitaDTO dto = new EmailCapitaDTO();
                        dto.CountryCode = int.Parse(capita.Pais.Codigo);
                        dto.CountryName = capita.Pais.Nombre;
                        dto.ModuleCode = ModuleCode == null || ModuleCode.Length == 0 ? "-" : ModuleCode.Trim();
                        dto.To = txtMailTo.Text;
                        dto.Nombre = txtNombre.Text;
                        dto.Apellido = txtApellido.Text;
                        dto.PaxType = ddlTipoDocumento.SelectedItem.Text;
                        dto.PaxPassport = txtDocumento.Text;
                        dto.ProductName = capita.Descripcion;
                        dto.ProductCode = capita.Codigo;
                        dto.RateName = capita.Plan.Descripcion;
                        dto.RateCode = capita.Plan.Codigo;
                        dto.IdLanguage = int.Parse(ddlIdioma.SelectedItem.Value);

                        ServiceLocator.Instance().GetSendMailService().SendMailCapita(dto);
                        lblPopUp.Text = GetLocalResourceObject("CAPITA_ENVIADA").ToString();
                    }
                    else
                    {
                        lblPopUp.Text = GetLocalResourceObject("NOT_CAPITA").ToString();
                    }
                    mpePopUpMail.Show();
                }
            }
            catch (Exception ex)
            {
                string msg = "ERROR_SEND_EMAIL";
                if (ex.Message == "NOT_CAPITA_LINKS") msg = ex.Message;
                lblPopUp.Text = GetLocalResourceObject(msg).ToString();
                mpePopUpMail.Show();
            }
        }
        
        protected void GetCapitaSel(EMailAdmin.BackEnd.Domain.Capita capitaSel)
        {
            if (capitaSel != null)
            {
                SessionManager.SetCapitaSeleccionada(capitaSel, Session);
                txtCapitaSel.Text = capitaSel.Descripcion;
                txtCapitaSel.ToolTip = capitaSel.Codigo + " - " + capitaSel.Descripcion;
                txtPlan.Text = capitaSel.Plan.Descripcion;
                txtPlan.ToolTip = capitaSel.Plan.Codigo + " - " + capitaSel.Plan.Descripcion;
                txtPais.Text = capitaSel.Pais.Nombre;
                txtPais.ToolTip = capitaSel.Pais.Codigo + " - " + capitaSel.Pais.Nombre;
            }
            else
            {
                txtCapitaSel.Text = "";
                txtCapitaSel.ToolTip= "";
                txtPlan.ToolTip = "";
                txtPlan.Text = "";
            }
        }

        protected void BuscarCapita(object sender, EventArgs e)
        {
            ((CapitaSelector)sender).CodigoPaisDefault = PaisHome.Obtener(UsuarioLogueadoDTO().IdPais).Codigo;
        }
    }
}