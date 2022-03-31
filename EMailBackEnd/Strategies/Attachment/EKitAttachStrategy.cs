using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using CapaNegocioDatos.CapaHome;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.BackEnd.Utils;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Home;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;

namespace EMailAdmin.BackEnd.Strategies.Attachment
{
    public class EKitAttachStrategy : AttachStrategy
    {
        #region Constants

        private const string ATTACH_NAME = "ASSISTCARD.pdf";

        private const string ATTACH_TYPE = "application/pdf";
                
        #endregion Constants

        public EKitAttachStrategy()
        {
            ATTACHNAME = ATTACH_NAME;
            ATTACHTYPE = ATTACH_TYPE;
        }

        public byte[] GetAttachmentEkit(string emailBody, int idLanguage)
        {
            IdLanguage = idLanguage;

            var body = PdfUtils.GetPdf(emailBody) ;
            if (body.Success)
                return body.Data;
            else
                throw body.Message;
        }
    }
}