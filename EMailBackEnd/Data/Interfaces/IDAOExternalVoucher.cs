using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain.External;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOExternalVoucher
    {
        IList<ExternalVoucher> Find(int countryCode, DateTime fromDate, DateTime toDate);
    }
}
