using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Parametro;
using EMailAdmin.BackEnd.Data.Interfaces;
using AssistCard.ServerDAC.SqlClient;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOPointsReportHistory : DAOObjetoNegocio<PointsReportHistory>, IDAOPointsReportHistory
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override Parametros ParametrosCrear(PointsReportHistory objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("ReportDate", objetoNegocio.ReportDate);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override Parametros ParametrosModificar(PointsReportHistory objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("ReportDate", objetoNegocio.ReportDate);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override Parametros ParametrosEliminar(PointsReportHistory objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("ReportDate", objetoNegocio.ReportDate);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }


        protected override void Completar(PointsReportHistory ObjetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            if (dr["REPORTDATE"] != DBNull.Value)
                ObjetoPersistido.ReportDate = Convert.ToDateTime(dr["REPORTDATE"]);
        }

        public DateTime ObtenerFechaUltimoReporte()
        {
            DateTime fechaUltReporte = DateTime.Now.AddMonths(-1);
            using (SqlConnection oConn = new SqlConnection(ConnectionString()))
            {
                oConn.Open();
                SqlDataReader dr = SqlConnectivity.ExecuteSqlDataReader(NombreConnectionString(),
                        "SELECT MAX(ReportDate) AS MaxReportDate FROM PointsReportHistory WHERE IdStatus NOT IN (25002)");
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        if (!dr.IsDBNull(0))
                            fechaUltReporte = Convert.ToDateTime(dr.GetDateTime(0));
                    }
                }
                oConn.Dispose();
                //oConn.Close();
            }

            return fechaUltReporte;
        }
    }
}
