using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class InsuranceCompany : ObjetoPersistido
    {
        #region Constants

        private const string NAME = "InsuranceCompany";

        #endregion

        #region Attributes

        private string _Name = "";

        #endregion

        #region Properties

        public int CountryCode { get; set; }

        public string ProductCode { get; set; }

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        #endregion

        public override string ObtenerNombre()
        {
            return NAME;
        }
    }
}
