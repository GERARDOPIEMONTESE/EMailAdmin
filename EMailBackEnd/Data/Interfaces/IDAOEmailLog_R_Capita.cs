using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOEmailLog_R_Capita : IDAOObjetoNegocio
    {
        IList<EmailLog_R_Capita> Find(string Nombre, string Apellido, string documento, string capita, string plan, int CountryCode, int envioLinks, Nullable<DateTime> fechaDesde, Nullable<DateTime> fechaHasta);
        EmailLog_R_Capita Find(int idEmailLog_R_Capita);
    }
}
