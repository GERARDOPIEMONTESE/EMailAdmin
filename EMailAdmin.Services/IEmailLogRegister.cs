using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace EMailAdmin.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEmailLogRegister" in both code and config file together.
    [ServiceContract]
    public interface IEmailLogRegister
    {
        [OperationContract]
        void RegisterEMailReception(int countryCode, string voucherCode, int idTemplateType);
    }
}
