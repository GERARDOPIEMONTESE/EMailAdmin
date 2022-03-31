using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOCountryVisibleTextType : IDAOObjetoNegocio
    {
        CountryVisibleTextType Get(int id);

        CountryVisibleTextType GetByCode(string code);

        IList<CountryVisibleTextType> FindAll();

        IList<CountryVisibleTextType> FindByFilters(string description);
    }
}
