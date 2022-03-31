using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Home
{
    public class EMailProcessTypeHome
    {
        public static EMailProcessType Get(int id)
        {
            return DAOLocator.Instance().GetDaoEMailProcessType().Get(id);
        }

        public static EMailProcessType Get(string code)
        {
            return DAOLocator.Instance().GetDaoEMailProcessType().Get(code);
        }

        public static IList<EMailProcessType> FindAll()
        {
            return DAOLocator.Instance().GetDaoEMailProcessType().FindAll();
        }
    }
}
