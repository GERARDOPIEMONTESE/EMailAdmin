using System;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.Controls.Selectors.Link
{
    public partial class LinkSelector : System.Web.UI.UserControl
    {
        #region Delegate

        public delegate void LinkUploaded(object sender, EventArgs e);
        public delegate void LinkCancel(object sender, EventArgs e);

        #endregion

        #region Properties

        public BackEnd.Domain.Link Link
        {
            get
            {
                if (ddlLinkType.SelectedValue == LinkType.FIXED || ddlLinkType.SelectedValue == LinkType.QR)
                {
                    if (ddlLinkFixedName.SelectedValue == "0" && ddlLinkQR.SelectedValue=="0")
                    {
                        return new BackEnd.Domain.Link
                                   {
                                       Name = txtName.Text,
                                       Url = txtUrl.Text,
                                       LinkType = LinkTypeHome.Get(ddlLinkType.SelectedValue)
                                   };
                    }
                    else
                    {
                        if (ddlLinkType.SelectedValue == LinkType.FIXED)
                            return LinkHome.Get(int.Parse(ddlLinkFixedName.SelectedValue));

                        if (ddlLinkType.SelectedValue == LinkType.QR)
                            return LinkHome.Get(int.Parse(ddlLinkQR.SelectedValue));
                    }
                }

                if (ddlLinkType.SelectedValue == LinkType.CONTEXT)
                    return LinkHome.Get(int.Parse(ddlLinkName.SelectedValue));

                return new BackEnd.Domain.Link
                           {
                               Name = ddlLinkName.SelectedItem.Text,
                               Url = "",
                               LinkType = LinkTypeHome.Get(ddlLinkType.SelectedValue)
                           };
            }
        }

        #endregion

        #region Events

        public event LinkUploaded LinkUploadedCompleted;
        public event LinkCancel LinkCanceled;

        public void OnLinkUploadedCompleted(EventArgs e)
        {   
            var handler = LinkUploadedCompleted;
            if (handler != null) handler(this, e);
        }

        public void OnLinkCanceled(EventArgs e)
        {
            var handler = LinkCanceled;
            if (handler != null) handler(this, e);
        }

        #endregion

        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadList();
            }
        }

        protected void BtnAcceptOnClick(object sender, EventArgs e)
        {
            OnLinkUploadedCompleted(EventArgs.Empty);
            ddlLinkFixedName.ClearSelection();
        }

        protected void BtnCancelOnClick(object sender, EventArgs e)
        {
            OnLinkCanceled(EventArgs.Empty);
            ddlLinkFixedName.ClearSelection();
        }
        
        #endregion

        #region Private Methods

        private void LoadList()
        {
            ddlLinkType.DataSource = LinkTypeHome.FindAll();
            ddlLinkType.DataBind();

            ddlLinkQR.DataSource = LinkHome.LinksFixed(LinkType.QR);
            ddlLinkQR.DataBind();
            ddlLinkQR.Items.Insert(0, new System.Web.UI.WebControls.ListItem() { Value = "0", Text = "-- Crear nuevo link --" });

            ddlLinkFixedName.DataSource = LinkHome.LinksFixed(LinkType.FIXED);
            ddlLinkFixedName.DataBind();
            ddlLinkFixedName.Items.Insert(0, new System.Web.UI.WebControls.ListItem() { Value="0", Text = "-- Crear nuevo link --" });

            ddlLinkName.DataSource = VariableTextHome.FindByType(VariableTextType.LINKTYPE);
            ddlLinkName.DataBind();
        }

        #endregion Private Methods
    }
}