using FrameworkDAC.Negocio;
using EMailAdmin.BackEnd.Data;
using CapaNegocioDatos.CapaNegocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class SignatureContent : ObjetoNegocio
    {
        private const string NAME = "SignatureContent";

        #region Attributes

        private int _IdSignature;

        private Idioma _Language;

        private string _Content;

        #endregion

        #region Properties

        public int IdSignature
        {
            get
            {
                return _IdSignature;
            }
            set
            {
                _IdSignature = value;
            }
        }

        public Idioma Language
        {
            get
            {
                return _Language;
            }
            set
            {
                _Language = value;
            }
        }

        public string Content
        {
            get
            {
                return _Content;
            }
            set
            {
                _Content = value;
            }
        }

        #endregion

        public override FrameworkDAC.Dato.IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoSignatureContent();
        }

        public override string ObtenerNombre()
        {
            return NAME;
        }
    }
}
