using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Home
{
    public class DistributionTypeHome
    {
        public static DistributionType Get(int idDistributionType)
        {
            return DAOLocator.Instance().GetDaoDistributionType().Get(idDistributionType);
        }

        public static DistributionType Get(string code)
        {
            return DAOLocator.Instance().GetDaoDistributionType().Get(code);
        }

        public static IList<DistributionType> FindAll()
        {
            return DAOLocator.Instance().GetDaoDistributionType().FindAll();
        }
    }
}
