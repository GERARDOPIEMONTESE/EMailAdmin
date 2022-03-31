using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Home
{
    public class EMailListTypeHome
    {
        public static IList<EMailListType> FindAll()
        {
            return DAOLocator.Instance().GetDaoEMailListType().FindAll();
        }

        public static IList<EMailListType> FindByFilters(string description)
        {
            return DAOLocator.Instance().GetDaoEMailListType().FindByFilters(description);
        }

        public static IList<EMailListType> Find(string code)
        {
            return DAOLocator.Instance().GetDaoEMailListType().Find(code);
        }

        public static EMailListType Get(int id)
        {
            return DAOLocator.Instance().GetDaoEMailListType().Get(id);
        }
    }
}
