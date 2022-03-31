using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using CapaNegocioDatos.Servicios;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.Services.Execution;
using System.Threading;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Negocio;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.Services
{
    /// <summary>
    /// Summary description for EMailSenderPrepPaxService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class EMailSenderPrepPaxService : System.Web.Services.WebService
    {
        [WebMethod]
        public void SendEMailBuy( int boxPaxCode, string user, string password)
        {
            SendEmail( setDTO("BoxPaxBuy", boxPaxCode), user, password);
        }

        [WebMethod]
        public void SendEMailBalance(int boxPaxCode, string group, int countryCode, string user, string password)        
        {
            EMailPrepurchasePaxDTO dto = setDTO("BoxPaxBalance", boxPaxCode);
            dto.groupVoucher = group;
            dto.CountryCode = countryCode;
            SendEmail(dto, user, password);
        }

        [WebMethod]
        public void SendEMailCancel(int boxPaxCode, int countryCode, string user, string password)
        {
            EMailPrepurchasePaxDTO dto = setDTO("BoxPaxCancel", boxPaxCode);
            dto.groupVoucher = "0";
            dto.CountryCode = countryCode;
            SendEmail(dto, user, password);
        }
        
        private void SendEmail( EMailPrepurchasePaxDTO dto, string user, string password)
        {
            ServicioBroker.Instancia().ObtenerServicioUsuario().ChequearUsuario(user, password);
            ServiceLocator.Instance().GetSendMailService().SendMailPrepurchasePax(dto);
        }

        private EMailPrepurchasePaxDTO setDTO(string KeyTemplate, int boxPaxCode)
        {
            EMailPrepurchasePaxDTO dto = new EMailPrepurchasePaxDTO()
            {
                BoxPaxCode = boxPaxCode,
                TemplateType = TemplateTypeHome.Get(KeyTemplate),
                ModuleCode = "ACCOM",
                IdLanguage = 1,
                VoucherCode = boxPaxCode.ToString()
            };

            return dto;
        }
    }
}
