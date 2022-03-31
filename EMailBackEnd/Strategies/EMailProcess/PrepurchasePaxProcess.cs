using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.BackEnd.DTO;
using CapaNegocioDatos.Servicios;
using EMailAdmin.BackEnd.Domain.External;

namespace EMailAdmin.BackEnd.Strategies.EMailProcess
{
    public class PrepurchasePaxProcess : AbstractEMailProcess
    {
        public override string GetTypeCode()
        {
            return "PrepurchasePax";
        }

        protected override void SendMails()
        {
            SendMails(-1);
        }

        protected override void SendMails(int id)
        {
            if (!ServicioBroker.Instancia().ObtenerServicioCodigoActivador().ValidarHabilitarPrepurchasePaxMail())
            {
                return;
            }

            var dto = new EMailPrepurchasePaxDTO();
            dto.BoxPaxCode = id;
            ServiceLocator.Instance().GetSendMailService().SendMailPrepurchasePax(dto);
        }

    }
}
