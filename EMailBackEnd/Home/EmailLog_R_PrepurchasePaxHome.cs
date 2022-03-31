using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Home
{
    public class EmailLog_R_PrepurchasePaxHome
    {
        public static EmailLog_R_PrepurchasePax GetByCodigoVerif(int CodigoPaxBox, string codigoVerif, int countryCode, string voucherGroup)
        {
            return DAOLocator.Instance().GetDaoEmailLog_R_PrepurchasePax().Find(CodigoPaxBox, codigoVerif, countryCode, voucherGroup);
        }
    }
}
