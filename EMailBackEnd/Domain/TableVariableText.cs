using FrameworkDAC.Negocio;
using EMailAdmin.BackEnd.Data;
using CapaNegocioDatos.CapaNegocio;
using FrameworkDAC.Dato;
using System.Collections.Generic;
using System.Linq;

namespace EMailAdmin.BackEnd.Domain
{
    public class TableVariableText : ObjetoNegocio
    {
        #region Constants

        private const string NAME = "TableVariableText";
        
        #endregion

        #region Properties
                
        public string Name { get; set; }        

        public IList<TableVariableTextContent> Contents { get; set; }

        #endregion

        public TableVariableText()
        {
        }
      
        public override IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoTableVariableText();
        }

        public override string ObtenerNombre()
        {
            return NAME;
        }

        public TableVariableTextContent GetContent(int idLanguage)
        {
            TableVariableTextContent tc = null;
            try
            {
                tc = this.Contents.Where(c => c.Language.Id == idLanguage).First();
            }
            catch
            {
                if (this.Contents.Count == 1)
                    tc = this.Contents.First();
            }
            return tc;
        }
    }
}
