using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain.Information
{
    public class WelcomeBackInformation : ObjetoPersistido
    {
        private const string NAME = "WelcomeBackInformation";

        #region Properties

        public int CountryCode { get; set; }

        public string VoucherCode { get; set; }

        public string AgencyCode { get; set; }

        public int BranchNumber { get; set; }

        public string PaxEMail { get; set; }

        public string PaxName { get; set; }

        public string PaxSurname { get; set; }

        #endregion

        public override string ObtenerNombre()
        {
            return NAME;
        }
    }
}
