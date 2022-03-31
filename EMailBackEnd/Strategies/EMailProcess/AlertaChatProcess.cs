using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Service;
using CapaNegocioDatos.CapaHome;

namespace EMailAdmin.BackEnd.Strategies.EMailProcess
{
    public class AlertaChatProcess : AbstractEMailProcess
    {
         public override string GetTypeCode()
        {
            return "ALERTCHATS";
        }

        private IList<EmailAlertChat> GetAlertsChats()
        {
            return IMessengerHome.GetChatsEnEspera();
        }

        protected override void SendMails()
        {
            EmailAlertChatsDTO dto = new EmailAlertChatsDTO();

            IList<EmailAlertChat> alertsChats = GetAlertsChats();
            if (alertsChats == null || alertsChats.Count == 0)
                return;
            else
            {
                dto.ItemsAlerta = GetChatsInfo(alertsChats);
                dto.Total = alertsChats.Sum(x => x.Cantidad);
                dto.To = GetEMails();
                dto.ModuleCode = "IMessenger";
                dto.IdLanguage = 1;
                dto.CountryCode = 540;
                dto.MailFromAppearance = (ConfigurationManager.AppSettings["MailIMessengerFromAppearance"] == null ? "ASSIST CARD" : ConfigurationManager.AppSettings["MailIMessengerFromAppearance"].ToString());
                ServiceLocator.Instance().GetSendMailService().SendMailAlertChats(dto);
            }
        }

        private string GetEMails()
        {
            var usus = EMailListHome.FindUsersMailList(-1, "CHATSWAIT");
            string emails = "";

            foreach (var emailUsu in usus)
            {
                if (emails != "") emails += ",";
                emails += emailUsu.CorreoElectronico;
            }
            return emails;
        }

        protected override void SendMails(int id)
        {
            throw new NotImplementedException();
        }

        private string GetChatsInfo(IList<EmailAlertChat> alertsChats)
        {
            string itemsData = "";
            foreach (var pciaccount in alertsChats)
            {
                itemsData += EmailAlertChatsDTO.GetInfoMail(pciaccount);
            }
            return itemsData;
        }   
    }
}
