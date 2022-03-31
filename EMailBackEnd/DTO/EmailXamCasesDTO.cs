using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Service.Interfaces;
using EMailAdmin.BackEnd.Domain.External;

namespace EMailAdmin.BackEnd.DTO
{
    public class EmailXamCasesDTO : AbstractEMailDTO, ITableBody
    {
        private const string NAME = "XamCases";

        public string detailsCases { get; set; }
        
        public static string GetInfoMail(EmailAlertXamCases item)
        {
            // ACI1889
            string info = "<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td></tr>";

            info = string.Format(info, ParserLink(item), item.Coordinator, item.AssistanceCountry, item.Country, item.Delay, item.Region);
            return info;
        }

        private static object ParserLink(EmailAlertXamCases caso)
        {
            return "<a href='" + caso.Url + "'>" + caso.CaseId + "</a>";
        }

        public string ParseBody(string bodyName)
        {
            return this.detailsCases;
        }

        public string[] ParseBodyArray(string bodyName)
        {
            throw new NotImplementedException();
        }

        public string ParseHeader(string bodyName)
        {
            throw new NotImplementedException();
        }
    }
}
