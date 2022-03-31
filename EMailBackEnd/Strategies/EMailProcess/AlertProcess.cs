using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Properties;
using AssistCard.ServerMSG.Message;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.BackEnd.Strategies.EMailProcess
{
    public class AlertProcess : AbstractEMailProcess
    {
        public override string GetTypeCode()
        {
            return "Alert";
        }

        protected override void SendMails()
        {
            EMailLog log = DAOLocator.Instance().GetDaoEMailLog().GetLastByProcessStatus(EMailLog.OK);
            
            int maxTimeAlert = ConfigurationValueHome.GetMaxTimeAlert();

            double minutes = DateTime.Now.Subtract(log.Fecha).TotalMinutes;

            if (maxTimeAlert > 0 && (log.Id == 0 ||  minutes >= maxTimeAlert))
            {
                string mailsTo  = GetEMails();
                Messaging.SendMailThread(mailsTo, "", 
                    "ATENCION: Envio mails", "Hacen " + ((int)minutes) + " minutos que no se envian mails desde EMailAdmin. Por favor verifique su funcionamiento", 
                    new List<System.Net.Mail.Attachment>());
            }
        }

        private string GetEMails()
        {
            var usus = EMailListHome.FindUsersMailList(-1, "AlertMails");
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
            SendMails();
        }
    }
}
