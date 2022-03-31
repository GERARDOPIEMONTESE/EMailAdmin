using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Service.Interfaces
{
    public interface IGroupService
    {
        void Save(Group group);
        void Delete(Group group);
    }
}