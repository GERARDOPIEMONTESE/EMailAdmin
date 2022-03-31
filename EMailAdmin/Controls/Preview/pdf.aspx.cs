using System.Web.UI;
using EMailAdmin.Utils;

namespace EMailAdmin.Controls.Preview
{
    public partial class pdf : Page
    {
        protected void Page_Load()
        {
            Refresh();
        }

        public void Refresh()
        {
            var body = SessionManager.GetBodyHTML(Session);
            if (!string.IsNullOrEmpty(body))
            {
                Context.Response.Write(body);
            }
        }
    }
}