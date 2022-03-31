using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMailAdmin.BackEnd.DTO
{
    public class ContinuaCompraDTO : AbstractEMailDTO
    {
        public int IdQuoteLog { get; set; }
        public string Code { get; set; }
        public string ISO2Code { get; set; }
    }
}
