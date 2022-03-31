using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Dato;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOPointsReportHistory : IDAOObjetoNegocio
    {
        DateTime ObtenerFechaUltimoReporte();
    }
}
