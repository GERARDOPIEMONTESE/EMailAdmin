using System.Collections.Generic;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Home
{
    public class LinkTypeHome
    {
        public static LinkType Get(int id)
        {
            return DAOLocator.Instance().GetDaoLinkType().Get(id);
        }

        public static LinkType Get(string code)
        {
            return DAOLocator.Instance().GetDaoLinkType().Get(code);
        }
        
        public static IList<LinkType> FindAll()
        {
            return DAOLocator.Instance().GetDaoLinkType().FindAll();
        }
    }
}
