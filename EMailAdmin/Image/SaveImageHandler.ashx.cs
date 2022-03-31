using System.Drawing;
using System.IO;
using System.Web;
using EMailAdmin.Utils;

namespace EMailAdmin.Image
{
    /// <summary>
    /// Summary description for SaveImageHandler
    /// </summary>
    public class SaveImageHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
                string fileExtension = Path.GetExtension(context.Request.Files[0].FileName).ToLower();

                HttpPostedFile file = context.Request.Files[0];

                System.Drawing.Image img = new Bitmap(file.InputStream);
                System.Drawing.Image thumb = Utils.FileUtils.Thumb(img, 115, 115);

                var ms = new MemoryStream();

                //jpg|png|jpeg|gif
                switch (fileExtension.ToUpper())
                {
                    case ".JPG":
                        thumb.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case ".PNG":
                        thumb.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    case ".JPEG":
                        thumb.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case ".GIF":
                        thumb.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    default:
                        thumb.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                }

                AddPictureToContext(context, ms.ToArray(), fileExtension);
                context.Response.ContentType = "text/plain";
                context.Response.Write("OK");
        }

        private void AddPictureToContext(HttpContext context, byte[] picture, string extension)
        {
            SessionManager.SetImage(picture, context.Session);
            SessionManager.SetImageExtension(extension, context.Session);
            SessionManager.SetSaveImage(true, context.Session);
        }

        public bool IsReusable
        {
            get { return false; }
        } 
    }
}