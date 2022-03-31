using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Domain;
using System.Configuration;
using EMailAdmin.BackEnd.Home;
using CapaNegocioDatos.CapaNegocio;
using CapaNegocioDatos.CapaHome;

namespace EMailAdmin.BackEnd.Strategies.EMailProcess
{
    public class ACCOMAlertNotIssue : AbstractEMailProcess
    {
        public override string GetTypeCode()
        {
            return "ACCOMISSUE";
        }

        protected override void SendMails()
        {
            EmailAlertNotIssueDTO dto = new EmailAlertNotIssueDTO();

            IList<ACCOMNotIssue> alertsIssues = GetACCOMNotIssue();
            if (alertsIssues == null || alertsIssues.Count == 0)
                return;
            else
            {
                foreach (var item in alertsIssues)
                {
                    dto.To = GetEMails(item.CountryCode);
                    if (dto.To.Length > 0)
                    {
                        dto.CountryCode = item.CountryCode;
                        dto.CountryName = item.CountryName;
                        dto.GatewayName = item.Gateway;
                        dto.LastIssue = item.QuoteLogDate.ToString();
                        dto.LastConfirmation = item.LastConfirmationDate.ToString();
                        dto.PurchaseProcessTypeDesc = item.PurchaseProcessTypeDesc;
                        dto.ModuleCode = "ACCOM";
                        dto.IdLanguage = 1;
                        ServiceLocator.Instance().GetSendMailService().SendMailACCOMNotIssue(dto);
                    }
                }
            }
        }

        private IList<ACCOMNotIssue> GetACCOMNotIssue()
        {
            return ACCOMHome.FindAll();
        }

        protected override void SendMails(int id)
        {
            throw new NotImplementedException();
        }

        private string GetEMails(int CountryCode)
        {
            var usus = EMailListHome.FindUsersMailList(CountryCode, "COMNISSUE");
            string emails = "";

            foreach (var emailUsu in usus)
            {
                if (emails != "") emails += ",";
                emails += emailUsu.CorreoElectronico;
            }
            return emails;
        }
    }
}
