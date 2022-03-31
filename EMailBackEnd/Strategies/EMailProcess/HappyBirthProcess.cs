using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.BackEnd.DTO;
using CapaNegocioDatos.Servicios;
using CapaNegocioDatos.CapaNegocio;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.BackEnd.Strategies.EMailProcess
{
    public class HappyBirthProcess : AbstractEMailProcess
    {
        public override string GetTypeCode()
        {
            return "HappyB";
        }

        protected override void SendMails(int id)
        {
            throw new NotImplementedException();
        }

        protected override void SendMails()
        {            
            if (!ServicioBroker.Instancia().ObtenerServicioCodigoActivador().ValidarHabilitarHappyBirthday())
            {
                return;
            }

            try
            {
                var lst = ServiceLocator.Instance().GetSendMailPaxCumpleanosServices().GetAll();

                var templateType = TemplateTypeHome.GetHappyBirth();

                foreach (var item in lst)
                {
                    if (item.debug || (!item.debug && ServiceLocator.Instance().GetSendMailPaxCumpleanosServices().CheckSendEmail(item, templateType.Id)))
                    {
                        var dto = new EMailEkitDTO();
                        dto.To = item.EMAIL;
                        dto.ModuleCode = "EMailAdmin";
                        dto.PaxPassport = string.Concat(item.COUNTRYCODE.ToString(), item.NATIONALID);
                        dto.CountryCode = item.COUNTRYCODE;
                        dto.RecipientName = item.NAME;
                        dto.RecipientSurname = item.SURNAME;
                        dto.RecipientFullName = item.NAME + " " + item.SURNAME;
                        dto.RecipientDocumentNumber = item.NATIONALID;                        
                        dto.PaxPhone = item.CELLPHONE;
                        dto.BirthDate = item.BIRTHDATE.ToShortDateString();
                        dto.IdLanguage = GetIdIdioma(item.LANGUAGE);
                        dto.IdClienteUnico = item.IDCLIENTEUNICO;
                        dto.TemplateType = templateType;

                        ServiceLocator.Instance().GetSendMailService().SendMailHappyBirthday(dto);
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public int GetIdIdioma(string language)
        {
            switch (language)
            {
                case "es": return Convert.ToInt32(Idioma.ESPANOL);
                case "en": return Convert.ToInt32(Idioma.INGLES);
                case "pt": return Convert.ToInt32(Idioma.PORTUGUES);
                default: return Convert.ToInt32(Idioma.ESPANOL);
            }
        }
    }
}
