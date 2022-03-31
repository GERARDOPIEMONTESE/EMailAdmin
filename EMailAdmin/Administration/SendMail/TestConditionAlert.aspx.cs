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
    public partial class TestConditionAlert : CustomPage
    {
        #region Constants

        private const string ModuleCode = "ACNET";
        private const string UserACNETService = "gsuarez@assist-card.com";
        private const string PassACNETService = "P4ssw0rd01";

        #endregion Constants

        #region Constructor

        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            var dto = new ConditionAlertDTO
            {
                CountryCode = 540,
                //VoucherCode = Request.QueryString["voucher"],
                IdLanguage = 1,
                ModuleCode = "EMailAdmin", //ModuleCode,
                ReportDate = DateTime.Now
            };

            ServiceLocator.Instance().GetSendMailService().SendMailConditionAlert(dto);

            /*Response.Expires = 0;
            Response.Buffer = true;
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=emisiones.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/ms-excel";
            Response.BinaryWrite(outBuf);
            Response.End();*/

        }

        #endregion
    }
}