using System;
using System.Configuration;
using System.Web.UI.WebControls;
using ControlMenu;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.Utils;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.Type.EMailList
{
    public partial class EMailListTypeList : CustomPage
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

        protected void GrvEMailListTypePageIndexChange(object sender, GridViewPageEventArgs e)
        {
            grvEMailListType.PageIndex = e.NewPageIndex;
            Bind();
        }

        protected void GrvEMailListTypeRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
                e.Row.Cells[0].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = false;
            }
        }

        protected void GrvEMailListTypeRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                var id = Convert.ToInt32(grvEMailListType.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);
                Response.Redirect("EMailListTypeInformation.aspx?IdEMailListType=" + id);                
            }
        }
        
        protected void BtnSearchOnClick(object sender, EventArgs e)
        {
            Bind();
        }

        protected void BtnNewOnClick(object sender, EventArgs e)
        {
            Response.Redirect("EMailListTypeInformation.aspx");
        }

        #endregion

        #region Private Methods

        private void Bind()
        {
            grvEMailListType.DataSource = EMailListTypeHome.FindByFilters(txtDescription.Text);
            grvEMailListType.DataBind();
        }

        #endregion

    }
}