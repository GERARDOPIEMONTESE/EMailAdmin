using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class QuoteExchangeBody : ObjetoPersistido
    {
        private const string NAME = "QuoteExchangeBody";

        public string Body { get; set; }

        public override string ObtenerNombre()
        {
            return NAME;
        }
    }
}
