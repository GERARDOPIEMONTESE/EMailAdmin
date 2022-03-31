using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Home
{
    public class UpgradeVariableTextHome
    {
        #region Static methods used by front-end

        public static IList<UpgradeVariableText> FindAll()
        {
            return DAOLocator.Instance().GetDaoUpgradeVariableText().FindAll();
        }

        public static IList<UpgradeVariableText> FindByFilters(int idType, int idUpgrade, string name)
        {
            return DAOLocator.Instance().GetDaoUpgradeVariableText().FindByFilters(idType, idUpgrade, name);
        }

        public static IList<UpgradeVariableText> FindByName(string name)
        {
            return DAOLocator.Instance().GetDaoUpgradeVariableText().FindByName(name);
        }

        public static UpgradeVariableText Get(int id)
        {
            return DAOLocator.Instance().GetDaoUpgradeVariableText().Get(id);
        }

        #endregion
    }
}
