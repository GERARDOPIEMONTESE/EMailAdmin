using System.Collections.Generic;
using System.Text;
using CapaNegocioDatos.CapaNegocio;
using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Dato;

namespace EMailAdmin.BackEnd.Domain
{
    public class Signature : VariableText
    {
        #region Constant

        private const string NAME = "Signature";

        #endregion Constant

        #region Attributes

        //OJO! Dejar asi porque es necesaria la inicializacion.
        private IList<Locacion> _Countries = new List<Locacion>();

        private IList<SignatureType> _SignatureTypes = new List<SignatureType>();

        private IList<SignatureContent> _Content = new List<SignatureContent>();

        #endregion

        #region Properties

        public IList<Locacion> Countries 
        {
            get
            {
                return _Countries;
            }
            set
            {
                _Countries = value;
            }
        }

        public IList<SignatureType> SignatureTypes 
        {
            get
            {
                return _SignatureTypes;
            }
            set
            {
                _SignatureTypes = value;
            }
        }

        public IList<SignatureContent> Content 
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

        public string SignatureTypesDescription
        {
            get
            {
                var sb = new StringBuilder();
                if (SignatureTypes != null)
                {
                    if (SignatureTypes.Count > 0)
                    {
                        sb.Append(SignatureTypes[0].Description);
                    }
                    if (SignatureTypes.Count > 1)
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
            return DAOLocator.Instance().GetDaoSignature();
        }

        protected override string GetConcreteName()
        {
            return NAME;
        }

        public SignatureContent GetContent(int idLanguage)
        {
            foreach (SignatureContent content in Content)
            {
                if (content.Language.Id == idLanguage)
                {
                    return content;
                }
            }
            return new SignatureContent();
        }

        #endregion Methods
    }
}
