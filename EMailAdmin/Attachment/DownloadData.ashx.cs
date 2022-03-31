using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMailAdmin.Attachment
{
    /// <summary>
    /// Descripción breve de DownloadData
    /// </summary>
    public class DownloadData : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string url = "http://172.17.1.40:8080/servlet/app?handler=PrintPolizaHandler&accion=printCertificadoByVoucher&pais=550&codigo=8988196";
            byte[] imageData;
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                imageData = client.DownloadData(url);
            }

            if (System.Text.Encoding.UTF8.GetString(imageData).Contains("error@assist-card.com") == false)
            {
                context.Response.ContentType = "application/pdf";  // Change the content type if necessary
                context.Response.OutputStream.Write(imageData, 0, imageData.Length);
                context.Response.Flush();
                context.Response.End();
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