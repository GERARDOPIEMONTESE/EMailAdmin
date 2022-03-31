using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Service;
using CapaNegocioDatos.Servicios;

namespace EMailAdmin.BackEnd.Strategies.EMailProcess
{
    public class ConditionProcess : AbstractEMailProcess
    {
        public override string GetTypeCode()
        {
            return "CndAlt";
        }

        protected override void SendMails()
        {
            SendMails(-1);
        }

        protected override void SendMails(int id)
        {
            PointsReportDTO dto = new PointsReportDTO();
            dto.ModuleCode = "EMailAdmin";
            dto.CountryCode = 540;
            dto.IdLanguage = 1;
            // Esta fecha se completa mas adelante en
            // la estrategia MailAlertAttachStrategy
            //dto.ReportDate = DateTime.Now;

            ServiceLocator.Instance().GetSendMailService().SendMailConditionAlert(dto);
        }
    }
}
