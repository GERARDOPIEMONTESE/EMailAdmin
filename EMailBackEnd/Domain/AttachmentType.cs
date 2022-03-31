using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class AttachmentType : ObjetoCodificado
    {
        #region Constants

        private const string NAME = "AttachmentType";

        public const string FIXED = "FIXED";

        public const string STRATEGY = "STRATEGY";

        #endregion Constants

        #region Properties

        public string Code { get; set; }
        public string Description { get { return Descripcion; } }

        #endregion Properties

        #region Methods

        public override string ObtenerNombre()
        {
            return NAME;
        }

        #endregion Methods
    }
}
