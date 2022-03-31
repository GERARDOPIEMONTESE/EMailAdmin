using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.BackEnd.DTO;
using CapaNegocioDatos.CapaHome;
using CapaNegocioDatos.CapaNegocio;
using CapaNegocioDatos.ExcepcionesPersonalizadas;
using FrameworkDAC.Negocio;
using System.Web.Configuration;
using System.Security.Cryptography;
using System.Configuration;
using System.Text;
using EMailAdmin.Services.Properties;
using System.Web.Hosting;
using CapaNegocioDatos.Utilitarios;
using System.Threading;
using EMailAdmin.Services.Execution;
using CapaNegocioDatos.Servicios;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.Services.Request;
//

namespace EMailAdmin.Services
{
    /// <summary>
    /// Summary description for EMailSenderService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class EMailSenderService : System.Web.Services.WebService
    {
        [WebMethod]
        public string SendMailEkit(int countryCode, string voucherCode, string moduleCode, 
            string user, string password)
        {
            try
            {
                ServicioBroker.Instancia().ObtenerServicioUsuario().ChequearUsuario(user, password);

                EMailExecution execution = new EMailExecution();

                execution.CountryCode = countryCode;
                execution.VoucherCode = voucherCode.Trim();
                execution.ModuleCode = moduleCode.Trim();                
                
                Thread oThread = new Thread(new ThreadStart(execution.ExecuteSendMail));
                oThread.Start(); 

                return "OK " + countryCode + " " + voucherCode;
            }
            catch (Exception ex)
            {
                EMailLog log = new EMailLog();
                log.CountryCode = countryCode;
                log.VoucherCode = voucherCode;
                log.ModuleCode = moduleCode;
                log.InvokeInformation = "User: " + user + " - Password: " + password;
                log.ContextInformation = new byte[] {};
                log.ErrorMessage = ex.Message;
                log.IdTemplateType = 0;
                log.Fecha = DateTime.Now;
                log.EndDate = DateTime.Now;
                log.ProcessStatus = EMailLog.ERROR;
                log.IdEstado = ObjetoNegocio.Creado();
                   
                ServiceLocator.Instance().GetEMailLogService().SaveLog(log);
                return "ERROR: " + ex.Message; 
            }
        }

        [WebMethod]
        public string SendMultipleMailEkit(int countryCode, string voucherCodes, string moduleCode, 
            string user, string password)
        {
            try
            {
                ServicioBroker.Instancia().ObtenerServicioUsuario().ChequearUsuario(user, password);

                EMailEkitVouchersDTO vouchersDto = new EMailEkitVouchersDTO();

                vouchersDto = (EMailEkitVouchersDTO)ServicioConversionXml.Instancia().
                    DeserializeObject(voucherCodes, vouchersDto.GetType());

                EMailMultipleExecution execution = new EMailMultipleExecution();

                execution.CountryCode = countryCode;
                execution.VouchersCode = vouchersDto.VoucherCodes;
                execution.ModuleCode = moduleCode.Trim();

                Thread oThread = new Thread(new ThreadStart(execution.ExecuteSendMail));
                oThread.Start();

                //if (vouchersDto.VoucherCodes != null)
                //{
                //    foreach (string voucherCode in vouchersDto.VoucherCodes)
                //    {
                //        EMailExecution execution = new EMailExecution();

                //        execution.CountryCode = countryCode;
                //        execution.VoucherCode = voucherCode.Trim();
                //        execution.ModuleCode = moduleCode.Trim();

                //        Thread oThread = new Thread(new ThreadStart(execution.ExecuteSendMail));
                //        oThread.Start();
                //    }
                //}

                return "OK";
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message;
            }
        }

        [WebMethod]
        public string SendMultipleMailEkitTo(int countryCode, string voucherCodes, string emails, string moduleCode,
            string user, string password)
        {
            try
            {
                ServicioBroker.Instancia().ObtenerServicioUsuario().ChequearUsuario(user, password);

                EMailEkitVouchersDTO vouchersDto = new EMailEkitVouchersDTO();

                vouchersDto = (EMailEkitVouchersDTO)ServicioConversionXml.Instancia().
                    DeserializeObject(voucherCodes, vouchersDto.GetType());

                EMailMultipleExecution execution = new EMailMultipleExecution();

                execution.CountryCode = countryCode;
                execution.VouchersCode = vouchersDto.VoucherCodes;
                execution.ModuleCode = moduleCode.Trim();
                execution.Emails = emails;

                Thread oThread = new Thread(new ThreadStart(execution.ExecuteSendMail));
                oThread.Start();

                return "OK";
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message;
            }
        }

        [WebMethod]
        public void InitEMailProcess(string user, string password)
        {
            ServicioBroker.Instancia().ObtenerServicioUsuario().ChequearUsuario(user, password);
         
            Thread oThread = new Thread(new ThreadStart(this.ProcessEmails));
            oThread.Start();

            return;
        }

        public void ProcessEmails()
        {
            ServiceLocator.Instance().GetSendMailService().ProcessEMails();
        }

        [WebMethod]
        public string SendMailCancelacionPoliza(int countryCode, string voucherCode, string polizaId, string moduleCode, string user, string password)
        {
            try
            {
                ServicioBroker.Instancia().ObtenerServicioUsuario().ChequearUsuario(user, password);

                EMailPolizaExecution execution = new EMailPolizaExecution();
                execution.Cancelacion = true;
                execution.CountryCode = countryCode;
                execution.VoucherCode = voucherCode.Trim();
                execution.ModuleCode = moduleCode.Trim();
                execution.PolizaVoidId = polizaId.Trim();

                Thread oThread = new Thread(new ThreadStart(execution.ExecuteSendMail));
                oThread.Start();

                return "OK " + countryCode + " " + voucherCode;
            }
            catch (Exception ex)
            {
                EMailLog log = new EMailLog();
                log.CountryCode = countryCode;
                log.VoucherCode = voucherCode;
                log.ModuleCode = moduleCode;
                log.InvokeInformation = "User: " + user + " - Password: " + password;
                log.ContextInformation = new byte[] { };
                log.ErrorMessage = ex.Message;
                log.IdTemplateType = 0;
                log.Fecha = DateTime.Now;
                log.EndDate = DateTime.Now;
                log.ProcessStatus = EMailLog.ERROR;
                log.IdEstado = ObjetoNegocio.Creado();

                ServiceLocator.Instance().GetEMailLogService().SaveLog(log);
                return "ERROR: " + ex.Message;
            }
        }

        [WebMethod]
        public string SendMailBalanceRequest(MailBalanceRequest request, string user, string password)
        {
            try
            {
                ServicioBroker.Instancia().ObtenerServicioUsuario().ChequearUsuario(user, password);
                
                Thread oThread = new Thread(new ThreadStart(request.ExecuteSendMail));
                oThread.Start();

                return "OK";
            }
            catch (Exception ex)
            {
                EMailLog log = new EMailLog();
                log.Body = request.ToString();
                log.InvokeInformation = "User: " + user + " - Password: " + password;
                log.ContextInformation = new byte[] { };
                log.ErrorMessage = ex.Message;
                log.IdTemplateType = 0;
                log.Fecha = DateTime.Now;
                log.EndDate = DateTime.Now;
                log.ProcessStatus = EMailLog.ERROR;
                log.IdEstado = ObjetoNegocio.Creado();

                ServiceLocator.Instance().GetEMailLogService().SaveLog(log);
                return "ERROR: " + ex.Message;
            }
        }

        [WebMethod]
        public string SendMailEndoso(int CountryCode, string VoucherCode, string FullName, string Email, string ModuleCode, string user, string password)
        {
            try
            {
                ServicioBroker.Instancia().ObtenerServicioUsuario().ChequearUsuario(user, password);

                EmailEndosoExecution execution = new EmailEndosoExecution();
                execution.CountryCode = CountryCode;
                execution.VoucherCode = VoucherCode;
                execution.ModuleCode = ModuleCode;
                execution.FullName = FullName;
                execution.Email = Email;

                Thread thread = new Thread(new ThreadStart(execution.ExecuteSendMail));
                thread.Start();

                return "OK " + CountryCode + " " + VoucherCode;
            }
            catch (Exception e)
            {
                EMailLog log = new EMailLog();
                log.CountryCode = CountryCode;
                log.VoucherCode = VoucherCode;
                log.ModuleCode = ModuleCode;
                log.InvokeInformation = "User: " + user + " - Password: " + password;
                log.ContextInformation = new byte[] { };
                log.ErrorMessage = e.Message;
                log.IdTemplateType = 0;
                log.Fecha = DateTime.Now;
                log.EndDate = DateTime.Now;
                log.ProcessStatus = EMailLog.ERROR;
                log.IdEstado = ObjetoNegocio.Creado();

                ServiceLocator.Instance().GetEMailLogService().SaveLog(log);
                return "ERROR: " + e.Message;
            }
        }

        [WebMethod]
        public string SendMailModificacionPoliza(int countryCode, string voucherCode, string polizaId, string polizaVoidId, string moduleCode, string user, string password)
        {
            try
            {
                ServicioBroker.Instancia().ObtenerServicioUsuario().ChequearUsuario(user, password);

                EMailPolizaExecution execution = new EMailPolizaExecution();
                execution.Cancelacion = false;
                execution.CountryCode = countryCode;
                execution.VoucherCode = voucherCode.Trim();                
                execution.ModuleCode = moduleCode.Trim();
                execution.PolizaId = polizaId.Trim();
                execution.PolizaVoidId = polizaVoidId.Trim();

                Thread oThread = new Thread(new ThreadStart(execution.ExecuteSendMail));
                oThread.Start();

                return "OK " + countryCode + " " + voucherCode;
            }
            catch (Exception ex)
            {
                EMailLog log = new EMailLog();
                log.CountryCode = countryCode;
                log.VoucherCode = voucherCode;
                log.ModuleCode = moduleCode;
                log.InvokeInformation = "User: " + user + " - Password: " + password;
                log.ContextInformation = new byte[] { };
                log.ErrorMessage = ex.Message;
                log.IdTemplateType = 0;
                log.Fecha = DateTime.Now;
                log.EndDate = DateTime.Now;
                log.ProcessStatus = EMailLog.ERROR;
                log.IdEstado = ObjetoNegocio.Creado();

                ServiceLocator.Instance().GetEMailLogService().SaveLog(log);
                return "ERROR: " + ex.Message;
            }
        }

        [WebMethod]
        public string SendMailCotizacion(MailCotizacion cotizacion, string user, string password)
        {
            try
            {
                ServicioBroker.Instancia().ObtenerServicioUsuario().ChequearUsuario(user, password);

                //MailCotizacion execution = new MailCotizacion();
               // execution = (MailCotizacion)ServicioConversionXml.Instancia().DeserializeObject(xml, execution.GetType());
                Thread oThread = new Thread(new ThreadStart(cotizacion.ExecuteSendMail));
                oThread.Start();

                return "OK " + cotizacion.Productos[0].Country + " " + cotizacion.Productos[0].ProductName;
            }
            catch (Exception ex)
            {
                EMailLog log = new EMailLog();
                log.Body = cotizacion.ToString();
                log.InvokeInformation = "User: " + user + " - Password: " + password;
                log.ContextInformation = new byte[] { };
                log.ErrorMessage = ex.Message;
                log.IdTemplateType = 0;
                log.Fecha = DateTime.Now;
                log.EndDate = DateTime.Now;
                log.ProcessStatus = EMailLog.ERROR;
                log.IdEstado = ObjetoNegocio.Creado();

                ServiceLocator.Instance().GetEMailLogService().SaveLog(log);
                return "ERROR: " + ex.Message;
            }
        }

        [WebMethod]
        public string SendMailBotonDePago(int countryCode, string voucherCode, string url, string moduleCode, string emailTo, int idiom, string user, string password)
        {
            try
            {
                ServicioBroker.Instancia().ObtenerServicioUsuario().ChequearUsuario(user, password);

                EMailBotonPagoExecution execution = new EMailBotonPagoExecution();
                execution.EmailTo = emailTo;
                execution.ModuleCode = moduleCode;
                execution.Url = url;
                execution.countryCode = countryCode;
                execution.voucherCode = voucherCode;
                execution.Idiom = idiom;

                Thread oThread = new Thread(new ThreadStart(execution.ExecuteSendMail));
                oThread.Start();

                return "OK Email boton de pago a " + emailTo;
            }
            catch (Exception ex)
            {
                EMailLog log = new EMailLog();
                log.CountryCode = countryCode;
                log.VoucherCode = voucherCode;
                log.ModuleCode = moduleCode;
                log.InvokeInformation = "User: " + user + " - Password: " + password;
                log.ContextInformation = new byte[] { };
                log.ErrorMessage = ex.Message;
                log.IdTemplateType = 0;
                log.Fecha = DateTime.Now;
                log.EndDate = DateTime.Now;
                log.ProcessStatus = EMailLog.ERROR;
                log.IdEstado = ObjetoNegocio.Creado();

                ServiceLocator.Instance().GetEMailLogService().SaveLog(log);
                return "ERROR: " + ex.Message;
            }
        }

        [WebMethod]
        public string SendMailExternalPaymentFinish(ExternalPaymentFinishRequest request, string User, string Password)
        {
            try
            {
                ServicioBroker.Instancia().ObtenerServicioUsuario().ChequearUsuario(User, Password);

                EmailExternalPaymentFinishExecution execution = new EmailExternalPaymentFinishExecution();
                foreach(Passenger item in request.Passengers){
                    execution.FullName += item.clientName + " - " + item.voucherCode + "</br>";
                }
                execution.CountryCode = request.CountryCode;
                execution.Amount = request.Total;
                execution.EmailTo = request.EmailTo;
                execution.IdLanguage = request.IdLanguage;
                Thread oThread = new Thread(new ThreadStart(execution.ExecuteSendMail));
                oThread.Start();

                return "Mail enviado a " + request.EmailTo;
            }
            catch (Exception ex)
            {
                EMailLog log = new EMailLog();
                //log.CountryCode = CountryCode;
                //log.VoucherCode = VoucherCode;
                log.ModuleCode = "ACNET";
                log.InvokeInformation = "User: " + User + " - Password: " + Password;
                log.ContextInformation = new byte[] { };
                log.ErrorMessage = ex.Message;
                log.IdTemplateType = 0;
                log.Fecha = DateTime.Now;
                log.EndDate = DateTime.Now;
                log.ProcessStatus = EMailLog.ERROR;
                log.IdEstado = ObjetoNegocio.Creado();

                ServiceLocator.Instance().GetEMailLogService().SaveLog(log);
                return "Error al enviar el mail de confirmacion a " + request.EmailTo;
            }
        }
    }
}
