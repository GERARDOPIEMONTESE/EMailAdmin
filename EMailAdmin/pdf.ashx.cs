using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using CrystalDecisions.CrystalReports.Engine;
using EMailAdmin.BackEnd.Reports.Objects;

namespace EMailAdmin
{
    /// <summary>
    /// Summary description for pdf
    /// </summary>
    public class pdf : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var voucherCode = context.Request.QueryString["VoucherCode"];

            context.Response.ContentType = "application/pdf";
            context.Response.BinaryWrite(GenerateReport(voucherCode));
        }

        private byte[] GenerateReport(string voucher)
        {
            var customerReport = new ReportDocument();
            string path = "~/Reports/USAReport.rpt";

            customerReport.Load(HttpContext.Current.Server.MapPath(path));
            customerReport.SetDataSource(GenerateDataSource(voucher));

            var crv =
                new CrystalDecisions.Web.CrystalReportPartsViewer { ReportSource = customerReport, Visible = false };

            var memorystream = (MemoryStream)customerReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

            byte[] report = memorystream.ToArray();

            customerReport.Dispose();
            crv.Dispose();
            memorystream.Dispose();

            return report;
        }

        private IEnumerable<USAReportObject> GenerateDataSource(string voucher)
        {
            var report = new List<USAReportObject>();
            var source = new USAReportObject();
            source.AdminFee = "0";
            source.BenefitsTable = "BENEFITS TABLE.";
            source.Total = "0";
            report.Add(source);
            return report;
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