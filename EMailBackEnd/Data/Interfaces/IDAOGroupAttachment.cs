using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOGroupAttachment : IDAOObjetoNegocio
    {
        IList<GroupAttachment> Find(GroupAttachment filter = null);
        GroupAttachment Obtener(int Id);
        bool CanDelete(int Id, out string _InUseTemplates);
    }

    public interface IDAOGroupAttachmentValid
    {
        IList<GroupAttachmentUseTemplate> InUseTemplates(int IdGroupAttachment);
    }
}
