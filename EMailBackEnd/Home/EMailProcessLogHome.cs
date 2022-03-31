using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Home
{
    public class EMailProcessLogHome
    {
        public static EMailProcessLog Get(int id)
        {
            return DAOLocator.Instance().GetDaoEMailProcessLog().Get(id);
        }

        public static EMailProcessLog GetLastLog(int idEMailProcessType)
        {
            return DAOLocator.Instance().GetDaoEMailProcessLog().GetLastLog(idEMailProcessType);
        }

        public static IList<EMailProcessLog> Find(DateTime fromDate, DateTime toDate)
        {
            return DAOLocator.Instance().GetDaoEMailProcessLog().Find(fromDate, toDate);
        }
    }
}
