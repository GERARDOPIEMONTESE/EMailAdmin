using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Service.Interfaces;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Home;
using System.Configuration;
using EMailAdmin.BackEnd.Properties;
using CapaNegocioDatos.CapaHome;
using CapaNegocioDatos.CapaNegocio;
using EMailAdmin.ExternalServices.Service;

namespace EMailAdmin.BackEnd.Service
{
    public class CapitaService : ICapitaService 
    {
        public void CompleteInformation(DTO.AbstractEMailDTO dto)
        {
            EmailCapitaDTO dtoCapita = (EmailCapitaDTO)dto;            
            dto.TemplateType = TemplateTypeHome.GetCapita();
            dto.MailFromAppearance = ConfigurationManager.AppSettings["CapitaMailFromAppearance"].ToString();
            dto.ApplicationUrl = ConfigurationValueHome.GetApplicationUrl();
            dto.RecipientName = dtoCapita.Nombre;
            dto.RecipientSurname = dtoCapita.Apellido;
            Pais pais = PaisHome.ObtenerPorCodigo(dtoCapita.CountryCode.ToString());
            dto.IdLanguage = dto.IdLanguage;
            dto.CountryName = pais.Nombre;
            dto.CountryCode = dto.CountryCode;            
            dtoCapita.DocumentosDTO = ExternalServiceLocator.Instance().GetCondicionesService().GetDocumentsInformation(dtoCapita.CountryCode,
                dtoCapita.ProductCode,
                dtoCapita.RateCode);
            dtoCapita.BenefitsText = dtoCapita.GetLinks();
        }
    }
}
