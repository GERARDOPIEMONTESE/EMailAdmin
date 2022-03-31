using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOGroupType
    {
        GroupType Get(int id);

        GroupType GetByCode(string code);

        IList<GroupType> FindAll();
    }
}
