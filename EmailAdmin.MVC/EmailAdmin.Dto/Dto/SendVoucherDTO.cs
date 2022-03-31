using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailAdmin.Dto
{
    public class SendVouchersDTO
    {
        public string moduleCode { get; set; }
        public List<VoucherDTO> vouchers { get; set; }
        public string EmailTo { get; set; }
        public string user { get; set; }
        public string password { get; set; }
    }

    public class VoucherDTO
    {
        public int pais { get; set; }
        public string voucher { get; set; }    
    }
}