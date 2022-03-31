using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;
using ControlMenu;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.Utils;
using MenuPortalLibrary.CapaDTO;

namespace EMailAdmin.Associations
{
    public partial class AssociationsList : CustomPage
    {
        #region Constructor

        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFilters();
                Bind();
                SessionManager.SetLoguedUser(UsuarioLogueadoDTO(), Session);
                SessionManager.SetNewAssociation(false, Session);
                SessionManager.SetGroupType(GroupTypeHome.Find(GroupType.TEMPLATEGROUP), Session);
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

        protected void GrvAssociationPageIndexChange(object sender, GridViewPageEventArgs e)
        {
            grvAssociation.PageIndex = e.NewPageIndex;
            Bind();
        }

        protected void GrvAssociationRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
                e.Row.Cells[0].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = false;
            }
        }

        protected void GrvAssociationRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int id = Convert.ToInt32(grvAssociation.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);
                Response.Redirect("AssociationsInformation.aspx?IdTemplate=" + id);
            }
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            Bind();
        }

        protected void btnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("AssociationsInformation.aspx");
        }

        #endregion

        #region Private Methods

        private void LoadFilters()
        {
            ddlType.Items.Clear();
            var types = new List<TemplateType> {new TemplateType {Id = -1, Descripcion = "", Nombre = ""}};
            types.AddRange(TemplateTypeHome.FindAll());
            ddlType.DataSource = types;
            ddlType.DataBind();
        }

        private void Bind()
        {
            grvAssociation.DataSource = TemplateHome.Find(Convert.ToInt32(ddlType.SelectedValue),
                                                                          txtName.Text);
            grvAssociation.DataBind();
        }

        #endregion
    }
}