using System;
using System.Configuration;
using System.Web.UI.WebControls;
using ControlMenu;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.Utils;

namespace EMailAdmin.EmailAddress
{
    public partial class EmailAddressList : CustomPage
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

        protected void GrvEmailAddressPageIndexChange(object sender, GridViewPageEventArgs e)
        {
            grvEmailAddress.PageIndex = e.NewPageIndex;
            Bind();
        }

        protected void GrvEmailAddressRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
                e.Row.Cells[0].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = false;
            }
        }

        protected void GrvEmailAddressRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                var id = Convert.ToInt32(grvEmailAddress.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);
                Response.Redirect("EmailAddressInformation.aspx?IdEmailAddress=" + id);
            }
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            Bind();
        }

        protected void btnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("EmailAddressInformation.aspx");
        }

        #endregion

        #region Private Methods

        private void Bind()
        {
            grvEmailAddress.DataSource = EMailAddressHome.FindByFilters(txtName.Text, txtAddress.Text);
            grvEmailAddress.DataBind();
        }

        #endregion
    }
}