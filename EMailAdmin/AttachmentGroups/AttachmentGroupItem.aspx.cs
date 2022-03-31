using ControlMenu;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;
using MenuPortalLibrary.CapaDTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMailAdmin.AttachmentGroups
{
    public partial class AttachmentGroupItem : CustomPage
    {
        protected override void CustomPageLoad(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                btnCancel.Text = "Cancel";

                if (string.IsNullOrEmpty(Request.QueryString["IdAttachmentGroup"]))
                {
                    btnInsertSave.Text = "Create";
                    btnDelete.Visible = false;
                    divInUseTemplates.Visible = false;
                }
                else
                {
                    CargarDatos();
                    btnInsertSave.Text = "Save";
                }
            }
        }

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

        #region Controles / Datos
        protected void CargarDatos()
        {
            GroupAttachment item = AttachmentGroupHome.Get(Convert.ToInt32(Request.QueryString["IdAttachmentGroup"].ToString()));

            txName.Text = item.GroupName.ToString();
            txtDisplayText_EN.Text = item.AttachName_EN;
            txtDisplayText_ES.Text = item.AttachName_ES;
            txtDisplayText_PT.Text = item.AttachName_PT;
            txtAttachOrder.Text = item.AttachOrder.ToString();
            btnDelete.Visible = item.CanDeleted;

            txName.Enabled = !item.IsGroupEVoucher;
            //info de porque no puede borrarlo
            divInUseTemplates.Visible = !item.CanDeleted;
            lblInUseTemplatesName.Text = item.InUseTemplates;
        }

        #endregion

        #region Metodos Botones
        protected void btnInsertSave_Click(object sender, EventArgs e)
        {
            var name = txName.Text;
            GroupAttachment item;

            if (string.IsNullOrEmpty(Request.QueryString["IdAttachmentGroup"]))
            {
                item = new GroupAttachment();
            }
            else
            {
                item = AttachmentGroupHome.Get(Convert.ToInt32(Request.QueryString["IdAttachmentGroup"].ToString()));
            }
                        
            item.GroupName = name;
            item.AttachName_EN = txtDisplayText_EN.Text;
            item.AttachName_ES = txtDisplayText_ES.Text;
            item.AttachName_PT = txtDisplayText_PT.Text;
            item.AttachOrder = Convert.ToInt32(txtAttachOrder.Text);
            item.IdUsuario = UsuarioLogueadoDTO().Id;
            item.IdEstado = item.ObtenerCreado();

            item.Persistir();

            btnCancel.Text = "Back";

            Response.Redirect("ListAttachmentGroup.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var name = txName.Text;

            var item = AttachmentGroupHome.Get(Convert.ToInt32(Request.QueryString["IdAttachmentGroup"].ToString()));
            item.IdUsuario = UsuarioLogueadoDTO().Id;

            item.Eliminar();

            Response.Redirect("ListAttachmentGroup.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListAttachmentGroup.aspx");
        }
        #endregion
    }
}