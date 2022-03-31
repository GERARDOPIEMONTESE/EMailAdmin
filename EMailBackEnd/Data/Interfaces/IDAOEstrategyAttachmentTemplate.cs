using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOEstrategyAttachmentTemplate : IDAOObjetoNegocio
    {
        IList<EstrategyAttachmentTemplate> FindAttachmentTemplates(int IdTemplate, int IdAttachment);
    }
}
