using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ControlMenu;
using EMailAdmin.BackEnd.Service;

namespace EMailAdmin.Administration.Zip
{
    public partial class EMailLogZip : CustomPage
    {
        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewPending();
            }
        }

        protected void BtnProcessOnClick(object sender, EventArgs e)
        {
            ServiceLocator.Instance().GetEMailLogService().ZipContextInformation();
            ViewPending();
        }

        private void ViewPending()
        {
            lblPending.Text = "Cant. a procesar " + ServiceLocator.Instance().GetEMailLogService().ViewZipContextInformation();
        }
    }
}