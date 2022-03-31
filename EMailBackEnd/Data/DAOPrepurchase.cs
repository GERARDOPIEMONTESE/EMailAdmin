using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.ExternalServices.Domain;
using FrameworkDAC.Dato;
using EMailAdmin.ExternalServices.Data.Interfaces;
using EMailAdmin.BackEnd.Domain.External;
using System.Data.OracleClient;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Parametro;

namespace EMailAdmin.ExternalServices.Data
{
    public class DAOPrepurchase : DAOObjetoPersistido<PrepurchaseInformation>, IDAOPrepurchase
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
            //return "ACNet"; //oracle
        }

        protected override void Completar(PrepurchaseInformation objetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            objetoPersistido.CountryCode = Convert.ToInt32(dr["PAIS"]);
            objetoPersistido.AgencyCode = dr["CODIGO"].ToString();
            objetoPersistido.AgencyName = dr["RAZON_SOCIAL"].ToString();
            objetoPersistido.AgencySuc = dr["CODIGO_SUC"].ToString();
            objetoPersistido.Type = PrepurchaseInformation.ConvertToUnitType(dr["TIPO_COMPRA"].ToString());
            objetoPersistido.Cant_precompra = Double.Parse(dr["CANT_CPRA"].ToString());
            objetoPersistido.Cant_utilizado = Double.Parse(dr["CANT_UTI"].ToString());
            objetoPersistido.Product = dr["PRODUCTO"].ToString();
            objetoPersistido.Tarif = dr["COD_TARI"].ToString();
        }

        protected override void Completar(PrepurchaseInformation objetoPersistido, OracleDataReader dr)
        {
            objetoPersistido.CountryCode = Convert.ToInt32(dr["PAIS"]);
            objetoPersistido.AgencyCode = dr["CODIGO"].ToString();
            objetoPersistido.AgencyName = dr["RAZON_SOCIAL"].ToString();
            objetoPersistido.AgencySuc = dr["CODIGO_SUC"].ToString();
            objetoPersistido.Type = PrepurchaseInformation.ConvertToUnitType(dr["TIPO_COMPRA"].ToString());
            objetoPersistido.Cant_precompra = Double.Parse(dr["CANT_CPRA"].ToString());
            objetoPersistido.Cant_utilizado = Double.Parse(dr["CANT_UTI"].ToString());
            objetoPersistido.Product = dr["PRODUCTO"].ToString();
            objetoPersistido.Tarif = dr["COD_TARI"].ToString();

        }

        //protected override void CompletarComposicion(PrepurchaseInformation ObjetoPersistido)
        //{
        //    ObjetoPersistido.EMails = DAOLocator.Instance().GetDaoEMailListUsuarioDTO().FindForPrepurchace(ObjetoPersistido.CountryCode);
        //}

        public IList<PrepurchaseInformation> FindMinimunBalance()
        {
            var parameters = new Parametros();
            return Buscar(new Filtro(parameters, "Prepurchase_Balance"));
        }

        //oracle
        public IList<PrepurchaseInformation> FindMinimunBalance(int saldoMinDias, int saldoMinTarjetas)
        {
            string sql = @"SELECT AGV.PAIS, AGV.CODIGO, AGV.CODIGO_SUC, AGV.RAZON_SOCIAL, 
                    PRE.PRODUCTO, PRE.COD_TARI, PRE.CANT_CPRA, PRE.CANT_UTI, PRE.TIPO_COMPRA 
                    FROM WAN.O_PRECOMPRAS PRE, ICARD.AGENCIAS AGV 
                    WHERE PRE.ZONA = AGV.PAIS 
                    AND PRE.CLIENTE = LPAD(AGV.CODIGO, 5, '0') 
                    AND PRE.SUCURSAL = AGV.CODIGO_SUC 
                    AND AGV.FECHA_BAJA IS NULL 
                    AND (AGV.TIPO_AGENCIA = '2' OR AGV.TIPO_AGENCIA = '3') 
                    AND DBSCHEMAID =  LPAD(AGV.PAIS, 20, '0') 
                    AND CANT_CPRA > 0 AND CANT_UTI > 0 
                    AND ((TIPO_COMPRA = 'D' AND (CANT_CPRA - CANT_UTI) < {0}) 
                    OR (TIPO_COMPRA = 'T' AND (CANT_CPRA - CANT_UTI) < {1}))
                    ";
            
            return BuscarOracle(string.Format(sql, saldoMinDias, saldoMinTarjetas));
        }     
    }
}
