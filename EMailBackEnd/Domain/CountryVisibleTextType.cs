using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class CountryVisibleTextType : ObjetoNegocio
    {
        #region Constants

        private const string NAME = "CountryVisibleTextType";

        #endregion Constants

        #region Properties

        public string Code { get; set; }
        public string Description { get; set; }

        #endregion Properties

        #region Methods

        public override string ObtenerNombre()
        {
            return NAME;
        }

        public override IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoCountryVisibleTextType();
        }

        #endregion Methods
    }
}
