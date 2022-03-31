using FrameworkDAC.Negocio;
using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Dato;

namespace EMailAdmin.BackEnd.Domain
{
    public class VariableText : ObjetoNegocio
    {
        #region Constants

        private const string NAME = "VariableText";
        
        #endregion

        #region Properties

        public string Name { get; set; }


        public bool IsDynamicValue
        {
            get
            {
                return (Name == "DynamicValue");
            }
        }

        #endregion

        #region Methods

        protected virtual IDAOObjetoNegocio GetConcreteDao()
        {
            return DAOLocator.Instance().GetDaoVariableText();
        }

        protected virtual string GetConcreteName()
        {
            return NAME;
        }

        public override IDAOObjetoNegocio ObtenerDAO()
        {
            return GetConcreteDao();
        }

        public override string ObtenerNombre()
        {
            return GetConcreteName();
        }

        #endregion Methods
    }
}
