using System;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.Controls.Selectors.VariableText
{
    public partial class VariableTextSelector : System.Web.UI.UserControl
    {
        #region Delegate

        public delegate void VariableTextUploaded(object sender, EventArgs e);
        public delegate void VariableTextCancel(object sender, EventArgs e);

        #endregion

        #region Properties

        public BackEnd.Domain.VariableText VariableText
        {
            get
            {
                return new BackEnd.Domain.VariableText
                {
                    Name = ddlName.SelectedItem.Text,
                    Id = Convert.ToInt32(ddlName.SelectedValue)
                };
            }
        }

        #endregion

        #region Events

        public event VariableTextUploaded VariableTextUploadedCompleted;
        public event VariableTextCancel VariableTextCanceled;

        public void OnVariableTextUploadedCompleted(EventArgs e)
        {
            var handler = VariableTextUploadedCompleted;
            if (handler != null) handler(this, e);
        }

        public void OnVariableTextCanceled(EventArgs e)
        {
            var handler = VariableTextCanceled;
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
            OnVariableTextUploadedCompleted(EventArgs.Empty);
        }

        protected void BtnCancelOnClick(object sender, EventArgs e)
        {
            OnVariableTextCanceled(EventArgs.Empty);
        }

        #endregion

        #region Private Methods

        private void LoadList()
        {
            ddlName.DataSource = VariableTextHome.FindByType(VariableTextType.TEXTTYPE);
            ddlName.DataBind();
        }

        #endregion Private Methods
    }
}