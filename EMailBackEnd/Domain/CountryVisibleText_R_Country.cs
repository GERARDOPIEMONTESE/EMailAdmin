using CapaNegocioDatos.CapaNegocio;
using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class CountryVisibleText_R_Country : ObjetoNegocio
    {
        #region Constant

        private const string NAME = "CountryVisibleText_R_Country";

        #endregion Constant

        #region Properties

        public int CountryVisibleTextId { get; set; }

        public Locacion Country { get; set; }

        #endregion Properties

        #region Methods

        public override string ObtenerNombre()
        {
            return NAME;
        }

        public override IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoCountryVisibleText_R_Country();
        }

        #endregion Methods
    }
}
