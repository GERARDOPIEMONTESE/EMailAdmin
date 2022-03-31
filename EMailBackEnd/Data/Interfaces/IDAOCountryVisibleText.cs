using System.Collections.Generic;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOCountryVisibleText : IDAOObjetoNegocio
    {
        IList<CountryVisibleText> FindAll();

        IList<CountryVisibleText> FindByFilters(int idType, int idCountry, string name);

        IList<CountryVisibleText> FindByFilters(int idType, int idCountry);

        IList<CountryVisibleText> FindByName(string name);

        CountryVisibleText Get(int id);
    }
}
