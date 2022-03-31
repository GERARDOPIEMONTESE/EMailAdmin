using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class Content_R_ContentCountryVisibleTextType : ObjetoNegocio
    {
        #region Constants

        private const string NAME = "Content_R_ContentCountryVisibleTextType";

        #endregion

        #region Properties

        public int IdContent { get; set; }
        public CountryVisibleTextType CountryVisibleTextType { get; set; }

        #endregion

        #region Methods

        public override string ObtenerNombre()
        {
            return NAME;
        }

        public override IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoContent_R_ContentCountryVisibleTextType();
        }

        #endregion Methods
    }
}

