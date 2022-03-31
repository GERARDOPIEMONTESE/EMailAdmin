using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOIMessengerAlerts
    {
        IList<EmailAlertChat> GetAlertaChatsEspera();
    }
}
