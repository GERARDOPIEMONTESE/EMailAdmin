using System.IO;
using System.Web;
using System.Web.Hosting;
using EMailAdmin.Utils;

namespace EMailAdmin.Image
{
    /// <summary>
    /// Summary description for ShowImageHandler
    /// </summary>
    public class ShowImageHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            byte[] i = null;
            if (SessionManager.GetImage(context.Session) != null)
            {
                i = SessionManager.GetImage(context.Session);
            }
            else
            {
                var im = FileUtils.GetDefaultImage(HostingEnvironment.MapPath("~/IMG/Empty.gif"));
                var ms = new MemoryStream();
                im.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                i = ms.ToArray();
            }
            context.Response.BinaryWrite(i);
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}