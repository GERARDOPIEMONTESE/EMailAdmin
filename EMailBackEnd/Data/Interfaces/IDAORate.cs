using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAORate
    {
        IList<Rate> FindAllByCountryAndProduct(string countryCode, int idProduct);

        Rate GetByProductCode(int idProduct, string code);
    }
}
