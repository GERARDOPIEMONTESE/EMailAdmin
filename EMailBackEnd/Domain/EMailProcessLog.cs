using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Domain
{
    public class EMailProcessLog : ObjetoNegocio
    {
        private const string NAME = "EMailProcessLog";

        public int IdEMailProcessType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public EMailProcessType EMailProcessType { get; set; }

        public Nullable<int> IdLote { get; set; }

        public string ProcessTypeDescription
        {
            get
            {
                return EMailProcessType == null ? "-" : EMailProcessType.Descripcion;
            }
        }

        public override FrameworkDAC.Dato.IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoEMailProcessLog();
        }

        public override string ObtenerNombre()
        {
            return NAME;
        }
    }
}
