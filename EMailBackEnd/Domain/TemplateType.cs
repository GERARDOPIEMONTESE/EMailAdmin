using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class TemplateType : ObjetoCodificado
    {
        private const string NAME = "TemplateType";

        public override string ObtenerNombre()
        {
            return NAME;
        }

        public enum PrepurchaseType
        {
            BoxPaxBuy,
            BoxPaxBalance,
            BoxPaxCancel
        }
    }
}
