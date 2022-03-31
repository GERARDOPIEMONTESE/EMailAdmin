using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain.External;
using EMailAdmin.BackEnd.Service.External;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.DTO;

namespace EMailAdmin.BackEnd.Strategies.EMailProcess
{
    public class AlertaXAMCasosProcess : AbstractEMailProcess
    {
        public override string GetTypeCode()
        {
            return "MXAMCASES";
        }

        protected override void SendMails()
        {            
            var xamData = ServiceLocator.Instance().GetSendMailXAMServices().GetAll();
            
            EmailXamCasesDTO dto =new EmailXamCasesDTO();
            dto.ModuleCode = "XAM";
            dto.IdLanguage = 1;

            if (xamData == null || xamData.Count() == 0)
            {
                dto.detailsCases = "<td colspan=6><b>There are no cases with delayed management</b></td>";
            }
            else
            {
                dto.detailsCases = GetCasesInfo(xamData);
            }
            
            dto.To = GetEMails();
            
            ServiceLocator.Instance().GetSendMailService().SendMailXAMCases(dto);
        }

        protected override void SendMails(int id)
        {
            throw new NotImplementedException();
        }

        private string GetEMails()
        {
            var usus = EMailListHome.FindUsersMailList(-1, "XAMCASES");
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

    }
}
