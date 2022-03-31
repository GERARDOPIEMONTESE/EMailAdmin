using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EMailAdmin.BackEnd.Domain;
using System.Collections.Specialized;

namespace EMailAdmin.Services
{
    /// <summary>
    /// Descripción breve de TrackingEmail
    /// </summary>
    public class TrackingEmail : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var data = context.Request.QueryString;

            PixelData pixel = new PixelData();

            SetData(data, ref pixel);

            TrackEmail te = pixel.ConvertToTrackEmail();

            te.Modificar();
        }

        private void SetData(NameValueCollection data, ref PixelData pixel)
        {
            var typeObj = pixel.GetType();

            foreach (var prop in typeObj.GetProperties())
            {
                if (!string.IsNullOrEmpty(data[prop.Name]))
                {
                    string valor = data[prop.Name].ToString();

                    prop.SetValue(pixel, Convert.ChangeType(valor, prop.PropertyType), null);
                }
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