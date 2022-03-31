using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;

namespace EMailAdmin.ExternalServices.Domain
{
    public class IssuanceInformation : ObjetoPersistido
    {
        private const string NAME = "IssuanceInformation";

        #region Properties

        public int CountryCode { get; set; }

        public string VoucherCode { get; set; }

        public string AgencyCode { get; set; }

        public int BranchNumber { get; set; }

        public string ProductCode { get; set; }

        public string RateCode { get; set; }

        public string ProductName 
        {
            get
            {
                return RateInformation != null ? RateInformation.Name : "-";
            }
        }

        public string PaxType { get; set; }

        public string EffectiveStartDate { get; set; }

        public string EffectiveEndDate { get; set; }

        public string Days { get; set; }

        public string Area { get; set; }

        public string Amount { get; set; }

        public string PaxName { get; set; }

        public string PaxSurname { get; set; }

        public string PaxPassport { get; set; }

        public string PaxEMail { get; set; }

        public string PaxAge { get; set; }

        public string PaxPhone { get; set; }

        public string PaxAddress { get; set; }

        public string EmergencyContact { get; set; }

        public string EmergencyPhone { get; set; }

        public string EmergencyAddress { get; set; }

        public RateInformation RateInformation { get; set; }

        public string MainBenefits { get; set; }

        #endregion

        public override string ObtenerNombre()
        {
            return NAME;
        }
    }
}
