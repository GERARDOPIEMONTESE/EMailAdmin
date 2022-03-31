using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.ExternalServices.Domain;
using FrameworkDAC.Dato;
using EMailAdmin.ExternalServices.Data.Interfaces;
using System.Data.OracleClient;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.ExternalServices.Data
{
    public class DAOIssuanceInformation : DAOObjetoPersistido<IssuanceInformation>, IDAOIssuanceInformation
    {
        public bool bSqlActivado { get; set; }
        protected override string NombreConnectionString()
        {
            if (bSqlActivado)
                return "ACNetSql";
            else
                return "ACNet";
        }


        public DAOIssuanceInformation()
        {
            bSqlActivado = CapaNegocioDatos.CapaHome.CodigoActivadorHome.HabilitarAcnetSqlServer();
        }
        private static DAOIssuanceInformation _instance;

        public static DAOIssuanceInformation Instance()
        {
            return _instance ?? (_instance = new DAOIssuanceInformation());
        }


        protected override void Completar(IssuanceInformation objetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {

            objetoPersistido.CountryCode = Convert.ToInt32(dr["PAIS"]);
            objetoPersistido.VoucherCode = dr["CODIGO"].ToString();
            objetoPersistido.AgencyCode = dr["AGENCIA"].ToString();
            objetoPersistido.BranchNumber = Convert.ToInt32(dr["SUC_AGENCIA"]);
            objetoPersistido.Amount = dr["TARIFA_IMPRESA"].ToString();
            objetoPersistido.ProductCode = dr["PRODUCTO"].ToString();
            objetoPersistido.RateCode = dr["COD_TARIFA"].ToString();
            objetoPersistido.PaxType = dr["TIPO_PAX_VOUCHER"].ToString();
            objetoPersistido.EffectiveStartDate = Convert.ToDateTime(dr["FEC_VIG_INIC"].ToString()).ToShortDateString();
            objetoPersistido.EffectiveEndDate = Convert.ToDateTime(dr["FEC_VIF_FIN"].ToString()).ToShortDateString();
            objetoPersistido.Days = dr["CANT_DIAS"].ToString();
            objetoPersistido.Area = dr["AREA"].ToString();
            objetoPersistido.PaxName = dr["NOMBRE"].ToString();
            objetoPersistido.PaxSurname = dr["APELLIDO"].ToString();
            objetoPersistido.PaxPassport = dr["PASAPORTE"].ToString();
            objetoPersistido.PaxEMail = dr["EMAIL"].ToString();
            objetoPersistido.PaxAge = dr["EDAD"].ToString();
            objetoPersistido.PaxPhone = dr["TEL_PARTICULAR"].ToString();
            objetoPersistido.PaxAddress = dr["DOMICILIO"].ToString();
            objetoPersistido.EmergencyContact = dr["EMERG_CONTACTO"].ToString();
            objetoPersistido.EmergencyAddress = dr["EMERG_DOMICILIO"].ToString();
            objetoPersistido.EmergencyPhone = dr["EMERG_TEL1"].ToString();

        }

        protected override void Completar(IssuanceInformation objetoPersistido, OracleDataReader dr)
        {
            objetoPersistido.CountryCode = Convert.ToInt32(dr["PAIS"]);
            objetoPersistido.VoucherCode = dr["CODIGO"].ToString();
            objetoPersistido.AgencyCode = dr["AGENCIA"].ToString();
            objetoPersistido.BranchNumber = Convert.ToInt32(dr["SUC_AGENCIA"]);
            objetoPersistido.Amount = dr["TARIFA_IMPRESA"].ToString();
            objetoPersistido.ProductCode = dr["PRODUCTO"].ToString();
            objetoPersistido.RateCode = dr["COD_TARIFA"].ToString();
            objetoPersistido.PaxType = dr["TIPO_PAX_VOUCHER"].ToString();
            objetoPersistido.EffectiveStartDate = Convert.ToDateTime(dr["FEC_VIG_INIC"].ToString()).ToShortDateString();
            objetoPersistido.EffectiveEndDate = Convert.ToDateTime(dr["FEC_VIF_FIN"].ToString()).ToShortDateString();
            objetoPersistido.Days = dr["CANT_DIAS"].ToString();
            objetoPersistido.Area = dr["AREA"].ToString();
            objetoPersistido.PaxName = dr["NOMBRE"].ToString();
            objetoPersistido.PaxSurname = dr["APELLIDO"].ToString();
            objetoPersistido.PaxPassport = dr["PASAPORTE"].ToString();
            objetoPersistido.PaxEMail = dr["EMAIL"].ToString();
            objetoPersistido.PaxAge = dr["EDAD"].ToString();
            objetoPersistido.PaxPhone = dr["TEL_PARTICULAR"].ToString();
            objetoPersistido.PaxAddress = dr["DOMICILIO"].ToString();
            objetoPersistido.EmergencyContact = dr["EMERG_CONTACTO"].ToString();
            objetoPersistido.EmergencyAddress = dr["EMERG_DOMICILIO"].ToString();
            objetoPersistido.EmergencyPhone = dr["EMERG_TEL1"].ToString();
        }

        protected override void CompletarComposicion(IssuanceInformation ObjetoPersistido)
        {
            ObjetoPersistido.RateInformation = ExternalDAOLocator.Instance().GetDaoRateInformation().Get(
                ObjetoPersistido.CountryCode, ObjetoPersistido.ProductCode, ObjetoPersistido.RateCode, ObjetoPersistido.Days);
        }
        public IssuanceInformation Get(int countryCode, string voucherCode)
        {
            if (bSqlActivado)
                return GetQuerySql(countryCode, voucherCode);
            else
                return GetQueryOracle(countryCode, voucherCode);
        }
        public IList<IssuanceInformation> FindEffectiveEndDate(int countryCode)
        {
            if (bSqlActivado)
                return FindEffectiveEndDateSql(countryCode);
            else
                return FindEffectiveEndDateOracle(countryCode);
        }
        public IList<IssuanceInformation> FindEffectiveStartDate(int countryCode, int daysBefore)
        {
            if (bSqlActivado)
                return FindEffectiveStartDateSql(countryCode, daysBefore);
            else
                return FindEffectiveStartDateOracle(countryCode, daysBefore);
        }

        public IssuanceInformation GetQueryOracle(int countryCode, string voucherCode)
        {
            return ObtenerOracle(
                "SELECT VOUCHER.PAIS, VOUCHER.CODIGO, VOUCHER.AGENCIA, VOUCHER.SUC_AGENCIA, VOUCHER.TARIFA_IMPRESA, VOUCHER.PRODUCTO, " +
                "VOUCHER.COD_TARIFA, VOUCHER.TIPO_PAX_VOUCHER, VOUCHER.FEC_VIG_INIC, VOUCHER.FEC_VIF_FIN, VOUCHER.CANT_DIAS, " +
                "VOUCHER.AREA, CLIENTES.NOMBRE, CLIENTES.APELLIDO, CLIENTES.PASAPORTE, CLIENTES.FEC_NACIMIENTO, " +
                "CLIENTES.TEL_PARTICULAR, CLIENTES.DOM_CALLE DOMICILIO, CLIENTES.EMAIL, " +
                "CLIENTES.EMERG_CONTACTO, EMERG_CALLE EMERG_DOMICILIO, EMERG_TEL1, " +
                "((TO_NUMBER(TO_CHAR(SYSDATE,'YYYY')) - TO_NUMBER(TO_CHAR(FEC_NACIMIENTO,'YYYY'))) - CASE WHEN TO_CHAR(SYSDATE,'MMDD') < TO_CHAR(FEC_NACIMIENTO,'MMDD') THEN 1 ELSE 0 END) EDAD " +
                "FROM ICARD.VOUCHER, ICARD.CLIENTES " +
                "WHERE VOUCHER.PAIS = CLIENTES.PAIS AND VOUCHER.CLIENTE = CLIENTES.CODIGO " +
                "AND VOUCHER.PAIS = " + countryCode.ToString() + " " +
                "AND VOUCHER.CODIGO = " + voucherCode, false);
        }
        public IssuanceInformation GetQuerySql(int countryCode, string voucherCode)
        {
            return Obtener(
                "SELECT VOUCHER.PAIS, VOUCHER.CODIGO, VOUCHER.AGENCIA, VOUCHER.SUC_AGENCIA, VOUCHER.TARIFA_IMPRESA, VOUCHER.PRODUCTO, " +
                "VOUCHER.COD_TARIFA, VOUCHER.TIPO_PAX_VOUCHER, VOUCHER.FEC_VIG_INIC, VOUCHER.FEC_VIF_FIN, VOUCHER.CANT_DIAS, " +
                "VOUCHER.AREA, CLIENTES.NOMBRE, CLIENTES.APELLIDO, CLIENTES.PASAPORTE, CLIENTES.FEC_NACIMIENTO, " +
                "CLIENTES.TEL_PARTICULAR, CLIENTES.DOM_CALLE DOMICILIO, CLIENTES.EMAIL, " +
                "CLIENTES.EMERG_CONTACTO, EMERG_CALLE EMERG_DOMICILIO, EMERG_TEL1, " +
                "(cast(datediff(dd,FEC_NACIMIENTO,DATEADD(dd, 0, DATEDIFF(dd, 0, getdate())) ) / 365.25 as int))  EDAD " +
                "FROM ICARD.VOUCHER, ICARD.CLIENTES " +
                "WHERE VOUCHER.PAIS = CLIENTES.PAIS AND VOUCHER.CLIENTE = CLIENTES.CODIGO " +
                "AND VOUCHER.PAIS = " + countryCode.ToString() + " " +
                "AND VOUCHER.CODIGO = " + voucherCode, false);
        }
        public IList<IssuanceInformation> FindEffectiveEndDateOracle(int countryCode)
        {
            return BuscarOracle("SELECT * FROM VOUCHER " +
                "WHERE PAIS = " + countryCode.ToString() + " AND PRODUCTO NOT IN ('BU') AND FEC_BAJA IS NULL " +
                "AND FEC_VIF_FIN BETWEEN TO_DATE(SYSDATE, 'dd/MM/yyyy') AND TO_DATE(SYSDATE +1, 'dd/MM/yyyy')");
        }
        public IList<IssuanceInformation> FindEffectiveEndDateSql(int countryCode)
        {
            return Buscar("SELECT * FROM VOUCHER " +
                "WHERE PAIS = " + countryCode.ToString() + " AND PRODUCTO NOT IN ('BU') AND FEC_BAJA IS NULL " +
                "AND FEC_VIF_FIN BETWEEN DATEADD(dd, 0, DATEDIFF(dd, 0, getdate())) and DATEADD(dd, 1, DATEDIFF(dd, 0, getdate()))");
        }
        public IList<IssuanceInformation> FindEffectiveStartDateOracle(int countryCode, int daysBefore)
        {
            /*
                   return BuscarOracle("SELECT * FROM VOUCHER " +
                "WHERE PRODUCTO NOT IN ('BU') AND FEC_BAJA IS NULL " +
                "AND FEC_VIG_INIC BETWEEN TO_DATE(SYSDATE - " + daysBefore + ", 'dd/MM/yyyy') " +
                "AND TO_DATE(SYSDATE - " + (daysBefore - 1) + ", 'dd/MM/yyyy')'");
             */
            string filtroAGV = "";
            string agvs = CapaNegocioDatos.CapaHome.CodigoActivadorHome.ObtenerTripAGV_filtroIN();
            if (agvs!="")
                filtroAGV = " AND AGENCIA IN (" + agvs + ")";

            return BuscarOracle("SELECT VOUCHER.PAIS, VOUCHER.CODIGO, VOUCHER.AGENCIA, VOUCHER.SUC_AGENCIA, VOUCHER.TARIFA_IMPRESA, VOUCHER.PRODUCTO, " +
                "VOUCHER.COD_TARIFA, VOUCHER.TIPO_PAX_VOUCHER, VOUCHER.FEC_VIG_INIC, VOUCHER.FEC_VIF_FIN, VOUCHER.CANT_DIAS, " +
                "VOUCHER.AREA, CLIENTES.NOMBRE, CLIENTES.APELLIDO, CLIENTES.PASAPORTE, CLIENTES.FEC_NACIMIENTO, " +
                "CLIENTES.TEL_PARTICULAR, CLIENTES.DOM_CALLE DOMICILIO, CLIENTES.EMAIL, " +
                "CLIENTES.EMERG_CONTACTO, EMERG_CALLE EMERG_DOMICILIO, EMERG_TEL1, " +
                "((TO_NUMBER(TO_CHAR(SYSDATE,'YYYY')) - TO_NUMBER(TO_CHAR(FEC_NACIMIENTO,'YYYY'))) - CASE WHEN TO_CHAR(SYSDATE,'MMDD') < TO_CHAR(FEC_NACIMIENTO,'MMDD') THEN 1 ELSE 0 END) EDAD " +
                " FROM ICARD.VOUCHER, ICARD.CLIENTES " +
                " WHERE VOUCHER.PAIS = CLIENTES.PAIS AND VOUCHER.CLIENTE = CLIENTES.CODIGO " +
                " AND PRODUCTO NOT IN ('BU') AND FEC_BAJA IS NULL " + filtroAGV +
                " AND TRUNC(FEC_VIG_INIC) = TRUNC(SYSDATE+" + daysBefore+ ")");
        }

        public IList<IssuanceInformation> FindEffectiveStartDateSql(int countryCode, int daysBefore)
        {
     
            string filtroAGV = "";
            string agvs = CapaNegocioDatos.CapaHome.CodigoActivadorHome.ObtenerTripAGV_filtroIN();
            if (agvs != "")
                filtroAGV = " AND AGENCIA IN (" + agvs + ")";

            return Buscar("SELECT VOUCHER.PAIS, VOUCHER.CODIGO, VOUCHER.AGENCIA, VOUCHER.SUC_AGENCIA, VOUCHER.TARIFA_IMPRESA, VOUCHER.PRODUCTO, " +
                "VOUCHER.COD_TARIFA, VOUCHER.TIPO_PAX_VOUCHER, VOUCHER.FEC_VIG_INIC, VOUCHER.FEC_VIF_FIN, VOUCHER.CANT_DIAS, " +
                "VOUCHER.AREA, CLIENTES.NOMBRE, CLIENTES.APELLIDO, CLIENTES.PASAPORTE, CLIENTES.FEC_NACIMIENTO, " +
                "CLIENTES.TEL_PARTICULAR, CLIENTES.DOM_CALLE DOMICILIO, CLIENTES.EMAIL, " +
                "CLIENTES.EMERG_CONTACTO, EMERG_CALLE EMERG_DOMICILIO, EMERG_TEL1, " +
                "(cast(datediff(dd,FEC_NACIMIENTO,DATEADD(dd, 0, DATEDIFF(dd, 0, getdate())) ) / 365.25 as int))  EDAD " +
                " FROM ICARD.VOUCHER, ICARD.CLIENTES " +
                " WHERE VOUCHER.PAIS = CLIENTES.PAIS AND VOUCHER.CLIENTE = CLIENTES.CODIGO " +
                " AND PRODUCTO NOT IN ('BU') AND FEC_BAJA IS NULL " + filtroAGV +
                " AND DATEADD(dd, 0, DATEDIFF(dd, 0, FEC_VIG_INIC)) = DATEADD(dd, " + daysBefore + ", DATEDIFF(dd, 0, FEC_VIG_INIC))");
        }
        public IList<IssuanceInformation> Find()
        {
            throw new NotImplementedException();
        }


        public void LimpiarBaseEnvio(int pais, int codigo, string agencia, int sucursal)
        {
            throw new NotImplementedException();
        }

    
    }
}
