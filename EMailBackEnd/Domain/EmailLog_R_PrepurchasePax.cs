using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Domain
{
    public class EmailLog_R_PrepurchasePax : ObjetoNegocio
    {
        private const string NAME = "EmailLog_R_PrepurchasePax";

        public int CodigoPaxBox { get; set; }
        public string CodigoVerif { get; set; }
        public string VoucherGroup { get; set; }
        public EMailLog EmailLog { get; set; }
        public CapaNegocioDatos.CapaNegocio.Pais Pais { get; set; }

        public override IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoEmailLog_R_PrepurchasePax();
        }

        public override string ObtenerNombre()
        {
            return NAME;
        }

        public string MailTo
        {
            get
            {
                return (EmailLog != null ? EmailLog.MailTo : "");
            }
        }

        public string TemplateName
        {
            get
            {
                return (EmailLog!=null? EmailLog.TemplateName : "");
            }
        }

        public int ProcessStatus
        {
            get
            {
                return (EmailLog != null ? EmailLog.ProcessStatus : 0);
            }
        }

        public string ProcessStatusName
        {
            get
            {
                return (EmailLog!=null ? Enum.Parse(typeof(EMailLog.StatusEmailLog), EmailLog.ProcessStatus.ToString()).ToString():"");
            }
        }

        public string PaisNombre
        {
            get
            {
                return Pais.Nombre;
            }
        }
        public DateTime StartDate
        {
            get
            {
                return (EmailLog != null ? EmailLog.Fecha : new DateTime());
            }
        }       

    }
}
