using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.Utils;
using System.Linq;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.Controls.Selectors.Attachment
{
    public partial class AttachmentSelector : System.Web.UI.UserControl
    {
        #region Delegate

        public delegate void AttachmentClose(object sender, EventArgs e);
        public delegate void AttachmentSearch(object sender, EventArgs e);
        public delegate void AttachmentPageIndexChange(object sender, EventArgs e);
        public delegate void AttachmentContentEdit(int IdAttachment, string CodeRPT);
        public delegate void AttachmentContentClose(object sender, EventArgs e);
        public delegate void AttachmentGroupEdit(int IdAttachment);
        public delegate void AttachmentGroupClose(object sender, EventArgs e);
        public delegate void AttachmentTemplatesEdit(int IdAttachment);
        public delegate void AttachmentTemplatesClose(object sender, EventArgs e);

        #endregion

        #region Constants

        private const int IDCELL = 0;
        private const int CHECKBOXCELL = 1;
        private const int NAMECELL = 2;
        private const int GROUPCELL = 6;

        #endregion Constants

        #region Constructor

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadList();
                Bind();                
            }
        }

        #endregion Constructor

        #region Properties

        public string CodeRPT { get; set; }

        //public void SetAttachmentGroup(int idAttachment, GroupAttachment groupAttachment, int order)
        //{
        //    foreach (GridViewRow row in grvAttachment.Rows)
        //    {
        //        if (idAttachment == Convert.ToInt32(row.Cells[IDCELL].Text))
        //        {
        //            var lblGroupOrder = row.FindControl("lblGroupOrder");
        //            ((Label)lblGroupOrder).Text = groupAttachment.GroupName+":"+order.ToString();

        //            var hdfIdGroupAttachment = row.FindControl("hdfIdGroupAttachment");
        //            ((HiddenField)hdfIdGroupAttachment).Value = groupAttachment.Id.ToString();

        //            var hdfAttachOrder = row.FindControl("hdfAttachOrder");
        //            ((HiddenField)hdfAttachOrder).Value = order.ToString();
        //        }
        //    }
        //}

        public void Refresh()
        {
            Bind();
        }

        private void SetAttachment(IList<BackEnd.Domain.Attachment> iAttachments)
        {
            if (iAttachments == null)
            {
                return;
            }

            foreach (var attachment in iAttachments)
            {
                foreach (GridViewRow row in grvAttachment.Rows)
                {
                    if (attachment.Id == Convert.ToInt32(row.Cells[IDCELL].Text))
                    {
                        bool bVisible = false;
                        var btn = row.FindControl("btnContentAttachment");
                        var btnFooter = row.FindControl("btnContentAttachmentFooter");
                        var btnAttachmentTemplates = row.FindControl("btnAttachmentTemplates");

                        var checkRow = (DataControlFieldCell)row.Cells[CHECKBOXCELL];
                        ((CheckBox)checkRow.Controls[1]).Checked = true;

                        if (attachment.GroupAttachment.Id > 0)
                            ((LinkButton)checkRow.FindControl("btnAttachmentGroup")).Text = "Grupo:"+ attachment.GroupAttachment.GroupName;

                        if (attachment.AttachOrder > 0)
                            ((LinkButton)checkRow.FindControl("btnAttachmentGroup")).Text += " Orden:" + attachment.AttachOrder.ToString();

                        if (attachment.AttachmentTemplates.Count() > 0)
                            ((LinkButton)checkRow.FindControl("btnAttachmentTemplates")).Text = "Templates:" + attachment.AttachmentTemplates.Count().ToString();

                        if (btn != null)
                            bVisible = attachment.EsSTRATEGY;

                        ((LinkButton)btn).Visible = bVisible;
                        ((LinkButton)btnFooter).Visible = bVisible;
                        ((LinkButton)btnAttachmentTemplates).Visible = bVisible;

                        break;
                    }
                }
            }
        }

        public IList<BackEnd.Domain.Attachment> Attachments
        {
            get
            {
                IList<BackEnd.Domain.Attachment> result = SessionManager.GetTemplate(Session).IAttachments;
                foreach (GridViewRow row in grvAttachment.Rows)
                {
                    var checkRow = (DataControlFieldCell)row.Cells[CHECKBOXCELL];
                    int IdAttachment = Convert.ToInt32(row.Cells[IDCELL].Text);

                    if (((CheckBox)checkRow.Controls[1]).Checked)
                    {
                        var hdfIdGroupAttachment = row.FindControl("hdfIdGroupAttachment");
                        int IdGroupAttachment = 0;
                        int.TryParse(((HiddenField)hdfIdGroupAttachment).Value, out IdGroupAttachment);

                        var hdfAttachOrder = row.FindControl("hdfAttachOrder");
                        int nroOrder = 0;
                        int.TryParse(((HiddenField)hdfAttachOrder).Value, out nroOrder);

                        var attachment = new BackEnd.Domain.Attachment
                        {
                            Id = IdAttachment,
                            Name = row.Cells[NAMECELL].Text,
                            GroupAttachment = new GroupAttachment()
                            {
                                Id = IdGroupAttachment,
                                AttachOrder = nroOrder
                            }
                        };

                        if (!Contains(result, attachment))
                        {
                            result.Add(attachment);
                        }
                    }
                    else
                    {
                        var attachDel = result.FirstOrDefault(x=> x.Id == IdAttachment);
                        if (attachDel != null)
                            result.Remove(attachDel);
                    }
                }
                return result;
            }
            set
            {
                IAttachments = value;
                if (value != null)
                {
                    foreach (var attachment in value)
                    {
                        foreach (GridViewRow row in grvAttachment.Rows)
                        {
                            if (attachment.Id == Convert.ToInt32(row.Cells[IDCELL].Text))
                            {
                                var checkRow = (DataControlFieldCell)row.Cells[CHECKBOXCELL];
                                ((CheckBox)checkRow.Controls[1]).Checked = true;
                            }
                        }
                    }
                }
            }
        }

        public IList<BackEnd.Domain.Attachment> IAttachments { get; set; }

        #endregion

        #region Events

        public event AttachmentClose AttachmentClosed;

        public void OnAttachmentClose(EventArgs e)
        {
            var handler = AttachmentClosed;
            if (handler != null) handler(this, e);
        }

        public event AttachmentSearch AttachmentSearched;

        public void OnAttachmentSearched(EventArgs e)
        {
            var handler = AttachmentSearched;
            if (handler != null) handler(this, e);
        }

        public event AttachmentPageIndexChange AttachmentPageIndexChanged;

        public void OnAttachmentPageIndexChanged(EventArgs e)
        {
            var handler = AttachmentPageIndexChanged;
            if (handler != null) handler(this, e);
        }


        public event AttachmentContentEdit AttachmentContentEdited;

        public void OnAttachmentContentEdit(int IdAtachment, string CodeRPT)
        {
            var handler = AttachmentContentEdited;
            if (handler != null) handler(IdAtachment, CodeRPT);
        }

        public event AttachmentContentClose AttachmentContentClosed;

        public void OnAttachmentContentClose(EventArgs e)
        {
            var handler = AttachmentContentClosed;
            if (handler != null) handler(this, e);
        }

        public event AttachmentGroupEdit AttachmentGroupEdited;

        public void OnAttachmentGroupEdit(int IdAtachment)
        {
            var handler = AttachmentGroupEdited;
            if (handler != null) handler(IdAtachment);
        }

        public event AttachmentGroupClose AttachmentGroupClosed;

        public void OnAttachmentGroupClose(EventArgs e)
        {
            var handler = AttachmentGroupClosed;
            if (handler != null) handler(this, e);
        }

        public event AttachmentTemplatesEdit AttachmentTemplatesEdited;
        public void OnAttachmentTemplatesEdit(int IdAtachment)
        {
            var handler = AttachmentTemplatesEdited;
            if (handler != null) handler(IdAtachment);
        }

        public event AttachmentTemplatesClose AttachmentTemplatesClosed;

        public void OnAttachmentTemplatesClose(EventArgs e)
        {
            var handler = AttachmentTemplatesClosed;
            if (handler != null) handler(this, e);
        }

        #endregion

        #region Methods

        protected bool Contains(IList<BackEnd.Domain.Attachment> attachments, BackEnd.Domain.Attachment attachment)
        {

            foreach(BackEnd.Domain.Attachment attach in attachments) 
            {
                if (attachment.Id == attach.Id)
                {
                    return true;
                }
            }

            return false;
        }

        protected void BtnCloseOnClick(object sender, EventArgs e)
        {
            OnAttachmentClose(EventArgs.Empty);
        }

        protected void BtnSearchOnClick(object sender, EventArgs e)
        {
            Bind();
            OnAttachmentSearched(EventArgs.Empty);
        }

        protected void GrvAttachmentPageIndexChange(object sender, GridViewPageEventArgs e)
        {
            grvAttachment.PageIndex = e.NewPageIndex;
            IAttachments = Attachments;
            Bind();
            OnAttachmentPageIndexChanged(EventArgs.Empty);
        }

        protected void GrvAttachmentRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
                e.Row.Cells[0].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
                e.Row.Cells[0].Visible = false;
        }

        #endregion Methods

        #region Private Methods

        private void LoadList()
        {
            ddlType.DataSource = AttachmentTypeHome.FindAll();
            ddlType.DataBind();
        }

        private void Bind()
        {
            try
            {
                if (chkSoloAsociados.Checked == true)
                    grvAttachment.DataSource = SessionManager.GetTemplate(Session).IAttachments;
                else
                    grvAttachment.DataSource = AttachmentHome.FindByNameType(txtName.Text,Convert.ToInt32(ddlType.SelectedValue));
                   
                grvAttachment.DataBind();
                SetAttachment(SessionManager.GetTemplate(Session).IAttachments);
            }
            catch (Exception)
            {
            }
        }

        #endregion Private Methods

        protected void btnContentAttachment_Command(object sender, CommandEventArgs e)
        {
            int id;
            if (int.TryParse(((LinkButton)sender).CommandArgument, out id))
            {
                AttachmentContentEdited(id, BackEnd.Domain.ContentAttachment.HEADER);
            }
        }

        protected void btnContentAttachmentFooter_Command(object sender, CommandEventArgs e)
        {
            int id;
            if (int.TryParse(((LinkButton)sender).CommandArgument, out id))
            {   
                AttachmentContentEdited(id,BackEnd.Domain.ContentAttachment.FOOTER);
            }
        }

        protected void btnAttachmentGroup_Command(object sender, CommandEventArgs e)
        {
            int id;
            if (int.TryParse(((LinkButton)sender).CommandArgument, out id))
            {
                AttachmentGroupEdited(id);
            }
        }

        protected void btnAttachmentTemplates_Command(object sender, CommandEventArgs e)
        {
            int id;
            if (int.TryParse(((LinkButton)sender).CommandArgument, out id))
            {
                AttachmentTemplatesEdited(id);
            }
        }
    }
}