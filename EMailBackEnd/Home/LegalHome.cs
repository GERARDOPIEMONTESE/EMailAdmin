using System.Collections.Generic;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Home
{
    public class LegalHome
    {
        public static IList<Legal> Find(int countryCode, string voucherCode, string email, string templateName)
        {
            return DAOLocator.Instance().GetDaoLegal().Find(countryCode, voucherCode, email, templateName);
        }
    }
}
