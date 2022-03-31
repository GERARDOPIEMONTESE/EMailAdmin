using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaNegocioDatos.Servicios;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.DTO;
using CapaNegocioDatos.CapaNegocio;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Strategies.EMailProcess
{
   public class ContinuaTuCompraProcess : AbstractEMailProcess
    {
        public override string GetTypeCode()
        {
            return "KEEPBUY";
        }

        protected override void SendMails(int id)
        {
            throw new NotImplementedException();
        }

        public override int? GetIdLote()
        {
            return ServiceLocator.Instance().GetSendMailPaxContinuaCompraService().GetIdLote();
        }

        protected override void SendMails()
        {
            if (!ConfigurationValueHome.ValidarHabilitarContinuaTuCompra())
            {
                return;
            }

            try
            {
                var lst = ServiceLocator.Instance().GetSendMailPaxContinuaCompraService().GetAll();                
               
                if (lst.Count>0)
                {
                    var templateType = TemplateTypeHome.GetContinuaTuCompra();

                    foreach (var item in lst)
                    {
                        var dto = new ContinuaCompraDTO();
                        dto.To = item.EMAIL;
                        dto.ModuleCode = "EMailAdmin";
                        dto.RecipientFullName = item.FULLNAME;
                        dto.RecipientName = item.FULLNAME;
                        dto.ISO2Code = item.ISO2CODE;
                        int codigoPais = 0;
                        var pais = CapaNegocioDatos.CapaHome.PaisHome.ObtenerPorCodigoISOA2(item.ISO2CODE);
                        int.TryParse(pais.Codigo, out codigoPais);
                        dto.CountryCode = codigoPais;
                        dto.Code = item.URLBASE64;
                        dto.IdQuoteLog = item.IDQUOTELOG;
                        dto.IdLanguage = GetIdIdioma(item.LANGUAGE);
                        dto.TemplateType = templateType;
                        dto.IdLote = item.LISTA_ID;
                        dto.IdClienteUnico = item.IDCLIENTEUNICO;
                        ServiceLocator.Instance().GetSendMailService().SendMailContinuaCompra(dto);
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
