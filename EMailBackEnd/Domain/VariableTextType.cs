using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class VariableTextType : ObjetoCodificado
    {
        #region Constants
        private const string NAME = "VariableTextType";
        public const string LINKTYPE = "LINK";
        public const string TEXTTYPE = "TEXT";
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
