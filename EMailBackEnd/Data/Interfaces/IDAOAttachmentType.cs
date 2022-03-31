using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOAttachmentType
    {
        AttachmentType Get(int id);

        AttachmentType GetByCode(string code);

        IList<AttachmentType> FindAll();
    }
}
