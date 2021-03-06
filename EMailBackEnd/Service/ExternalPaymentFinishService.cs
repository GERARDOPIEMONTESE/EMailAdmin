using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Service.Interfaces;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.BackEnd.Service
{
    public class ExternalPaymentFinishService : IExternalPaymentFinishService
    {
        public void CompleteInformation(AbstractEMailDTO dto){
            EmailExternalPaymentFinishDTO botonPagoFinish = (EmailExternalPaymentFinishDTO)dto;
            botonPagoFinish.TemplateType = TemplateTypeHome.GetBotonPagoFinish();
            botonPagoFinish.IdLanguage = 1;
        }
    }
}
