using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.ExternalServices.Domain;
using FrameworkDAC.Dato;
using EMailAdmin.ExternalServices.Data.Interfaces;

namespace EMailAdmin.ExternalServices.Data
{
    public class DAORateInformation : DAOObjetoPersistido<RateInformation>, IDAORateInformation
    {
        public bool bSqlActivado { get; set; }
        protected override string NombreConnectionString()
        {
            if (bSqlActivado)
                return "ACNetSql";
            else
                return "ACNet";
        }


        public DAORateInformation()
        {
            bSqlActivado = CapaNegocioDatos.CapaHome.CodigoActivadorHome.HabilitarAcnetSqlServer();
        }
        private static DAORateInformation _instance;

        public static DAORateInformation Instance()
        {
            return _instance ?? (_instance = new DAORateInformation());
        }


        protected override void Completar(RateInformation objetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            objetoPersistido.CountryCode = Convert.ToInt32(dr["PAIS"]);
            objetoPersistido.ProductCode = dr["PRODUCTO"].ToString();
            objetoPersistido.Code = dr["TARIFA"].ToString();
            objetoPersistido.Days = dr["CANT_DIAS"].ToString();
            objetoPersistido.Name = dr["NOMBRE"].ToString();
        }

        protected override void Completar(RateInformation objetoPersistido, System.Data.OracleClient.OracleDataReader dr)
        {
            objetoPersistido.CountryCode = Convert.ToInt32(dr["PAIS"]);
            objetoPersistido.ProductCode = dr["PRODUCTO"].ToString();
            objetoPersistido.Code = dr["TARIFA"].ToString();
            objetoPersistido.Days = dr["CANT_DIAS"].ToString();
            objetoPersistido.Name = dr["NOMBRE"].ToString();
        }

        public RateInformation Get(int countryCode, string productCode, string code, string days)
        {
            if (bSqlActivado)
                return GetSql(countryCode,  productCode,  code,  days);
            else
                return GetOracle(countryCode, productCode, code, days);
        }

        public RateInformation GetOracle(int countryCode, string productCode, string code, string days)
        {
            return ObtenerOracle(
                "SELECT * FROM ICARD.TARIFAS T WHERE T.PAIS = " + countryCode.ToString() + " " +
                "AND T.PRODUCTO = '" + productCode + "' AND T.TARIFA = '" + code + "' AND T.ACTIVO = 'S' " + 
                "AND T.CANT_DIAS = (" +
                    "SELECT MAX(CANT_DIAS) FROM ICARD.TARIFAS T1 WHERE T1.PAIS = T.PAIS " +
                    "AND T1.PRODUCTO = T.PRODUCTO AND T1.TARIFA = T.TARIFA AND " + 
                    "T1.ACTIVO = 'S' AND T1.CANT_DIAS <= " + days + ")", false);
        }

        public RateInformation GetSql(int countryCode, string productCode, string code, string days)
        {
            return ObtenerOracle(
                "SELECT * FROM ICARD.TARIFAS T WHERE T.PAIS = " + countryCode.ToString() + " " +
                "AND T.PRODUCTO = '" + productCode + "' AND T.TARIFA = '" + code + "' AND T.ACTIVO = 'S' " +
                "AND T.CANT_DIAS = (" +
                    "SELECT MAX(CANT_DIAS) FROM ICARD.TARIFAS T1 WHERE T1.PAIS = T.PAIS " +
                    "AND T1.PRODUCTO = T.PRODUCTO AND T1.TARIFA = T.TARIFA AND " +
                    "T1.ACTIVO = 'S' AND T1.CANT_DIAS <= " + days + ")", false);
        }
    }
}
