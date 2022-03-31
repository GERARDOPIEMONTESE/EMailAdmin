using CapaNegocioDatos.CapaNegocio;
using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class Signature_R_Country : ObjetoNegocio
    {
        #region Constant

        private const string NAME = "Signature_R_Country";

        #endregion Constant

        #region Properties

        public int SignatureId { get; set; }

        public Locacion Country { get; set; }

        #endregion Properties

        #region Methods

        public override string ObtenerNombre()
        {
            return NAME;
        }

        public override IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoSignature_R_Country();
        }

        #endregion Methods
    }
}
