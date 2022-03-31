using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Service.Interfaces
{
    public interface IGroup_R_TemplateService
    {
        void Save(Group_R_Template groupRTemplate);
        void Delete(Group_R_Template groupRTemplate);
        void Delete(int idGroupRTemplate);
    }
}
