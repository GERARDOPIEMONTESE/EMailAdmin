using System.Collections.Generic;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Home
{
    public class CountryVisibleTextTypeHome
    {
        #region Static methods used by front-end

        public static IList<CountryVisibleTextType> FindAll()
        {
            return DAOLocator.Instance().GetDaoCountryVisibleTextType().FindAll();
        }

        public static CountryVisibleTextType Get(int id)
        {
            return DAOLocator.Instance().GetDaoCountryVisibleTextType().Get(id);
        }

        public static CountryVisibleTextType GetByCode(string code)
        {
            return DAOLocator.Instance().GetDaoCountryVisibleTextType().GetByCode(code);
        }

        public static IList<CountryVisibleTextType> FindByFilters(string description)
        {
            return DAOLocator.Instance().GetDaoCountryVisibleTextType().FindByFilters(description);
        } 

        #endregion
    }
}
