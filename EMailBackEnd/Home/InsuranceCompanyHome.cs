using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Home
{
    public class InsuranceCompanyHome
    {
        public static InsuranceCompany Get(int countryCode, string productCode)
        {
            return DAOLocator.Instance().GetDaoInsuranceCompany().Get(countryCode, productCode);
        }
    }
}
