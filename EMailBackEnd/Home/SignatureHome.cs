using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Home
{
    public class SignatureHome
    {
        #region Static methods used by front-end

        public static IList<Signature> FindAll()
        {
            return DAOLocator.Instance().GetDaoSignature().FindAll();
        }

        public static IList<Signature> FindByFilters(int idType, int idCountry, string name)
        {
            return DAOLocator.Instance().GetDaoSignature().FindByFilters(idType, idCountry, name);
        }

        public static IList<Signature> FindByName(string name)
        {
            return DAOLocator.Instance().GetDaoSignature().FindByName(name);
        }

        public static Signature Get(int id)
        {
            return DAOLocator.Instance().GetDaoSignature().Get(id);
        }

        #endregion
    }
}
