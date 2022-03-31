using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMailAdmin.BackEnd.Reports.Objects
{
    public class VoucherTagReportObject
    {
        #region Properties

        public string CompleteVoucherCode { get; set; }

        public string FullPaxName { get; set; }

        public string EffectiveEndDate { get; set; }

        public string ProductName { get; set; }

        public string Modality { get; set; }

        #endregion
    }
}
