using CapaNegocioDatos.CapaNegocio;
using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class Group_R_Template : ObjetoNegocio
    {
        #region Constants

        private const string NAME = "Group_R_Template";

        #endregion

        #region Properties

        public int IdGroup { get; set; }
        public int IdTemplate { get; set; }
        public Template Template { get; set; }
        public Modulo Module { get; set; }
        public bool Receive { get; set; }
        public Group Group { get; set; }
        public string GroupName
        {
            get
            {
                if (Group != null)
                {
                    return Group.Name;
                }
                if (IdGroup != 0)
                {
                    Group = DAOLocator.Instance().GetDaoGroup().Get(IdGroup);
                    return Group != null ? Group.Name : "";
                }
                return "";
            }
        }
        public string ModuleName { get { return Module != null ? Module.Descripcion : ""; } }
        public string ReceiveDescription { get { return Receive ? "True" : "False"; } }

        #endregion

        #region Methods

        public override string ObtenerNombre()
        {
            return NAME;
        }

        public override IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoGroup_R_Template();
        }

        public Group_R_Template Clone()
        {
            Group_R_Template clone = new Group_R_Template();

            clone.Module = Module;
            clone.Receive = Receive;
            clone.Group = Group;
            clone.IdGroup = IdGroup;

            return clone;
        }
    
        #endregion Methods
    }
}