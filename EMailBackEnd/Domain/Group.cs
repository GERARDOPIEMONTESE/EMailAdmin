using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Negocio;
using System.Collections.Generic;

namespace EMailAdmin.BackEnd.Domain
{
    public class Group : ObjetoNegocio
    {
        #region Constants

        private const string NAME = "Group";

        #endregion

        #region Attributes

        private IList<GroupCondition> _conditions = new List<GroupCondition>();

        #endregion

        #region Properties

        public string Name { get; set; }

        public int TotalWeight { get; set; }

        public GroupType GroupType { get; set; }

        public string GroupTypeDescription {get { return GroupType.Descripcion; }}

        public IList<GroupCondition> Conditions 
        {
            get
            {
                return _conditions;
            }
            set
            {
                _conditions = value;
            }
        }
        
        #endregion

        #region Methods

        public override FrameworkDAC.Dato.IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoGroup();
        }

        public override string ObtenerNombre()
        {
            return NAME;
        }

        #endregion Methods
    }
}
