using System;
using System.Web.UI;
using EMailAdmin.EmailSender;

namespace EMailAdmin
{
    public partial class pdfTest : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //var service = new EMailSenderService { Timeout = -1 };
            //service.InitEMailProcess("mailservice@assist-card.com", "123456");
        }

        protected void Ver(object sender, EventArgs e)
        {
            Response.Redirect("pdf.ashx?VoucherCode=" + txtVoucher.Text);
        }
    }
}