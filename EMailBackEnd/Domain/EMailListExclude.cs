using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Domain
{
    public class EMailListExclude : ObjetoNegocio
    {
        #region Constant

        private const string NAME = "EMailListExclude";

        #endregion Constant

        #region Constructor
        public EMailListExclude()
        {
        }
        #endregion

        #region Properties

        public string AgencyCode { get; set; }
        public int Branch { get; set; }
        public int CountryCode { get; set; }
        
        //solo para visualizar
        public string RazonSocial { get; set; }
        public string Denominacion { get; set; }
        public string Pais { get; set; }

        public string FullName
        {
            get
            {
                return RazonSocial + " - " + Denominacion;
            }
        }

        #endregion

        #region Methods

        public override string ObtenerNombre()
        {
            return NAME;
        }

        public override IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoEMailListExclude();
        }

        #endregion Methods
    }
}
    
