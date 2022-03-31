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
        protected override string NombreConnectionString()
        {
            return "ACNet";
        }

        protected override void Completar(RateInformation objetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
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
