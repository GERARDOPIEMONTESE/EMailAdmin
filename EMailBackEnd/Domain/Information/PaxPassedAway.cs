using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Domain.Information
{
    public class PaxPassedAway : ObjetoNegocio
    {
        private const string NAME = "PaxPassedAway";

        #region Properties

        public int CountryCode { get; set; }

        public string VoucherCode { get; set; }

        public string NationalId { get; set; }

        public bool IsDead { get; set; }

        #endregion

        public override FrameworkDAC.Dato.IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoPaxPassedAway();
        }

        public override string ObtenerNombre()
        {
            return NAME;
        }
    }
}
