using System.Collections.Generic;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;
using System.Transactions;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOCountryVisibleTextContent : IDAOObjetoNegocio
    {
        IList<CountryVisibleTextContent> GetByIdCountryVisibleText(int idCountryVisibleText);

        CountryVisibleTextContent Get(int id);

        void DeleteByIdCountryVisibleText(int idCountryVisibleText);

        void DeleteByIdCountryVisibleText(int idCountryVisibleText, TransactionScope ts);
    }
}
