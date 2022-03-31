using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;
using ControlMenu;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.Utils;

namespace EMailAdmin.UpgradeVariableText
{
    public partial class UpgradeVariableTextList : CustomPage
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

        protected void GrvUpgradeVariableTextPageIndexChange(object sender, GridViewPageEventArgs e)
        {
            grvUpgradeVariableText.PageIndex = e.NewPageIndex;
            Bind();
        }

        protected void GrvUpgradeVariableTextRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
                e.Row.Cells[0].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = false;
            }
        }

        protected void GrvUpgradeVariableTextRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                var id = Convert.ToInt32(grvUpgradeVariableText.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);
                Response.Redirect("UpgradeVariableTextInformation.aspx?IdUpgradeVariableText="+id);
            }
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            Bind();
        }

        protected void btnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("UpgradeVariableTextInformation.aspx");
        }

        #endregion

        #region Private Methods

        private void LoadFilters()
        {
            //ddlCountry.Items.Clear();
            //var countries = new List<Pais> {new Pais {IdLocacion = -1, Nombre = ""}};
            //countries.AddRange(PaisHome.BuscarLazy());
            //ddlCountry.DataSource = countries;
            //ddlCountry.DataBind();

            ddlType.Items.Clear();
            var types = new List<UpgradeVariableTextType>{new UpgradeVariableTextType{Id = -1, Description = ""}};
            types.AddRange(UpgradeVariableTextTypeHome.FindAll());
            ddlType.DataSource = types;
            ddlType.DataBind();
        }

        private void Bind()
        {
            grvUpgradeVariableText.DataSource = UpgradeVariableTextHome.FindByFilters(Convert.ToInt32(ddlType.SelectedValue), -1, txtName.Text);
            grvUpgradeVariableText.DataBind();
        }

        #endregion
    }
}
