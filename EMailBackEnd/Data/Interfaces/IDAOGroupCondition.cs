using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using System.Collections.Generic;
using System.Transactions;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOGroupCondition : IDAOObjetoNegocio
    {
        GroupCondition Get(int id);
        IList<GroupCondition> Find(int idGroup);
        IList<GroupCondition> Find(int idGroup, bool complete);
        IList<GroupCondition> FindWithValues(int idGroup);
        IList<GroupCondition> FindWithValues(int idGroup, bool complete);
        void DeleteAll(int idGroup, int idUser, TransactionScope ts);
    }
}
