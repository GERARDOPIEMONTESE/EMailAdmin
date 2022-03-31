using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class EMailContactType : ObjetoNegocio
    {
        #region Constant

        private const string NAME = "EMailContactType";

        #endregion Constant

        #region Properties

        public string Code { get; set; }
        public string Description { get; set; }

        #endregion 

        #region Methods

        public override string ObtenerNombre()
        {
            return NAME;
        }

        public override IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoEMailContactType();
        }

        #endregion Methods
    }
}
