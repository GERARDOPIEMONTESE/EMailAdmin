using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Home
{
    public class EMailAddressHome
    {
        public static IList<EMailAddress> FindAll()
        {
            return DAOLocator.Instance().GetDaoEMailAddress().FindAll();
        }

        public static EMailAddress Get(int id)
        {
            return DAOLocator.Instance().GetDaoEMailAddress().Get(id);
        }

        public static IList<EMailAddress> FindByFilters(string name, string address)
        {
            return DAOLocator.Instance().GetDaoEMailAddress().FindByFilters(name, address);
        }
    }
}
