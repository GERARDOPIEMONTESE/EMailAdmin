using System.Collections.Generic;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Home
{
    public class GroupHome
    {
        public static IList<Group> FindAll()
        {
            return DAOLocator.Instance().GetDaoGroup().FindAll();
        }

        public static IList<Group> FindByGroupType(int idGroupType, bool lazy)
        {
            return DAOLocator.Instance().GetDaoGroup().FindByGroupType(idGroupType, lazy);
        } 

        public static IList<Group> FindByName(string name)
        {
            return DAOLocator.Instance().GetDaoGroup().FindByName(name);
        }

        public static IList<Group> FindByFilters(string name, int idGroupType)
        {
            return DAOLocator.Instance().GetDaoGroup().FindByFilters(name, idGroupType);
        }

        public static IList<Group> FindByFilters(string name, int idGroupType, bool lazy)
        {
            return DAOLocator.Instance().GetDaoGroup().FindByFilters(name, idGroupType, lazy);
        }

        public static Group FindByFilters(int idGroup)
        {
            return DAOLocator.Instance().GetDaoGroup().FindByFilters(idGroup);
        }

        public static bool CanDelete(int id)
        {
            return DAOLocator.Instance().GetDaoGroup().CanDelete(id);
        }

        public static Group Get(int id)
        {
            return DAOLocator.Instance().GetDaoGroup().Get(id);
        }

        public static bool Exist(string name)
        {
            return DAOLocator.Instance().GetDaoGroup().Exist(name);
        }
    }
}