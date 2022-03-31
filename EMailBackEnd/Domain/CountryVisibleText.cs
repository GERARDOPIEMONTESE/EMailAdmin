using System.Collections.Generic;
using System.Text;
using CapaNegocioDatos.CapaNegocio;
using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Dato;

namespace EMailAdmin.BackEnd.Domain
{
    public class CountryVisibleText : VariableText
    {
        #region Constant

        private const string NAME = "CountryVisibleText";

        #endregion Constant

        #region Attributes

        //OJO! Dejar asi porque es necesaria la inicializacion.
        private IList<Locacion> _countries = new List<Locacion>();

        private IList<CountryVisibleTextType> _countryVisibleTextTypes = new List<CountryVisibleTextType>();

        private IList<CountryVisibleTextContent> _content = new List<CountryVisibleTextContent>();

        #endregion

        #region Properties

        public IList<Locacion> Countries 
        {
            get
            {
                return _countries;
            }
            set
            {
                _countries = value;
            }
        }

        public IList<CountryVisibleTextType> CountryVisibleTextTypes 
        {
            get
            {
                return _countryVisibleTextTypes;
            }
            set
            {
                _countryVisibleTextTypes = value;
            }
        }

        public IList<CountryVisibleTextContent> Content 
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

        public string CountriesDescription
        {
            get
            {
                var sb = new StringBuilder();
                if (Countries != null)
                {
                    if (Countries.Count > 0)
                    {
                        sb.Append(Countries[0].Id == 0 ? "All" : Countries[0].Nombre);
                    }
                    if (Countries.Count > 1)
                    {
                        sb.Append("...");
                    }
                }
                return sb.ToString();
            }
        }

        public string CountryVisibleTextTypesDescription
        {
            get
            {
                var sb = new StringBuilder();
                if (CountryVisibleTextTypes != null)
                {
                    if (CountryVisibleTextTypes.Count > 0)
                    {
                        sb.Append(CountryVisibleTextTypes[0].Description);
                    }
                    if (CountryVisibleTextTypes.Count > 1)
                    {
                        sb.Append("...");
                    }
                }
                return sb.ToString();
            }
        }

        #endregion

        #region Methods

        protected override IDAOObjetoNegocio GetConcreteDao()
        {
            return DAOLocator.Instance().GetDaoCountryVisibleText();
        }

        protected override string GetConcreteName()
        {
            return NAME;
        }

        public CountryVisibleTextContent GetContent(int idLanguage)
        {
            foreach (CountryVisibleTextContent content in Content)
            {
                if (content.Language.Id == idLanguage)
                {
                    return content;
                }
            }
            return new CountryVisibleTextContent();
        }

        #endregion Methods
    }
}
