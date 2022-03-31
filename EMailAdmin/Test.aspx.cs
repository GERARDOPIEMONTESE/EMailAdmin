using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ControlMenu;
using EMailAdmin.EmailSender;


namespace EMailAdmin
{
    public partial class Test : CustomPage
    {
        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            try
            {
                EMailSenderService service = new EMailSenderService();
                service.InitEMailProcess("mailservice@assist-card.com", "123456");
            }
            catch (Exception ex)
            {
                string ms = ex.Message;
                Response.Write(ms);
            }
        }
        
    }
}