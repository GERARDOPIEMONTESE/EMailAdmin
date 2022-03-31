using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class Content_R_ContentLink : ObjetoNegocio
    {
        #region Constants

        private const string NAME = "Content_R_ContentLink";

        #endregion

        #region Properties

        public int IdContent { get; set; }
        public Link Link { get; set; }
        public int Order { get; set; }

        #endregion

        #region Methods

        public override string ObtenerNombre()
        {
            return NAME;
        }

        public override IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoContent_R_ContentLink();
        }

        #endregion Methods
    }
}
