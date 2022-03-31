using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class QuoteMailACCOM : ObjetoPersistido
    {
        private const string NAME = "QuoteMailACCOM";

        public int IdQuoteLog { get; set; }
        public string HTMLBody { get; set; }
        public string Email { get; set; }

        public string Phone { get; set; }
        public string AuxPhone { set; get; }
        public string FullName { set; get; }
        public string Modality { set; get; }
        public string Product { set; get; }
        public string Destination { set; get; }
        public string DaysQuantity { set; get; }
        public string PurchaseProcessCode { set; get; }
        public int CountryCode { set; get; }

        public override string ObtenerNombre()
        {
            return NAME;
        }

    }
}
