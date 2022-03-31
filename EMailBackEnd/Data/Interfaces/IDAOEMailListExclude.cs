using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;


namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOEMailListExclude : IDAOObjetoNegocio
    {
        IList<EMailListExclude> FindAll();

        EMailListExclude Get(int id);

        EMailListExclude GetByAccount(string codeAccount, int Branch, int CountryCode);

        EMailListExclude GetExcludeAccount(string codeAccount, int Branch, int CountryCode);

        IList<EMailListExclude> GetByFilters(string AccountCode, int Branch, int countryCode);
    }
}
