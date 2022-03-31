using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Home
{
    public class EMailContactTypeHome
    {
        public static IList<EMailContactType> FindAll()
        {
            return DAOLocator.Instance().GetDaoEMailContactType().FindAll();
        }

        public static IList<EMailContactType> FindByFilters(string description)
        {
            return DAOLocator.Instance().GetDaoEMailContactType().FindByFilters(description);
        }
        
        public static IList<EMailContactType> Find(string code)
        {
            return DAOLocator.Instance().GetDaoEMailContactType().Find(code);
        }

        public static EMailContactType Get(int id)
        {
            return DAOLocator.Instance().GetDaoEMailContactType().Get(id);
        }
    }
}
