using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Service;

namespace EMailAdmin.Register
{
    public partial class EMailRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["CountryCode"] != null && 
                Request.QueryString["VoucherCode"] != null && Request.QueryString["TemplateType"] != null)
            {
                int countryCode = 0;
                Int32.TryParse(Request.QueryString["CountryCode"], out countryCode);

                TemplateType templateType = TemplateTypeHome.Get(Request.QueryString["TemplateType"]);

                ServiceLocator.Instance().GetEMailLogService().RegisterEMailReception(
                    countryCode, Request.QueryString["VoucherCode"], templateType.Id);
            }
        }
    }
}