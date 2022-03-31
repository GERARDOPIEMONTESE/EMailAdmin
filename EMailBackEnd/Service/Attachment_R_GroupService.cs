using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Service.Interfaces;

namespace EMailAdmin.BackEnd.Service
{
    public class Attachment_R_GroupService : IAttachment_R_GroupService
    {
        public IDAOAttachment_R_Group DaoAttachment_R_Group { get; set; }

        #region IAttachment_R_GroupService Members

        public void Delete(Attachment_R_Group groupRTemplate)
        {
        }

        public void Delete(int idGroupRTemplate)
        {
            
        }

        #endregion
    }
}