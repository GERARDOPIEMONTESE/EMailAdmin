using System.Collections.Generic;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Home
{
    public class SignatureTypeHome
    {
        #region Static methods used by front-end

        public static IList<SignatureType> FindAll()
        {
            return DAOLocator.Instance().GetDaoSignatureType().FindAll();
        }

        public static SignatureType Get(int id)
        {
            return DAOLocator.Instance().GetDaoSignatureType().Get(id);
        }

        public static SignatureType GetByCode(string code)
        {
            return DAOLocator.Instance().GetDaoSignatureType().GetByCode(code);
        }

        public static IList<SignatureType> FindByFilters(string description)
        {
            return DAOLocator.Instance().GetDaoSignatureType().FindByFilters(description);
        } 

        #endregion
    }
}
