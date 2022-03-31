using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMailAdmin.BackEnd.Service.Interfaces
{
    public interface IEMailSenderPrepPaxService
    {
        void ForwardEmailBuy(int boxPaxCode);
        void ForwardEmailBalance(int boxPaxCode, string group, int countryCode);
        void ForwardEmailCancel(int boxPaxCode, string group, int countryCode);
    }
}
