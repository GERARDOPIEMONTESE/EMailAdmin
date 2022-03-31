using DTOMapper;
using DTOMapper.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EMailAdmin.BackEnd.Utils
{
    public class LogExternalApiCallHelper
    {        
        public static void LogExternalApiCall(object requestData)
        {
            string jsonRequest = JsonConvert.SerializeObject(requestData);
            DTOFilter filter = new DTOFilter();
            filter.Parameters.Add("ApplicationEventsTableName", "EmailAdminCommunicationsLog");
            filter.Parameters.Add("Data", jsonRequest);
            LogApplicationEvent(filter);
        }

        private static void LogApplicationEvent(DTOFilter filter)
        {
            string applicationEventLogApi = ConfigurationManager.AppSettings["ApplicationEventLogApi"].ToString();
            string relativeUrl = "Logger/LogApplicationEvent";
            string jsonContext = JsonConvert.SerializeObject(filter);
            var response = ApiHelper.Post(applicationEventLogApi, relativeUrl, jsonContext);
        }
    }
}
