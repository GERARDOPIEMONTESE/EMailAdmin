using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain.External;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Data.Interfaces;

namespace EMailAdmin.BackEnd.Data.External
{
    public class DAOExternalVoucher : DAOObjetoPersistido<ExternalVoucher>, IDAOExternalVoucher
    {
        public bool bSqlActivado { get; set; }
        protected override string NombreConnectionString()
        {
            if (bSqlActivado)
                return "ACNetSql";
            else
                return "ACNet";
        }


        public DAOExternalVoucher()
        {
            bSqlActivado = CapaNegocioDatos.CapaHome.CodigoActivadorHome.HabilitarAcnetSqlServer();
        }
        private static DAOExternalVoucher _instance;

        public static DAOExternalVoucher Instance()
        {
            return _instance ?? (_instance = new DAOExternalVoucher());
        }


        protected override void Completar(ExternalVoucher ObjetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            ObjetoPersistido.CountryCode = Convert.ToInt32(dr["PAIS"]);
            ObjetoPersistido.Code = dr["CODIGO"].ToString();
        }

        protected override void Completar(ExternalVoucher ObjetoPersistido, System.Data.OracleClient.OracleDataReader dr)
        {
            ObjetoPersistido.CountryCode = Convert.ToInt32(dr["PAIS"]);
            ObjetoPersistido.Code = dr["CODIGO"].ToString();
        }

        public IList<ExternalVoucher> Find(int countryCode, DateTime fromDate, DateTime toDate)
        {
            return BuscarOracle(GetQuery(countryCode, fromDate, toDate));
        }

        #region Queries

        private string GetFormattedDate(DateTime date)
        {
            return (date.Day < 10 ? "0" : "") + date.Day + "/" + 
                (date.Month < 10 ? "0" : "") + date.Month + "/" + date.Year + " " + date.Hour + ":" + date.Minute;
        }

        public string GetQuery(int countryCode, DateTime fromDate, DateTime toDate)
        {
            if (bSqlActivado)
                return GetQuerySql(countryCode, fromDate, toDate);
            else
                return GetQueryOracle(countryCode, fromDate, toDate);
        }

        private string GetQueryOracle(int countryCode, DateTime fromDate, DateTime toDate)
        {
            string query = "SELECT * FROM VOUCHER WHERE FEC_BAJA IS NULL AND FECHA_EMISION >= TO_DATE('" + GetFormattedDate(fromDate) +
                "', 'dd/MM/yyyy HH24:MI') AND FECHA_EMISION <= TO_DATE('" + GetFormattedDate(toDate) + "', 'dd/MM/yyyy HH24:MI')";
            if (countryCode != 0)
            {
                query += " AND PAIS = " + countryCode; 
            }

            return query;
        }
        private string GetQuerySql(int countryCode, DateTime fromDate, DateTime toDate)
        {
            string query = "SELECT * FROM VOUCHER V WHERE FEC_BAJA IS NULL AND DATEADD(dd, 0, DATEDIFF(dd, 0, getdate())) between V.FEC_VIG_INIC and V.FEC_VIF_FIN ";
            if (countryCode != 0)
            {
                query += " AND PAIS = " + countryCode;
            }

            return query;
        }
        #endregion
    }
}
