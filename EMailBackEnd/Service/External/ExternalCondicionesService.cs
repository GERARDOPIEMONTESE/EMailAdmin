using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Service.Interfaces;
using EMailAdmin.BackEnd.DTO.EkitBenefits;
using CapaNegocioDatos.Utilitarios;
using EMailAdmin.BackEnd.Properties;
using LibreriaUtilitarios;
using System.Xml;
using EMailAdmin.ExternalServices.Service.Interface;
using EMailAdmin.BackEnd.Service;

namespace EMailAdmin.ExternalServices.Service
{
    public class ExternalCondicionesService: IExternalCondicionesService
    {
        public DocumentoDTO[] GetDocumentsInformation(int countryCode, string capitaCode, string planCode)
        {
            List<DocumentoDTO> lstDocs = new List<DocumentoDTO>();
            try
            {
                ConsultaCondicionesDTO condiciones = new ConsultaCondicionesDTO();
                condiciones.CodigoPais = countryCode;
                condiciones.CodigoProducto = capitaCode;
                condiciones.CodigoTarifa = planCode;

                string xmlFiltro = ServicioConversionXml.Instancia().SerializeObject(condiciones, @"ConsultaCondicionesDTO", true, true);
                
                ServicioCondiciones ws = new ServicioCondiciones();                
                var xml = ws.ObtenerCondicionesXml(xmlFiltro);

                string xmlTagDocumentos = ServicioConversionXml.Instancia().GetValue("Documentos", xml);

                XmlDocument xmlDocument = XmlParser.GetDocument(xmlTagDocumentos);

                IList<string> xmlDocs = XmlParser.GetXmlValues(xmlDocument, "DocumentoDTO");

                foreach (string docXml in xmlDocs)
                {
                    DocumentoDTO doc = new DocumentoDTO();
                    doc = (DocumentoDTO)ServicioConversionXml.Instancia().DeserializeObject(docXml, doc.GetType());
                    if (doc != null)
                    {
                        lstDocs.Add(doc);
                    }
                }
            }
            catch
            {

            }
            return lstDocs.ToArray();
        }       
    }
}
