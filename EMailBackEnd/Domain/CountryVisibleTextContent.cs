using FrameworkDAC.Negocio;
using EMailAdmin.BackEnd.Data;
using CapaNegocioDatos.CapaNegocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class CountryVisibleTextContent : ObjetoNegocio
    {
        private const string NAME = "CountryVisibleTextContent";

        #region Attributes

        private int _idCountryVisibleText;

        private Idioma _language;

        private string _content;

        #endregion

        #region Properties

        public int IdCountryVisibleText
        {
            get
            {
                return _idCountryVisibleText;
            }
            set
            {
                _idCountryVisibleText = value;
            }
        }

        public Idioma Language
        {
            get
            {
                return _language;
            }
            set
            {
                _language = value;
            }
        }

        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
            }
        }

        #endregion

        public override FrameworkDAC.Dato.IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoCountryVisibleTextContent();
        }

        public override string ObtenerNombre()
        {
            return NAME;
        }
    }
}
