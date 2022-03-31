using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMailAdmin.BackEnd.DTO
{
    public class HappyBirthDTO : AbstractEMailDTO
    {
        public string HtmlBody { get; set; }
        public string Locations { get; set; }
    }
}
