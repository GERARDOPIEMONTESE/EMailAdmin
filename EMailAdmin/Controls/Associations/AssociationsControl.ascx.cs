using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.Utils;

namespace EMailAdmin.Controls.Associations
{
    public partial class AssociationsControl : System.Web.UI.UserControl
    {
        #region Delegates

        public delegate void AddButtonPress(object sender, EventArgs e);
        public delegate void DeleteButtonPress(object sender, EventArgs e);

        #endregion Delegates

        #region Constructor

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (SessionManager.GetGroupType(Session).Codigo == GroupType.TEMPLATEGROUP)
                {
                    grvAssociations.Columns[2].Visible = true;
                    var list = AssociationHome.GetTemplateAssociations(SessionManager.GetTemplate(Session).Id);
                    SessionManager.SetTemplateAssociations(list, Session);
                }
                else if (SessionManager.GetGroupType(Session).Codigo == GroupType.ATTACHMENTGROUP)
                {
                    grvAssociations.Columns[2].Visible = false;
                    var list = AssociationHome.GetAttachmentAssociations(SessionManager.GetAttachment(Session).Id);
                    SessionManager.SetAttachmentAssociations(list, Session);
                }
                Bind();
            }
        }

        #endregion Constructor

        #region Properties

        public IList<Attachment_R_Group> AttachmentGroups
        {
            get { return SessionManager.GetAttachmentAssociations(Session); }
        }

        public IList<Group_R_Template> TemplateGroups
        {
            get { return SessionManager.GetTemplateAssociations(Session); }
        }

        #endregion Properties

        #region Events

        public event DeleteButtonPress DeletePressed;

        public void OnDeleteButtonPressed(EventArgs e)
        {
            DeleteButtonPress handler = DeletePressed;
            if (handler != null) handler(this, e);
        }

        public event AddButtonPress AddPressed;

        public void OnAddButtonPressed(EventArgs e)
        {
            AddButtonPress handler = AddPressed;
            if (handler != null) handler(this, e);
        }

        #endregion Events

        #region Methods

        protected void BtnAddOnClick(object sender, EventArgs e)
        {
            OnAddButtonPressed(EventArgs.Empty);
        }

        protected void IbnDelete_Onclick(object sender, EventArgs e)
        {
            var bt = (ImageButton)sender;
            SessionManager.SetIdToDelete(((GridViewRow)bt.Parent.Parent).RowIndex, Session);
            OnDeleteButtonPressed(EventArgs.Empty);
        }

        #endregion Methods

        #region Private Methods

        public void DeleteAssociation()
        {
            var id = SessionManager.GetIdToDelete(Session);
            if (SessionManager.GetGroupType(Session).Codigo == GroupType.TEMPLATEGROUP)
            {
                var list = SessionManager.GetTemplateAssociations(Session);
                list.RemoveAt(id);
            }
            else if (SessionManager.GetGroupType(Session).Codigo == GroupType.ATTACHMENTGROUP)
            {
                var list = SessionManager.GetAttachmentAssociations(Session);
                list.RemoveAt(id);
            }
            Bind();
        }

        public void Bind()
        {
            if(SessionManager.GetGroupType(Session).Codigo == GroupType.TEMPLATEGROUP)
            {
                grvAssociations.DataSource = SessionManager.GetTemplateAssociations(Session);
            }
            else if (SessionManager.GetGroupType(Session).Codigo == GroupType.ATTACHMENTGROUP)
            {
                grvAssociations.DataSource = SessionManager.GetAttachmentAssociations(Session);
            }
            grvAssociations.DataBind();
            uplItems.Update();
        }

        #endregion
    }
}