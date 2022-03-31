using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;
using System.Transactions;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOConditionVariableTextContent : IDAOObjetoNegocio
    {
        IList<ConditionVariableTextContent> GetByIdConditionVariableText(int idConditionVariableText);

        ConditionVariableTextContent Get(int id);

        void DeleteByIdConditionVariableText(int idConditionVariableText);

        void DeleteByIdConditionVariableText(int idConditionVariableText, TransactionScope ts);
    }
}