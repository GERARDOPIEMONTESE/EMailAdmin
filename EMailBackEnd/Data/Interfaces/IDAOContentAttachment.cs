using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOContentAttachment : IDAOObjetoNegocio
    {
        ContentAttachment Get(int Id);
        IList<ContentAttachment> Find(int IdTemplate, int IdAttachment, int IdLanguage);
        IList<ContentAttachment> Find(int IdTemplate, int IdAttachment, string Type);
    }
}
