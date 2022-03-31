using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOEMailListType : IDAOObjetoNegocio
    {
        IList<EMailListType> FindAll();

        IList<EMailListType> Find(string code);

        IList<EMailListType> FindByFilters(string description);

        EMailListType Get(int id);

        EMailListType GetByCode(string code);
    }
}
