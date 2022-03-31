using System;
using System.Configuration;
using System.Web.UI.WebControls;
using ControlMenu;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.Utils;

namespace EMailAdmin.Type.Signature
{
    public partial class SignatureTypeList : CustomPage
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

        protected void GrvSignatureTypePageIndexChange(object sender, GridViewPageEventArgs e)
        {
            grvSignatureType.PageIndex = e.NewPageIndex;
            Bind();
        }

        protected void GrvSignatureTypeRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
                e.Row.Cells[0].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = false;
            }
        }

        protected void GrvSignatureTypeRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                var id = Convert.ToInt32(grvSignatureType.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);
                Response.Redirect("SignatureTypeInformation.aspx?IdSignatureType=" + id);
            }
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            Bind();
        }

        protected void btnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("SignatureTypeInformation.aspx");
        }

        #endregion

        #region Private Methods

        private void Bind()
        {
            grvSignatureType.DataSource = SignatureTypeHome.FindByFilters(txtName.Text);
            grvSignatureType.DataBind();
        }

        #endregion
    }
}