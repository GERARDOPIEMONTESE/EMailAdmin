using CapaNegocioDatos.CapaHome;
using EmailAdmin.Dto;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailAdmin.Api
{
    public class TemplateDynamicExecution
    {
        public SendEmailDto dtoSendEmail { get; set; }

        public AbstractEMailDTO ExecuteDynamicTemplate()
        {
            DynamicDTO dto = new DynamicDTO();
            AbstractEMailDTO result = null;
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

            if (!string.IsNullOrEmpty(dto.ModuleCode) && dto.Module == null)
                dto.Module = ModuloHome.ObtenerPorNombre(dto.ModuleCode);

            try
            {
                dto.XmlContextInformation = JsonConvert.SerializeObject(dtoSendEmail);
                result = ServiceLocator.Instance().GetSendMailService().TemplateDynamic(dto);
            }
            catch (Exception ex)
            {
                result = null;
            }
            
            return result;
        }

        public byte[] GetDynamicPDF()
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

            var pdf = ServiceLocator.Instance().GetSendMailService().GetPDFMail(dto);

            return pdf;
        }
    }
}