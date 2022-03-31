using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMailAdmin.BackEnd.Reports.Objects
{
public class VoucherInformationReportRimacObject
             
    {
        #region Properties

    //Datos para el informe

        public byte[] Header { get; set; }
        public byte[] Footer { get; set; }
        public string ReportTitle { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string LastNameNameTitle { get; set; }
        public DateTime CardEmissionDate { get; set; }
        public string CardEmissionDateTitle { get; set; }
        public DateTime CardValidFrom { get; set; }
        public DateTime CardValidTo { get; set; }
        public string CardValidFromTitle { get; set; }
        public string CardValidToTitle { get; set; }
        public string CardArea { get; set; }
        public string CardAreaTitle { get; set; }
        public string PersonalInfoPassportTitle { get; set; }
        public string PersonalInfoPassport { get; set; }
        public string PersonalInfoAddressTitle { get; set; }
        public string PersonalInfoAddress { get; set; }
        public string PersonalInfoCountryTitle { get; set; }
        public string PersonalInfoCountry { get; set; }
        public string PersonalInfoPhoneTitle { get; set; }
        public string PersonalInfoPhone { get; set; }

    //Leyendas agregadas para RIMAC
        public string SecureTypeTitle { get; set; }
        public string ParticularConditionTitle { get; set; }
        public string CompanyPolizaDataTitle { get; set; }
        public string PolizaTitle { get; set; }
        public string CompanyDataTitle { get; set; }
        public string RUCTitle { get; set; }
        public string AgencyAdressTitle { get; set; }
        public string AgencyProvinceTitle { get; set; }
        public string AgencyPhoneTitle { get; set; }
        public string AgencyDistrictTitle { get; set; }
        public string AgencyDepartamentTitle { get; set; }
        public string AgencyFaxTitle { get; set; }
        public string PaxTypeTitle { get; set; }
        public string PaxDocumentNumberTitle { get; set; }
        public string PaxCityTitle { get; set; }
        public string PaxBirthDateTitle { get; set; }
        public string PaxEmailTitle { get; set; }
        public string DependentTitle { get; set; }
        public string DependentNameTitle { get; set; }
        public string DependentBirthDayTitle { get; set; }
        public string BeneficiariesTitle { get; set; }
        public string InsuredTitle { get; set; }

    //Datos para el informe
        public string RUC { get; set; }
        public string AgencyProvince { get; set; }
        public string AgencyDistrict { get; set; }
        public string AgencyDepartament { get; set; }
        public string AgencyFax { get; set; }
        public string PaxType { get; set; }
        public string PaxDocumentNumber { get; set; }
        public string PaxCity { get; set; }
        public string PaxBirthDate { get; set; }
        public string PaxEmail { get; set; }
        public string AgencyAddress { get; set; }
        public string AgencyPhone { get; set; }
        public string PrimaNeta { get; set; }
        public string DerechoDeEmision { get; set; }
        public string Igv { get; set; }
        public string PrimaTotal { get; set; }
           
    //Mas leyendas y datos fijos
        public string Dependent { get; set; }
        public string Beneficiaries { get; set; }
        public string ConsiderationsTitle { get; set; }
        public string Considerations { get; set; }
        public string PaymentPlanTitle { get; set; }
        public string PrimaNetaTitle { get; set; }
        public string AllowanceTitle { get; set; }
        public string IGVTitle { get; set; }
        public string PrimaTotalTitle { get; set; }
        public string PaymentPlanNote { get; set; }
        public string FeaturesOfProductTitle { get; set; }
        public string MinAge { get; set; }
        public string MaxAge { get; set; }
        public string Others { get; set; }
        public string ProcedureTitle { get; set; }
        //public string Procedure { get; set; }
        public string EspecialConditionsTitle { get; set; }
        public string EspecialConditions { get; set; }
        public string ImportantTitle { get; set; }
        public string Important { get; set; }
        public string PrintDateTitle { get; set; }

    //Datos extra

        public string CardTitle { get; set; }
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public string CardDays { get; set; }
        public string CardDaysTitle { get; set; }     
        public string CardAgency { get; set; }
        public string CardAmount { get; set; }
        public string CardAmountTitle { get; set; }
        public string PersonalInfoTitle { get; set; }       
        public string PersonalInfoAgeTitle { get; set; }
        public string PersonalInfoAge { get; set; }      
        public string EmergencyTitle { get; set; }
        public string EmergencyContactTitle { get; set; }
        public string EmergencyPhoneTitle { get; set; }
        public string EmergencyAddressTitle { get; set; }
        public string EmergencyContact { get; set; }
        public string EmergencyPhone { get; set; }
        public string EmergencyAddress { get; set; }

        public string ProcedureOne { get; set; }
        public string ProcedureTwo { get; set; }
        public string ProcedureThree { get; set; }
        public string ProcedureFour { get; set; }
        public string ProcedureFive { get; set; }
        public string ProcedureSix { get; set; }
        public string ProcedureSeven { get; set; }
        public string ProcedureEigth { get; set; }
        public string Poliza { get; set; }
        public string ProductName { get; set; } 

        #endregion

        #region Constructor

        public VoucherInformationReportRimacObject(string reportTitle, string lastNameNameTitle, string cardTitle, string cardValidFromTitle, 
            string cardValidToTitle, string cardDaysTitle, string cardAreaTitle, string cardEmissionDateTitle, string cardAmountTitle, string personalInfoTitle, 
            string personalInfoPassportTitle, string personalInfoAgeTitle, string personalInfoAddressTitle, string personalInfoCountryTitle, 
            string personalInfoPhoneTitle, string emergencyTitle, string emergencyContactTitle, string emergencyPhoneTitle, string emergencyAddressTitle, 
            string secureTypeTitle, string particularConditionTitle, string companyPolizaDataTitle, string polizaTitle, string companyDataTitle, string rUCTitle, 
            string agencyAdressTitle, string agencyProvinceTitle, string agencyPhoneTitle, string agencyDistrictTitle, string agencyDepartamentTitle, 
            string agencyFaxTitle, string paxTypeTitle, string paxDocumentNumberTitle, string paxCityTitle, string paxBirthDateTitle, string paxEmailTitle, 
            string dependentTitle, string dependentNameTitle, string dependentBirthDayTitle, string beneficiariesTitle, string insuredTitle,
            
            string dependent, string beneficiaries, string considerationsTitle, string considerations, string paymentPlanTitle, string primaNetaTitle,
            string allowanceTitle, string iGVTitle, string primaTotalTitle, string paymentPlanNote, string featuresOfProductTitle, string minAge, string maxAge, string others, 
            string procedureTitle, /*string procedure,*/ string especialConditionsTitle, string especialConditions, string importantTitle, string important,
            string printDateTitle, string procedureOne, string procedureTwo, string procedureThree, string procedureFour, string procedureFive, string procedureSix, string procedureSeven, string procedureEigth)
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

            //Leyendas para el informe
            SecureTypeTitle = secureTypeTitle;
            ParticularConditionTitle = particularConditionTitle;
            CompanyPolizaDataTitle = companyPolizaDataTitle;
            PolizaTitle	= polizaTitle;
            CompanyDataTitle = companyDataTitle;
            RUCTitle = rUCTitle;
            AgencyAdressTitle = agencyAdressTitle;
            AgencyProvinceTitle	= agencyProvinceTitle;
            AgencyPhoneTitle = agencyPhoneTitle;
            AgencyDistrictTitle = agencyDistrictTitle;
            AgencyDepartamentTitle = agencyDepartamentTitle;
            AgencyFaxTitle = agencyFaxTitle;
            PaxTypeTitle = paxTypeTitle;
            PaxDocumentNumberTitle = paxDocumentNumberTitle;
            PaxCityTitle = paxCityTitle;
            PaxBirthDateTitle = paxBirthDateTitle;
            PaxEmailTitle = paxEmailTitle;
            DependentTitle = dependentTitle;
            DependentNameTitle = dependentNameTitle;
            DependentBirthDayTitle = dependentBirthDayTitle;
            BeneficiariesTitle = beneficiariesTitle;
            InsuredTitle = insuredTitle;

            //Mas leyendas y datos fijos

            Dependent = dependent;
            Beneficiaries = beneficiaries;
            ConsiderationsTitle = considerationsTitle;
            Considerations = considerations;
            PaymentPlanTitle = paymentPlanTitle;
            PrimaNetaTitle = primaNetaTitle;
            AllowanceTitle = allowanceTitle;
            IGVTitle = iGVTitle;
            PrimaTotalTitle = primaTotalTitle;
            PaymentPlanNote = paymentPlanNote;
            FeaturesOfProductTitle = featuresOfProductTitle;
            MinAge = minAge;
            MaxAge = maxAge;
            Others = others;
            ProcedureTitle = procedureTitle;
            EspecialConditionsTitle = especialConditionsTitle;
            EspecialConditions = especialConditions;
            ImportantTitle = importantTitle;
            Important = important;
            PrintDateTitle = printDateTitle;

            ProcedureOne = procedureOne;
            ProcedureTwo = procedureTwo;
            ProcedureThree = procedureThree;
            ProcedureFour = procedureFour;
            ProcedureFive = procedureFive;
            ProcedureSix = procedureSix;
            ProcedureSeven = procedureSeven;
            ProcedureEigth = procedureEigth;
        }

        #endregion Constructor
    }
}