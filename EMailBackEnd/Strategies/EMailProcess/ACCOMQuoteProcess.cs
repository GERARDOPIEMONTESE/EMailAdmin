using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaNegocioDatos.Servicios;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.BackEnd.DTO;

namespace EMailAdmin.BackEnd.Strategies.EMailProcess
{
    public class ACCOMQuoteProcess : AbstractEMailProcess
    {
        public override string GetTypeCode()
        {
            return "ACCOMQuote";
        }

        protected override void SendMails()
        {
            SendMails(-1);
        }

        protected override void SendMails(int id)
        {
            if (!ServicioBroker.Instancia().ObtenerServicioCodigoActivador().ValidarHabilitarACCOMQuoteMail())
            {
                return;
            }

            ACCOMQuoteDTO dto = new ACCOMQuoteDTO();
            dto.ModuleCode = "ACCOM";
            dto.IdLanguage = 1;

            ServiceLocator.Instance().GetSendMailService().SendMailACCOMQuote(dto);
        }
    }
}
