using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;
using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Dato;

namespace EMailAdmin.BackEnd.Domain
{
    public class ContentAttachment : ObjetoNegocio
    {
        public const string HEADER = "HEADER";
        public const string FOOTER = "FOOTER";

        #region Constants

        private const string NAME = "ContentAttachment";

        #endregion

        #region Properties

        public int IdTemplate { get; set; }
        public int IdAttachment { get; set; }
        public int IdLanguage { get; set; }
        public string CodeRPT { get; set; }
        public string Body { get; set; }

        #endregion

        public override IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoContentAttachment();
        }

        public override string ObtenerNombre()
        {
            return NAME;
        }
    }
}
