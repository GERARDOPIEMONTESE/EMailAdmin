using System.Collections.Generic;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.BackEnd.Strategies.EMailProcess
{
    public class EMailEKitProcess : AbstractEMailProcess
    {
        public override string GetTypeCode()
        {
            return "EKit";
        }

        private IDictionary<int, IList<EMailLog>> GetPendingVouchers(int id)
        {
            IDictionary<int, IList<EMailLog>> vouchers = new Dictionary<int, IList<EMailLog>>();
            IList<EMailLog> pendingLogs = id == -1 ? EMailLogHome.FindAllPendings() : EMailLogHome.Get(id);

            foreach (EMailLog log in pendingLogs)
            {
                if (vouchers.Keys.Contains(log.CountryCode) && vouchers[log.CountryCode] != null)
                {
                    vouchers[log.CountryCode].Add(log);
                }
                else
                {
                    IList<EMailLog> codes = new List<EMailLog>();
                    codes.Add(log);
                    vouchers.Add(log.CountryCode, codes);
                }
            }

            return vouchers;
        }


        protected override void SendMails()
        {
            SendMails(-1);
        }

        protected override void SendMails(int id)
        {
            IDictionary<int, IList<EMailLog>> vouchers = GetPendingVouchers(id);

            foreach (int countryCode in vouchers.Keys)
            {
                if (vouchers[countryCode] != null)
                {
                    foreach (EMailLog log in vouchers[countryCode])
                    {
                        var dto = new EMailEkitDTO();
                        dto.CountryCode = log.CountryCode;
                        dto.VoucherCode = log.VoucherCode;
                        dto.ModuleCode = "ACNET";

                        ServiceLocator.Instance().GetSendMailService().SendMailEkit(dto);
                        log.ProcessStatus = EMailLog.REPROCESSED;
                        ServiceLocator.Instance().GetEMailLogService().SaveLog(log);                        
                    }
                }
            }
        }
    }
}