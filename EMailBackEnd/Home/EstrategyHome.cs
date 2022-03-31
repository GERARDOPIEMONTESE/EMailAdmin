using System.Collections.Generic;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Home
{
    public class EstrategyHome
    {
        #region Static methods used by front-end

        public static IList<Estrategy> FindAll()
        {
            return DAOLocator.Instance().GetDaoEstrategy().FindAll();
        }

        public static Estrategy Get(int id)
        {
            return DAOLocator.Instance().GetDaoEstrategy().Get(id);
        }

        public static Estrategy GetByCode(string code)
        {
            return DAOLocator.Instance().GetDaoEstrategy().GetByCode(code);
        }

        internal static Estrategy GetByClass(string className)
        {
            return DAOLocator.Instance().GetDaoEstrategy().GetByClass(className);
        }
        #endregion
    }
}
