using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class GroupType : ObjetoCodificado
    {
        #region Constants

        private const string NAME = "GroupType";
        public const string ATTACHMENTGROUP = "ATTCH";
        public const string TEMPLATEGROUP = "TEMP";
        public const string NONE = "NONE";

        #endregion Constants

        #region Properties

        public string Description
        {
            get { return Descripcion; }
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