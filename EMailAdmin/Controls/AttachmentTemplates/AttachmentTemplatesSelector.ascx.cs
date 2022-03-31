using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMailAdmin.Controls.AttachmentTemplates
{
    public partial class AttachmentTemplatesSelector : System.Web.UI.UserControl
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

        public int IdTemplate
        {
            get
            {
                return Convert.ToInt32(hdfIdTemplate.Value);
            }
            set
            {
                hdfIdTemplate.Value = value.ToString();
            }
        }

        public delegate void AttachmentTemplatesClose(object sender, EventArgs e);

        public event AttachmentTemplatesClose AttachmentTemplatesClosed;

        public void OnAttachmentTemplatesClose(EventArgs e)
        {
            var handler = AttachmentTemplatesClosed;
            if (handler != null) handler(this, e);
        }

        public delegate void AttachmentTemplatesOpen(object sender, EventArgs e);

        public event AttachmentTemplatesOpen AttachmentTemplatesOpened;

        public void OnAttachmentTemplatesOpen(EventArgs e)
        {
            var handler = AttachmentTemplatesOpened;
            if (handler != null) handler(this, e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void initControls()
        {
            var template = SessionManager.GetTemplate(Session);
            hdfIdTemplate.Value = template.Id.ToString();

            var IAttachmentTemplates = template.IAttachments.FirstOrDefault(x => x.Id == IdAttachment).AttachmentTemplates;                
            SessionManager.SetAttachmentTemplates(IAttachmentTemplates, Session);

            ddlTemplates.DataSource = TemplateHome.FindAllList();
            ddlTemplates.DataBind();

            grvTemplatesBind();
        }
        private void grvTemplatesBind()
        {
            grvTemplates.DataSource = SessionManager.GetAttachmentTemplates(Session);
            grvTemplates.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {            
            var newItem = new EstrategyAttachmentTemplate()
            {
                IdTemplate = IdTemplate,
                IdAttachment = IdAttachment,
                IdTemplateAttachment = int.Parse(ddlTemplates.SelectedValue),
                TemplateName = ddlTemplates.SelectedItem.Text
            };

            SessionManager.AttachmentTemplates_AddItem(newItem, Session);

            grvTemplatesBind();

            OnAttachmentTemplatesOpen(EventArgs.Empty);
        }

        protected void BtnCloseOnClick(object sender, EventArgs e)
        {
            SetAttachmentTemplates();
            OnAttachmentTemplatesClose(EventArgs.Empty);
        }

        private void SetAttachmentTemplates()
        {
            var template = SessionManager.GetTemplate(Session);

            foreach (var item in template.IAttachments)
            {
                if (item.Id == Convert.ToInt32(hdfIdAttachment.Value))
                {
                    item.AttachmentTemplates = SessionManager.GetAttachmentTemplates(Session);                    
                    break;
                }
            }
            SessionManager.RemoveAttachmentTemplates(Session);
        }

        protected void IbnDelete_Onclick(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;
            int id = 0;
            if (int.TryParse(btn.CommandArgument, out id))
            {
                EstrategyAttachmentTemplate delItem = SessionManager.GetAttachmentTemplates(Session).FirstOrDefault(x => x.Id == id);
                if (delItem != null)
                    delItem.IdEstado = delItem.ObtenerEliminado();
            }
            grvTemplatesBind();

            OnAttachmentTemplatesOpen(EventArgs.Empty);
        }

        protected void grvTemplates_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var data = ((EstrategyAttachmentTemplate)e.Row.DataItem);
                if ((data).IdEstado == data.ObtenerEliminado())
                {
                    e.Row.Visible = false;
                }
            }
        }
    }
}