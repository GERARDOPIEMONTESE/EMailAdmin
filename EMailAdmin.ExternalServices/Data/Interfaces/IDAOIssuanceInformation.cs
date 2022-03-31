using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.ExternalServices.Domain;

namespace EMailAdmin.ExternalServices.Data.Interfaces
{
    public interface IDAOIssuanceInformation
    {
        IssuanceInformation Get(int countryCode, string voucherCode);
    }
}
