using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain.Information;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOPaxPassedAway : IDAOObjetoNegocio
    {
        PaxPassedAway Get(int id);

        PaxPassedAway Get(int countryCode, string voucherCode, string nationalId);
    }
}
