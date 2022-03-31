using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using System.Transactions;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOAttachment_R_Group : IDAOObjetoNegocio
    {
        IList<Attachment_R_Group> FindByGroupId(int idGroup);
        IList<Attachment_R_Group> FindByAttachmentId(int idAttachment);
        void DeleteByIdAttachment(int idAttachment);
        void DeleteByIdAttachment(int idAttachment, TransactionScope ts);
    }
}