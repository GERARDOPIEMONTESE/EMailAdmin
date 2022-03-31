using System.Collections.Generic;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Home.Information;
using EMailAdmin.BackEnd.Domain.Information;

namespace EMailAdmin.BackEnd.Strategies.EMailProcess
{
    public class WelcomeBackProcess : AbstractEMailProcess
    {
        public override string GetTypeCode()
        {
            return "Back";
        }

        private IList<WelcomeBackInformation> GetEffectiveEndDateIssuances()
        {
            return WelcomeBackInformationHome.FindEffectiveEndDate();
        }

        protected override void SendMails()
        {
            IList<WelcomeBackInformation> issuances = GetEffectiveEndDateIssuances();

            foreach (WelcomeBackInformation welcomeBack in issuances)
            {
                var dto = new DefaultEMailDTO();
                dto.IdLanguage = 1;
                dto.To = welcomeBack.PaxEMail;
                dto.CountryCode = 540;
                dto.VoucherCode = welcomeBack.VoucherCode;
                dto.ModuleCode = "ACNET";

                ServiceLocator.Instance().GetSendMailService().SendMailWelcomeBack(dto);
            }
        }

        protected override void SendMails(int id)
        {
        }
    }
}
