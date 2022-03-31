using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Service;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Text;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.pipeline.html;
using System.Net;
using System.Threading;
using EMailAdmin.Administration.SendMail;
using EMailAdmin.Utils;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace EMailAdmin.Administration.SendMail
{
    /// <summary>
    /// Descripción breve de SendEmailPdfHandler
    /// </summary>
    public class SendEmailPdfHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int idLanguage = Convert.ToInt32(context.Request.QueryString["IdLanguage"] != null ?
                context.Request.QueryString["IdLanguage"] : "0");
            string voucherCode = context.Request.QueryString["VoucherCode"];
            int countryCode = Convert.ToInt32(context.Request.QueryString["CountryCode"] != null ?
                context.Request.QueryString["CountryCode"] : "0");
            string moduleCode = context.Request.QueryString["ModuleCode"];

            bool modoDebug = false;
            if (context.Request.QueryString.AllKeys.Contains("Debug"))
                modoDebug = true;

            string fileName = string.Format("EKIT {0}-{1}.pdf", countryCode, voucherCode);

            EMailEkitDTO dto = new EMailEkitDTO();
            dto.CountryCode = countryCode;
            dto.VoucherCode = voucherCode == null || voucherCode.Length == 0 ? "0" : voucherCode.Trim();
            dto.ModuleCode = moduleCode == null || moduleCode.Length == 0 ? "-" : moduleCode.Trim();
            dto.IdLanguage = idLanguage;

            byte[] rst = ServiceLocator.Instance().GetSendMailService().GetMailEkit(dto);
                                         
            if (modoDebug)
            {
                context.Response.ContentType = "application/pdf";
                context.Response.BinaryWrite(rst);
            }
            else
            {
                MemoryStream ms = new MemoryStream(rst);
                context.Response.ContentType = "application/pdf";
                context.Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
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