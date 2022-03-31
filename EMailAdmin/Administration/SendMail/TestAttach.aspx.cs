using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocioDatos.CapaHome;
using ControlMenu;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Service;

namespace EMailAdmin.Administration.SendMail
{
    public partial class TestAttach : CustomPage
    {
        #region Constants

        private const string ModuleCode = "ACNET";
        private const string UserACNETService = "mailservice@assist-card.com";
        private const string PassACNETService = "123456";

        #endregion Constants

        #region Constructor

        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            var dto = new EMailEkitDTO
            {
                CountryCode = Convert.ToInt32(Request.QueryString["country"]), 
                VoucherCode = Request.QueryString["voucher"],
                ModuleCode = ModuleCode
            };

            var outBuf = ServiceLocator.Instance().GetSendMailService().GetAttachments(dto);

            Response.Expires = 0;
            // Specify the property to buffer the output page
            Response.Buffer = true;
            // Erase any buffered HTML output
            Response.ClearContent();
            //Add a new HTML header and value to the response sent to the client
            Response.AddHeader("content-disposition", "inline; filename=" + "output.pdf");
            // Specify the HTTP content type for response as Pdf
            Response.ContentType = "application/pdf";
            // Write specified information of current HTTP output to Byte array
            Response.BinaryWrite(outBuf);
            //end the processing of the current page to ensure that no other HTML content is sent
            Response.End();

        }

        #endregion
    }
}