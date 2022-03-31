using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMailAdmin.BackEnd.DTO
{
    public class EmailAlertNotIssueDTO: AbstractEMailDTO
    {
        public string GatewayName { get; set; }        
        public string LastIssue { get; set; }
        public string PurchaseProcessTypeDesc { get; set; }
        public string LastConfirmation { get; set; }
    }
}
