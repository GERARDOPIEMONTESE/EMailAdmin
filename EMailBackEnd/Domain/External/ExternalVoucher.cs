using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain.External
{
    public class ExternalVoucher : ObjetoPersistido
    {
        private const string NAME = "Voucher";

        #region Attributes

        public int CountryCode { get; set; }

        public string Code { get; set; }

        #endregion

        public override string ObtenerNombre()
        {
            return NAME;
        }
    }
}
