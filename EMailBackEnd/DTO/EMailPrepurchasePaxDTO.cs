using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Service.Interfaces;
using EMailAdmin.BackEnd.Domain.External;

namespace EMailAdmin.BackEnd.DTO
{
    public class EMailPrepurchasePaxDTO : DefaultEMailDTO, ITableBody
    {        
        public int BoxPaxCode { get; set; }

        public string groupVoucher { get; set; }

        public string BoxPaxPricePaid { get; set; }

        public string BoxPaxCodeVerifier { get; set; }

        public string PaxEMail { get; set; }        
        
        public int Days { get; set; }        

        public string ProductName { get; set; }
        
        public string ProductCode { get; set; }
        
        public string EffectiveStartDateFormat { get; set; }

        public string EffectiveEndDateFormat { get; set; }

        public Passenger[] BoxPaxPasajeros { get; set; }
       
        public string[] ParseBodyArray(string bodyName)
        {
            List<string> lst = new List<string>();
            foreach (var item in BoxPaxPasajeros)
            {
                lst.Add(item.Nombre + "," + item.NroTarjeta + "," + item.fechaVigenciaDesdeFormat +","+ item.fechaVigenciaHastaFormat);
            }
            return lst.ToArray();
        }
        
        public string ParseBody(string bodyName)
        {
            throw new NotImplementedException();
        }

        public string ParseHeader(string bodyName)
        {
            throw new NotImplementedException();
        }
    }
}
