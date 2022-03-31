using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.Utils;

namespace EMailAdmin.Image
{
    /// <summary>
    /// Summary description for ContentImageHandler
    /// </summary>
    public class ContentImageHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState 
    {

        public void ProcessRequest(HttpContext context)
        {
            int idContent = Convert.ToInt32(context.Request.QueryString["IdContent"] != null ?
                context.Request.QueryString["IdContent"] : "0");
            int idLanguage = Convert.ToInt32(context.Request.QueryString["IdLanguage"] != null ?
                context.Request.QueryString["IdLanguage"] : "0");
            string name = context.Request.QueryString["Name"] != null ? 
                context.Request.QueryString["Name"].ToString() : "";
            bool isPreview = context.Request.QueryString["IsPreview"] != null;

            int idContentImage = Convert.ToInt32(context.Request.QueryString["IdContentImage"] != null ?
                context.Request.QueryString["IdContentImage"] : "0");

            ContentImage contentImage = null;

            if (isPreview && HttpContext.Current.Session != null && 
                SessionManager.GetPreviewTemplate(HttpContext.Current.Session) != null)
            {
                contentImage = SessionManager.GetPreviewTemplate(HttpContext.Current.Session).
                    GetContent(idLanguage).GetContentImage(name);
            }

            if (contentImage == null)
            {
                contentImage = idContentImage != 0 ?
                    ContentImageHome.Get(idContentImage) :
                    ContentImageHome.Get(idContent, idLanguage, name);
            }

                context.Response.ContentType = "image";
                context.Response.BinaryWrite((byte[]) (contentImage.Content != null ?
                    contentImage.Content : new byte[1]));
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