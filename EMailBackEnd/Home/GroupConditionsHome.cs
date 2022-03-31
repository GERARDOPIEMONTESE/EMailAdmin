using System.Collections.Generic;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Domain;
using System;

namespace EMailAdmin.BackEnd.Home
{
    public class GroupConditionsHome
    {
        public static IList<GroupCondition> FindByIdGroup(int idGroup)
        {
            return DAOLocator.Instance().GetDaoGroupCondition().Find(idGroup, true);
        }

        public static IList<GroupCondition> FindByIdGroup(int idGroup, bool complete)
        {
            return DAOLocator.Instance().GetDaoGroupCondition().Find(idGroup, complete);
        }

        public static IList<GroupCondition> FindByIdGroupWithValues(int idGroup)
        {
            return DAOLocator.Instance().GetDaoGroupCondition().FindWithValues(idGroup, true);
        }

        public static IList<GroupCondition> FindByIdGroupWithValues(int idGroup, bool complete)
        {
            return DAOLocator.Instance().GetDaoGroupCondition().FindWithValues(idGroup, complete);
        }

        public static IList<ReportAssociation> Find(int idTemplateType, string groupName, int idLocacion, string accountCode, int idProduct,
                                string rateCode, DateTime effectiveDate, int idGroupType, int asociados)
        {
            return DAOLocator.Instance().GetDaoGroup_R_Template_Association().Find(idTemplateType, groupName, idLocacion, accountCode, idProduct, rateCode, effectiveDate, idGroupType, asociados);
        }

        public static GroupCondition Get(int id)
        {
            return DAOLocator.Instance().GetDaoGroupCondition().Get(id);
        }
    }
}
