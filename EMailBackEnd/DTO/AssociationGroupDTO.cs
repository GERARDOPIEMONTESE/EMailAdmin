using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMailAdmin.BackEnd.DTO
{
    public class AssociationGroupDTO
    {
        #region Properties

        public int IdLocation { get; set; }

        public int IdAccount { get; set; }

        public int IdProduct { get; set; }

        public int IdRate { get; set; }

        public int IdDistributionType { get; set; }

        #endregion
    }
}
