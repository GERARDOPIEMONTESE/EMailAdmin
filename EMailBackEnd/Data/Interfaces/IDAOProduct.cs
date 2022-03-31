using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOProduct
    {
        IList<Product> FindAllByCountry(string countryCode);

        IList<Product> FindAllUpgradesByCountry(string countryCode);

        Product Get(string countryCode, string code, int idType);

        Product Get(int id);
    }
}