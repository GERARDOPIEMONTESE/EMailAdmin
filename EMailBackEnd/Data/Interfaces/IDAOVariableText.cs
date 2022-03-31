using System.Collections.Generic;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOVariableText : IDAOObjetoNegocio
    {
        VariableText Get(int id);

        VariableText Get(string name);

        IList<VariableText> FindAll();

        IList<VariableText> FindByType(int idType);

        IList<VariableText> FindByType(string codeType);
    }
}
