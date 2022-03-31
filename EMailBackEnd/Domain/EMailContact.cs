using System.Collections.Generic;
using System.Text;
using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Dato;

namespace EMailAdmin.BackEnd.Domain
{
    public class EMailContact : VariableText
    {
        private const string NAME = "EMailContact";

        #region Attributes

        private IList<EMailContactContent> _content = new List<EMailContactContent>();

        private IList<EMailContactLocation> _location = new List<EMailContactLocation>();

        #endregion

        #region Properties

        public EMailContactType EMailContactType { get; set; }

        public string EMail { get; set; }

        public IList<EMailContactContent> Content
        {
            get { return _content; }
            set { _content = value; }
        }

        public IList<EMailContactLocation> Location
        {
            get { return _location; }
            set { _location = value; }
        }

        public string CountriesDescription
        {
            get
            {
                var sb = new StringBuilder();
                if (Location != null)
                {
                    if (Location.Count > 0)
                    {
                        sb.Append(Location[0].Id == 0 ? "All" : Location[0].Name);
                    }
                    if (Location.Count > 1)
                    {
                        sb.Append("...");
                    }
                }
                return sb.ToString();
            }
        }

        #endregion

        protected override IDAOObjetoNegocio GetConcreteDao()
        {
            return DAOLocator.Instance().GetDaoEMailContact();
        }

        protected override string GetConcreteName()
        {
            return NAME;
        }

        public EMailContactContent GetContactContent(int idLanguage)
        {
            foreach (EMailContactContent content in Content)
            {
                if (content.Language.Id == idLanguage)
                {
                    return content;
                }
            }
            return new EMailContactContent();
        }
    }
}