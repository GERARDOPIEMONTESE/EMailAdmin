using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Service;
using System.IO;

namespace EMailAdmin.Attachment
{
    /// <summary>
    /// Summary description for StrategyAttachmentHandler
    /// </summary>
    public class StrategyAttachmentHandler : IHttpHandler
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

                Nullable<bool> bMergeAttachs = null;
                if (context.Request.QueryString.AllKeys.Contains("mergeAttachs"))
                    bMergeAttachs = bool.Parse(context.Request.QueryString["mergeAttachs"]);

                EMailEkitDTO dto = new EMailEkitDTO();
                dto.CountryCode = countryCode;
                dto.VoucherCode = voucherCode == null || voucherCode.Length == 0 ? "0" : voucherCode.Trim();
                dto.ModuleCode = moduleCode == null || moduleCode.Length == 0 ? "-" : moduleCode.Trim();
                dto.IdLanguage = idLanguage;
                string attachName = "";

                byte[] attachment = ServiceLocator.Instance().GetSendMailService().FindAttachmentMailEkit(dto, out attachName, bMergeAttachs);

                if (modoDebug)
                {
                    context.Response.ContentType = "application/pdf";
                    context.Response.BinaryWrite(attachment);
                }
                else
                {
                    MemoryStream ms = new MemoryStream(attachment);
                    context.Response.ContentType = "application/pdf";
                    context.Response.AddHeader("content-disposition", "attachment;filename="+attachName);
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