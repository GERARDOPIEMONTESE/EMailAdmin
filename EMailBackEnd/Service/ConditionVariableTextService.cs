using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Service.Interfaces;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Exceptions;

namespace EMailAdmin.BackEnd.Service
{
    public class ConditionVariableTextService : IConditionVariableTextService
    {
        public IDAOConditionVariableText DaoConditionVariableText { get; set; }

        #region IConditionVariableTextService Members

        public void Save(Domain.ConditionVariableText conditionVariableText)
        {
            if (conditionVariableText.Name != "" && conditionVariableText.VariablesText.Count > 0 && conditionVariableText.Contents.Count > 0 && conditionVariableText.VariablesText.Count > 0)
            {
                DaoConditionVariableText.Persistir(conditionVariableText);
            }
            else
            {
                throw new NonSavedObjectException("ConditionVariableText not saved");
            }
        }

        public void Delete(Domain.ConditionVariableText conditionVariableText)
        {
            if (conditionVariableText.Id != 0)
            {
                DaoConditionVariableText.Eliminar(conditionVariableText);
            }
            else
            {
                throw new NonEliminatedObjectException("ConditionVariableText not deleted");
            }
        }

        #endregion
    }
}
