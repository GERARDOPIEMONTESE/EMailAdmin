using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Dato;
using System.Transactions;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOConditionVariableText_R_VariableText : IDAOObjetoNegocio
    {
        IList<ConditionVariableText_R_VariableText> FindByConditionVariableTextId(int idConditionVariableText);

        void DeleteByIdConditionVariableText(int idConditionVariableText);

        void DeleteByIdConditionVariableText(int idConditionVariableText, TransactionScope ts);
    }
}
