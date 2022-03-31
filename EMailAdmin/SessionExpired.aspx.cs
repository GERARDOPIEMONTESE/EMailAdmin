using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace EMailAdmin
{
    public partial class SessionExpired : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string QueryString = Request.QueryString.Count > 0 ? "?" + Request.QueryString.ToString() : "";

            Session.RemoveAll();
            Session.Clear();

            Page.ClientScript.RegisterClientScriptBlock
                    (GetType(), "script",
                     @"
                    <script language='javascript' type='text/javascript'>
                            parent.location.href = '" +
                     ConfigurationManager.AppSettings["LoginURL"] + QueryString +
                     "';</script>"
                    );
        }
    }
}