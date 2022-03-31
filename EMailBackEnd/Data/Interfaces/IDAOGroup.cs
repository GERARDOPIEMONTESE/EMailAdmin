using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOGroup : IDAOObjetoNegocio
    {
        IList<Group> FindAll();

        Group Get(int id);

        Group Get(int id, bool lazy);

        IList<Group> FindByName(string name);
                
        IList<Group> FindByFilters(string name, int idGroupType);

        IList<Group> FindByFilters(string name, int idGroupType, bool lazy);
        
        Group FindByFilters(int idGroup);
        
        IList<Group> FindByGroupType(int idGroupType);

        IList<Group> FindByGroupType(int idGroupType, bool lazy);

        bool CanDelete(int id);

        bool Exist(string name);
    }
}