using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class UpgradeVariableText_R_Upgrade : ObjetoNegocio
    {
        #region Constant

        private const string NAME = "UpgradeVariableText_R_Product";

        #endregion Constant

        #region Properties

        public int UpgradeVariableTextId { get; set; }

        public Product Upgrade { get; set; }

        #endregion Properties

        #region Methods

        public override string ObtenerNombre()
        {
            return NAME;
        }

        public override IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoUpgradeVariableText_R_Upgrade();
        }

        #endregion Methods
    }
}
