using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Home
{
    public class EMailLogHome
    {
        public static IList<EMailLog> FindAllPendings()
        {
            return DAOLocator.Instance().GetDaoEMailLog().FindAllPendings();
        }

        public static IList<EMailLog> FindAllPendings(string voucherCode, int idLocation, int idStatus, string fromDate, string toDate)
        {
            return DAOLocator.Instance().GetDaoEMailLog().FindAllPendings(voucherCode, idLocation, idStatus, fromDate, toDate);
        }

        public static IList<EMailLog> Get(int id)
        {
            return DAOLocator.Instance().GetDaoEMailLog().Find(id);
        }

        public static EMailLog Obtener(int id)
        {
            return DAOLocator.Instance().GetDaoEMailLog().Obtener(id);
        }

        public static bool IsVoucherValid(string countryCode, string voucherCode)
        {
            return DAOLocator.Instance().GetDaoEMailLog().IsVoucherValid(countryCode, voucherCode);    
        }
    }

}
