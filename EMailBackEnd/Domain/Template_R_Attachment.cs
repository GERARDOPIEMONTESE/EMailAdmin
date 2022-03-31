using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class Template_R_Attachment : ObjetoNegocio
    {
        #region Constant

        private const string NAME = "Template_R_Attachment";

        #endregion Constant

        #region Properties

        public int IdTemplate { get; set; }
        public Attachment Attachment { get; set; }        

        #endregion Properties

        #region Methods

        public override string ObtenerNombre()
        {
            return NAME;
        }

        public override IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoTemplate_R_Attachment();
        }

        #endregion Methods
    }
}
