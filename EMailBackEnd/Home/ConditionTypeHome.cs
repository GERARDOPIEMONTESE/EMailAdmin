using System.Collections.Generic;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Home
{
    public class ConditionTypeHome
    {
        #region Static methods used by front-end

        public static IList<ConditionType> FindAll()
        {
            return DAOLocator.Instance().GetDaoConditionType().FindAll();
        }

        public static ConditionType Get(int id)
        {
            return DAOLocator.Instance().GetDaoConditionType().Get(id);
        }

        #endregion
    }
}