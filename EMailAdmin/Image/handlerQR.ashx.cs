using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MessagingToolkit.QRCode.Codec;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Text;

namespace EMailAdmin.Image
{
    /// <summary>
    /// Descripción breve de handlerQR
    /// </summary>
    public class handlerQR : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string dataQR = "";
            string encodedString = "";
            string QRCodeScale = "";
            int[] escala = new int[2] { 150, 150 };

            if (context.Request.QueryString.AllKeys.Contains("QRCodeScale"))
            {
                QRCodeScale = context.Request.QueryString["QRCodeScale"].ToString();
                try
                {
                    int.TryParse(QRCodeScale.Split('x')[0], out escala[0]);
                    int.TryParse(QRCodeScale.Split('x')[1], out escala[1]);
                }
                catch { }
            }

            if (context.Request.QueryString.AllKeys.Contains("QRData"))
            {
                string sParametroData = "QRData=";
                int largo = context.Request.QueryString.ToString().IndexOf(sParametroData) + sParametroData.Length;

                dataQR = context.Request.QueryString.ToString().Substring(largo);
            }
            else
            {
                dataQR = context.Request.QueryString.ToString();
            }

            encodedString = System.Web.HttpUtility.UrlDecode(dataQR);            

            if (encodedString != "")
            {
                var imgQR = GetQRImage(encodedString, escala);
                context.Response.ContentType = "image/png";
                context.Response.BinaryWrite(imgQR);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private byte[] GetQRImage(string QRCode, int[] escala)
        {
            try
            {
                QRCodeEncoder encoder = new QRCodeEncoder();
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                encoder.QRCodeBackgroundColor = Color.White;
                Bitmap qrMap = new Bitmap((System.Drawing.Image)encoder.Encode(QRCode), new Size(escala[0], escala[1]));
                //Bitmap qrMap = encoder.Encode(QRCode);
                MemoryStream ms = new MemoryStream();
                qrMap.Save(ms, ImageFormat.Png);

                return ms.ToArray();
            }
            catch
            {
                return null;
            }
        }
    }
}