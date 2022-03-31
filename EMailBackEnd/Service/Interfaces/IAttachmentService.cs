using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.DTO;
using System.Collections.Generic;

namespace EMailAdmin.BackEnd.Service.Interfaces
{
    public interface IAttachmentService
    {

        void Save(Attachment attachment);
        void Delete(Attachment attachment);
        void SaveAssociations(IList<Attachment_R_Group> items);
        IList<AttachmentItem> GetItems(Attachment attachment, AbstractEMailDTO dto);
        Template GetTemplateAttach(int IdAttachment, AbstractEMailDTO dto, int IdMainTemplate);
        Template GetTemplateAttach(int IdAttachment, AbstractEMailDTO dto, Template MainTemplate);
    }
}
