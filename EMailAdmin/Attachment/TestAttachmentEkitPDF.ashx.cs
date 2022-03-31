using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Service;
using System.IO;
using EMailAdmin.Utils;
using System.Web.SessionState;
using EMailAdmin.BackEnd.Utils;
using EMailAdmin.BackEnd.Home;
using CapaNegocioDatos.CapaHome;

namespace EMailAdmin.Attachment
{
    /// <summary>
    /// Descripción breve de TestAttachmentEkitPDF
    /// </summary>
    public class TestAttachmentEkitPDF : IHttpHandler, IRequiresSessionState
    {
        //--http://localhost:19809/Attachment/TestAttachmentEkitPDF.ashx?IdLanguage=1&IdTemplate=4120&CodigoPais=540&Debug=1

        public void ProcessRequest(HttpContext context)
        {
            BackEnd.Domain.Template temp;
            bool modoDebug = false;
            if (context.Request.QueryString.AllKeys.Contains("Debug"))
                modoDebug = true;
            int idLanguage = Convert.ToInt32(context.Request.QueryString["IdLanguage"] != null ?
                context.Request.QueryString["IdLanguage"] : "0");
            int idTemplate = Convert.ToInt32(context.Request.QueryString["IdTemplate"] != null ?
                context.Request.QueryString["IdTemplate"] : "0");
            string countryCode = context.Request.QueryString["CodigoPais"] != null ?
                context.Request.QueryString["CodigoPais"] : "";

            if (idTemplate > 0)
                temp = TemplateHome.Get(idTemplate);
            else
            {
                modoDebug = true;
                temp = SessionManager.GetPreviewTemplate(context.Session);
            }
            
            int IdPais = (countryCode==""?SessionManager.GetLoguedUser(context.Session).IdPais:
                PaisHome.ObtenerPorCodigo(countryCode).Id);

            var bodyHtml = ServiceLocator.Instance().GetTemplateService().ParseBody(
                idLanguage, IdPais , temp, true);

            var rst = PdfUtils.GetPdf(bodyHtml);

            if (modoDebug || rst.Success == false)
            {
                if (rst.Success)
                {
                    context.Response.ContentType = "application/pdf";
                    context.Response.BinaryWrite(rst.Data);
                }
                else
                {
                    context.Response.ClearHeaders();
                    context.Response.ClearContent();
                    context.Response.ContentType = "text/html";
                    context.Response.Write(rst.Message.Message + "<br/>" + rst.Message.Source);
                    context.Response.End();
                }
            }
            else
            {
                MemoryStream ms = new MemoryStream(rst.Data);
                context.Response.ContentType = "application/pdf";
                context.Response.AddHeader("content-disposition", "attachment;filename=ASSISTCARD.pdf");
                context.Response.Buffer = true;
                ms.WriteTo(context.Response.OutputStream);
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