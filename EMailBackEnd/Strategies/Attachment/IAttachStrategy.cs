using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.DTO;

namespace EMailAdmin.BackEnd.Strategies.Attachment
{
    public interface IAttachStrategy
    {
        IList<AttachmentItem> GetAttachmentItems(AbstractEMailDTO dto);
    }
}
