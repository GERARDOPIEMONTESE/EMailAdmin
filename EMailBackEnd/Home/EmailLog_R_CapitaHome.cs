using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Home
{
    public class EmailLog_R_CapitaHome
    {
        public static IList<EmailLog_R_Capita> Find(string Nombre, string Apellido, string documento, string capita, string plan, 
            int CountryCode, int envioLinks, string fechaDesde, string fechaHasta)
        {
            return DAOLocator.Instance().GetDaoEmailLog_R_Capita().Find(Nombre, Apellido, documento, capita, plan,
                CountryCode, envioLinks, Utils.DateUtil.getFiltroFecha(fechaDesde), Utils.DateUtil.getFiltroFecha(fechaHasta));
        }        
    }
}
