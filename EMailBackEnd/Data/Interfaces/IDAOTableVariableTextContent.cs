using System.Collections.Generic;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOTableVariableTextContent : IDAOObjetoNegocio
    {
        IList<TableVariableTextContent> GetByIdTableVariableText(int idTableVariableText);
    }
}
