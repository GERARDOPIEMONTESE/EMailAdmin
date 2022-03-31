using System.Collections.Generic;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOTableVariableText : IDAOObjetoNegocio
    {        
        TableVariableText Get(string name);
        TableVariableText Get(int id);
        IList<TableVariableText> FindAll();
    }
}
