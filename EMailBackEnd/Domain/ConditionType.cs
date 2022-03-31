using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class ConditionType : ObjetoCodificado
    {
        #region Constants

        private const string NAME = "ConditionType";
        public const string DynamicValueCode = "DYNV";

        #endregion Constants

        #region Constants

        public static int Country
        {
            get { return 1; }
        }

        public static int Branch
        {
            get { return 2; }
        }

        public static int Product
        {
            get { return 3; }
        }

        public static int Rate
        {
            get { return 4; }
        }

        public static int Distribution
        {
            get { return 5; }
        }

        public static int DynamicValue
        {
            get { return 6; }
        }

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
