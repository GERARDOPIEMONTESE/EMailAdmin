using System.Collections.Generic;
using CapaNegocioDatos.CapaHome;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Home
{
    public class ProductHome
    {
        #region Static methods used by front-end

        public static IList<Product> FindAllByCountry(string countryCode)
        {
            return DAOLocator.Instance().GetDaoProduct().FindAllByCountry(countryCode);
        }

        public static Product Get(int id)
        {
            return DAOLocator.Instance().GetDaoProduct().Get(id);
        }

        public static Product Get(string countryCode, string code, int idType)
        {
            return DAOLocator.Instance().GetDaoProduct().Get(countryCode, code, idType);
        }

        public static Product Get(string code, int idCountry, int idType)
        {
            return Get(PaisHome.Obtener(idCountry).Codigo, code,idType);
        }

        public static IList<Product> FindAllUpgradesByCountry(string countryCode)
        {
            return DAOLocator.Instance().GetDaoProduct().FindAllUpgradesByCountry(countryCode);
        }

        #endregion
    }
}