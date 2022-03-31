using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;
using EMailAdmin.BackEnd.Domain.External;

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

        public string ProductName { get; set; }
       
        public string PaxType { get; set; }

        public string IssuanceDate { get; set; }

        public string EffectiveStartDate { get; set; }

        public string EffectiveEndDate { get; set; }

        public string Days { get; set; }

        public string Area { get; set; }

        public string Amount { get; set; }

        public string TripCost { get; set; }

        public string Fee { get; set; }

        public string BillRate { get; set; }

        public string PaxName { get; set; }

        public string PaxSurname { get; set; }

        public string PaxPassport { get; set; }

        public string PaxEMail { get; set; }

        public string PaxAge { get; set; }

        public string PaxPhone { get; set; }

        public string PaxAddress { get; set; }

        public string PaxCountry { get; set; }

        public string PaxGender { get; set; }

        public string PaxPostalCode { get; set; }

        public string PaxCity { get; set; }

        public string PaxState { get; set; }

        public string EmergencyContact { get; set; }

        public string EmergencyPhone { get; set; }

        public string EmergencyAddress { get; set; }

        public string AdditionalData { get; set; }

        public string AdditionalData2 { get; set; }

        public string XmlContextInformation { get; set; }

        public RateInformation RateInformation { get; set; }

        public IList<UpgradeInformation> Upgrades { get; set; }

        public string MainBenefits { get; set; }

        public string CardNumber { get; set; }

        public string DistributionTypeCode { get; set; }

        public string Cc { get; set; }

        public string DatosAdicionales { get; set; }

        public string BirthDate { set; get; }

        public string PolicyCode { get; set; }
        public string NetPrice { get; set; }
        public string Iof { get; set; }
        public string AditionalFractionation { get; set; }
        public string PolicyPrice { get; set; }
        public string TotalPrice { get; set; }

        //Datos para Rimac
        public string Ruc { get; set; }
        public string PrimaNeta { get; set; }
        public string DerechoDeEmision { get; set; }
        public string Igv { get; set; }
        public string PrimaTotal { get; set; }
        public string PasajerosGrupoEmision { get; set; }

        public IList<ClienteInfo> clientes { get; set; }
        public string Poliza { get; set; }

        public string CompleteVoucherCode { get; set; }

        //poliza brazil
        public string CodigoPoliza { get; set; }
        public string AgenciaVentaDirecta { get; set; }

        //New info
        public string Version { get; set; }
        public string TransactionDate { get; set; }
        public string ServiceStartDate { get; set; }
        public string ServiceEndDate { get; set; }
        public string LandingAddonsUrl { get; set; }
        public bool HasUpgrades { get; set; }

        public string TarifaEmitida { get; set; }
        public string TaxVoucher { get; set; }
        public string TotalVoucher { get; set; }

        public string NombreTarifaIntegraciones { get; set; }

        #endregion

        public override string ObtenerNombre()
        {
            return NAME;
        }
    }
}
