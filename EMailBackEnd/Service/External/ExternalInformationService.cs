using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.ExternalServices.Data.Interfaces;
using EMailAdmin.ExternalServices.Domain;
using LibreriaUtilitarios;
using System.Xml;
using EMailAdmin.ExternalServices.Service.Interface;
using EMailAdmin.BackEnd.Properties;
using EMailAdmin.BackEnd.DTO;
using CapaNegocioDatos.Utilitarios;
using EMailAdmin.BackEnd.Domain.External;
using EMailAdmin.BackEnd.DTO.EkitBenefits;
using CapaNegocioDatos.CapaHome;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.AssistCardService;
using EMailAdmin.BackEnd.Service;

namespace EMailAdmin.ExternalServices.Service
{
    public class ExternalInformationService : IExternalInformationService
    {
        public IDAOIssuanceInformation DaoIssuanceInformation { get; set; }

        public IssuanceInformation GetIssuanceInformation(int countryCode, string voucherCode)
        {
            int voucher = 0;
            Int32.TryParse(voucherCode, out voucher);
                        
            string nuevoServicioActivo = CodigoActivadorHome.Obtener("email.admin.nuevo.servicio").Valor;

            ServicioAssistCard service = new ServicioAssistCard();
            string xml = service.findVoucherAllWithTarjeta(countryCode, voucher);

            IssuanceInformation issuanceInformation = new IssuanceInformation();

            XmlDocument xmlDocument = XmlParser.GetDocument(xml);

            issuanceInformation.XmlContextInformation = xml;
            issuanceInformation.CountryCode = XmlParser.GetIntValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/pais");
            issuanceInformation.VoucherCode = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/codigo");
            issuanceInformation.AgencyCode = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/agencia");
            issuanceInformation.BranchNumber = XmlParser.GetIntValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/sucAgencia");
            issuanceInformation.ProductCode = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/producto");
            issuanceInformation.RateCode = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/codTarifa");
            issuanceInformation.ProductName = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/producto/leyendaImpresion");
            issuanceInformation.PaxType = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/tipoPaxVoucher");
            issuanceInformation.IssuanceDate = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/fechaEmision");
            issuanceInformation.EffectiveStartDate = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/fecVigInic");
            issuanceInformation.EffectiveEndDate = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/fecVifFin");
            issuanceInformation.Days = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/cantDias");
            issuanceInformation.Area = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/area");
            issuanceInformation.Amount = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/tarifaImpresa");
            issuanceInformation.AdditionalData = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/datoAdic1");
            issuanceInformation.AdditionalData2 = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/datoAdic2");
            issuanceInformation.PaxName = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/cliente/nombre");
            issuanceInformation.PaxSurname = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/cliente/apellido");
            issuanceInformation.PaxPassport = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/cliente/nroDocumento");
            issuanceInformation.PaxEMail = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/cliente/email");
            issuanceInformation.Cc = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/datoAdic2");            
            issuanceInformation.DatosAdicionales = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/datosAdicionales");
            issuanceInformation.CodigoPoliza = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/codigoPoliza");
            //issuanceInformation. = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/codigoPoliza");

            try
            {
                issuanceInformation.CompleteVoucherCode = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/codigoCompletoVoucher");
            }
            catch (Exception)
            {
                issuanceInformation.CompleteVoucherCode = "";
            }

            try
            {
                issuanceInformation.PaxAge = DateUtility.GetYears(Convert.ToDateTime(
                    XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/cliente/fecNacimiento")), DateTime.Now).ToString();
            }
            catch (Exception)
            {
                issuanceInformation.PaxAge = "";
            }

            issuanceInformation.PaxPhone = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/cliente/telParticular");
            issuanceInformation.PaxAddress = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/cliente/domCalle");
            issuanceInformation.PaxCountry = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/cliente/domPais");
            issuanceInformation.PaxCity = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/cliente/domCiudad");
            issuanceInformation.PaxPostalCode = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/cliente/domCp");
            issuanceInformation.PaxState = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/cliente/domProvincia");
            issuanceInformation.PaxGender = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/cliente/sexo");
            issuanceInformation.EmergencyContact = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/cliente/emergContacto");
            issuanceInformation.EmergencyPhone = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/cliente/emergTel1");
            issuanceInformation.EmergencyAddress = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/cliente/emergCalle");

            try
            {
                issuanceInformation.BirthDate = Convert.ToDateTime(XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/cliente/fecNacimiento")).ToShortDateString();
            }
            catch(Exception)
            {
                issuanceInformation.BirthDate = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/cliente/fecNacimiento");
            }

            issuanceInformation.CardNumber = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/tarjeta");
            issuanceInformation.TripCost = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/tarifaImpresa");
            issuanceInformation.Fee = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/fee");
            issuanceInformation.BillRate = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/tarifaFactura");

            // Deberia hacer esta validacion? Asi como esta definitivamente no...
            //if(issuanceInformation.CountryCode == 550)
            issuanceInformation.PolicyCode = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/codigoPoliza");
            issuanceInformation.NetPrice = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/polizaDTO/premioLiquido");
            issuanceInformation.Iof = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/polizaDTO/iof");
            issuanceInformation.AditionalFractionation = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/polizaDTO/adicionalFraccionamiento");
            issuanceInformation.PolicyPrice = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/polizaDTO/costoPoliza");
            issuanceInformation.TotalPrice = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/polizaDTO/premioTotal");
            

            string distributionType = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/tarjetaDTO/distribucion");

            issuanceInformation.DistributionTypeCode = distributionType == null || distributionType.Length == 0 ? "-" :
                distributionType.Equals("4") || distributionType.Equals("8") ? DistributionType.VIRTUAL : DistributionType.PERSONAL;

            IList<string> upgrades = XmlParser.GetXmlValues(xmlDocument, "/VOUCHERALL/datosVoucher/upgrades/com.icard.voucher.UpgradeAsociado");

            issuanceInformation.Upgrades = new List<UpgradeInformation>();

            foreach (string upgradeXml in upgrades)
            {
                UpgradeInformation upgradeInformation = new UpgradeInformation();

                upgradeInformation.CountryCode = XmlParser.GetIntValue(upgradeXml, "/com.icard.voucher.UpgradeAsociado/pais");
                upgradeInformation.Upgrade = XmlParser.GetValue(upgradeXml, "/com.icard.voucher.UpgradeAsociado/codigoUpgrade");
                upgradeInformation.UpgradeRateCode = XmlParser.GetValue(upgradeXml, "/com.icard.voucher.UpgradeAsociado/codigoTarifaUpgrade");

                issuanceInformation.Upgrades.Add(upgradeInformation);
            }

            int initIndex = xml.IndexOf("<GrupoClausulaDTO>");
            int endIndex = xml.IndexOf("</GrupoClausulaDTO>") + "</GrupoClausulaDTO>".Length;

            issuanceInformation.MainBenefits = xml.Substring(initIndex, endIndex - initIndex);

            return issuanceInformation;
        }

        public IssuanceInformation GetIssuanceInformationMore(int countryCode, string voucherCode)
        {
            int voucher = 0;
            Int32.TryParse(voucherCode, out voucher);

            
            string nuevoServicioActivo = CodigoActivadorHome.Obtener("email.admin.nuevo.servicio").Valor;

            ServicioAssistCard service = new ServicioAssistCard();
            string xml = service.getVoucherInfo(countryCode, Convert.ToInt32(voucher));

            IssuanceInformation issuanceInformation = new IssuanceInformation();

            XmlDocument xmlDocument = XmlParser.GetDocument(xml);

            if (xml.IndexOf("<VOUCHERALL>") < 0)
                throw new Exception(XmlParser.GetValue(xmlDocument, "/response/descripcion").ToUpper()); 

            issuanceInformation.XmlContextInformation = xml;
            issuanceInformation.CountryCode = XmlParser.GetIntValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/pais");
            issuanceInformation.VoucherCode = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/codigo");
            issuanceInformation.AgencyCode = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/agencia");
            issuanceInformation.BranchNumber = XmlParser.GetIntValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/sucAgencia");
            issuanceInformation.ProductCode = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/producto");
            issuanceInformation.RateCode = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/codTarifa");
            issuanceInformation.ProductName = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/producto/leyendaImpresion");
            issuanceInformation.PaxType = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/tipoPaxVoucher");
            issuanceInformation.IssuanceDate = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/fechaEmision");
            issuanceInformation.EffectiveStartDate = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/fecVigInic");
            issuanceInformation.EffectiveEndDate = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/fecVifFin");
            issuanceInformation.Days = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/cantDias");
            issuanceInformation.Area = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/area");
            issuanceInformation.Amount = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/tarifaImpresa");
            issuanceInformation.AdditionalData = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/datoAdic1");
            issuanceInformation.AdditionalData2 = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/datoAdic2");
            issuanceInformation.PaxName = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/cliente/nombre");
            issuanceInformation.PaxSurname = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/cliente/apellido");
            issuanceInformation.PaxPassport = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/cliente/nroDocumento");
            issuanceInformation.PaxEMail = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/cliente/email");
            issuanceInformation.Cc = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/datoAdic2");
            issuanceInformation.DatosAdicionales = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/datosAdicionales");
            issuanceInformation.CodigoPoliza = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/codigoPoliza");
            issuanceInformation.TarifaEmitida = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/tarifaEmitidaLocalCurrency");
            issuanceInformation.TaxVoucher = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/taxVoucherLocalCurrency");
            issuanceInformation.TotalVoucher = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/totalVoucherLocalCurrency");
            issuanceInformation.NombreTarifaIntegraciones = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/nombreTarifaIntegraciones");
            
            try
            {
                issuanceInformation.PaxAge = DateUtility.GetYears(Convert.ToDateTime(
                    XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/cliente/fecNacimiento")), DateTime.Now).ToString();
            }
            catch (Exception)
            {
                issuanceInformation.PaxAge = "";
            }

            try
            {
                issuanceInformation.CompleteVoucherCode = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/codigoCompletoVoucher");
            }
            catch (Exception)
            {
                issuanceInformation.CompleteVoucherCode = "";
            }

            issuanceInformation.PaxPhone = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/cliente/telParticular");
            issuanceInformation.PaxAddress = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/cliente/domCalle");
            issuanceInformation.PaxCountry = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/cliente/domPais");
            issuanceInformation.PaxCity = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/cliente/domCiudad");
            issuanceInformation.PaxPostalCode = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/cliente/domCp");
            issuanceInformation.PaxState = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/cliente/domProvincia");
            issuanceInformation.PaxGender = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/cliente/sexo");
            issuanceInformation.EmergencyContact = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/cliente/emergContacto");
            issuanceInformation.EmergencyPhone = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/cliente/emergTel1");
            issuanceInformation.EmergencyAddress = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/cliente/emergCalle");

            try
            {
                issuanceInformation.BirthDate = Convert.ToDateTime(XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/cliente/fecNacimiento")).ToShortDateString();
            }
            catch (Exception)
            {
                issuanceInformation.BirthDate = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/cliente/fecNacimiento");
            }

            issuanceInformation.CardNumber = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/tarjeta");
            issuanceInformation.TripCost = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/tarifaImpresa");
            issuanceInformation.Fee = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/fee");
            issuanceInformation.BillRate = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/tarifaFactura");

            // Deberia hacer esta validacion? Asi como esta definitivamente no...
            //if(issuanceInformation.CountryCode == 550)
            issuanceInformation.PolicyCode = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/codigoPoliza");
            issuanceInformation.NetPrice = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/polizaDTO/premioLiquido");
            issuanceInformation.Iof = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/polizaDTO/iof");
            issuanceInformation.AditionalFractionation = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/polizaDTO/adicionalFraccionamiento");
            issuanceInformation.PolicyPrice = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/polizaDTO/costoPoliza");
            issuanceInformation.TotalPrice = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/polizaDTO/premioTotal");


            string distributionType = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/tarjetaDTO/distribucion");

            issuanceInformation.DistributionTypeCode = distributionType == null || distributionType.Length == 0 ? "-" :
                distributionType.Equals("4") || distributionType.Equals("8") ? DistributionType.VIRTUAL : DistributionType.PERSONAL;

            IList<string> upgrades = XmlParser.GetXmlValues(xmlDocument, "/VOUCHERALL/datosVoucher/upgrades/com.icard.voucher.UpgradeAsociado");

            issuanceInformation.Upgrades = new List<UpgradeInformation>();

            foreach (string upgradeXml in upgrades)
            {
                UpgradeInformation upgradeInformation = new UpgradeInformation();

                upgradeInformation.CountryCode = XmlParser.GetIntValue(upgradeXml, "/com.icard.voucher.UpgradeAsociado/pais");
                upgradeInformation.Upgrade = XmlParser.GetValue(upgradeXml, "/com.icard.voucher.UpgradeAsociado/codigoUpgrade");
                upgradeInformation.UpgradeRateCode = XmlParser.GetValue(upgradeXml, "/com.icard.voucher.UpgradeAsociado/codigoTarifaUpgrade");
                upgradeInformation.UpgradeIssuanceDate = XmlParser.GetValue(upgradeXml, "/com.icard.voucher.UpgradeAsociado/fechaEmision");
                upgradeInformation.UpgradeStartDate = XmlParser.GetValue(upgradeXml, "/com.icard.voucher.UpgradeAsociado/fecVigInic");
                upgradeInformation.UpgradeEndDate = XmlParser.GetValue(upgradeXml, "/com.icard.voucher.UpgradeAsociado/fecVigFin");
                upgradeInformation.UpgradeName = XmlParser.GetValue(upgradeXml, "/com.icard.voucher.UpgradeAsociado/nombreUpgrade");
                if(upgradeXml.Contains("leyendaUpgrade"))
                    upgradeInformation.UpgradeLegend = XmlParser.GetValue(upgradeXml, "/com.icard.voucher.UpgradeAsociado/leyendaUpgrade");
                issuanceInformation.Upgrades.Add(upgradeInformation);
            }

            int initIndex = xml.IndexOf("<GrupoClausulaDTO>");
            if (initIndex < 0) throw new Exception("NOT CONDITIONS VOUCHER "); 
            int endIndex = xml.IndexOf("</GrupoClausulaDTO>") + "</GrupoClausulaDTO>".Length;

            issuanceInformation.MainBenefits = xml.Substring(initIndex, endIndex - initIndex);


            int inicio = xml.IndexOf("<pasajerosGrupoEmision>");
            int fin = xml.IndexOf("</pasajerosGrupoEmision>") + "</pasajerosGrupoEmision>".Length;



            IList<string> clientesAdd = XmlParser.GetXmlValues(xmlDocument, "/VOUCHERALL/pasajerosGrupoEmision");

            issuanceInformation.clientes = new List<ClienteInfo>();

            foreach (string clientesMas in clientesAdd)
            {
                ClienteInfo client = new ClienteInfo();

                client.apellido = XmlParser.GetValue(clientesMas, "/pasajerosGrupoEmision/cliente/apellido");
                client.nombre = XmlParser.GetValue(clientesMas, "/pasajerosGrupoEmision/cliente/nombre");
                client.fecNacimiento = XmlParser.GetValue(clientesMas, "/pasajerosGrupoEmision/cliente/fecNacimiento");
                client.codigo = XmlParser.GetIntValue(clientesMas, "/pasajerosGrupoEmision/cliente/codigo");

                issuanceInformation.clientes.Add(client);
            }
            
            issuanceInformation.PasajerosGrupoEmision = xml.Substring(inicio, fin - inicio);

            issuanceInformation.Poliza = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosAdicionales/polizaRimac");
            issuanceInformation.Ruc = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/agencia/cuit");
            issuanceInformation.PrimaNeta = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/tarifaEmitida");
            issuanceInformation.DerechoDeEmision = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosAdicionales/derechoEmision");
            issuanceInformation.Igv = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosAdicionales/impuestoIgv");
            issuanceInformation.PrimaTotal = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosVoucher/voucher/taxEmitida");

            issuanceInformation.Version = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosAdicionales/versionado");
            issuanceInformation.TransactionDate = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosAdicionales/fechaTransaccion");
            issuanceInformation.ServiceStartDate = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosAdicionales/inicioVigenciaServicio");
            issuanceInformation.ServiceEndDate = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosAdicionales/finVigenciaServicio");
            issuanceInformation.LandingAddonsUrl = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosAdicionales/landingAddonsUrl");
            bool hasUpgrades = false;
            Boolean.TryParse(XmlParser.GetValue(xmlDocument, "/VOUCHERALL/datosAdicionales/tieneUpgrades"), out hasUpgrades);
            issuanceInformation.HasUpgrades = hasUpgrades;

            try
            {
                issuanceInformation.AgenciaVentaDirecta = XmlParser.GetValue(xmlDocument, "/VOUCHERALL/agencia/ventaDirecta");
            }
            catch {}

            return issuanceInformation;
        }
    }
}
