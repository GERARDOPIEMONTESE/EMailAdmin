using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class ReportLanguage : ObjetoCodificado
    {
        #region Constants

        private const string NAME = "ReportLanguage";

        public const string REPORT = "ReportTitle";
        public const string CONDITIONSREPORT = "ConditionsReportTitle";
        public const string LASTNAMENAME = "LastNameNameTitle";
        public const string CARD = "CardTitle";
        public const string CARDVALIDFROM = "CardValidFromTitle";
        public const string CARDVALIDTO = "CardValidToTitle";
        public const string CARDDAYS = "CardDaysTitle";
        public const string CARDAREA = "CardAreaTitle";
        public const string CARDEMISSIONDATE = "CardEmissionDateTitle";
        public const string CARDAMOUNT = "CardAmountTitle";
        public const string PERSONALINFO = "PersonalInfoTitle";
        public const string PERSONALINFOPASSPORT = "PersonalInfoPassportTitle";
        public const string PERSONALINFOAGE = "PersonalInfoAgeTitle";
        public const string PERSONALINFOADDRESS = "PersonalInfoAddressTitle";
        public const string PERSONALINFOCOUNTRY = "PersonalInfoCountryTitle";
        public const string PERSONALINFOPHONE = "PersonalInfoPhoneTitle";
        public const string EMERGENCY = "EmergencyTitle";
        public const string EMERGENCYCONTACT = "EmergencyContactTitle";
        public const string EMERGENCYPHONE = "EmergencyPhoneTitle";
        public const string EMERGENCYADDRESS = "EmergencyAddressTitle";

        //Datos agregados para RIMAC

        public const string SECURETYPETITLE = "SecureTypeTitle";
        public const string	PARTICULARCONDITIONTITLE = "ParticularConditionTitle";
        public const string	COMPANYPOLIZADATATITLE = "CompanyPolizaDataTitle";
        public const string	POLIZATITLE	= "PolizaTitle";
        public const string	COMPANYDATATITLE = "CompanyDataTitle";
        public const string	RUCTITLE = "RUCTitle";
        public const string	AGENCYADRESSTITLE =	"AgencyAdressTitle";
        public const string	AGENCYPROVINCETITLE	= "AgencyProvinceTitle";
        public const string	AGENCYPHONETITLE = "AgencyPhoneTitle";
        public const string	AGENCYDISTRICTTITLE	= "AgencyDistrictTitle";
        public const string	AGENCYDEPARTAMENTTITLE = "AgencyDepartamentTitle";
        public const string	AGENCYFAXTITLE = "AgencyFaxTitle";
        public const string	PAXTYPETITLE = "PaxTypeTitle";
        public const string	PAXDOCUMENTNUMBERTITLE = "PaxDocumentNumberTitle";
        public const string	PAXCITYTITLE = "PaxCityTitle";
        public const string	PAXBIRTHDATETITLE =	"PaxBirthDateTitle";
        public const string	PAXEMAILTITLE =	"PaxEmailTitle";
        public const string	DEPENDENTTITLE = "DependentTitle";
        public const string	DEPENDENTNAMETITLE = "DependentNameTitle";
        public const string	DEPENDENTBIRTHDAYTITLE = "DependentBirthDayTitle";
        public const string	BENEFICIARIESTITLE = "BeneficiariesTitle";
        public const string INSUREDTITLE = "InsuredTitle";

        //Mas datos RIMAC
        public const string DEPENDENT = "Dependent";
        public const string BENEFICIARIES = "Beneficiaries";
        public const string CONSIDERATIONSTITLE = "ConsiderationsTitle";
        public const string CONSIDERATIONS = "Considerations";
        public const string PAYMENTPLANTITLE = "PaymentPlanTitle";
        public const string PRIMANETATITLE = "PrimaNetaTitle";
        public const string ALLOWANCETITLE = "AllowanceTitle";
        public const string IGVTITLE = "IGVTitle";
        public const string PRIMATOTALTITLE = "PrimaTotalTitle";
        public const string PAYMENTPLANNOTE = "PaymentPlanNote";
        public const string FEATURESOFPRODUCTTITLE = "FeaturesOfProductTitle";
        public const string MINAGE = "MinAge";
        public const string MAXAGE = "MaxAge";
        public const string OTHERS = "Others";
        public const string PROCEDURETITLE = "ProcedureTitle";
        public const string ESPECIALCONDITIONSTITLE = "EspecialConditionsTitle";
        public const string ESPECIALCONDITIONS = "EspecialConditions";
        public const string IMPORTANTTITLE = "ImportantTitle";
        public const string IMPORTANT = "Important";
        public const string PRINTDATETITLE = "PrintDateTitle";
        public const string PROCEDUREONE = "ProcedureOne";
        public const string PROCEDURETWO = "ProcedureTwo";
        public const string PROCEDURETHREE = "ProcedureThree";
        public const string PROCEDUREFOUR = "ProcedureFour";
        public const string PROCEDUREFIVE = "ProcedureFive";
        public const string PROCEDURESIX = "ProcedureSix";
        public const string PROCEDURESEVEN = "ProcedureSeven";
        public const string PROCEDUREEIGTH = "ProcedureEigth";

        public const string CONTACTINFO1TITLE = "ContactInfo1Title";
        public const string CONTACTINFO2TITLE = "ContactInfo2Title";
        public const string PERSONALINFOHEADER = "PersonalInfoHeader";
        public const string PERSONALINFORUCTITLE = "PersonalInfoRucTitle";
        public const string PRODUCTNAMETITLE = "ProductNameTitle";
        public const string CARDVALIDTITLE = "CardValidTitle";
        public const string VOUCHERCODETITLE = "VoucherCodeTitle";
        public const string DATEVALIDITYTO = "DateValidityTo";
        public const string DATEVALIDITYINCLUSIVE = "DateValidityInclusive";
        public const string CONDITIONSTEXT = "ConditionsText";
        public const string LOCATIONSTITLE = "LocationsTitle";
        public const string CONDITIONSREPORTTITLE = "ConditionsReportLabel";
        public const string REPORTFOOTER = "ReportFooter";
        public const string CONTACTEMAIL = "ContactEmail";
        public const string CENTRAL1LABEL = "Central1Label";
        public const string CENTRAL2LABEL = "Central2Label";
        public const string CENTRAL3LABEL = "Central3Label";
        public const string CENTRAL4LABEL = "Central4Label";

        #endregion Constants

        #region Properties

        public int IdLanguage { get; set; }

        public int IdStrategy { get; set; }

        public string Key { get; set; }

        public string Value
        {
            get { return Descripcion; }
        }

        #endregion Properties

        #region Methods

        public override string ObtenerNombre()
        {
            return NAME;
        }

        #endregion Methods
    }
}