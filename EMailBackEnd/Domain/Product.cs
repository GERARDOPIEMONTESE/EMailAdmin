using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class Product : ObjetoCodificado
    {
        #region Constants

        private const string NAME = "Product";

        public const int PRODUCT = 1;

        public const int UPGRADE = 2;

        #endregion Constants

        #region Properties

        public string Code { get; set; }

        public string CountryCode { get; set; }

        public string Description
        {
            get { return Descripcion; }
        }

        public string FullDescription
        {
            get { return Code + " - " + Descripcion; }
        }

        #endregion Properties

        #region Methods

        public override string ObtenerNombre()
        {
            return NAME;
        }

        #endregion Methods
    }
}