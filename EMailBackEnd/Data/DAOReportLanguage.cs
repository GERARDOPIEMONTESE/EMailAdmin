using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOReportLanguage : DAOObjetoCodificado<ReportLanguage>, IDAOReportLanguage
    {
        #region IDAOReportLanguage Members

        public IList<ReportLanguage> Find(int id)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdReportLanguage", id);
            return Buscar(new Filtro(parameters, "ReportLanguage_Tx_Filters"));
        }

        public IList<ReportLanguage> FindByIdLanguage(int idLanguage, int idStrategy)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("idLanguage", idLanguage);
            parameters.AgregarParametro("idStrategy", idStrategy);
            return Buscar(new Filtro(parameters, "ReportLanguage_Tx_Filters"));
        }

        public IList<ReportLanguage> Find(int idLanguage, int idStrategy, string key)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("idLanguage", idLanguage);
            parameters.AgregarParametro("idStrategy", idStrategy);
            parameters.AgregarParametro("key", key);
            return Buscar(new Filtro(parameters, "ReportLanguage_Tx_Filters"));
        }

        public IDictionary<string, string> FindByLanguage(int idLanguage, int idStrategy)
        {
            return FindByIdLanguage(idLanguage, idStrategy).ToDictionary(reportLanguage => reportLanguage.Key, reportLanguage => reportLanguage.Value);
        }

        #endregion

        #region Methods

        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override void Completar(ReportLanguage ObjetoPersistido, SqlDataReader dr)
        {
            ObjetoPersistido.Id = Convert.ToInt32(dr["IdReportLanguage"]);
            ObjetoPersistido.IdLanguage = Convert.ToInt32(dr["IdLanguage"]);
            ObjetoPersistido.IdStrategy = Convert.ToInt32(dr["IdStrategy"]);
            ObjetoPersistido.Key = dr["key"].ToString();
            ObjetoPersistido.Descripcion = dr["Value"].ToString();
        }

        #endregion Methods
    }
}