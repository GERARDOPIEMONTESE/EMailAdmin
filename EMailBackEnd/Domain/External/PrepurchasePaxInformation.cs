using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMailAdmin.BackEnd.Domain.External
{
    public class PrepurchasePaxInformation
    {
        public int BoxPaxCode { get; set; }

        public string BoxPaxCodeVerifier { get; set; }

        public string BoxPaxPricePaid { get; set; }        

        public string EffectiveStartDate { get; set; }

        public string EffectiveStartDateFormat {get; set;}

        public string EffectiveEndDate { get; set; }

        public string EffectiveEndDateFormat { get; set; }

        public int Days { get; set; }

        public string PaxName { get; set; }

        public string PaxSurname { get; set; }
        
        public string PaxEMail { get; set; }

        public int CountryCode { get; set; }

        public int Cant_precompra { get; set; }

        public int Cant_utilizado { get; set; }
        
        public string AgencyCode { get; set; }

        public string AgencyName { get; set; }

        public string AgencySuc { get; set; }

        public string Product { get; set; }

        public string Product_Name { get; set; }

        public string Tarif { get; set; }

        public Passenger[] passengers { get; set; }

        public string XmlContextInformation { get; set; }        

    }

    public class Passenger
    {
        public string Nombre { get; set; }
        public string NroTarjeta { get; set; }
        public DateTime fechaVigenciaDesde { get; set; }
        public DateTime fechaVigenciaHasta { get; set; }
        public string fechaVigenciaDesdeFormat { get; set; }
        public string fechaVigenciaHastaFormat { get; set; }
    }
}
