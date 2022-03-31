using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class Rate : ObjetoCodificado
    {
        #region Constants

        private const string NAME = "Rate";

        #endregion Constants

        #region Properties

        public string Code { get; set; }

        public string Description
        {
            get { return Descripcion; }
        }

        public bool Annual { get; set; }

        public string Modality { get; set; }

        #endregion Properties

        #region Methods

        public override string ObtenerNombre()
        {
            return NAME;
        }

        #endregion Methods
    }
}
