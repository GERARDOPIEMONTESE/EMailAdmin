using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class UpgradeVariableText_R_UpgradeVariableTextType : ObjetoNegocio
    {
        #region Constant

        private const string NAME = "UpgradeVariableText_R_UpgradeVariableTextType";

        #endregion Constant

        #region Properties

        public UpgradeVariableTextType UpgradeVariableTextType { get; set; }

        public int UpgradeVariableTextId { get; set; }

        #endregion Properties

        #region Methods

        public override string ObtenerNombre()
        {
            return NAME;
        }

        public override IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoUpgradeVariableText_R_UpgradeVariableTextType();
        }

        #endregion Methods
    }
}
