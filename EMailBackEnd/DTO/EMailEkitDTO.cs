using EMailAdmin.BackEnd.DTO.EkitBenefits;
using EMailAdmin.BackEnd.Properties;
using System;
using System.Collections.Generic;
using CapaNegocioDatos.CapaNegocio;
using EMailAdmin.BackEnd.Home;
using Newtonsoft.Json;
using EMailAdmin.BackEnd.Utils;
using System.Xml.Serialization;
using System.Collections;
namespace EMailAdmin.BackEnd.DTO
{
    public class EMailEkitDTO : DefaultEMailDTO, IDynamicDTO
    {
        public bool BilleteAttachment { get; set; }
        public string AgencyCode { get; set; }

        public int BranchNumber { get; set; }

        public string ProductCode { get; set; }

        public string RateCode { get; set; }

        public string ProductName { get; set; }

        public string PaxType { get; set; }

        public string IssuanceDate { get; set; }

        public string EffectiveStartDate { get; set; }

        public string EffectiveEndDate { get; set; }

        public string AdditionalData { get; set; }

        public string AdditionalData2 { get; set; }

        public string Days { get; set; }

        public string Area { get; set; }

        public string Amount { get; set; }

        public string PaxPassport { get; set; }

        public string PaxAge { get; set; }

        public string PaxPhone { get; set; }

        public string PaxDocumentNumber { get; set; }

        public string PaxAddress { get; set; }

        public string PaxCountry { get; set; }

        public string PaxGender { get; set; }

        public string PaxPostalCode { get; set; }

        public string PaxCity { get; set; }

        public string PaxState { get; set; }

        public string EmailTo { get; set; }

        public string EmergencyContact { get; set; }

        public string EmergencyPhone { get; set; }

        public string EmergencyAddress { get; set; }

        public GrupoClausulaDTO GrupoClausulaDTO { get; set; }

        public EMailEKitUpgradeDTO[] Upgrades { get; set; }

        public string CardType { get; set; }

        public string CardNumber { get; set; }

        public string CompleteVoucherCode { get; set; }

        public string EffectiveStartDateFormat { get; set; }

        public string EffectiveEndDateFormat { get; set; }

        public string IssuanceDateFormat { get; set; }

        public string IssuanceDateShortFormat { get; set; }

        public string EffectiveStartDateShortFormat { get; set; }

        public string EffectiveEndDateShortFormat { get; set; }

        public string ConsecutiveDays { get; set; }

        public string RCIExchangeAditional { get; set; }

        public string AnnualText { get; set; }

        public string PersonalInformationSecurity { get; set; }

        public string DistributionTypeCode { get; set; }

        public string Underwritter { get; set; }

        public string TripCost { get; set; }

        public string Fee { get; set; }

        public string BillRate { get; set; }

        public string RateModality { get; set; }

        public string BenefitsText { get; set; }

        public string AnualTextEEUU { get; set; }

        public string ClauseC422 { get; set; }

        public string ClauseC4211 { get; set; }

        public string ClauseC4 { get; set; }

        public string DatosAdicionales { get; set; }

        public string BirthDate { set; get; }

        public string PolicyCode { get; set; }

        public SucursalDTO Sucursal { get; set; }

        public override string GetLinks()
        {
            string links = "";

            if (GrupoClausulaDTO != null && this.GrupoClausulaDTO.Documentos != null)
            {
                string urlMainBenefitsDocumentLinks = ConfigurationValueHome.GetMainBenefitsDocumentLinks();

                links = "<p>";
                foreach (DocumentoDTO document in this.GrupoClausulaDTO.Documentos)
                {
                    if (document.IdIdioma == IdLanguage)
                    {
                        links += "<p><a href='" + urlMainBenefitsDocumentLinks +
                            document.IdDocumento.ToString() + "'>" + document.Observaciones + "</a></p>";
                    }
                }
                links += "</p>";
            }

            return links;
        }

        public string GetConsecutiveDays()
        {
            int days = 0;
            Int32.TryParse(Days, out days);

            return days >= 365 && GrupoClausulaDTO != null ? GrupoClausulaDTO.GetConsecutiveDays(IdLanguage) : "";
        }

        public string GetRCIExchangeAditional()
        {
            return ProductName.ToUpper().Contains("EXCHANGE") ? AdditionalData : "0";
        }

        public string GetAnnualText()
        {
            int days = 0;
            Int32.TryParse(Days, out days);
            return days < 365 ? "" : ", (D&Iacute;AS CONSECUTIVOS POR VIAJE: HASTA 30 D&Iacute;AS M&Aacute;XIMO)";
        }

        public string GetPersonalInformationSecurity()
        {
            return CountryCode == 540 ?
                "La informaci&oacute;n de este correo es confidencial y concierne &uacute;nicamente a la persona a quien est&aacute; dirigida. Si por error Ud no es el destinatario de este mensaje, por favor abst&eacute;ngase de continuar leyendo su contenido,copiarlo o derivarlo a cualquier otra persona que no sea aquella a quien va dirigido, luego de lo cual, rogamos que lo destruya. No se puede responsabilizar a Assist-Card de las consecuencias o da&ntilde;os que puedan resultar del apropiado env&iacute;o y recepci&oacute;n de este correo" : "";
        }

        public string NetPrice { get; set; }
        public string Iof { get; set; }
        public string AditionalFractionation { get; set; }
        public string PolicyPrice { get; set; }
        public string TotalPrice { get; set; }

        public string Ruc { get; set; }
        public string AgencyAddress { get; set; }
        public string AgencyProvince { get; set; }
        public string AgencyPhone { get; set; }
        public string AgencyDistric { get; set; }
        public string AgencyDepartament { get; set; }
        public string AgencyFax { get; set; }
        public string PrimaNeta { get; set; }
        public string DerechoDeEmision { get; set; }
        public string Igv { get; set; }
        public string PrimaTotal { get; set; }
        public List<ClienteInfoDTO> clientes { get; set; }
        public string Poliza { get; set; }
        public string CodigoPoliza { get; set; }
        public bool AgenciaVentaDirecta { get; set; }
        public string Version { get; set; }
        public string TransactionDate { get; set; }
        public string ServiceStartDate { get; set; }
        public string ServiceEndDate { get; set; }
        public string LandingAddonsUrl { get; set; }
        public bool HasUpgrades { get; set; }

        public string TransactionDateFormat { get; set; }
        public string ServiceStartDateFormat { get; set; }
        public string ServiceEndDateFormat { get; set; }

        public string TarifaLocalCurrency { get; set; }
        public string TaxLocalCurrency { get; set; }
        public string TotalLocalCurrency { get; set; }

        public string NameRateIntegration { get; set; }

        public MillasAerolineas Millas { get; set; }

        public int MillasCantidad
        {
            get
            {
                return (Millas != null ? Millas.millas_basic : 0);
            }
        }

        public DynamicItemDTO GetDicValue(string pKey)
        {
            object pValue = null;

            var pProperty = this.GetType().GetProperty(pKey);

            if (pProperty != null) 
                pValue = pProperty.GetValue(this);
            else //M.PERALTA: SI NO EXISTE COMO PROPIEDAD, LO BUSCO COMO MÉTODO
                pValue = this.GetType().GetMethod(pKey).Invoke(this, null);

            DynamicDTO pDynamicDTO = new DynamicDTO();

            pDynamicDTO.AddDicValue(pKey, (IEnumerable)pValue);

            return pDynamicDTO.GetDicValue(pKey);
        }

        public IEnumerable GetUpgrades()
        {
            var pListEMailEKitUpgradeDTO = new List<EMailEKitUpgradeDTO>();

            if (Upgrades != null)
            {
                foreach (var pEMailEKitUpgradeDTO in Upgrades)
                {
                    pEMailEKitUpgradeDTO.UpgradeIssuanceDate = getDateText(pEMailEKitUpgradeDTO.UpgradeIssuanceDate);
                    pEMailEKitUpgradeDTO.UpgradeStartDate = getDateText(pEMailEKitUpgradeDTO.UpgradeStartDate);
                    pEMailEKitUpgradeDTO.UpgradeEndDate = getDateText(pEMailEKitUpgradeDTO.UpgradeEndDate);
                    pListEMailEKitUpgradeDTO.Add(pEMailEKitUpgradeDTO);
                }
            }
            return pListEMailEKitUpgradeDTO;
        }

        private string getDateText(string pDateString)
        {
            return DateTime.Parse(pDateString).ToString("dd/MM/yyyy");
        }
    }

    public class EMailEKitUpgradeDTO
    {
        #region Properties

        public int IdLocation { get; set; }

        public int IdUpgrade { get; set; }

        public int IdUpgradeRate { get; set; }

        public int CountryCode { get; set; }

        public string Upgrade { get; set; }

        public string UpgradeRateCode { get; set; }
        public string UpgradeIssuanceDate { get; set; }
        public string UpgradeStartDate { get; set; }
        public string UpgradeEndDate { get; set; }
        public string UpgradeName { get; set; }
        public string UpgradeLegend { get; set; }

        #endregion
    }

    public class ClienteInfoDTO
    {
        public string apellido { get; set; }
        public string nombre { get; set; }
        public string fecNacimiento { get; set; }
        public int codigo { get; set; }
    }

    public class MillasAerolineas
    {
        public int nro_pf { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int millas_basic { get; set; }
        public string tipo_doc { get; set; }
        public string nro_doc { get; set; }
    }

}
