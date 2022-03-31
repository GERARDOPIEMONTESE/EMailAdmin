using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;

namespace EMailAdmin.ExternalServices.Domain
{
    public class RateInformation : ObjetoPersistido
    {
        private const string NAME = "RateInformation";

        #region Properties

        public int CountryCode { get; set; }

        public string ProductCode { get; set; }

        public string Code { get; set; }

        public string Days { get; set; }

        public string Name { get; set; }

        #endregion

        public override string ObtenerNombre()
        {
            return NAME;
        }
    }
}
