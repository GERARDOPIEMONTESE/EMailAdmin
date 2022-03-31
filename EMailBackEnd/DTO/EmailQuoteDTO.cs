using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Service.Interfaces;
using FrameworkDAC.Negocio;
using System.Xml.Serialization;

namespace EMailAdmin.BackEnd.DTO 
{
    public class EmailQuoteDTO : AbstractEMailDTO, ITableBody
    {
        public List<ProductQuoteDTO> Products { get; set; }
        public String EffectiveStartDate { get; set; }
        public String EffectiveEndDate { get; set; }
        public String From { get; set; }
        public int Country { get; set; }
        public bool ApplyPolicy { get; set; }
        public string TableBody;
        public string TableHeader;
        public String DollarQuote { get; set; }
        public string HeaderEnQuoteAcnet { get; set; }
        public string HeaderEsQuoteAcnet { get; set; }
        public string HeaderPrQuoteAcnet { get; set; }
        public string FooterEnQuoteAcnet { get; set; }
        public string FooterEsQuoteAcnet { get; set; }
        public string FooterPrQuoteAcnet { get; set; }
        public string CountryName { get; set; }
        public int Days { get; set; }
        public string PaxName { get; set; }
        public string SellerData { get; set; }
        public string AdditionalData { get; set; }
        public String Destination { get; set; }
        public String PassengersEn { get; set; }
        public String PassengersEs { get; set; }
        public String PassengersPr { get; set; }
        //prueba

        public string ParseBody(string bodyName)
        {
            return this.TableBody;
        }

        public string ParseHeader(string bodyName)
        {
            return this.TableHeader;
        }

        public string[] ParseBodyArray(string bodyName)
        {
            throw new NotImplementedException();
        }
    }

    public class ProductQuoteDTO : ObjetoPersistido
    {
        public String ProductName { get; set; }
        [XmlIgnore]
        public Dictionary<string, string> ClausesMap { get; set; }
        public String Total { get; set; }
        public String Insurance { get; set; }
        public String Assistance { get; set; }
        public String AveragePerPerson { get; set; }

        private const string NAME = "ProductQuoteDTO";

        public override string ObtenerNombre()
        {
            return NAME;
        }
    }

    public class ClausesDTO
    {

        public int Country { get; set; }
        public String Product { get; set; }
        public String Rate { get; set; }
        public String Days { get; set; }
        public String ClauseId { get; set; }
        public String Leyend { get; set; }
        public int Position { get; set; }
        public String Title { get; set; }
        public String CodigoTipoClausula { get; set; }
    }

}
