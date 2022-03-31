using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Service.Interfaces
{
    public interface IAttachment_R_GroupService
    {
        void Delete(Attachment_R_Group groupRTemplate);
        void Delete(int idGroupRTemplate);
    }
}