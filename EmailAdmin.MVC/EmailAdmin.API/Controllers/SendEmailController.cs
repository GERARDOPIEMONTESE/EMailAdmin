using System;
using System.Threading;
using System.Web.Http;
using CapaNegocioDatos.Servicios;
using DTOMapper.Helpers;
using DTOMapper;
using EmailAdmin.Dto;
using EMailAdmin.BackEnd.Utils;
using EMailAdmin.BackEnd.Home;
using EmailAdmin.Api.Utilities;
using System.Collections.Generic;
using EMailAdmin.BackEnd.Service;
using EmailAdmin.Api;
using EMailAdmin.BackEnd.DTO;

namespace EmailAdmin.Api.Controllers
{
    public class SendEmailController : ApiController
    {
        [HttpPost]
        public DTOResponseListScalar<string> GetEmailListRecipients(DTOFilter filter)
        {
            try
            {
                APISecurityHelper.CheckApiAuthentication(filter);
                int countryCode = RequestParameterHelper.GetGenericParameter<int>(filter, "CountryCode");
                string emailListTypeCode = RequestParameterHelper.GetGenericParameter<string>(filter, "EmailListTypeCode");
                return DTOResponseListScalar<string>.GetListScalarOkResponse(EmailRecipientsHelper.GetEmailListTypeRecipients(countryCode, emailListTypeCode));
            }
            catch (HttpErrorCustomizedException ex)
            {
                DTOApplicationErrorData dtoApplicationErrorData = ex.ApplicationErrorData;
                ApplicationEventLogHelper.LogApplicationErrorDataCatched(ref dtoApplicationErrorData);
                return DTOResponseListScalar<string>.GetApplicationErrorDataResponse(dtoApplicationErrorData);
            }
            catch (Exception ex)
            {
                DTOApplicationErrorData applicationErrorData = ApplicationEventLogHelper.LogErrorCatched(ex, filter);
                return DTOResponseListScalar<string>.GetApplicationErrorDataResponse(applicationErrorData);
            }
        }

        [HttpPost]
        public DTOResponseExecution SendDynamicEmail(DTOFilter filter)
        {
            //Este método a diferencia del obsoleto NO es asincrónico. Lo que debe ser asincrónico, es el llamado a esta api
            //NO estoy poniendo que loguee los llamados, porque no hay key para activar/desactivarlo
            try
            {
                APISecurityHelper.CheckApiAuthentication(filter);
                int countryCode = RequestParameterHelper.GetGenericParameter<int>(filter, "CountryCode");
                string moduleCode = RequestParameterHelper.GetGenericParameter<string>(filter, "ModuleCode");
                string templateCode = RequestParameterHelper.GetGenericParameter<string>(filter, "TemplateCode");
                string strategyCode = RequestParameterHelper.GetGenericOptionalParameter<string>(filter, "StrategyCode");
                string uiCulture = RequestParameterHelper.GetGenericParameter<string>(filter, "UICulture");
                string cc = RequestParameterHelper.GetGenericOptionalParameter<string>(filter, "Cc");
                string bcc = RequestParameterHelper.GetGenericOptionalParameter<string>(filter, "Bcc");
                Dictionary<string, object> data = RequestParameterHelper.GetGenericOptionalParameter<Dictionary<string, object>>(filter, "Data");
                string emailListCodeParameterName = "EmailListCode";
                string toParameterName = "To";
                string emailListCode = RequestParameterHelper.GetGenericOptionalParameter<string>(filter, emailListCodeParameterName);
                string to = RequestParameterHelper.GetGenericOptionalParameter<string>(filter, toParameterName);

                KeyValuePair<string, object> elcKvp = new KeyValuePair<string, object>(emailListCodeParameterName, emailListCode);
                KeyValuePair<string, object> toKvp = new KeyValuePair<string, object>(toParameterName, to);
                RequestParameterHelper.CheckParameterCombination(RequestParameterHelper.ParameterCombinationFlags.OneAsTooManyCanBeAssigned | RequestParameterHelper.ParameterCombinationFlags.AtLeastOneMustBeAssigned, elcKvp, toKvp);


                SendEmailDto sendEmailDto = new Dto.SendEmailDto();
                sendEmailDto.user = RequestParameterHelper.GetGenericParameter<string>(filter, "User"); ;
                sendEmailDto.password = RequestParameterHelper.GetGenericParameter<string>(filter, "Password"); ;
                sendEmailDto.CountryCode = countryCode;
                sendEmailDto.ModuleCode = moduleCode;
                sendEmailDto.TemplateCode = templateCode;
                sendEmailDto.StrategyCode = strategyCode;
                sendEmailDto.UICulture = uiCulture;
                sendEmailDto.EmailListCode = emailListCode;
                sendEmailDto.To = to;
                sendEmailDto.Cc = cc;
                sendEmailDto.Bcc = bcc;
                sendEmailDto.data = data;

                EMailDynamicExecution execution = new EMailDynamicExecution() { dtoSendEmail = sendEmailDto };
                execution.ExecuteSendDynamicMail();
                return DTOResponseExecution.GetExecutedOkResponse();
            }
            catch (HttpErrorCustomizedException ex)
            {
                DTOApplicationErrorData dtoApplicationErrorData = ex.ApplicationErrorData;
                ApplicationEventLogHelper.LogApplicationErrorDataCatched(ref dtoApplicationErrorData);
                return DTOResponseExecution.GetApplicationErrorDataResponse(dtoApplicationErrorData);
            }
            catch (Exception ex)
            {
                DTOApplicationErrorData applicationErrorData = ApplicationEventLogHelper.LogErrorCatched(ex, filter);
                return DTOResponseExecution.GetApplicationErrorDataResponse(applicationErrorData);
            }
        }

        //[HttpPost]
        //public DTOResponseExecution GetDynamicTemplate2(DTOFilter filter)
        //{
        //    //Este método a diferencia del obsoleto NO es asincrónico. Lo que debe ser asincrónico, es el llamado a esta api
        //    //NO estoy poniendo que loguee los llamados, porque no hay key para activar/desactivarlo
        //    try
        //    {
        //        APISecurityHelper.CheckApiAuthentication(filter);
        //        int countryCode = RequestParameterHelper.GetGenericParameter<int>(filter, "CountryCode");
        //        string moduleCode = RequestParameterHelper.GetGenericParameter<string>(filter, "ModuleCode");
        //        string templateCode = RequestParameterHelper.GetGenericParameter<string>(filter, "TemplateCode");
        //        string strategyCode = RequestParameterHelper.GetGenericOptionalParameter<string>(filter, "StrategyCode");
        //        string uiCulture = RequestParameterHelper.GetGenericParameter<string>(filter, "UICulture");
        //        string cc = RequestParameterHelper.GetGenericOptionalParameter<string>(filter, "Cc");
        //        string bcc = RequestParameterHelper.GetGenericOptionalParameter<string>(filter, "Bcc");
        //        Dictionary<string, object> data = RequestParameterHelper.GetGenericOptionalParameter<Dictionary<string, object>>(filter, "Data");
        //        string emailListCodeParameterName = "EmailListCode";
        //        string toParameterName = "To";
        //        string emailListCode = RequestParameterHelper.GetGenericOptionalParameter<string>(filter, emailListCodeParameterName);
        //        string to = RequestParameterHelper.GetGenericOptionalParameter<string>(filter, toParameterName);

        //        KeyValuePair<string, object> elcKvp = new KeyValuePair<string, object>(emailListCodeParameterName, emailListCode);
        //        KeyValuePair<string, object> toKvp = new KeyValuePair<string, object>(toParameterName, to);
        //        RequestParameterHelper.CheckParameterCombination(RequestParameterHelper.ParameterCombinationFlags.OneAsTooManyCanBeAssigned | RequestParameterHelper.ParameterCombinationFlags.AtLeastOneMustBeAssigned, elcKvp, toKvp);


        //        SendEmailDto sendEmailDto = new Dto.SendEmailDto();
        //        sendEmailDto.user = RequestParameterHelper.GetGenericParameter<string>(filter, "User"); ;
        //        sendEmailDto.password = RequestParameterHelper.GetGenericParameter<string>(filter, "Password"); ;
        //        sendEmailDto.CountryCode = countryCode;
        //        sendEmailDto.ModuleCode = moduleCode;
        //        sendEmailDto.TemplateCode = templateCode;
        //        sendEmailDto.StrategyCode = strategyCode;
        //        sendEmailDto.UICulture = uiCulture;
        //        sendEmailDto.EmailListCode = emailListCode;
        //        sendEmailDto.To = to;
        //        sendEmailDto.Cc = cc;
        //        sendEmailDto.Bcc = bcc;
        //        sendEmailDto.data = data;

        //        TemplateDynamicExecution execution = new TemplateDynamicExecution() { dtoSendEmail = sendEmailDto };
        //      AbstractEMailDTO result = execution.ExecuteDynamicTemplate();
        //        return DTOResponseExecution.GetExecutedOkResponse();
        //    }
        //    catch (HttpErrorCustomizedException ex)
        //    {
        //        DTOApplicationErrorData dtoApplicationErrorData = ex.ApplicationErrorData;
        //        ApplicationEventLogHelper.LogApplicationErrorDataCatched(ref dtoApplicationErrorData);
        //        return DTOResponseExecution.GetApplicationErrorDataResponse(dtoApplicationErrorData);
        //    }
        //    catch (Exception ex)
        //    {
        //        DTOApplicationErrorData applicationErrorData = ApplicationEventLogHelper.LogErrorCatched(ex, filter);
        //        return DTOResponseExecution.GetApplicationErrorDataResponse(applicationErrorData);
        //    }
        //}

        [Obsolete]
        public bool SendDynamic(SendEmailDto obj)
        {
            DateTime dateTimeBeforeApiCall = DateTime.Now;
            try
            {
                //TODO: que se loguee o no, debe ser configurable
                //LogExternalApiCallHelper.LogExternalApiCall(obj);

                ServicioBroker.Instancia().ObtenerServicioUsuario().ChequearUsuario(obj.user, obj.password);

                EMailDynamicExecution execution = new EMailDynamicExecution();

                execution.dtoSendEmail = obj;

                Thread oThread = new Thread(new ThreadStart(execution.ExecuteSendDynamicMail));
                oThread.Start();

                return true;
            }
            catch (HttpErrorCustomizedException ex)
            {
                DTOApplicationErrorData dtoApplicationErrorData = ex.ApplicationErrorData;
                ApplicationEventLogHelper.LogApplicationErrorDataCatched(ref dtoApplicationErrorData);
                return false;
            }
            catch (Exception ex)
            {
                DTOApplicationErrorData applicationErrorData = ApplicationEventLogHelper.LogErrorCatched(ex);
                return false;
            }
        }

        //[HttpPost]
        //public AbstractEMailDTO GetDynamicTemplate(SendEmailDto obj)
        //{
        //    DateTime dateTimeBeforeApiCall = DateTime.Now;
        //    try
        //    {
        //        //TODO: que se loguee o no, debe ser configurable
        //        //LogExternalApiCallHelper.LogExternalApiCall(obj);
        //        AbstractEMailDTO result = null;

        //        ServicioBroker.Instancia().ObtenerServicioUsuario().ChequearUsuario(obj.user, obj.password);

        //        TemplateDynamicExecution execution = new TemplateDynamicExecution();

        //        execution.dtoSendEmail = obj;

        //        result = execution.ExecuteDynamicTemplate();

        //        return result;
        //    }
        //    catch (HttpErrorCustomizedException ex)
        //    {
        //        DTOApplicationErrorData dtoApplicationErrorData = ex.ApplicationErrorData;
        //        ApplicationEventLogHelper.LogApplicationErrorDataCatched(ref dtoApplicationErrorData);
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        DTOApplicationErrorData applicationErrorData = ApplicationEventLogHelper.LogErrorCatched(ex);
        //        return null;
        //    }
        //}

        public bool SendEmailVoucher(SendVouchersDTO obj)
        {
            try
            {
                ServicioBroker.Instancia().ObtenerServicioUsuario().ChequearUsuario(obj.user, obj.password);

                VoucherSender execution = new VoucherSender();

                execution.dtoSendVouchers = obj;

                Thread oThread = new Thread(new ThreadStart(execution.ExecuteSendDynamicMail));
                oThread.Start();

                return true;
            }
            catch (HttpErrorCustomizedException ex)
            {
                DTOApplicationErrorData dtoApplicationErrorData = ex.ApplicationErrorData;
                ApplicationEventLogHelper.LogApplicationErrorDataCatched(ref dtoApplicationErrorData);
                return false;
            }
            catch (Exception ex)
            {
                DTOApplicationErrorData applicationErrorData = ApplicationEventLogHelper.LogErrorCatched(ex);
                return false;
            }
        }
    }
}