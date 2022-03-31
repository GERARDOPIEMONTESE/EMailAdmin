using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using System.Transactions;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOAttachmentItem : IDAOObjetoNegocio
    {
        IList<AttachmentItem> FindByAttachmentId(int idAttachment);
        void DeleteByAttachmentId(int idAttachment);
        void DeleteByAttachmentId(int idAttachment, TransactionScope ts);
    }
}
