using System;
using System.Configuration;
using System.Web.UI.WebControls;
using ControlMenu;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.Utils;

namespace EMailAdmin.Type.EMailContact
{
    public partial class EMailContactTypeList : CustomPage
    {
        #region Constructor

        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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

        protected void GrvEMailContactTypePageIndexChange(object sender, GridViewPageEventArgs e)
        {
            grvEMailContactType.PageIndex = e.NewPageIndex;
            Bind();
        }

        protected void GrvEMailContactTypeRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
                e.Row.Cells[0].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = false;
            }
        }

        protected void GrvEMailContactTypeRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                var id = Convert.ToInt32(grvEMailContactType.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);
                Response.Redirect("EMailContactTypeInformation.aspx?IdEMailContactType=" + id);
            }
        }

        protected void BtnSearchOnClick(object sender, EventArgs e)
        {
            Bind();
        }

        protected void BtnNewOnClick(object sender, EventArgs e)
        {
            Response.Redirect("EMailContactTypeInformation.aspx");
        }

        #endregion

        #region Private Methods

        private void Bind()
        {
            grvEMailContactType.DataSource = EMailContactTypeHome.FindByFilters(txtDescription.Text);
            grvEMailContactType.DataBind();
        }

        #endregion
    }
}