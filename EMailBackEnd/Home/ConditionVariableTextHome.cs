using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Home
{
    public class ConditionVariableTextHome
    {
        #region Static methods used by front-end

        public static IList<ConditionVariableText> FindAll()
        {
            return DAOLocator.Instance().GetDaoConditionVariableText().FindAll();
        }

        public static IList<ConditionVariableText> Find(int IdVariableText, string Name, string Condicion)
        {
            return DAOLocator.Instance().GetDaoConditionVariableText().Find(IdVariableText, Name, Condicion);
        }

        public static ConditionVariableText FindByName(string Name, bool EqualName = false)
        {
            return DAOLocator.Instance().GetDaoConditionVariableText().FindByName(Name, EqualName);
        }
        
        public static ConditionVariableText Get(int id)
        {
            return DAOLocator.Instance().GetDaoConditionVariableText().Get(id);
        }

        #endregion
    }
}
