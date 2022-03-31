using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Domain;
using System.IO;
using System.Web.SessionState;
using MenuPortalLibrary.CapaDTO;
using CapaNegocioDatos.CapaNegocio;
using System.Text;
using System.Web.UI;

namespace EMailAdmin.Attachment
{
    /// <summary>
    /// Summary description for FixedAttachmentHandler
    /// </summary>
    public class FixedAttachmentHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            UsuarioDTO UsuDto = (UsuarioDTO)context.Session["UsuarioDTO"];

            int idAttachment = Convert.ToInt32(context.Request.QueryString["IdAttachment"] != null ?
                           context.Request.QueryString["IdAttachment"] : "0");
            int idLanguage = Convert.ToInt32(context.Request.QueryString["IdLanguage"] != null ?
                context.Request.QueryString["IdLanguage"] : (UsuDto != null ? UsuDto.Ididioma.ToString() : "0"));


            BackEnd.Domain.Attachment attachment = AttachmentHome.Get(idAttachment);

            AttachmentItem item = attachment.GetItem(idLanguage);

            if (item.Id != 0)
            {
                MemoryStream ms = new MemoryStream((byte[])(item.Content != null ? item.Content : new byte[1]));
                context.Response.ContentType = "application/" + item.Type;
                context.Response.AddHeader("content-disposition", "attachment;filename=" + item.Name);
                context.Response.Buffer = true;
                ms.WriteTo(context.Response.OutputStream);
            }
            else
            {
                string msg = "";
                switch (idLanguage.ToString())
                {
                    case Idioma.INGLES:
                        msg = "A file is not found in English.";
                        break;
                    case Idioma.ESPANOL:
                        msg = "No se encontro archivo en Español.";
                        break;
                    case Idioma.PORTUGUES:
                        msg = "Um arquivo não é encontrado em Portugues.";
                        break;
                    default:
                        msg = "A file is not found for that language.";
                        break;
                }

                context.Response.ClearHeaders();
                context.Response.ClearContent();
                context.Response.ContentType = "text/html";
                context.Response.Write(attachment.Name+"<br/>" +msg);
                context.Response.End();
            }
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