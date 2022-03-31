using EmailAdmin.Dto;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailAdmin.Api
{
    public class VoucherSender
    {
        public SendVouchersDTO dtoSendVouchers { get; set; }

        public void ExecuteSendDynamicMail()
        {
            foreach (var item in dtoSendVouchers.vouchers)
            {
                EMailEkitDTO dto = new EMailEkitDTO();
                dto.CountryCode = item.pais;
                dto.VoucherCode = item.voucher;
                dto.ModuleCode = dtoSendVouchers.moduleCode;
                if (!string.IsNullOrEmpty(dtoSendVouchers.EmailTo))
                {
                    dto.GivenToAddress = true;
                    dto.To = dtoSendVouchers.EmailTo;
                }
                ServiceLocator.Instance().GetSendMailService().SendMailEkit(dto);
            }
        }
    }
}