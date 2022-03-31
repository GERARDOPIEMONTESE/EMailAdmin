using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.Services
{
    /// <summary>
    /// Descripción breve de TrackingLink
    /// </summary>
    public class TrackingLink : IHttpHandler
    {
        public string UrlDestino { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            var data = context.Request.QueryString;

            UrlDestino = data["URLDESTINO"].Replace('|','&').ToString();

            Tracking_Link(data);
            
            context.Response.Redirect(UrlDestino, true);
        }

        private void Tracking_Link(System.Collections.Specialized.NameValueCollection data)
        {
            try
            {
                int idLink = 0;
                int.TryParse(data["IDLINK"].ToString(), out idLink);
                int IdEmailLog = 0;
                int.TryParse(data["IDEMAILLOG"].ToString(), out IdEmailLog);

                TrackLink tl = new TrackLink()
                {
                    IdLink = idLink,
                    IdEmailLog = IdEmailLog,
                    UrlDestino = UrlDestino
                };

                tl.Modificar();
            }
            catch
            {
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