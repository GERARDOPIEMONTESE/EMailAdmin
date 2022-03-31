using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EmailLogRegister" in code, svc and config file together.
    public class EmailLogRegister : IEmailLogRegister
    {
        public void RegisterEMailReception(int countryCode, string voucherCode, int idTemplateType)
        {
            TemplateType templateType = TemplateTypeHome.Get(idTemplateType);
            var received = new ReceivedConditions
            {
                CountryCode = countryCode,
                VoucherCode = voucherCode,
                IdTemplate = templateType.Id
            };

            received.Persistir();
        }
    }
}
