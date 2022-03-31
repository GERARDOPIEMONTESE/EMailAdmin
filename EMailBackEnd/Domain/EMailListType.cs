using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;
using System.Collections.Generic;

namespace EMailAdmin.BackEnd.Domain
{
    public class EMailListType : ObjetoNegocio
    {
        #region Constant
        public const string CODEPREPURCHASELIST = "LPC";
        private const string NAME = "EMailListType";

        #endregion Constant

        #region Properties

        public string Code { get; set; }
        public string Description { get; set; }
        public int UsuariosAsignados { get; set; }

        #endregion 

        #region Methods

        public override string ObtenerNombre()
        {
            return NAME;
        }

        public override IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoEMailListType();
        }

        #endregion Methods
    }
}
