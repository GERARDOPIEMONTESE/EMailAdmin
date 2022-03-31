using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMailAdmin.BackEnd.Reports.Objects
{
    public class BrazilVoucherInformationObject
    {
        #region Properties

        public string PolicyCode { get; set; }

        public string Certificado { get; set; }
        
        public string CardNumber { get; set; }

        public string PersonalInfoFullName { get; set; }
        public string PersonalInfoDocument { get; set; }
        public string CardValidFrom { get; set; }
        public string CardValidTo { get; set; }

        public string PersonalInfoAddress { get; set; }
        public string PersonalInfoNeighbor { get; set; }
        public string PersonalInfoCity { get; set; }

        public string PersonalInfoPostalCode { get; set; }
        public string PersonalInfoState { get; set; }
        public string PersonalInfoContact { get; set; }
        public string PersonalInfoEmail { get; set; }
        public string PersonalInfoPhone { get; set; }

        public string CorretorType { get; set; }
        public string CorretorName { get; set; }
        public string CorretorDocument { get; set; }
        public string CorretorCode { get; set; }

        public string CorretorAddress { get; set; }
        public string CorretorNeighbor { get; set; }
        public string CorretorCity { get; set; }

        public string CorretorPostalCode { get; set; }
        public string CorretorState { get; set; }
        public string CorretorContact { get; set; }
        public string CorretorEmail { get; set; }
        public string CorretorPhone { get; set; }
        public string CorretorUnidadFederativa { get; set; }

        public string AgencyType { get; set; }
        public string AgencyName { get; set; }
        public string AgencyDocument { get; set; }
        public string AgencyCode { get; set; }

        public string AgencyAddress { get; set; }
        public string AgencyNeighbor { get; set; }
        public string AgencyCity { get; set; }

        public string AgencyPostalCode { get; set; }
        public string AgencyState { get; set; }
        public string AgencyContact { get; set; }
        public string AgencyEmail { get; set; }
        public string AgencyPhone { get; set; }
        public string AgencyUnidadFederativa { get; set; }

        public string BenefitsTable { get; set; }
        public string IssuanceDate { get; set; }
        public string Conditions { get { return "1"; } }
        
        public string NetPrice { get; set; }
        public string Iof { get; set; }
        public string AditionalFractionation { get; set; }
        public string PolicyPrice { get; set; }
        public string TotalPrice { get; set; }

        public bool upgrade { get; set; }

        public string IssuanceDateTime { get; set; }

        #endregion
    }
}
