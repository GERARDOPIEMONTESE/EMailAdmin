using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Domain
{
    public class EmailLog_R_Capita : ObjetoNegocio
    {
        private const string NAME = "EmailLog_R_Capita";
        public EMailLog EmailLog { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public CapaNegocioDatos.CapaNegocio.TipoDocumento TipoDocumento { get; set; }
        public string Documento { get; set; }
        public CapaNegocioDatos.CapaNegocio.Pais Pais { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string RateName { get; set; }
        public string RateCode { get; set; }
        public bool bEnvioLinks { get; set; }
        
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
                return (EmailLog != null ? EmailLog.TemplateName : "");
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
                return (EmailLog != null ? Enum.Parse(typeof(EMailLog.StatusEmailLog), EmailLog.ProcessStatus.ToString()).ToString() : "");
            }
        }

        public string PaisNombre
        {
            get
            {
                return Pais.Nombre;
            }
        }

        public string ApellidoNombre
        {
            get
            {
                return Apellido + " " + Nombre;
            }
        }
        public DateTime StartDate
        {
            get
            {
                return (EmailLog != null ? EmailLog.Fecha : new DateTime());
            }
        }

        public string TipoDocumentoDescripcion
        {
            get
            {
                return (TipoDocumento!=null ?  TipoDocumento.Descripcion: "");
            }
        }

        public override FrameworkDAC.Dato.IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoEmailLog_R_Capita();
        }

        public override string ObtenerNombre()
        {
            return NAME;
        }
    }
}
