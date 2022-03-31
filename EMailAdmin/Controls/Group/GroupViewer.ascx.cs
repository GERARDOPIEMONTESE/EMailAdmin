using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocioDatos.CapaHome;
using CapaNegocioDatos.CapaNegocio;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.Utils;
using System.Linq;

namespace EMailAdmin.Controls.Group
{
    public partial class GroupViewer : UserControl
    {
        #region Delegate

        public delegate void ButtonAddGroupPress(object sender, Group_R_Template grt);
        public delegate void ButtonDelGroupPress(object sender, Group_R_Template grt);
        public delegate void ButtonAddAttachmentPress(object sender, Attachment_R_Group atrg);
        public delegate void ButtonDelAttachmentPress(object sender, Attachment_R_Group atrg); 

        #endregion

        #region Properties

        public IList<Group_R_Template> TemplateGroups
        {
            get
            {
                return SessionManager.GetTemplateAssociations(Session);
            }
        }

        public IList<Attachment_R_Group> AttachmentGroups
        {
            get
            {
                return SessionManager.GetAttachmentAssociations(Session);
            }
        }

        internal void ReloadGroups()
        {
            throw new NotImplementedException();
        }

        public bool CanSave
        {
            get
            {
                return SessionManager.GetGroupType(Session).Codigo == GroupType.TEMPLATEGROUP
                           ? TemplateGroups.Count > 0
                           : false;
            }
        }

        public void ClearControls()
        {
            BindGridGrupos();
            ddlModule.SelectedIndex = -1;
        }

        public GroupType currentGroupType { get; set; }

        #endregion Properties

        #region Events

        public event ButtonDelGroupPress ButtonDelGroupPressed;

        public void OnButtonDelGroupPressed(Group_R_Template grt)
        {
            ButtonDelGroupPress handler = ButtonDelGroupPressed;
            if (handler != null) handler(this, grt);
        }

        public event ButtonAddGroupPress ButtonAddGroupPressed;

        public void OnButtonAddGroupPressed(Group_R_Template grt)
        {
            ButtonAddGroupPress handler = ButtonAddGroupPressed;
            if (handler != null) handler(this, grt);
        }

        public event ButtonAddAttachmentPress ButtonAddAttachmentPressed;

        public void OnButtonAddAttachmentPressed(Attachment_R_Group atrg)
        {
            ButtonAddAttachmentPress handler = ButtonAddAttachmentPressed;
            if (handler != null) handler(this, atrg);
        }

        public event ButtonDelAttachmentPress ButtonDelAttachmentPressed;

        public void OnButtonDelAttachmentPressed(Attachment_R_Group atrg)
        {
            ButtonDelAttachmentPress handler = ButtonDelAttachmentPressed;
            if (handler != null) handler(this, atrg);
        }

        #endregion

        #region Constructor

        protected void Page_Load(object sender, EventArgs e)
        {
            currentGroupType = SessionManager.GetGroupType(Session);

            if (!Page.IsPostBack)
            {
                BindGridGrupos();
                LoadControls();
            }
        }

        #endregion Constructor

        #region Private Methods

        public void BindGridGrupos()
        {            
            if ( currentGroupType.Codigo == GroupType.TEMPLATEGROUP)
                BindGridGrupos<Group_R_Template>(TemplateGroups);
            else
                BindGridGrupos<Attachment_R_Group>(AttachmentGroups);
        }             
    
        public void BindGridGrupos<T>(IList<T> lstGroups)
        {
            gvGruposAgregados.DataSource = lstGroups;
            gvGruposAgregados.DataBind();            
        }

        private void LoadControls()
        {
            bool bRecive = (currentGroupType.Codigo == GroupType.TEMPLATEGROUP ? true : false);
            lblRecive.Visible = bRecive;
            chkRecive.Visible = bRecive;

            gvGruposAgregados.Columns[3].Visible = bRecive;

            ddlModule.DataSource = ModuloHome.FindAll();
            ddlModule.DataBind();

            ddlGroup.DataSource = GroupHome.FindByGroupType(SessionManager.GetGroupType(Session).Id, true);
            ddlGroup.DataBind();
        }

        private void ShowControls(bool show)
        {
            pnlTemplate.Visible = show;
            lblGroup.Visible = show;
            ddlGroup.Visible = show;
        }

        #endregion Private Methods

        protected void btnAddGroup_Click(object sender, EventArgs e)
        {
            addGroup();
            BindGridGrupos();
        }

        private void addGroup()
        {
            if (currentGroupType.Codigo == GroupType.TEMPLATEGROUP)
                addTemplateGroup();
            else
                addAttachmentGroup();
        }

        private void addAttachmentGroup()
        {
            BackEnd.Domain.Attachment t = SessionManager.GetAttachment(Session);
            var art = new Attachment_R_Group()
            {
                IdAttachment = t.Id,
                Module = CapaNegocioDatos.CapaHome.ModuloHome.Obtener(ddlModule.SelectedValue),
                Group = GroupHome.FindByFilters(Int32.Parse(ddlGroup.SelectedValue)),
                IdGroup = Int32.Parse(ddlGroup.SelectedValue),
                IdUsuario = SessionManager.GetLoguedUser(Session).Id                
            };

            OnButtonAddAttachmentPressed(art);
        }

        private void addTemplateGroup()
        {
            BackEnd.Domain.Template t = SessionManager.GetTemplate(Session);
            var grt = new Group_R_Template()
            {
                IdTemplate = t.Id,
                Template = t,
                Receive = chkRecive.Checked,
                Module = CapaNegocioDatos.CapaHome.ModuloHome.Obtener(ddlModule.SelectedValue),
                Group = GroupHome.FindByFilters(Int32.Parse(ddlGroup.SelectedValue)),
                IdGroup = Int32.Parse(ddlGroup.SelectedValue),
                IdUsuario = SessionManager.GetLoguedUser(Session).Id
            };
            
            OnButtonAddGroupPressed(grt);
        }

        private void delTemplateGroup(int idRowGroupDel)
        {
            OnButtonDelGroupPressed(TemplateGroups[idRowGroupDel]);
            BindGridGrupos();
        }

        protected void btnDelGroup_Command(object sender, CommandEventArgs e)
        {
            delGroup(int.Parse(e.CommandArgument.ToString()));
        }

        private void delGroup(int idGroup)
        {
            if (currentGroupType.Codigo == GroupType.TEMPLATEGROUP)
                delTemplateGroup(idGroup);
            else
                delAttachmentGroup(idGroup);
        }

        private void delAttachmentGroup(int idRowGroupDel)
        {

            OnButtonDelAttachmentPressed(AttachmentGroups[idRowGroupDel]);
            BindGridGrupos();
        }

        protected void gvGruposAgregados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (currentGroupType.Codigo == GroupType.TEMPLATEGROUP)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Group_R_Template grt = (Group_R_Template)e.Row.DataItem;
                    if (e.Row.FindControl("chkRecive") != null)
                        ((CheckBox)e.Row.FindControl("chkRecive")).Checked = grt.Receive;
                }
            }
        }        
    }
}