using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class LinkType : ObjetoCodificado
    {
        #region Constants

        private const string NAME = "LinkType";

        public const string FIXED = "FIXED";

        public const string CONTEXT = "CONTEXT";

        public const string QR = "QR";

        public const string FIXEDIMG = "FIXEDIMG";

        #endregion Constants

        #region Properties

        public string Code { get { return Codigo; } }
        
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
