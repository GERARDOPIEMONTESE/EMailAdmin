using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Service.Interfaces;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Domain.External;
using EMailAdmin.ExternalServices.Service;
using EMailAdmin.BackEnd.Utils;
using System.Configuration;

namespace EMailAdmin.BackEnd.Service
{
    public class PrepurchasePaxService : IPrepurchasePaxService
    {
        private string BoxPaxEmailToTest = (ConfigurationManager.AppSettings["BoxPaxEmailToTest"]!=null? ConfigurationManager.AppSettings["BoxPaxEmailToTest"].ToString() : "");

        public void CompleteInformation(DTO.AbstractEMailDTO dto)
        {
            EMailPrepurchasePaxDTO ekitDto = (EMailPrepurchasePaxDTO)dto;

            PrepurchasePaxInformation prepurchasePaxInformation = GetInfo(ekitDto);

            if (ekitDto.PaxEMail != null && ekitDto.PaxEMail != "") 
                dto.To = ekitDto.PaxEMail;
            else
                dto.To = (BoxPaxEmailToTest == "" ? prepurchasePaxInformation.PaxEMail : BoxPaxEmailToTest);

            ekitDto.RecipientFullName = prepurchasePaxInformation.PaxSurname + " " + prepurchasePaxInformation.PaxName;
            ekitDto.Days = prepurchasePaxInformation.Days;
            ekitDto.BoxPaxCode = prepurchasePaxInformation.BoxPaxCode;
            ekitDto.BoxPaxPricePaid = prepurchasePaxInformation.BoxPaxPricePaid;
            ekitDto.BoxPaxCodeVerifier = prepurchasePaxInformation.BoxPaxCodeVerifier;
            ekitDto.ProductName = prepurchasePaxInformation.Product_Name;
            ekitDto.ProductCode = prepurchasePaxInformation.Product;
            ekitDto.CountryCode = prepurchasePaxInformation.CountryCode;
            ekitDto.EffectiveStartDateFormat = DateUtil.FormatToShortDate(
                DateUtil.ConvertToDate(prepurchasePaxInformation.EffectiveStartDate), dto.IdLanguage);
            ekitDto.EffectiveEndDateFormat = DateUtil.FormatToShortDate(
                DateUtil.ConvertToDate(prepurchasePaxInformation.EffectiveEndDate), dto.IdLanguage);
            ekitDto.BoxPaxPasajeros = FormatLanguage(prepurchasePaxInformation.passengers, dto.IdLanguage);
        }

        private Passenger[] FormatLanguage(Passenger[] passenger, int idLanguage)
        {
            foreach (var item in passenger)
            {
                item.fechaVigenciaDesdeFormat = DateUtil.FormatToShortDate(item.fechaVigenciaDesde, idLanguage);
                item.fechaVigenciaHastaFormat= DateUtil.FormatToShortDate(item.fechaVigenciaHasta, idLanguage);
            }

            return passenger;
        }

        private PrepurchasePaxInformation GetInfo(EMailPrepurchasePaxDTO ekitDto)
        {
            if (ekitDto.TemplateType.Codigo == "BoxPaxBalance" || ekitDto.TemplateType.Codigo == "BoxPaxCancel")
            {
                if (ekitDto.BoxPaxCode>0 && ekitDto.groupVoucher != "" && ekitDto.CountryCode > 0)
                    return ExternalServiceLocator.Instance().GetPrepurchasePaxService().Get(ekitDto.BoxPaxCode, ekitDto.groupVoucher, ekitDto.CountryCode);
                else
                    throw new Exception("DATABOXPAXSEARCH");
            }
            else
                if (ekitDto.BoxPaxCode>0)
                    return ExternalServiceLocator.Instance().GetPrepurchasePaxService().Get(ekitDto.BoxPaxCode);
                else
                    throw new Exception("DATABOXPAXBUYSEARCH");
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

    }
}
