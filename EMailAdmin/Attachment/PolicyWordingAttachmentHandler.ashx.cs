using CapaNegocioDatos.CapaHome;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace EMailAdmin.Attachment
{
    /// <summary>
    /// Summary description for PolicyWordingAttachmentHandler
    /// </summary>
    public class PolicyWordingAttachmentHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            int idLanguage = Convert.ToInt32(context.Request.QueryString["IdLanguage"] != null ?
                context.Request.QueryString["IdLanguage"] : "0");
            string voucherCode = context.Request.QueryString["VoucherCode"];
            int countryCode = Convert.ToInt32(context.Request.QueryString["CountryCode"] != null ?
                context.Request.QueryString["CountryCode"] : "0");
            string moduleCode = context.Request.QueryString["ModuleCode"];

            string AttachmentKey = "";
            if (context.Request.QueryString.AllKeys.Contains("AttachmentKey"))
                AttachmentKey = context.Request.QueryString["AttachmentKey"];

            if (string.IsNullOrEmpty(AttachmentKey))
                AttachmentKey = ConfigurationValueHome.GetByCode("PolicyWordingAttachmentKey").Value;

            bool modoDebug = false;
            if (context.Request.QueryString.AllKeys.Contains("Debug"))
                modoDebug = true;

            string attachName = "";
            var idioma = IdiomaHome.Obtener(idLanguage);
            var culture = new CultureInfo(idioma.Cultura);

            EMailEkitDTO dto = new EMailEkitDTO();
            dto.CountryCode = countryCode;
            dto.VoucherCode = voucherCode == null || voucherCode.Length == 0 ? "0" : voucherCode.Trim();
            dto.ModuleCode = moduleCode == null || moduleCode.Length == 0 ? "-" : moduleCode.Trim();
            dto.IdLanguage = idLanguage;

            FiltersAttachsDTO filtersAttach = new FiltersAttachsDTO() { AttachName = AttachmentKey };
            byte[] attachment = ServiceLocator.Instance().GetSendMailService().FindAttachmentMailEkit(dto, filtersAttach);

            if (modoDebug)
            {
                context.Response.ContentType = "application/pdf";
                context.Response.BinaryWrite(attachment);
            }
            else
            {
                MemoryStream ms = new MemoryStream(attachment);
                context.Response.ContentType = "application/pdf";
                context.Response.AddHeader("content-disposition", "attachment;filename=" + attachName);
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