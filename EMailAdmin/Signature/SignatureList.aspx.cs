using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;
using CapaNegocioDatos.CapaHome;
using CapaNegocioDatos.CapaNegocio;
using ControlMenu;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.Utils;

namespace EMailAdmin.Signature
{
    public partial class SignatureList : CustomPage
    {
        #region Constructor

        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFilters();
                Bind();
                SessionManager.SetLoguedUser(UsuarioLogueadoDTO(), Session);
            }
        }

        #endregion Constructor

        #region Propiedades

        protected override void ChequearSession()
        {
            var usuario = base.UsuarioLogueadoDTO();
            if (usuario == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings["ExpireSessionRedirect"]);
            }
        }

        #endregion Propiedades

        #region Methods

        protected void GrvSignaturePageIndexChange(object sender, GridViewPageEventArgs e)
        {
            grvSignature.PageIndex = e.NewPageIndex;
            Bind();
        }

        protected void GrvSignatureRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
                e.Row.Cells[0].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = false;
            }
        }

        protected void GrvSignatureRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                var id = Convert.ToInt32(grvSignature.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);
                Response.Redirect("SignatureInformation.aspx?IdSignature="+id);
            }
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            Bind();
        }

        protected void btnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("SignatureInformation.aspx");
        }

        #endregion

        #region Private Methods

        private void LoadFilters()
        {
            ddlCountry.Items.Clear();
            var countries = new List<Pais> {new Pais {IdLocacion = -1, Nombre = ""}};
            countries.AddRange(PaisHome.BuscarLazy());
            ddlCountry.DataSource = countries;
            ddlCountry.DataBind();

            ddlType.Items.Clear();
            var types = new List<SignatureType>{new SignatureType{Id = -1, Description = ""}};
            types.AddRange(SignatureTypeHome.FindAll());
            ddlType.DataSource = types;
            ddlType.DataBind();
        }

        private void Bind()
        {
            grvSignature.DataSource = SignatureHome.FindByFilters(Convert.ToInt32(ddlType.SelectedValue), Convert.ToInt32(ddlCountry.SelectedValue), txtName.Text);
            grvSignature.DataBind();
        }

        #endregion
    }
}
