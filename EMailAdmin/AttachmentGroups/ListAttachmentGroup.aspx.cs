using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;
using ControlMenu;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Exceptions;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.Utils;
using MenuPortalLibrary.CapaDTO;

namespace EMailAdmin.AttachmentGroups
{
    public partial class ListAttachmentGroup : CustomPage
    {
        #region Constructor

        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            GroupAttachment data = new GroupAttachment() { GroupName = txtName.Text };
            Search(data);
        }

        protected void Search(GroupAttachment data)
        {
            IList<GroupAttachment> links = AttachmentGroupHome.Buscar(data);

            grvLinks.DataSource = links;
            grvLinks.DataBind();

        }

        protected void btnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("AttachmentGroupItem.aspx");
        }

        protected void OnRowCommand_EditItem(object sender, GridViewCommandEventArgs e)
        {
            int id = (int)this.grvLinks.DataKeys[Int32.Parse(e.CommandArgument.ToString())].Value;

            if (e.CommandName.Equals("Edit"))
            {
                Response.Redirect("AttachmentGroupItem.aspx?IdAttachmentGroup=" + id);
            }
        }
    }
}