using System.Collections.Generic;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Home
{
    public class UpgradeVariableTextTypeHome
    {
        #region Static methods used by front-end

        public static IList<UpgradeVariableTextType> FindAll()
        {
            return DAOLocator.Instance().GetDaoUpgradeVariableTextType().FindAll();
        }

        public static UpgradeVariableTextType Get(int id)
        {
            return DAOLocator.Instance().GetDaoUpgradeVariableTextType().Get(id);
        }

        public static UpgradeVariableTextType GetByCode(string code)
        {
            return DAOLocator.Instance().GetDaoUpgradeVariableTextType().GetByCode(code);
        }

        public static IList<UpgradeVariableTextType> FindByFilters(string description)
        {
            return DAOLocator.Instance().GetDaoUpgradeVariableTextType().FindByFilters(description);
        } 

        #endregion
    }
}
