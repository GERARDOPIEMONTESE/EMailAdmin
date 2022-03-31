using System;
using System.Web.UI;
using AjaxControlToolkit;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.Utils;

namespace EMailAdmin.Controls.Selectors.Image
{
    public partial class ImageSelector : UserControl
    {
        #region Delegate

        public delegate void ImageUploaded(object sender, EventArgs e);
        public delegate void ImageCancel(object sender, EventArgs e);

        #endregion

        #region Properties

        public ContentImage Image
        {
            get { return SessionManager.GetTemplateImage(Session); }
        }

        #endregion

        #region Events

        public event ImageUploaded ImageUploadedCompleted;
        public event ImageCancel ImageCanceled;

        public void OnImageUploadedCompleted(EventArgs e)
        {
            var handler = ImageUploadedCompleted;
            if (handler != null) handler(this, e);
        }

        public void OnImageCanceled(EventArgs e)
        {
            var handler = ImageCanceled;
            if (handler != null) handler(this, e);
        }

        #endregion

        #region Methods

        protected void BtnAcceptOnClick(object sender, EventArgs e)
        {
            OnImageUploadedCompleted(EventArgs.Empty);
        }

        protected void BtnCancelOnClick(object sender, EventArgs e)
        {
            OnImageCanceled(EventArgs.Empty);
        }

        protected void FupImageOnUploadedComplete(object sender, AsyncFileUploadEventArgs args)
        {
            var image = FileUtils.Image(fupImage);
            SessionManager.SetTemplateImage(image, Session);
        }

        #endregion
    }
}