using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain.Information;

namespace EMailAdmin.BackEnd.Service.Interfaces
{
    public interface IPaxPassedAwayService
    {
        void Save(int countryCode, string voucherCode, string nationalId, bool isDead);

        PaxPassedAway Get(int countryCode, string voucherCode, string nationalId);
    }
}
