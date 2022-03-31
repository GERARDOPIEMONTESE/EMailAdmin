using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOConditionType
    {
        IList<ConditionType> FindAll();

        ConditionType Get(int id);
    }
}