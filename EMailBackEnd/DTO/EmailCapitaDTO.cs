using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.DTO.EkitBenefits;
using EMailAdmin.BackEnd.Properties;
using CapaNegocioDatos.CapaNegocio;
using CapaNegocioDatos.CapaHome;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.BackEnd.DTO
{
    public class EmailCapitaDTO : AbstractEMailDTO
    {
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string RateName { get; set; }
        public string RateCode { get; set; }
        public string BenefitsText { get; set; }
        public string PaxType { get; set; }
        public string PaxPassport { get; set; } 

        public DocumentoDTO[] DocumentosDTO { get; set; }

        public override string GetLinks()
        {
            string links = "";
            if (DocumentosDTO != null)
            {
                string urlMainBenefitsDocumentLinks = ConfigurationValueHome.GetMainBenefitsDocumentLinks();

                string[] idsTipoDocumento = CodigoActivadorHome.CapitaTiposDocumento();
                int[] idsTipoDoc_habilitados = CapitaHome.CondicionesTiposDocumento(idsTipoDocumento);
                                
                var docs = from d in this.DocumentosDTO
                           where idsTipoDoc_habilitados.Contains(d.IdTipoDocumento)
                           select d;

                if (docs != null)
                {
                    links = "<p>";
                    foreach (DocumentoDTO document in this.DocumentosDTO)
                    {
                        if (document.IdIdioma == IdLanguage)
                        {
                            string docObs = (document.Observaciones!=""? document.Observaciones : "[DOC]");
                            links += "<p><a href='" + urlMainBenefitsDocumentLinks +
                                document.IdDocumento.ToString() + "'>" + docObs + "</a></p>";
                        }
                    }
                    links += "</p>";
                }
            }

            links = (links == "<p></p>" ? "" : links);
            return links;
        }
    }
}
