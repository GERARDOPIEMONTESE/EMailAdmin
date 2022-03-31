using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Domain.External;

namespace EMailAdmin.BackEnd.Strategies.EMailProcess
{
    public class XAMCasesExtendedProcess : AbstractEMailProcess
    {   
        public override string GetTypeCode()
        {
            return "EXTXAMCASE";
        }

        protected override void SendMails()
        {
            var xamData = ServiceLocator.Instance().GetSendMailXAMServices().GetAllExtended(); 
            if (xamData == null || xamData.Count() == 0)
                return;
            else
            {
                EmailXamCasesDTO dto = new EmailXamCasesDTO();
                dto.ModuleCode = "XAM";
                dto.IdLanguage = 1;
                dto.detailsCases = GetCasesInfo(xamData);
                dto.To = GetEMails();
                ServiceLocator.Instance().GetSendMailService().SendMailXAMCases(dto);
            }
        }

        private string GetEMails()
        {
            var usus = EMailListHome.FindUsersMailList(-1, "EXTXAMCASE");
            string emails = "";

            foreach (var emailUsu in usus)
            {
                if (emails != "") emails += ",";
                emails += emailUsu.CorreoElectronico;
            }
            return emails;
        }

        private string GetCasesInfo(IList<EmailAlertXamCases> alertsChats)
        {
            string itemsData = "";
            foreach (var xamCase in alertsChats)
            {
                itemsData += EmailXamCasesDTO.GetInfoMail(xamCase);
            }
            return itemsData;
        }

        protected override void SendMails(int id)
        {
            throw new NotImplementedException();
        }

    }
}
