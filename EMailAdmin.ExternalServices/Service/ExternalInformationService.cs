using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.ExternalServices.Data.Interfaces;
using EMailAdmin.ExternalServices.Domain;
using EMailAdmin.ExternalServices.ACNetService;
using LibreriaUtilitarios;
using System.Xml;
using EMailAdmin.ExternalServices.Properties;
using EMailAdmin.ExternalServices.Service.Interface;

namespace EMailAdmin.ExternalServices.Service
{
    public class ExternalInformationService : IExternalInformationService
    {
        public IDAOIssuanceInformation DaoIssuanceInformation { get; set; }

        public IssuanceInformation GetIssuanceInformation(int countryCode, string voucherCode)
        {
            IssuanceInformation issuanceInformation = DaoIssuanceInformation.Get(countryCode, voucherCode);

            MembershipInformationService service = new MembershipInformationService();
            string xml = service.getMembershipInformation(Settings.Default.MembershipUser, Settings.Default.MembershipPassword,
                                                          countryCode.ToString(), voucherCode);

            XmlDocument xmlDocument = XmlParser.GetDocument(xml);

            issuanceInformation.MainBenefits = XmlParser.GetValue(xmlDocument, "/membership_information/voucher/ggcc");

            issuanceInformation.MainBenefits = issuanceInformation.MainBenefits == null ? "" :
                issuanceInformation.MainBenefits.Substring(issuanceInformation.MainBenefits.IndexOf("?>") + 2);

            return issuanceInformation;
        }
    }
}
