using System.Collections.Generic;
using System.Text;
using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Dato;

namespace EMailAdmin.BackEnd.Domain
{
    public class UpgradeVariableText : VariableText
    {
        #region Constant

        private const string NAME = "UpgradeVariableText";

        #endregion Constant

        #region Attributes

        //OJO! Dejar asi porque es necesaria la inicializacion.
        private IList<Product> _upgrades = new List<Product>();

        private IList<UpgradeVariableTextType> _upgradeVariableTextTypes = new List<UpgradeVariableTextType>();

        private IList<UpgradeVariableTextContent> _content = new List<UpgradeVariableTextContent>();

        #endregion

        #region Properties

        public IList<Product> Upgrades 
        {
            get
            {
                return _upgrades;
            }
            set
            {
                _upgrades = value;
            }
        }

        public IList<UpgradeVariableTextType> UpgradeVariableTextTypes 
        {
            get
            {
                return _upgradeVariableTextTypes;
            }
            set
            {
                _upgradeVariableTextTypes = value;
            }
        }

        public IList<UpgradeVariableTextContent> Content 
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

        public string UpgradeDesription
        {
            get
            {
                var sb = new StringBuilder();
                if (Upgrades != null)
                {
                    if (Upgrades.Count > 0)
                    {
                        sb.Append(Upgrades[0].Id == 0 ? "All" : Upgrades[0].Description);
                    }
                    if (Upgrades.Count > 1)
                    {
                        sb.Append("...");
                    }
                }
                return sb.ToString();
            }
        }

        public string UpgradeVariableTextTypesDescription
        {
            get
            {
                var sb = new StringBuilder();
                if (UpgradeVariableTextTypes != null)
                {
                    if (UpgradeVariableTextTypes.Count > 0)
                    {
                        sb.Append(UpgradeVariableTextTypes[0].Description);
                    }
                    if (UpgradeVariableTextTypes.Count > 1)
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
            return DAOLocator.Instance().GetDaoUpgradeVariableText();
        }

        protected override string GetConcreteName()
        {
            return NAME;
        }

        public UpgradeVariableTextContent GetContent(int idLanguage)
        {
            foreach (UpgradeVariableTextContent content in Content)
            {
                if (content.Language.Id == idLanguage)
                {
                    return content;
                }
            }
            return new UpgradeVariableTextContent();
        }

        #endregion Methods
    }
}
