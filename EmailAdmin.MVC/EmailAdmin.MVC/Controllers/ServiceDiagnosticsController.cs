using DTOMapper;
using DTOMapper.Diagnostics;
using DTOMapper.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace EmailAdmin.MVC.Controllers
{
    public class ServiceDiagnosticsController : ApiController
    {
        [HttpPost]
        public DTOResponseScalar<string> PostDiagnosticMethod(DTOFilter filter)
        {
            try
            {
                string methodName = RequestParameterHelper.GetGenericParameter<string>(filter, "MethodName");
                MethodInfo methodInfo = typeof(ServiceDiagnosticsHelper).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
                string retVal = JsonConvert.SerializeObject(methodInfo.Invoke(null, null));
                return DTOResponseScalar<string>.GetScalarOkResponse(retVal);
            }
            catch (HttpErrorCustomizedException ex)
            {
                DTOApplicationErrorData dtoApplicationErrorData = ex.ApplicationErrorData;
                ApplicationEventLogHelper.LogApplicationErrorDataCatched(ref dtoApplicationErrorData);
                return DTOResponseScalar<string>.GetApplicationErrorDataResponse(dtoApplicationErrorData);
            }
            catch (Exception ex)
            {
                DTOApplicationErrorData applicationEventLogResponse = ApplicationEventLogHelper.LogErrorCatched(ex, filter);
                return DTOResponseScalar<string>.GetApplicationErrorDataResponse(applicationEventLogResponse);
            }
        }

        [HttpGet]
        public DTOResponseScalar<string> GetDiagnosticMethod(string methodName)
        {
            try
            {
                MethodInfo methodInfo = typeof(ServiceDiagnosticsHelper).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
                string retVal = JsonConvert.SerializeObject(methodInfo.Invoke(null, null));
                return DTOResponseScalar<string>.GetScalarOkResponse(retVal);
            }
            catch (HttpErrorCustomizedException ex)
            {
                DTOApplicationErrorData dtoApplicationErrorData = ex.ApplicationErrorData;
                ApplicationEventLogHelper.LogApplicationErrorDataCatched(ref dtoApplicationErrorData);
                return DTOResponseScalar<string>.GetApplicationErrorDataResponse(dtoApplicationErrorData);
            }
            catch (Exception ex)
            {
                DTOApplicationErrorData applicationEventLogResponse = ApplicationEventLogHelper.LogErrorCatched(ex);
                return DTOResponseScalar<string>.GetApplicationErrorDataResponse(applicationEventLogResponse);
            }
        }

        //El monitor de IT le pega directo a este método
        [HttpGet]
        public string GetServiceUpSince()
        {
            return ServiceDiagnosticsHelper.GetCurrentProcessUpSince().ResponseScalar.ToString();
        }
    }
}