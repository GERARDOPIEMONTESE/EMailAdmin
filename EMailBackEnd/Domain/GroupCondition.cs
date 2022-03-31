using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class DynamicCondition
    {
        public string DynamicKey { get; set; }
        public string Value { get; set; }
    }

    public class GroupCondition : ObjetoNegocio
    {
        #region Constants

        private const string NAME = "GroupCondition";

        #endregion

        #region Properties

        public int IdGroup { get; set; }
        public ConditionType ConditionType { get; set; }
        public string Value { get; set; }
        public string VisibleCode { get; set; }
        public string VisibleValue { get; set; }
        public string VisibleCountryOfValue { get; set; }
        public string VisibleProductOfValue { get; set; }
        public string DynamicKey { get; set; }

        public string ConditionTypeText
        {
            get { return ConditionType.Description; }
        }

        #endregion

        #region Methods

        public override FrameworkDAC.Dato.IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoGroupCondition();
        }

        public override string ObtenerNombre()
        {
            return NAME;
        }

        #endregion Methods
    }
}
