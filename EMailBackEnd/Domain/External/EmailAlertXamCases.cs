using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMailAdmin.BackEnd.Domain.External
{
    public class EmailAlertXamCases
    {
        public string AssistanceCountry { get; set; } // ACI1889
        public string CaseId { get; set; }
        public string Coordinator { get; set; }
        public string Country { get; set; }
        public string Url { get; set; }
        public string Delay { get; set; }
        public string Region { get; set; }
    }
}
