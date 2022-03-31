using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Domain
{
    public class EMailProcessType : ObjetoNegocio
    {        
        private const string NAME = "EMailProcessType";

        public int Period { get; set; }

        public string PeriodHours { get; set; }

        public bool CheckLote { get; set; }

        public string Codigo { get; set; }

        public string Descripcion { get; set; }

        public override string ObtenerNombre()
        {
            return NAME;
        }

        public override FrameworkDAC.Dato.IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoEMailProcessType();
        }
    }
}
