using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOConditionVariableText : IDAOObjetoNegocio
    {
        IList<ConditionVariableText> FindAll();

        IList<ConditionVariableText> Find(int IdVariableText, string name, string condicion);

        ConditionVariableText Get(int id);

        ConditionVariableText FindByName(string Name, bool EqualName = false);
    }
}