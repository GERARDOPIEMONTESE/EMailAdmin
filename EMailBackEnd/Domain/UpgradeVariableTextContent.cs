using FrameworkDAC.Negocio;
using EMailAdmin.BackEnd.Data;
using CapaNegocioDatos.CapaNegocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class UpgradeVariableTextContent : ObjetoNegocio
    {
        #region Constants

        private const string NAME = "UpgradeVariableTextContent";
        
        #endregion

        #region Properties

        public int IdUpgradeVariableText { get; set; }

        public Idioma Language { get; set; }

        public string Content { get; set; }

        #endregion

        public override FrameworkDAC.Dato.IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoUpgradeVariableTextContent();
        }

        public override string ObtenerNombre()
        {
            return NAME;
        }
    }
}
