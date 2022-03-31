using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailAdmin.Dto
{
    public class SendEmailDto
    {
        public string user { get; set; }
        public string password { get; set; }
        public int CountryCode { get; set; }
        public string ModuleCode { get; set; }
        public string TemplateCode { get; set; }
        public string StrategyCode { get; set; }
        public string UICulture { get; set; }        
        public string EmailListCode { get; set; }   //si esta lleno busca los emails de la lista configurada y lo pone  en To     
        public string To { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public Dictionary<string, object> data { get; set; }

        public SendEmailDto()
        {
            data = new Dictionary<string, object>();
        }
    }
}