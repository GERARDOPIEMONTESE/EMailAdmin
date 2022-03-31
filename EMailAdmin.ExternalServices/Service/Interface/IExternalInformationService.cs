using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.ExternalServices.Domain;

namespace EMailAdmin.ExternalServices.Service.Interface
{
    public interface IExternalInformationService
    {
        IssuanceInformation GetIssuanceInformation(int countryCode, string voucherCode);
    }
}
