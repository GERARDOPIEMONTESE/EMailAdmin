using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain.External;

namespace EMailAdmin.ExternalServices.Service.Interface
{
    public interface IExternalPrepurchasePaxService
    {
        PrepurchasePaxInformation Get(int codeBoxPax);
        PrepurchasePaxInformation Get(int codeBoxPax, string group, int countryCode);
    }
}
