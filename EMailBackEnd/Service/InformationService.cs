using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Service.Interfaces;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Properties;
using System.Xml;
using LibreriaUtilitarios;
using EMailAdmin.ExternalServices.Service;
using EMailAdmin.ExternalServices.Domain;
using EMailAdmin.BackEnd.DTO.EkitBenefits;
using CapaNegocioDatos.Utilitarios;
using EMailAdmin.BackEnd.Domain.External;
using EMailAdmin.BackEnd.Utils;
using System.Web.Script.Serialization;
using System.Configuration;

namespace EMailAdmin.BackEnd.Service
{
    public class InformationService : IInformationService
    {
        #region IInformationService Members

        public void CompleteInformation(DTO.AbstractEMailDTO dto)
        {
            EMailEkitDTO ekitDto = (EMailEkitDTO)dto;

            IssuanceInformation issuanceInformation = ExternalServiceLocator.Instance().
                GetSignatureService().GetIssuanceInformation(ekitDto.CountryCode, ekitDto.VoucherCode);

            ekitDto.AgencyCode = issuanceInformation.AgencyCode;
            ekitDto.BranchNumber = issuanceInformation.BranchNumber;
            ekitDto.ProductCode = issuanceInformation.ProductCode;
            ekitDto.ProductName = issuanceInformation.ProductName;
            ekitDto.RateCode = issuanceInformation.RateCode;
            ekitDto.PaxType = issuanceInformation.PaxType == null || issuanceInformation.PaxType == "" ?
                "0" : issuanceInformation.PaxType;
            ekitDto.IssuanceDate = issuanceInformation.IssuanceDate;
            ekitDto.EffectiveStartDate = issuanceInformation.EffectiveStartDate;
            ekitDto.EffectiveEndDate = issuanceInformation.EffectiveEndDate;
            ekitDto.Days = issuanceInformation.Days;
            ekitDto.Area = issuanceInformation.Area;
            ekitDto.Amount = issuanceInformation.Amount;
            ekitDto.TripCost = issuanceInformation.TripCost;
            ekitDto.BillRate = issuanceInformation.BillRate;
            ekitDto.Fee = issuanceInformation.Fee;
            ekitDto.RecipientName = issuanceInformation.PaxName;
            ekitDto.RecipientSurname = issuanceInformation.PaxSurname;
            ekitDto.PaxPassport = issuanceInformation.PaxPassport;
            ekitDto.To = ekitDto.GivenToAddress && ekitDto.To != null && ekitDto.To.Length > 0 ?
                ekitDto.To : issuanceInformation.PaxEMail;
            ekitDto.Cc = IsValidEmail(issuanceInformation.Cc) ? issuanceInformation.Cc : string.Empty;
            ekitDto.PaxAge = issuanceInformation.PaxAge;
            ekitDto.PaxPhone = issuanceInformation.PaxPhone;
            ekitDto.PaxAddress = issuanceInformation.PaxAddress;
            ekitDto.PaxCountry = issuanceInformation.PaxCountry;
            ekitDto.PaxPostalCode = issuanceInformation.PaxPostalCode;
            ekitDto.PaxCity = issuanceInformation.PaxCity;
            ekitDto.PaxState = issuanceInformation.PaxState;
            ekitDto.PaxGender = issuanceInformation.PaxGender;
            ekitDto.EmergencyContact = issuanceInformation.EmergencyContact;
            ekitDto.EmergencyPhone = issuanceInformation.EmergencyPhone;
            ekitDto.EmergencyAddress = issuanceInformation.EmergencyAddress;
            ekitDto.AdditionalData = issuanceInformation.AdditionalData;
            ekitDto.AdditionalData2 = issuanceInformation.AdditionalData2;
            ekitDto.CardNumber = issuanceInformation.CardNumber;
            ekitDto.DistributionTypeCode = issuanceInformation.DistributionTypeCode;
            ekitDto.XmlContextInformation = issuanceInformation.XmlContextInformation;
            ekitDto.DatosAdicionales = issuanceInformation.DatosAdicionales == null || issuanceInformation.DatosAdicionales == "" ?
                "" : issuanceInformation.DatosAdicionales;
            ekitDto.BirthDate = issuanceInformation.BirthDate;

            GrupoClausulaDTO gruposDTO = new GrupoClausulaDTO();
            gruposDTO = (GrupoClausulaDTO)ServicioConversionXml.Instancia().DeserializeObject(
                issuanceInformation.MainBenefits, gruposDTO.GetType());

            IList<EMailEKitUpgradeDTO> upgradesDTO = new List<EMailEKitUpgradeDTO>();
            foreach (UpgradeInformation upgradeInformation in issuanceInformation.Upgrades)
            {
                EMailEKitUpgradeDTO upgradeDTO = new EMailEKitUpgradeDTO();
                upgradeDTO.CountryCode = upgradeInformation.CountryCode;
                upgradeDTO.Upgrade = upgradeInformation.Upgrade;
                upgradeDTO.UpgradeRateCode = upgradeInformation.UpgradeRateCode;

                upgradesDTO.Add(upgradeDTO);
            }

            ekitDto.Upgrades = upgradesDTO.ToArray<EMailEKitUpgradeDTO>();

            ekitDto.GrupoClausulaDTO = gruposDTO;

            List<ClienteInfoDTO> clientesInfo = new List<ClienteInfoDTO>();

            foreach (ClienteInfo client in issuanceInformation.clientes)
            {
                ClienteInfoDTO clienteInfo = new ClienteInfoDTO();

            }
            //ekitDto.clientes = issuanceInformation.clientes;

            // Brazil pdf necesary fields
            ekitDto.PolicyCode = issuanceInformation.PolicyCode;
            ekitDto.NetPrice = issuanceInformation.NetPrice;
            ekitDto.Iof = issuanceInformation.Iof;
            ekitDto.AditionalFractionation = issuanceInformation.NetPrice;
            ekitDto.PolicyPrice = issuanceInformation.NetPrice;
            ekitDto.TotalPrice = issuanceInformation.NetPrice;
        }


        public void CompleteInformationMore(DTO.AbstractEMailDTO dto)
        {
            EMailEkitDTO ekitDto = (EMailEkitDTO)dto;

            IssuanceInformation issuanceInformation = ExternalServiceLocator.Instance().
                GetSignatureService().GetIssuanceInformationMore(ekitDto.CountryCode, ekitDto.VoucherCode);

            if (ekitDto.BilleteAttachment)//Valido si vengo de BilleteAttachmentHandler
            {
                bool ClientCode = Convert.ToBoolean(ConfigurationManager.AppSettings["ClientCodeKey"]);
                if (issuanceInformation.clientes[0].codigo != ekitDto.clientes[0].codigo && ClientCode)
                {
                    throw new Exception("Code error for: " + ekitDto.CountryCode + " " + ekitDto.VoucherCode);
                }
            }
            ekitDto.AgencyCode = issuanceInformation.AgencyCode;
            ekitDto.BranchNumber = issuanceInformation.BranchNumber;
            ekitDto.ProductCode = issuanceInformation.ProductCode;
            ekitDto.ProductName = issuanceInformation.ProductName;
            ekitDto.RateCode = issuanceInformation.RateCode;
            ekitDto.PaxType = issuanceInformation.PaxType == null || issuanceInformation.PaxType == "" ?
                "0" : issuanceInformation.PaxType;
            ekitDto.IssuanceDate = issuanceInformation.IssuanceDate;
            ekitDto.EffectiveStartDate = issuanceInformation.EffectiveStartDate;
            ekitDto.EffectiveEndDate = issuanceInformation.EffectiveEndDate;
            ekitDto.Days = issuanceInformation.Days;
            ekitDto.Area = issuanceInformation.Area;
            ekitDto.Amount = issuanceInformation.Amount;
            ekitDto.TripCost = issuanceInformation.TripCost;
            ekitDto.BillRate = issuanceInformation.BillRate;
            ekitDto.Fee = issuanceInformation.Fee;
            ekitDto.RecipientName = issuanceInformation.PaxName;
            ekitDto.RecipientSurname = issuanceInformation.PaxSurname;
            ekitDto.PaxPassport = issuanceInformation.PaxPassport;
            ekitDto.To = ekitDto.GivenToAddress && ekitDto.To != null && ekitDto.To.Length > 0 ?
                ekitDto.To : issuanceInformation.PaxEMail;
            ekitDto.Cc = IsValidEmail(issuanceInformation.Cc) ? issuanceInformation.Cc : string.Empty;
            ekitDto.Millas = GetValidMillasAerolineas(issuanceInformation.Cc);
            ekitDto.PaxAge = issuanceInformation.PaxAge;
            ekitDto.PaxPhone = issuanceInformation.PaxPhone;
            ekitDto.PaxAddress = issuanceInformation.PaxAddress;
            ekitDto.PaxCountry = issuanceInformation.PaxCountry;
            ekitDto.PaxPostalCode = issuanceInformation.PaxPostalCode;
            ekitDto.PaxCity = issuanceInformation.PaxCity;
            ekitDto.PaxState = issuanceInformation.PaxState;
            ekitDto.PaxGender = issuanceInformation.PaxGender;
            ekitDto.EmergencyContact = issuanceInformation.EmergencyContact;
            ekitDto.EmergencyPhone = issuanceInformation.EmergencyPhone;
            ekitDto.EmergencyAddress = issuanceInformation.EmergencyAddress;
            ekitDto.AdditionalData = issuanceInformation.AdditionalData;
            ekitDto.AdditionalData2 = issuanceInformation.AdditionalData2;
            ekitDto.CardNumber = issuanceInformation.CardNumber;
            ekitDto.DistributionTypeCode = issuanceInformation.DistributionTypeCode;
            ekitDto.XmlContextInformation = issuanceInformation.XmlContextInformation;
            ekitDto.DatosAdicionales = issuanceInformation.DatosAdicionales == null || issuanceInformation.DatosAdicionales == "" ?
                "" : issuanceInformation.DatosAdicionales;
            ekitDto.BirthDate = issuanceInformation.BirthDate;
            ekitDto.CompleteVoucherCode = issuanceInformation.CompleteVoucherCode;
            ekitDto.Version = issuanceInformation.Version;
            ekitDto.TransactionDate = issuanceInformation.TransactionDate;
            ekitDto.ServiceEndDate = issuanceInformation.ServiceEndDate;
            ekitDto.ServiceStartDate = issuanceInformation.ServiceStartDate;
            ekitDto.LandingAddonsUrl = issuanceInformation.LandingAddonsUrl;
            ekitDto.HasUpgrades = issuanceInformation.HasUpgrades;

            GrupoClausulaDTO gruposDTO = new GrupoClausulaDTO();
            gruposDTO = (GrupoClausulaDTO)ServicioConversionXml.Instancia().DeserializeObject(
                issuanceInformation.MainBenefits, gruposDTO.GetType());

            IList<EMailEKitUpgradeDTO> upgradesDTO = new List<EMailEKitUpgradeDTO>();
            foreach (UpgradeInformation upgradeInformation in issuanceInformation.Upgrades)
            {
                EMailEKitUpgradeDTO upgradeDTO = new EMailEKitUpgradeDTO();
                upgradeDTO.CountryCode = upgradeInformation.CountryCode;
                upgradeDTO.Upgrade = upgradeInformation.Upgrade;
                upgradeDTO.UpgradeRateCode = upgradeInformation.UpgradeRateCode;
                upgradeDTO.UpgradeIssuanceDate = upgradeInformation.UpgradeIssuanceDate;
                upgradeDTO.UpgradeLegend = upgradeInformation.UpgradeLegend;
                upgradeDTO.UpgradeName = upgradeInformation.UpgradeName;
                upgradeDTO.UpgradeStartDate = upgradeInformation.UpgradeStartDate;
                upgradeDTO.UpgradeEndDate = upgradeInformation.UpgradeEndDate;

                upgradesDTO.Add(upgradeDTO);
            }

            ekitDto.Upgrades = upgradesDTO.ToArray<EMailEKitUpgradeDTO>();

            ekitDto.GrupoClausulaDTO = gruposDTO;

            // Brazil pdf necesary fields
            ekitDto.PolicyCode = issuanceInformation.PolicyCode;
            ekitDto.NetPrice = issuanceInformation.NetPrice;
            ekitDto.Iof = issuanceInformation.Iof;
            ekitDto.AditionalFractionation = issuanceInformation.NetPrice;
            ekitDto.PolicyPrice = issuanceInformation.NetPrice;
            ekitDto.TotalPrice = issuanceInformation.NetPrice;

            //Rimac pdf neceary fields
            ekitDto.Ruc = issuanceInformation.Ruc;
            ekitDto.PrimaNeta = issuanceInformation.PrimaNeta;
            ekitDto.DerechoDeEmision = issuanceInformation.DerechoDeEmision;
            ekitDto.Igv = issuanceInformation.Igv;
            ekitDto.PrimaTotal = issuanceInformation.PrimaTotal;
            ekitDto.clientes = new List<ClienteInfoDTO>();
            ekitDto.Poliza = issuanceInformation.Poliza;
            ekitDto.CodigoPoliza = issuanceInformation.CodigoPoliza;
            ekitDto.AgenciaVentaDirecta = (issuanceInformation.AgenciaVentaDirecta == "1" ? true : false);

            //User Story 14559: Ekit Vietnam - mostrar precio del voucher
            //Campos que muestran informacion de Tarifa, Tax y Total en moneda local
            ekitDto.TarifaLocalCurrency = issuanceInformation.TarifaEmitida;
            ekitDto.TaxLocalCurrency = issuanceInformation.TaxVoucher;
            ekitDto.TotalLocalCurrency = issuanceInformation.TotalVoucher;

            //User Story 15821: Configurar nombre de tarifa para servicio externo
            ekitDto.NameRateIntegration = issuanceInformation.NombreTarifaIntegraciones;

            foreach (ClienteInfo clientInfo in issuanceInformation.clientes)
            {
                ClienteInfoDTO clientDTO = new ClienteInfoDTO();
                clientDTO.apellido = clientInfo.apellido;
                clientDTO.nombre = clientInfo.nombre;
                clientDTO.fecNacimiento = clientInfo.fecNacimiento;

                ekitDto.clientes.Add(clientDTO);
            }
        }

        public DTO.MillasAerolineas GetValidMillasAerolineas(string dataJson)
        {
            try
            {
                JavaScriptSerializer jsSer = new JavaScriptSerializer();
                DTO.MillasAerolineas millas = new DTO.MillasAerolineas();
                object objData = jsSer.Deserialize(dataJson, millas.GetType());
                return (DTO.MillasAerolineas)objData;
            }
            catch
            {
                return null;
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}