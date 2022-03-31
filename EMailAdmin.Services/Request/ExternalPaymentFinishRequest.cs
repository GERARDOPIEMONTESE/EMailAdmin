using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMailAdmin.Services.Request
{
    public class ExternalPaymentFinishRequest
    {
        public String EmailTo { get; set; }
        public List<Passenger> Passengers { get; set; }
        public float Total;
        public int CountryCode { get; set; }
        public int IdLanguage { get; set; }
    }
}