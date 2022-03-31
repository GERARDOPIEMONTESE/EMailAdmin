using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using System.Transactions;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOCountryVisibleText_R_CountryVisibleTextType : IDAOObjetoNegocio
    {
        IList<CountryVisibleText_R_CountryVisibleTextType> FindByCountryVisibleTextId(int idCountryVisibleText);
        
        void DeleteByIdCountryVisibleText(int idCountryVisibleText);

        void DeleteByIdCountryVisibleText(int idCountryVisibleText, TransactionScope ts);
    }
}
