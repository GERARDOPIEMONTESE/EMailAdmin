using DTOMapper;
using DTOMapper.Helpers;
using EmailAdmin.Api.Utilities;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace EmailAdmin.Api.Controllers
{
    public class ScheduledController : ApiController
    {
        [HttpPost]
        public DTOResponseScalar<bool> ResetSchedulerFactory()
        {
            try
            {
                SchedulerFactory.ResetAllJobs();
                return DTOResponseScalar<bool>.GetScalarOkResponse(true);

            }
            catch (HttpErrorCustomizedException ex)
            {
                DTOApplicationErrorData dtoApplicationErrorData = ex.ApplicationErrorData;
                ApplicationEventLogHelper.LogApplicationErrorDataCatched(ref dtoApplicationErrorData);
                return DTOResponseScalar<bool>.GetApplicationErrorDataResponse(dtoApplicationErrorData);
            }
            catch (Exception ex)
            {
                DTOApplicationErrorData applicationErrorData = ApplicationEventLogHelper.LogErrorCatched(ex);
                return DTOResponseScalar<bool>.GetApplicationErrorDataResponse(applicationErrorData);
            }
        }

        [HttpPost]
        public DTOResponseScalar<bool> ShutdownJob(DTOFilter filter)
        {
            try
            {
                string jobName;
                GetParams(filter, out jobName);
                return DTOResponseScalar<bool>.GetScalarOkResponse(SchedulerFactory.ShutdownJob(jobName));

            }
            catch (HttpErrorCustomizedException ex)
            {
                DTOApplicationErrorData dtoApplicationErrorData = ex.ApplicationErrorData;
                ApplicationEventLogHelper.LogApplicationErrorDataCatched(ref dtoApplicationErrorData);
                return DTOResponseScalar<bool>.GetApplicationErrorDataResponse(dtoApplicationErrorData);
            }
            catch (Exception ex)
            {
                DTOApplicationErrorData applicationErrorData = ApplicationEventLogHelper.LogErrorCatched(ex, filter);
                return DTOResponseScalar<bool>.GetApplicationErrorDataResponse(applicationErrorData);
            }
        }

        [HttpPost]
        public DTOResponseScalar<bool> StartJob(DTOFilter filter)
        {
            try
            {

                string jobName;
                GetParams(filter, out jobName);
                return DTOResponseScalar<bool>.GetScalarOkResponse(SchedulerFactory.StartJob(jobName));

            }
            catch (HttpErrorCustomizedException ex)
            {
                DTOApplicationErrorData dtoApplicationErrorData = ex.ApplicationErrorData;
                ApplicationEventLogHelper.LogApplicationErrorDataCatched(ref dtoApplicationErrorData);
                return DTOResponseScalar<bool>.GetApplicationErrorDataResponse(dtoApplicationErrorData);
            }
            catch (Exception ex)
            {
                DTOApplicationErrorData applicationErrorData = ApplicationEventLogHelper.LogErrorCatched(ex, filter);
                return DTOResponseScalar<bool>.GetApplicationErrorDataResponse(applicationErrorData);
            }
        }

        private void GetParams(DTOFilter filter, out string Name)
        {
            Name = RequestParameterHelper.GetGenericParameter<string>(filter, "jobName");
        }
    }
}