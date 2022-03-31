using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Web.UI.WebControls;
using ControlMenu;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Exceptions;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.Utils;
using MenuPortalLibrary.CapaDTO;
using Content = EMailAdmin.BackEnd.Domain.Content;

namespace EMailAdmin.Links
{
    public partial class EdicionDeLinks : CustomPage
    {
        protected override void CustomPageLoad(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                CargarControles();
                btnCancel.Text = "Cancel";

                if (string.IsNullOrEmpty(Request.QueryString["IdLink"]))
                {
                    btnInsertSave.Text = "Create";
                    btnDelete.Visible = false;
                }
                else
                {
                    CargarDatos();
                    btnInsertSave.Text = "Save";
                }
            }
        }

        #region Controles / Datos
        protected void CargarDatos() {
            Link link = LinkHome.Get(Convert.ToInt32(Request.QueryString["IdLink"].ToString()));

            txName.Text = link.Name.ToString();
            txUrl.Text = link.Url.ToString();
            txtDisplayText_EN.Text = link.DisplayText_EN;
            txtDisplayText_ES.Text = link.DisplayText_ES;
            txtDisplayText_PT.Text = link.DisplayText_PT;
            txtImageName.Text = link.ImageName;
            txtStyle.Text = link.Style;
            chkEnabledDeepLink.Checked = link.EnabledDeepLink;

            IList<LinkType> types = LinkTypeHome.FindAll();

            ddlLinkType.DataSource = types;
            ddlLinkType.DataBind();
            ddlLinkType.SelectedValue = link.LinkType.Codigo.ToString();
        }

        protected void CargarControles() 
        {
            IList<LinkType> types = LinkTypeHome.FindAll();

            ddlLinkType.DataSource = types;
            ddlLinkType.DataBind();
            ddlLinkType.SelectedValue = "FIXED";

        }
        #endregion

        #region Metodos Botones
        protected void btnInsertSave_Click(object sender, EventArgs e)
        {
            var name = txName.Text;
            var url = txUrl.Text;
            Link link;

            if (string.IsNullOrEmpty(Request.QueryString["IdLink"]))
            {
                link = new Link();   
            }
            else
            {
                link = LinkHome.Get(Convert.ToInt32(Request.QueryString["IdLink"].ToString()));
            }    

            LinkType type = LinkTypeHome.Get(ddlLinkType.SelectedValue.ToString());

            link.LinkType = type;
            link.Name = name;
            link.Url = url;
            link.ImageName = txtImageName.Text;
            link.Style = txtStyle.Text;
            link.DisplayText_EN = txtDisplayText_EN.Text;
            link.DisplayText_ES = txtDisplayText_ES.Text;
            link.DisplayText_PT = txtDisplayText_PT.Text;
            link.EnabledDeepLink = chkEnabledDeepLink.Checked;
            link.IdUsuario = UsuarioLogueadoDTO().Id;
            link.IdEstado = link.ObtenerCreado();

            link.Persistir();

            btnCancel.Text = "Back";

            Response.Redirect("ListaDeLinks.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var name = txName.Text;
            var url = txUrl.Text;

            Link nuevo = LinkHome.Get(Convert.ToInt32(Request.QueryString["IdLink"].ToString()));
            nuevo.IdUsuario = UsuarioLogueadoDTO().Id;

            nuevo.Eliminar();

            Response.Redirect("ListaDeLinks.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListaDeLinks.aspx");
        }
        #endregion
    }
}