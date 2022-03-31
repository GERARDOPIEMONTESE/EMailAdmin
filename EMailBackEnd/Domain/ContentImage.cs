using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class ContentImage : ObjetoNegocio
    {
        #region Constants

        private const string NAME = "ContentImage";

        #endregion

        #region Properties

        public object Content { get; set; }
        public string Type { get; set; }
        public decimal Dimenssion { get; set; }
        public string Name { get; set; }

        #endregion

        #region Methods

        public override string ObtenerNombre()
        {
            return NAME;
        }

        public override IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoContentImage();
        }

        #endregion Methods
    }
}
