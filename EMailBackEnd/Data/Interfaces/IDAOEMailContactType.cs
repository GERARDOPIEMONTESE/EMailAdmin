using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOEMailContactType : IDAOObjetoNegocio
    {
        IList<EMailContactType> FindAll();

        IList<EMailContactType> Find(string code);

        IList<EMailContactType> FindByFilters(string description);

        EMailContactType Get(int id);

        EMailContactType GetByCode(string code);
    }
}
