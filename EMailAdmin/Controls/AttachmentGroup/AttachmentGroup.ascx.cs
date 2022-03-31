using EMailAdmin.BackEnd.Home;
using EMailAdmin.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMailAdmin.Controls.AttachmentGroup
{
    public partial class AttachmentGroup : System.Web.UI.UserControl
    {
        public int IdAttachment
        {
            get
            {
                return Convert.ToInt32(hdfIdAttachment.Value);
            }
            set
            {
                hdfIdAttachment.Value = value.ToString();
            }
        }

        public delegate void AttachmentGroupClose(object sender, EventArgs e);

        public event AttachmentGroupClose AttachmentGroupClosed;

        public void OnAttachmentGroupClose(EventArgs e)
        {
            var handler = AttachmentGroupClosed;
            if (handler != null) handler(this, e);
        }

        public delegate void AttachmentGroupOpen(object sender, EventArgs e);

        public event AttachmentGroupOpen AttachmentGroupOpened;

        public void OnAttachmentGroupOpen(EventArgs e)
        {
            var handler = AttachmentGroupOpened;
            if (handler != null) handler(this, e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void InitControl()
        {
            var template = SessionManager.GetTemplate(Session);
            hdfIdTemplate.Value = template.Id.ToString();

            var IAttachment = template.IAttachments.FirstOrDefault(x => x.Id == IdAttachment);
            
            ddlGroupAttachment.DataSource = AttachmentGroupHome.Buscar();
            ddlGroupAttachment.DataBind();
            ddlGroupAttachment.Items.Insert(0, new ListItem());            

            if (IAttachment != null)
            {
                if (IAttachment.GroupAttachment!=null && ddlGroupAttachment.Items.FindByValue(IAttachment.GroupAttachment.Id.ToString()) != null)
                    ddlGroupAttachment.SelectedValue = IAttachment.GroupAttachment.Id.ToString();

                txtAttachOrder.Text = IAttachment.AttachOrder.ToString();
            }
        }

        protected void BtnCloseOnClick(object sender, EventArgs e)
        {
            SetAttachmentGroup();
            OnAttachmentGroupClose(EventArgs.Empty);
        }

        private void SetAttachmentGroup()
        {    
            int IdGroupSel = 0;

            if (int.TryParse(ddlGroupAttachment.SelectedValue, out IdGroupSel))
            {
                var template = SessionManager.GetTemplate(Session);
            
                foreach (var item in template.IAttachments)
                {
                    if (item.Id == Convert.ToInt32(hdfIdAttachment.Value))
                    {
                        item.GroupAttachment.Id = IdGroupSel;
                        item.GroupAttachment.GroupName = ddlGroupAttachment.SelectedItem.Text;
                        item.AttachOrder = (txtAttachOrder.Text == "" ? 0 : int.Parse(txtAttachOrder.Text));
                        break;
                    }
                }
            }
        }
        
        protected void btnQuitar_Click(object sender, EventArgs e)
        {
            DelAttachmentGroup();
            OnAttachmentGroupClose(EventArgs.Empty);
        }

        protected void DelAttachmentGroup()
        {
            var template = SessionManager.GetTemplate(Session);

            foreach (var item in template.IAttachments)
            {
                if (item.Id == Convert.ToInt32(hdfIdAttachment.Value))
                {
                    item.GroupAttachment = new BackEnd.Domain.GroupAttachment();
                    item.AttachOrder = 0;
                    item.IdEstado = item.ObtenerEliminado();
                    break;
                }
            }
        }
    }
}