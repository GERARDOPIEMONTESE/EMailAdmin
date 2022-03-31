using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOVariableTextType
    {
        VariableTextType Get(int id);

        VariableTextType GetByCode(string code);

        IList<VariableTextType> FindAll();
    }
}