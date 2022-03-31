using System.Collections.Generic;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Home
{
    public class GroupTypeHome
    {
        public static IList<GroupType> FindAll()
        {
            return DAOLocator.Instance().GetDaoGroupType().FindAll();
        }

        public static GroupType Find(string code)
        {
            return DAOLocator.Instance().GetDaoGroupType().GetByCode(code);
        }

        public static GroupType Get(int id)
        {
            return DAOLocator.Instance().GetDaoGroupType().Get(id);
        }

        public static GroupType TemplateGroup()
        {
            return Find(GroupType.TEMPLATEGROUP);
        }

        public static GroupType AttachmentGroup()
        {
            return Find(GroupType.ATTACHMENTGROUP);
        }
    }
}
