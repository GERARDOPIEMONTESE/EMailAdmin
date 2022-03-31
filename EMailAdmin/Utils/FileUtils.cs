using System;
using System.Drawing;
using System.IO;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.Utils
{
    public class FileUtils
    {
        public static Stream Text(FileUpload fup)
        {
            Stream stream = fup.FileContent;
            return stream;
        }
        
        public static ContentImage Image(FileUpload fup)
        {
            string[] splited = fup.FileName.Split(new[] {"."}, StringSplitOptions.None);
            var image = new ContentImage
                            {
                                Content = fup.FileBytes,
                                Dimenssion = fup.FileBytes.Length,
                                Name = fup.FileName,
                                Type = splited[splited.Length - 1]
                            };
            return image;
        }

        public static ContentImage Image(AsyncFileUpload fup)
        {
            string[] splited = fup.FileName.Split(new[] { "." }, StringSplitOptions.None);
            var image = new ContentImage
            {
                Content = fup.FileBytes,
                Dimenssion = fup.FileBytes.Length,
                Name = fup.FileName,
                Type = splited[splited.Length - 1]
            };
            return image;
        }

        public static AttachmentItem AttachmentItem(AsyncFileUpload fup)
        {
            string[] splited = fup.FileName.Split(new[] { "." }, StringSplitOptions.None);
            var item = new AttachmentItem
            {
                Content = fup.FileBytes,
                Name = fup.FileName,
                Type = splited[splited.Length - 1],
                Dimenssion = fup.FileBytes.Length,
                Added = false,
            };
            return item;
        }

        public static System.Drawing.Image Thumb(System.Drawing.Image file, int width, int height)
        {
            System.Drawing.Image pThumbnail = null;
            var myCallback =
                new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
            try
            {
                decimal widthPercent = Math.Round(Convert.ToDecimal(width)/Convert.ToDecimal(file.Width)*100, 2);
                decimal heightPercent = Math.Round(Convert.ToDecimal(height)/Convert.ToDecimal(file.Height)*100, 2);
                decimal percent = file.Width > file.Height ? widthPercent : heightPercent;
                pThumbnail = new Bitmap(width, height, file.PixelFormat);

                pThumbnail = file.GetThumbnailImage((int) ((file.Width*percent)/100),
                                                    (int) ((file.Height*percent)/100),
                                                    myCallback, IntPtr.Zero);
            }
            catch
            {
                return pThumbnail;
            }

            return pThumbnail;
        }

        private static bool ThumbnailCallback()
        {
            return true;
        }

        public static System.Drawing.Image GetDefaultImage(string defaultImage)
        {
            return Thumb(defaultImage, 100, 100);
        }

        public static System.Drawing.Image Thumb(string lcFilename, int width, int height)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(lcFilename);
            return Thumb(img, width, height);
        }
    }
}