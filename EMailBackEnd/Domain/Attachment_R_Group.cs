using CapaNegocioDatos.CapaNegocio;
using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class Attachment_R_Group : ObjetoNegocio
    {
        #region Constants

        private const string NAME = "Attachment_R_Group";

        #endregion

        #region Properties

        public int IdGroup { get; set; }
        public int IdAttachment { get; set; }
        public Attachment Attachment { get; set; }
        public Modulo Module { get; set; }
        public Group Group { get; set; }
        public string GroupName 
        { 
            get
            {
                if(Group != null)
                {
                    return Group.Name;
                }
                if(IdGroup != 0)
                {
                    Group = DAOLocator.Instance().GetDaoGroup().Get(IdGroup);
                    return Group != null ? Group.Name : "";
                }
                return "";
            } 
        }
        public string ModuleName { get { return Module != null ? Module.Descripcion : ""; } }
        public string ReceiveDescription { get { return ""; } }

        #endregion

        #region Methods

        public override string ObtenerNombre()
        {
            return NAME;
        }

        public override IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoAttachment_R_Group();
        }

        public Attachment_R_Group Clone()
        {
            Attachment_R_Group clone = new Attachment_R_Group();

            clone.Module = Module;
            clone.Attachment = Attachment;

            return clone;
        }

        #endregion Methods
    }
}