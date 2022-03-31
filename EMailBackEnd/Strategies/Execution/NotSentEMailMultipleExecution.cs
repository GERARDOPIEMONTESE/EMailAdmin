using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain.External;
using EMailAdmin.ExternalServices.Data;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Service;

namespace EMailAdmin.BackEnd.Strategies.Execution
{
    public class NotSentEMailMultipleExecution
    {
        #region Attributes

        public int CountryCode { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string ModuleCode { get; set; }

        #endregion

        public void ExecuteSendMails()
        {
            IList<ExternalVoucher> vouchers = ExternalDAOLocator.Instance().GetDatoExternalVoucher().
                Find(CountryCode, FromDate, ToDate);

            foreach (ExternalVoucher voucher in vouchers)
            {
                EMailEkitDTO dto = new EMailEkitDTO();
                dto.CountryCode = voucher.CountryCode;
                dto.VoucherCode = voucher.Code.Trim();
                dto.ModuleCode = ModuleCode.Trim();

                ServiceLocator.Instance().GetSendMailService().SendMailEkit(dto);
            }
        }
    }
}
