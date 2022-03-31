using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMailAdmin.BackEnd.Domain.External
{
    public class UpgradeInformation
    {
        #region Properties

        public int CountryCode { get; set; }
        public string Upgrade { get; set; }
        public string UpgradeRateCode { get; set; }
        public string UpgradeIssuanceDate { get; set; }
        public string UpgradeStartDate { get; set; }
        public string UpgradeEndDate { get; set; }
        public string UpgradeName { get; set; }
        public string UpgradeLegend { get; set; }

        #endregion
    }
}
