using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.Controls.Selectors.Pixel
{
    public partial class PixelSelector : System.Web.UI.UserControl
    {
        #region Delegate

        public delegate void PixelUploaded(object sender, EventArgs e);
        public delegate void PixelCancel(object sender, EventArgs e);

        #endregion

        #region Properties

        public BackEnd.Domain.Pixel Pixel
        {
            get
            {
                return new BackEnd.Domain.Pixel
                {
                    Name = ddlName.SelectedItem.Text,
                    Id = Convert.ToInt32(ddlName.SelectedValue)
                };
            }
        }

        #endregion

        #region Events

        public event PixelUploaded PixelUploadedCompleted;
        public event PixelCancel PixelCanceled;

        public void OnVariableTextUploadedCompleted(EventArgs e)
        {
            var handler = PixelUploadedCompleted;
            if (handler != null) handler(this, e);
        }

        public void OnVariableTextCanceled(EventArgs e)
        {
            var handler = PixelCanceled;
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
            ddlName.DataSource = PixelHome.BuscarPixels("");
            ddlName.DataBind();
        }

        #endregion Private Methods
    }
}