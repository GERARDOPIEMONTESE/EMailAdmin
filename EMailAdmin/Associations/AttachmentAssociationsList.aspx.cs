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
    public partial class AttachmentAssociationsList : CustomPage
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
                SessionManager.SetGroupType(GroupTypeHome.Find(GroupType.ATTACHMENTGROUP), Session);
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

        protected void GrvAttachmentPageIndexChange(object sender, GridViewPageEventArgs e)
        {
            grvAttachment.PageIndex = e.NewPageIndex;
            Bind();
        }

        protected void GrvAttachmentRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
                e.Row.Cells[0].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = false;
            }
        }

        protected void GrvAttachmentRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                var id = Convert.ToInt32(grvAttachment.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);
                Response.Redirect("AttachmentAssociationsInformation.aspx?IdAttachment=" + id);
            }
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            Bind();
        }

        protected void btnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("AttachmentAssociationsInformation.aspx");
        }

        #endregion

        #region Private Methods

        private void LoadFilters()
        {
            ddlType.Items.Clear();
            var types = new List<AttachmentType> { new AttachmentType { Id = -1, Descripcion = "", Nombre = "" } };
            types.AddRange(AttachmentTypeHome.FindAll());
            ddlType.DataSource = types;
            ddlType.DataBind();

            ddlEstrategy.Items.Clear();
            var estrategies = new List<Estrategy> { new Estrategy { Id = -1, Descripcion = "", Nombre = "" } };
            estrategies.AddRange(EstrategyHome.FindAll());
            ddlEstrategy.DataSource = estrategies;
            ddlEstrategy.DataBind();
        }

        private void Bind()
        {
            grvAttachment.DataSource = AttachmentHome.FindByFilters(Convert.ToInt32(ddlType.SelectedValue), Convert.ToInt32(ddlEstrategy.SelectedValue), txtName.Text);
            grvAttachment.DataBind();
        }

        #endregion
    }
}