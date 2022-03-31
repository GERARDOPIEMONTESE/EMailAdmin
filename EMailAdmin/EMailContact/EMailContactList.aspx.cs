using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ControlMenu;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;
using CapaNegocioDatos.CapaHome;
using CapaNegocioDatos.CapaNegocio;
using EMailAdmin.Utils;

namespace EMailAdmin.EMailContact
{
    public partial class EMailContactList : CustomPage
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

        #region Methods

        protected void GrvEMailContactPageIndexChange(object sender, GridViewPageEventArgs e)
        {
            grvEMailContact.PageIndex = e.NewPageIndex;
            Bind();
        }

        protected void GrvEMailContactRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
                e.Row.Cells[0].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = false;
            }
        }

        protected void GrvEMailContactRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                var id = Convert.ToInt32(grvEMailContact.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);
                Response.Redirect("EMailContactInformation.aspx?IdEMailContact=" + id);
            }
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            Bind();
        }

        protected void btnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("EMailContactInformation.aspx");
        }

        #endregion

        #region Private Methods

        private void LoadFilters()
        {
            ddlCountry.Items.Clear();
            var countries = new List<Pais> {new Pais {Id = -1, Nombre = ""}};
            countries.AddRange(PaisHome.BuscarLazy());
            ddlCountry.DataSource = countries;
            ddlCountry.DataBind();

            ddlType.Items.Clear();
            var types = new List<EMailContactType> { new EMailContactType { Id = 0, Description = "" } };
            types.AddRange(EMailContactTypeHome.FindAll());
            ddlType.DataSource = types;
            ddlType.DataBind();
        }

        private void Bind()
        {
            grvEMailContact.DataSource = EMailContactHome.Find(txtName.Text, Convert.ToInt32(ddlCountry.SelectedValue), Convert.ToInt32(ddlType.SelectedValue));
            grvEMailContact.DataBind();
        }

        #endregion
    }
}