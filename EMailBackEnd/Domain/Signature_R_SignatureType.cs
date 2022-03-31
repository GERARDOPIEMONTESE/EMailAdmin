using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class Signature_R_SignatureType : ObjetoNegocio
    {
        #region Constant

        private const string NAME = "Signature_R_SignatureType";

        #endregion Constant

        #region Properties

        public SignatureType SignatureType { get; set; }

        public int SignatureId { get; set; }

        #endregion Properties

        #region Methods

        public override string ObtenerNombre()
        {
            return NAME;
        }

        public override IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoSignature_R_SignatureType();
        }

        #endregion Methods
    }
}
