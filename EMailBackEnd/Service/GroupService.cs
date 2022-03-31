using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Exceptions;
using EMailAdmin.BackEnd.Service.Interfaces;
using EMailAdmin.BackEnd.Data.Interfaces;

namespace EMailAdmin.BackEnd.Service
{
    public class GroupService : IGroupService
    {
        public IDAOGroup DaoGroup { get; set; }

        #region IGroupService Members

        public void Save(Group group)
        {
            if (group.Name != "" && group.Conditions.Count > 0)
            {
                DaoGroup.Persistir(group);
            }
            else
            {
                throw new NonSavedObjectException("Group not saved");
            }
        }

        public void Delete(Group group)
        {
            if (group != null)
            {
                DaoGroup.Eliminar(group);
            }
            else
            {
                throw new NonEliminatedObjectException("Group not deleted");
            }
        }

        #endregion
    }
}