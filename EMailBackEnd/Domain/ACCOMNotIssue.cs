using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class ACCOMNotIssue : ObjetoPersistido
    {
        private const string NAME = "ACCOMNotIssue";

        public int CountryCode { get; set; }
        public string CountryName { get; set; }        
        public string Gateway { get; set; }
        public DateTime QuoteLogDate { get; set; }
        public string PurchaseProcessTypeDesc { get; set; }
        public DateTime LastConfirmationDate { get; set; }

        public override string ObtenerNombre()
        {
            return NAME;
        }

    }
}
