using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.ExternalServices.Service.Interface;
using EMailAdmin.BackEnd.Domain.External;
using EMailAdmin.BackEnd.AssistCardService;
using EMailAdmin.BackEnd.Properties;
using System.Xml;
using LibreriaUtilitarios;
using EMailAdmin.BackEnd.ServiceBoxPax;
using EMailAdmin.BackEnd.Exceptions;

namespace EMailAdmin.BackEnd.Service.External
{
    public class ExternalPrepurchasePaxInfoService : IExternalPrepurchasePaxService
    {     
        public PrepurchasePaxInformation Get(int codeBoxPax)
        {
            AssistCardDaysAcquisitionService service = new AssistCardDaysAcquisitionService();            
            string xml = service.getDaysAcquisition(codeBoxPax);

            PrepurchasePaxInformation prepurchasePaxInformation = new PrepurchasePaxInformation();

            XmlDocument xmlDocument = XmlParser.GetDocument(xml);

            if (xmlDocument.GetElementsByTagName("DaysAcquisition").Count != 0)
            {
                prepurchasePaxInformation.PaxEMail = XmlParser.GetValue(xmlDocument, "/DaysAcquisition/pax/email");
                prepurchasePaxInformation.PaxSurname = XmlParser.GetValue(xmlDocument, "/DaysAcquisition/pax/apellido");
                prepurchasePaxInformation.PaxName = XmlParser.GetValue(xmlDocument, "/DaysAcquisition/pax/nombre");
                prepurchasePaxInformation.BoxPaxCodeVerifier = XmlParser.GetValue(xmlDocument, "/DaysAcquisition/daysAcquisition/codigoVerificacion");
                prepurchasePaxInformation.BoxPaxCode = XmlParser.GetIntValue(xmlDocument, "/DaysAcquisition/daysAcquisition/id");
                prepurchasePaxInformation.Product_Name = XmlParser.GetValue(xmlDocument, "/DaysAcquisition/product/nombre");
                prepurchasePaxInformation.Days = XmlParser.GetIntValue(xmlDocument, "/DaysAcquisition/daysAcquisition/saldoInicial");
                prepurchasePaxInformation.BoxPaxPricePaid = XmlParser.GetValue(xmlDocument, "/DaysAcquisition/daysAcquisition/tarifaImpresa");
                prepurchasePaxInformation.EffectiveStartDate = XmlParser.GetValue(xmlDocument, "/DaysAcquisition/daysAcquisition/fechaInicioVigencia");
                prepurchasePaxInformation.EffectiveEndDate = XmlParser.GetValue(xmlDocument, "/DaysAcquisition/daysAcquisition/fechaFinVigencia");
                prepurchasePaxInformation.CountryCode = XmlParser.GetIntValue(xmlDocument, "/DaysAcquisition/daysAcquisition/pais");
                prepurchasePaxInformation.passengers = getPassengers(xmlDocument);
                
            }
            else
                GetErrorWS(xmlDocument);

            return prepurchasePaxInformation;
        }

        private void GetErrorWS(XmlDocument xmlDocument)
        {
            string error = "{0} (Code:{1})";
            if (xmlDocument.GetElementsByTagName("codigo").Count != 0)
                error = string.Format(error, XmlParser.GetValue(xmlDocument, "/response/descripcion"), XmlParser.GetValue(xmlDocument, "/response/codigo"));
            else
                error = "Error!";

            throw new Exception(error);
        }

        public PrepurchasePaxInformation Get(int codeBoxPax, string group, int countryCode)
        {
                AssistCardDaysAcquisitionService service = new AssistCardDaysAcquisitionService();
                string xml = service.sendEmisionPreCompraMail(codeBoxPax, group, countryCode);

                PrepurchasePaxInformation prepurchasePaxInformation = new PrepurchasePaxInformation();
                XmlDocument xmlDocument = XmlParser.GetDocument(xml);

                if (xmlDocument.GetElementsByTagName("EmisionPreCompraMail").Count != 0)
                {                    
                    prepurchasePaxInformation.CountryCode = int.Parse(XmlParser.GetValue(xmlDocument, "/EmisionPreCompraMail/precompraPax/pais"));
                    prepurchasePaxInformation.BoxPaxCode = int.Parse(XmlParser.GetValue(xmlDocument, "/EmisionPreCompraMail/precompraPax/id"));
                    prepurchasePaxInformation.BoxPaxCodeVerifier = XmlParser.GetValue(xmlDocument, "/EmisionPreCompraMail/precompraPax/codigoVerificacion");
                    prepurchasePaxInformation.PaxEMail = XmlParser.GetValue(xmlDocument, "/EmisionPreCompraMail/cliente/email");
                    prepurchasePaxInformation.PaxSurname = XmlParser.GetValue(xmlDocument, "/EmisionPreCompraMail/cliente/apellido");
                    prepurchasePaxInformation.PaxName = XmlParser.GetValue(xmlDocument, "/EmisionPreCompraMail/cliente/nombre");
                    prepurchasePaxInformation.Product_Name = XmlParser.GetValue(xmlDocument, "/EmisionPreCompraMail/producto/nombre");
                    prepurchasePaxInformation.Days = XmlParser.GetIntValue(xmlDocument, "/EmisionPreCompraMail/precompraPax/saldoDisponible");
                    prepurchasePaxInformation.BoxPaxPricePaid = XmlParser.GetValue(xmlDocument, "/EmisionPreCompraMail/precompraPax/tarifaImpresa");
                    prepurchasePaxInformation.EffectiveStartDate = XmlParser.GetValue(xmlDocument, "/EmisionPreCompraMail/precompraPax/fechaInicioVigencia");
                    prepurchasePaxInformation.EffectiveEndDate = XmlParser.GetValue(xmlDocument, "/EmisionPreCompraMail/precompraPax/fechaFinVigencia");
                    prepurchasePaxInformation.passengers = getPassengers(xmlDocument);                    
                }
                else
                    GetErrorWS(xmlDocument);

                return prepurchasePaxInformation;
        }

        private Passenger[] getPassengers(XmlDocument xmlDocument)
        {
            List<Passenger> pasajeros = new List<Passenger>();

            IList<string> nodos = XmlParser.GetXmlValues(xmlDocument, "/EmisionPreCompraMail/precompraPax/vouchers/voucher");

            foreach (var item in nodos)
            {
                pasajeros.Add(new Passenger()
                {
                    Nombre = string.Format("{0} {1}", XmlParser.GetValue(item, "/voucher/pasajero/apellido"), XmlParser.GetValue(item, "/voucher/pasajero/nombre")),
                    NroTarjeta = XmlParser.GetValue(item, "/voucher/icardNro"),
                    fechaVigenciaDesde = DateTime.Parse(XmlParser.GetValue(item, "/voucher/fecVigInic")),
                    fechaVigenciaHasta = DateTime.Parse(XmlParser.GetValue(item, "/voucher/fecVifFin"))
                });
            }
            
            return pasajeros.ToArray();
        }

    }
}
