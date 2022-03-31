using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Service.Interfaces;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Exceptions;

namespace EMailAdmin.BackEnd.Service
{
    public class ContentAttachmentService : IContentAttachmentService
    {
        public IDAOContentAttachment DaoContentAttachment { get; set; }

        public void Save(Domain.ContentAttachment contentAttachment)
        {
            if (contentAttachment.Body != "")
            {
                DaoContentAttachment.Persistir(contentAttachment);
            }
            else
            {
                throw new NonSavedObjectException("Content Attachment not saved");
            }
        }

        public void Delete(Domain.ContentAttachment contentAttachment)
        {
            if (contentAttachment.Id != 0)
            {
                DaoContentAttachment.Eliminar(contentAttachment);
            }
            else
            {
                throw new NonEliminatedObjectException("Content Attachment not deleted");
            }
        }
    }
}
