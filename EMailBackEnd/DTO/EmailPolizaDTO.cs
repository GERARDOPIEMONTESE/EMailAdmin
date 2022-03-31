using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Properties;

namespace EMailAdmin.BackEnd.DTO
{
    public class EmailPolizaDTO : EMailEkitDTO
    {
        public bool Cancelacion { get; set; } //para saber de que tipo de template es

        public string VoucherCodeVoid { get; set; }

        public string PolizaId { get; set; }

        public string polizaVoidId { get; set; }
    }
}
