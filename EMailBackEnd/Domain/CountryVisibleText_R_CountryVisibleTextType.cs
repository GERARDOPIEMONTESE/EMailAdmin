using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class CountryVisibleText_R_CountryVisibleTextType : ObjetoNegocio
    {
        #region Constant

        private const string NAME = "CountryVisibleText_R_CountryVisibleTextType";

        #endregion Constant

        #region Properties

        public CountryVisibleTextType CountryVisibleTextType { get; set; }

        public int CountryVisibleTextId { get; set; }

        #endregion Properties

        #region Methods

        public override string ObtenerNombre()
        {
            return NAME;
        }

        public override IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoCountryVisibleText_R_CountryVisibleTextType();
        }

        #endregion Methods
    }
}
