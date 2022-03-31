using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Home
{
    public class LinkHome
    {
        public static Link Get(int id)
        {
            return DAOLocator.Instance().GetDaoLink().Get(id);
        }

        public static Link Get(string name)
        {
            return DAOLocator.Instance().GetDaoLink().Get(name);
        }

        public static IList<Link> BuscarLinks(string nombre, string url)
        {
            return DAOLocator.Instance().GetDaoLinkSearch().BuscarLinks(nombre, url);
        }

        public static IList<Link> LinksFixed()
        {
            return DAOLocator.Instance().GetDaoLinkSearch().LinksFixed();
        }

        public static IList<Link> LinksFixed(string linkType)
        {
            return DAOLocator.Instance().GetDaoLinkSearch().LinksFixed(linkType);
        }
    }
}
