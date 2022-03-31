using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Service.Interfaces
{
    public interface IConditionVariableTextService
    {
        void Save(ConditionVariableText conditionVariableText);
        void Delete(ConditionVariableText conditionVariableText);
    }
}
