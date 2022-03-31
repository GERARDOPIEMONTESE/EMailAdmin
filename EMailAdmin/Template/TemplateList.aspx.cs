using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;
using ControlMenu;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.Utils;

namespace EMailAdmin.Template
{
    public partial class TemplateList : CustomPage
    {
        #region Constructor

        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFilters();
                Bind();
                SessionManager.SetLoguedUser(UsuarioLogueadoDTO(), Session);
                SessionManager.SetGroupType(GroupTypeHome.Find(GroupType.TEMPLATEGROUP), Session);

                SessionManager.RemoveIsDeletedHeader(Session);
                SessionManager.RemoveIsDeletedFooter(Session);
                SessionManager.RemoveIsDeletedHeaderPDF(Session);
                SessionManager.RemoveIsDeletedFooterPDF(Session);      
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

        protected void GrvTemplatePageIndexChange(object sender, GridViewPageEventArgs e)
        {
            grvTemplate.PageIndex = e.NewPageIndex;
            Bind();
        }

        protected void GrvTemplateRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
                e.Row.Cells[0].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = false;
            }
        }

        protected void GrvTemplateRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                SessionManager.SetNewAssociation(false, Session);
                var id = Convert.ToInt32(grvTemplate.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);
                Response.Redirect("TemplateInformation.aspx?IdTemplate=" + id);
            }

            if (e.CommandName == "Copy")
            {
                SessionManager.SetNewAssociation(false, Session);
                var id = Convert.ToInt32(grvTemplate.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);
                Response.Redirect("TemplateInformation.aspx?IdCopyTemplate=" + id);
            }
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            Bind();
        }

        protected void btnNew_OnClick(object sender, EventArgs e)
        {
            SessionManager.SetNewAssociation(true, Session);
            Response.Redirect("TemplateInformation.aspx");
        }
        
        #endregion

        #region Private Methods

        private void LoadFilters()
        {
            ddlType.Items.Clear();
            var types = new List<TemplateType> { new TemplateType { Id = -1, Descripcion = "", Nombre = "" } };
            types.AddRange(TemplateTypeHome.FindAll());
            ddlType.DataSource = types;
            ddlType.DataBind();
        }

        private void Bind()
        {
            grvTemplate.DataSource = TemplateHome.FindDTO(Convert.ToInt32(ddlType.SelectedValue), txtName.Text);
            grvTemplate.DataBind();
        }

        #endregion
    }
}