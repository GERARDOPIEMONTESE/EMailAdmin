using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMailAdmin.BackEnd.DTO
{
    public class BalanceRequestDTO : AbstractEMailDTO
    {
        public string agencia { get; set; }
        public string sucursal { get; set; }
        public int pais { get; set; }
        
    }
}
