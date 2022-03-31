using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOEMailProcessLog : IDAOObjetoNegocio
    {
        EMailProcessLog Get(int id);

        EMailProcessLog GetLastLog(int idEMailProcessType);

        IList<EMailProcessLog> Find(DateTime fromDate, DateTime toDate);
    }
}
