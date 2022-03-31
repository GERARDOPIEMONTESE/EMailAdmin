using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class DistributionType : ObjetoCodificado
    {
        private const string NAME = "DistributionType";

        #region Constants

        public const string VIRTUAL = "VIR";

        public const string PERSONAL = "PER";

        #endregion

        public override string ObtenerNombre()
        {
            return NAME;
        }
    }
}
