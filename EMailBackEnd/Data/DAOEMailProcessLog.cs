using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using FrameworkDAC.Parametro;
using EMailAdmin.BackEnd.Data.Interfaces;
using System.Globalization;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOEMailProcessLog : DAOObjetoNegocio<EMailProcessLog>, IDAOEMailProcessLog
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override FrameworkDAC.Parametro.Parametros ParametrosCrear(EMailProcessLog objetoNegocio)
        {
            Parametros parameters = new Parametros();
            parameters.AgregarParametro("IdEMailProcessType", objetoNegocio.IdEMailProcessType);
            parameters.AgregarParametro("StartDate", objetoNegocio.StartDate);
            parameters.AgregarParametro("EndDate", objetoNegocio.EndDate);

            return parameters;
        }

        protected override FrameworkDAC.Parametro.Parametros ParametrosModificar(EMailProcessLog objetoNegocio)
        {
            Parametros parameters = new Parametros();
            parameters.AgregarParametro("IdEMailProcessLog", objetoNegocio.Id);
            parameters.AgregarParametro("IdEMailProcessType", objetoNegocio.IdEMailProcessType);
            parameters.AgregarParametro("StartDate", objetoNegocio.StartDate);
            parameters.AgregarParametro("EndDate", objetoNegocio.EndDate);
            parameters.AgregarParametro("IdLote", objetoNegocio.IdLote);
            return parameters;
        }

        protected override FrameworkDAC.Parametro.Parametros ParametrosEliminar(EMailProcessLog ObjetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override void Completar(EMailProcessLog objetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdEMailProcessLog"]);
            objetoPersistido.IdEMailProcessType = Convert.ToInt32(dr["IdEMailProcessType"]);
            objetoPersistido.StartDate = Convert.ToDateTime(dr["StartDate"]);
            objetoPersistido.EndDate = Convert.ToDateTime(dr["EndDate"]);
            objetoPersistido.EMailProcessType = DAOLocator.Instance().
                GetDaoEMailProcessType().Get(objetoPersistido.IdEMailProcessType);
            if (dr["IdLote"] != DBNull.Value)
                objetoPersistido.IdLote = Convert.ToInt32(dr["IdLote"]);
        }

        #region IDAOEMailProcessLog Members

        public EMailProcessLog Get(int id)
        {
            return Obtener(id);
        }

        public EMailProcessLog GetLastLog(int idEMailProcessType)
        {
            Parametros parameters = new Parametros();
            parameters.AgregarParametro("IdEMailProcessType", idEMailProcessType);

            return Obtener(new Filtro(parameters, "dbo.EMailProcessLog_Tx_Last_IdEMailProcessType"));
        }

        public IList<EMailProcessLog> Find(DateTime fromDate, DateTime toDate)
        {
            Parametros parameters = new Parametros();
            parameters.AgregarParametro("FromDate", fromDate.ToString("yyyy/MM/dd 00:00", CultureInfo.InvariantCulture));
            parameters.AgregarParametro("ToDate", toDate.ToString("yyyy/MM/dd 23:59", CultureInfo.InvariantCulture));

            return Buscar(new Filtro(parameters, "dbo.EMailProcessLog_Tx_Dates"));
        }

        #endregion
    }
}
