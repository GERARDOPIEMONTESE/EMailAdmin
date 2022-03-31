using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Service.Interfaces;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Strategies.EMailSender;
using CapaNegocioDatos.CapaNegocio;
using CapaNegocioDatos.CapaHome;

namespace EMailAdmin.BackEnd.Service
{
    public class BotonPagoService : IBotonPagoService
    {

        public void CompleteInformation(AbstractEMailDTO dto)
        {
            //sobreescribe el tipo de template
            EmailBotonPagoDTO dtoBotonPago = (EmailBotonPagoDTO)dto;
            dtoBotonPago.TemplateType = TemplateTypeHome.GetBotonPago();
        }
    }
}