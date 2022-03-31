using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Home
{
    public class EMailListExcludeHome
    {
        public static IList<EMailListExclude> FindByFilters(int countryCode = -1, string AccountCode = "", int Branch = -1)
        {
            return DAOLocator.Instance().GetDaoEMailListExclude().GetByFilters(AccountCode, Branch, countryCode);
        }

        public static EMailListExclude Get(int countryCode, string AccountCode, int Branch)
        {
            return DAOLocator.Instance().GetDaoEMailListExclude().GetByAccount(AccountCode, Branch, countryCode);
        }

        public static bool IsExclude(int countryCode, string AccountCode, int Branch)
        {
            EMailListExclude account = DAOLocator.Instance().GetDaoEMailListExclude().GetExcludeAccount(AccountCode, Branch, countryCode);
            return (account != null && account.Id > 0);
        }

        public static EMailListExclude Get(int idExclude)
        {
            return DAOLocator.Instance().GetDaoEMailListExclude().Get(idExclude);
        }
    }
}
