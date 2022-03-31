using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.DTO
{
    public class VoucherACNETDTO : ObjetoPersistido
    {
        private const string NAME = "VoucherACNETDTO";

        #region Attributes

        public string CountryCode { set; get; }
        public string VoucherCode { set; get; }
        public string ProductCode { set; get; }
        public string RateCode { set; get; }
        public string IssueDate { set; get; }
        public string AgencyCode { set; get; }
        public string BranchNumber { set; get; }
        public string Modality { set; get; }
        public string LastName { set; get; }
        public string Name { set; get; }
        public string Email { set; get; }
        public string Points { set; get; }

        public string UniqueVoucherId { set; get; }
        public string CountryName { set; get; }
        public string Days { set; get; }
        public string FamilyPlan { set; get; }
        public string VoucherGroup { set; get; }
        public string PromotionId { set; get; }
        public string RateAmount { set; get; }
        public string BirthDate { set; get; }
        public string DocNumber { set; get; }
        public string KMs { set; get; }
        public string ConcPaisProdTar { set; get; }
        public string ConcPaisAgvPrdTar { set; get; }

        #endregion

        public override string ObtenerNombre()
        {
            return NAME;
        }
    
        public string Age
        {
            get {
                string strAge = "0";
                try
                {
                    DateTime birth = Convert.ToDateTime(BirthDate, CultureInfo.CurrentCulture);
                    int age = DateTime.Now.Year - birth.Year;
                    if (DateTime.Now.Month < birth.Month || (DateTime.Now.Month == birth.Month && DateTime.Now.Day < birth.Day)) age--;
                    strAge = age.ToString();
                }
                catch { }
                return strAge;
            }
        }
    }
}
