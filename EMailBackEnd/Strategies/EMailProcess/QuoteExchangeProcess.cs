using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Service;
using CapaNegocioDatos.Servicios;

namespace EMailAdmin.BackEnd.Strategies.EMailProcess
{
    public class QuoteExchangeProcess : AbstractEMailProcess
    {
        public override string GetTypeCode()
        {
            return "QuoteEx";
        }

        protected override void SendMails()
        {
            SendMails(-1);
        }

        protected override void SendMails(int id)
        {
            if (!ServicioBroker.Instancia().ObtenerServicioCodigoActivador().ValidarHabilitarQuoteExchangeMail())
            {
                return;
            }

            QuoteExchangeDTO dto = new QuoteExchangeDTO();
            dto.ModuleCode = "QuoteExchange";
            dto.CountryCode = 540;
            dto.IdLanguage = 1;

            ServiceLocator.Instance().GetSendMailService().SendMailQuoteExchange(dto);
        }
    }
}
