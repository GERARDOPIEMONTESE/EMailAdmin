using FrameworkDAC.Negocio;
using EMailAdmin.BackEnd.Data;
using CapaNegocioDatos.CapaNegocio;
using FrameworkDAC.Dato;

namespace EMailAdmin.BackEnd.Domain
{
    public class TableVariableTextContent : ObjetoNegocio
    {
        public enum TableTypeContent
        {            
            HEADER=0,
            FOOTER=1,
            TABLESTYLE=2
        }
        #region Constants

        private const string NAME = "TableVariableTextContent";
        
        #endregion

        #region Properties

        public int IdTableVariableText { get; set; }

        public Idioma Language { get; set; }

        public string Content { get; set; }

        public TableTypeContent IdTypeContent { get; set; }

        #endregion

        public TableVariableTextContent()
        {
            Language = new Idioma();
        }

        public override IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoTableVariableTextContent();
        }

        public override string ObtenerNombre()
        {
            return NAME;
        }
    }
}
