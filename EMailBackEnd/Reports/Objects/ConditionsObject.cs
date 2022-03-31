using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMailAdmin.BackEnd.Reports.Objects
{
    public class ConditionsObject
    {
        public string Code { get; set; }
        public string Text { get; set; }
        public string Leyend { get; set; }
        public string Conditions {
            get { return "1"; }
        }
    }
}
