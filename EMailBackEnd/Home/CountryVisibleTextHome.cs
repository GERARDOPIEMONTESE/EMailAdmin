using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Home
{
    public class CountryVisibleTextHome
    {
        #region Static methods used by front-end

        public static IList<CountryVisibleText> FindAll()
        {
            return DAOLocator.Instance().GetDaoCountryVisibleText().FindAll();
        }

        public static IList<CountryVisibleText> FindByFilters(int idType, int idCountry, string name)
        {
            return DAOLocator.Instance().GetDaoCountryVisibleText().FindByFilters(idType, idCountry, name);
        }

        public static IList<CountryVisibleText> FindByName(string name)
        {
            return DAOLocator.Instance().GetDaoCountryVisibleText().FindByName(name);
        }

        public static CountryVisibleText Get(int id)
        {
            return DAOLocator.Instance().GetDaoCountryVisibleText().Get(id);
        }

        #endregion
    }
}
