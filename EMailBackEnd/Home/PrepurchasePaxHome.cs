using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Home
{
    public class PrepurchasePaxHome
    {
        public static IList<EmailLog_R_PrepurchasePax> FindAllByCodigoVerif(int codigoPaxBox ,string codigoVerif, string groupVoucher, int CountryCode)
        {
            return DAOLocator.Instance().GetDaoEmailLog_R_PrepurchasePax().Find(codigoPaxBox ,codigoVerif, groupVoucher, CountryCode);
        }
    }
}
