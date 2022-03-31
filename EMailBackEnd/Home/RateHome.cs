using System.Collections.Generic;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Home
{
    public class RateHome
    {
        public static IList<Rate> FindAllByCountryAndProduct(string countryCode, int idProduct)
        {
            return DAOLocator.Instance().GetDaoRate().FindAllByCountryAndProduct(countryCode, idProduct);
        }

        public static Rate GetByProductCode(int idProduct, string code)
        {
            return DAOLocator.Instance().GetDaoRate().GetByProductCode(idProduct, code);
        }
    }
}