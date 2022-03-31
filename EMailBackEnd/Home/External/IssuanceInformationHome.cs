using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.ExternalServices.Domain;
using EMailAdmin.ExternalServices.Data;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Home.External
{
    public class IssuanceInformationHome
    {
        public static IList<IssuanceInformation> FindEffectiveEndDate(int countryCode)
        {
            return ExternalDAOLocator.Instance().GetDaoIssuanceInformation().
                FindEffectiveEndDate(countryCode);
        }

        public static IList<IssuanceInformation> FindEffectiveStartDate(int countryCode, int daysBefore)
        {
            return ExternalDAOLocator.Instance().GetDaoIssuanceInformation().
                FindEffectiveStartDate(countryCode, daysBefore);
        }

        internal static IList<Domain.BaseEnvio> Find()
        {
            return DAOLocator.Instance().GetDAONiceTrip().Find();
        }
    }
}
