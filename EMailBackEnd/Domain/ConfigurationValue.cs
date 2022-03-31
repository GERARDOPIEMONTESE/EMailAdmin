using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Domain
{
    public class ConfigurationValue : ObjetoNegocio
    {
        private const string NAME = "ConfigurationValue";

        #region Attributes

        public string Code { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }

        #endregion

        public override string ObtenerNombre()
        {
            return NAME;
        }

        public override FrameworkDAC.Dato.IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOConfigurationValue.Instance();
        }
    }
}
