using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailAdmin.Dto
{
    public class ResponseDynamicDto
    {
        public const string ResponseOK = "SUCCESS";
        public object responseData { get; set; }
        public string status { get; set; }
        public string responseCode { get; set; }
        public string responseMessage { get; set; }

        public bool IsOK
        {
            get
            {
                return (responseMessage == ResponseOK);
            }
        }
    }
}