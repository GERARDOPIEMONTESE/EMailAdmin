using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Service.Interfaces;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain.Information;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Service.Information
{
    public class PaxPassedAwayService : IPaxPassedAwayService
    {
        public IDAOPaxPassedAway DaoPaxPassedAway { get; set; }

        public void Save(int countryCode, string voucherCode, string nationalId, bool isDead)
        {
            PaxPassedAway paxPassedAway = DAOLocator.Instance().GetDaoPaxPassedAway().
                Get(countryCode, voucherCode, nationalId);

            paxPassedAway.CountryCode = countryCode;
            paxPassedAway.VoucherCode = voucherCode;
            paxPassedAway.NationalId = nationalId;
            paxPassedAway.IsDead = isDead;

            DAOLocator.Instance().GetDaoPaxPassedAway().Persistir(paxPassedAway);
        }

        public PaxPassedAway Get(int countryCode, string voucherCode, string nationalId)
        {
            return DAOLocator.Instance().GetDaoPaxPassedAway().Get(countryCode, voucherCode, nationalId);
        }
    }
}
