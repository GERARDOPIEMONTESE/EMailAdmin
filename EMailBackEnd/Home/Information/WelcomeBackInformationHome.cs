using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain.Information;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Home.Information
{
    public class WelcomeBackInformationHome
    {
        public static IList<WelcomeBackInformation> FindEffectiveEndDate()
        {
            return DAOLocator.Instance().GetDaoWelcomeBackInformation().FindEffectiveEndDate();
        }
    }
}
