using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.DTO
{
    public class ACCOMQuoteDTO : AbstractEMailDTO
    {
        public int IdPendingQuoteMail { set; get; }
        public string HtmlBody { get; set; }
        public QuoteMailACCOM QuoteMailACCOM { set; get; }
        public string PurchaseProcessCode { set; get; }
    }

}
