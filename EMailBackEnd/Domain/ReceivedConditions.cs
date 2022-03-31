using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Domain
{
    public class ReceivedConditions: ObjetoNegocio
    {
        public override string ObtenerNombre()
        {
            return "ReceivedConditions";
        }

        public string VoucherCode { get; set; }
        public int CountryCode { get; set; }
        public int IdTemplate { get; set; }

        public override IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetReceivedConditions();
        }
    }
}
