using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMailAdmin.BackEnd.Domain
{
    public class EstrategyAttachmentTemplate : ObjetoNegocio
    {
        private const string NAME = "EstrategyAttachmentTemplate";
                
        public int IdAttachment { get; set; }

        public int IdTemplate { get; set; }

        public int IdTemplateAttachment { get; set; }

        public string TemplateName { get; set; }

        public override string ObtenerNombre()
        {
            return NAME;
        }

        public override FrameworkDAC.Dato.IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoEstrategyAttachmentTemplate();
        }
    }
}
