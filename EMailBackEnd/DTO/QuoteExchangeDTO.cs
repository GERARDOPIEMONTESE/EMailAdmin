using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMailAdmin.BackEnd.DTO
{
    public class QuoteExchangeDTO : AbstractEMailDTO
    {
        public string HtmlBody { set; get; }
        public string DolarExchange { set; get; }
        public string EuroExchange { set; get; }
    }
}
