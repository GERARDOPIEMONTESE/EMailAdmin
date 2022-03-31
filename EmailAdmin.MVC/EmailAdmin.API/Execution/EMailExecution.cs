using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CapaNegocioDatos.CapaHome;
using CapaNegocioDatos.CapaNegocio;
using EmailAdmin.Dto;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Service;
using Newtonsoft.Json;

namespace EmailAdmin.Api
{
    public class EMailDynamicExecution
    {
        public SendEmailDto dtoSendEmail { get; set; }
                
        public void ExecuteSendDynamicMail()
        {
            DynamicDTO dto = new DynamicDTO();
            dto.CountryCode = dtoSendEmail.CountryCode;
            dto.ModuleCode = dtoSendEmail.ModuleCode;
            dto.TemplateCode = dtoSendEmail.TemplateCode;
            dto.StrategyData = dtoSendEmail.StrategyCode;
            dto.CultureUI = dtoSendEmail.UICulture;
            dto.EmailListCode = dtoSendEmail.EmailListCode;            
            dto.To = dtoSendEmail.To;
            dto.Cc = dtoSendEmail.Cc;
            dto.Bcc = dtoSendEmail.Bcc;
            dto.SetdicValues(dtoSendEmail.data);

            if (!string.IsNullOrEmpty(dto.ModuleCode) && dto.Module== null)
                dto.Module = ModuloHome.ObtenerPorNombre(dto.ModuleCode);

            //para que se vea en el log
            dto.XmlContextInformation = JsonConvert.SerializeObject(dtoSendEmail);
            ServiceLocator.Instance().GetSendMailService().SendMailDynamic(dto);
        }

        public byte[] GetDynamicPDF(bool IsTag)
        {
            DynamicDTO dto = new DynamicDTO();
            dto.CountryCode = dtoSendEmail.CountryCode;
            dto.ModuleCode = dtoSendEmail.ModuleCode;
            dto.TemplateCode = dtoSendEmail.TemplateCode;
            dto.StrategyData = dtoSendEmail.StrategyCode;
            dto.CultureUI = dtoSendEmail.UICulture;
            dto.SetdicValues(dtoSendEmail.data);

            if (!string.IsNullOrEmpty(dto.ModuleCode) && dto.Module == null)
                dto.Module = ModuloHome.ObtenerPorNombre(dto.ModuleCode);

            var pdf = ServiceLocator.Instance().GetSendMailService().GetPDFMail(dto, IsTag);

            return pdf;
        }
    }
}