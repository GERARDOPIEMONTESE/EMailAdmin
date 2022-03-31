namespace EMailAdmin.BackEnd.Reports.Objects
{
    public class VoucherInformationReportObject
    {
        #region Properties

        public byte[] Header { get; set; }
        public byte[] Footer { get; set; }
        public int ColorRed { get; set; }
        public int ColorGreen { get; set; }
        public int ColorBlue { get; set; }
        public string ReportTitle { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string LastNameNameTitle { get; set; }
        public string CardTitle { get; set; }
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public string CardValidFrom { get; set; }
        public string CardValidTo { get; set; }
        public string CardValidFromTitle { get; set; }
        public string CardValidToTitle { get; set; }
        public string CardDays { get; set; }
        public string CardDaysTitle { get; set; }
        public string CardArea { get; set; }
        public string CardAreaTitle { get; set; }
        public string CardEmissionDate { get; set; }
        public string CardEmissionDateTitle { get; set; }
        public string CardAgency { get; set; }
        public string CardAmount { get; set; }
        public string CardAmountTitle { get; set; }
        public string PersonalInfoTitle { get; set; }
        public string PersonalInfoPassportTitle { get; set; }
        public string PersonalInfoPassport { get; set; }
        public string PersonalInfoAgeTitle { get; set; }
        public string PersonalInfoAge { get; set; }
        public string PersonalInfoAddressTitle { get; set; }
        public string PersonalInfoAddress { get; set; }
        public string PersonalInfoCountryTitle { get; set; }
        public string PersonalInfoCountry { get; set; }
        public string PersonalInfoPhoneTitle { get; set; }
        public string PersonalInfoPhone { get; set; }
        public string EmergencyTitle { get; set; }
        public string EmergencyContactTitle { get; set; }
        public string EmergencyPhoneTitle { get; set; }
        public string EmergencyAddressTitle { get; set; }
        public string EmergencyContact { get; set; }
        public string EmergencyPhone { get; set; }
        public string EmergencyAddress { get; set; }

        public string PaxPassport { get; set; }
        public string Ruc { get; set; }
        public string ProductName { get; set; }
        public string CompleteVoucherCode { get; set; }
        public string ContactInfo1Title { get; set; }
        public string ContactInfo2Title { get; set; }
        public string PersonalInfoHeader { get; set; }
        public string ProductNameTitle { get; set; }
        public string CardValidTitle { get; set; }
        public string PersonalInfoRucTitle { get; set; }
        public string VoucherCodeTitle { get; set; }
        public string DateValidityTo { get; set; }
        public string DateValidityInclusive { get; set; }
        public string ConditionsText { get; set; }
        public string ConditionsPrice { get; set; }
        public string LocationsTitle { get; set; }
        public string ConditionsReportTitle { get; set; }
        public string ReportFooter { get; set; }
        public string ContactEmail { get; set; }

        public string Central1Label { get; set; }
        public string Central2Label { get; set; }
        public string Central3Label { get; set; }
        public string Central4Label { get; set; }

        public string SeccionHeaderInfo { get; set; }

        public string SeccionFooterInfo { get; set; }

        #endregion

        #region Constructor

        public VoucherInformationReportObject(string reportTitle, string lastNameNameTitle, string cardTitle, string cardValidFromTitle, string cardValidToTitle, string cardDaysTitle, string cardAreaTitle, string cardEmissionDateTitle, string cardAmountTitle, string personalInfoTitle, string personalInfoPassportTitle, string personalInfoAgeTitle, string personalInfoAddressTitle, string personalInfoCountryTitle, string personalInfoPhoneTitle, string emergencyTitle, string emergencyContactTitle, string emergencyPhoneTitle, string emergencyAddressTitle)
        {
            ReportTitle = reportTitle;
            LastNameNameTitle = lastNameNameTitle;
            CardTitle = cardTitle;
            CardValidFromTitle = cardValidFromTitle;
            CardValidToTitle = cardValidToTitle;
            CardDaysTitle = cardDaysTitle;
            CardAreaTitle = cardAreaTitle;
            CardEmissionDateTitle = cardEmissionDateTitle;
            CardAmountTitle = cardAmountTitle;
            PersonalInfoTitle = personalInfoTitle;
            PersonalInfoPassportTitle = personalInfoPassportTitle;
            PersonalInfoAgeTitle = personalInfoAgeTitle;
            PersonalInfoAddressTitle = personalInfoAddressTitle;
            PersonalInfoCountryTitle = personalInfoCountryTitle;
            PersonalInfoPhoneTitle = personalInfoPhoneTitle;
            EmergencyTitle = emergencyTitle;
            EmergencyContactTitle = emergencyContactTitle;
            EmergencyPhoneTitle = emergencyPhoneTitle;
            EmergencyAddressTitle = emergencyAddressTitle;
        }

        #endregion Constructor
    }
}