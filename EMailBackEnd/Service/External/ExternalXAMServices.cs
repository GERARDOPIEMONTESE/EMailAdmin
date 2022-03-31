using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Service.Interfaces;
using EMailAdmin.BackEnd.Domain.External;

namespace EMailAdmin.BackEnd.Service.External
{
    class ExternalXAMServices : IExternalXAMServices
    {
        public EmailAlertXamCases[] GetAll()
        {
            IList<EmailAlertXamCases> lst = new List<EmailAlertXamCases>();

            ServicioXAM wsXam = new ServicioXAM();
            var xamData = wsXam.GetCasesForSendingMail();

            foreach (var item in xamData)
            {
                lst.Add(new EmailAlertXamCases()
                {
                    AssistanceCountry = item.AssistanceCountry, // ACI1889
                    CaseId = item.CaseId,
                    Coordinator = item.Coordinator,
                    Country = item.Country,
                    Url = item.Url,
                    Delay = item.Delay,
                    Region = item.Region
                });
            }
            
            return lst.ToArray();
        }

        public EmailAlertXamCases[] GetAllExtended()
        {
            IList<EmailAlertXamCases> lst = new List<EmailAlertXamCases>();

            //wsXAM.MailService wsXam = new wsXAM.MailService();
            //var xamData = wsXam.GetCasesMoreThanThreeDays();


            //foreach (var item in xamData)
            //{
            //    lst.Add(new EmailAlertXamCases()
            //    {
            //        CaseId = item.CaseId,
            //        Coordinator = item.Coordinator,
            //        Country = item.Country,
            //        Url = item.Url,
            //        Delay = item.Delay,
            //        Region = item.Region
            //    });
            //}

            return lst.ToArray();
        }
    }
}
