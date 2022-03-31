using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.ExternalServices.Domain;
using FrameworkDAC.Dato;
using EMailAdmin.ExternalServices.Data.Interfaces;
using System.Data.OracleClient;

namespace EMailAdmin.ExternalServices.Data
{
    public class DAOIssuanceInformation : DAOObjetoPersistido<IssuanceInformation>, IDAOIssuanceInformation
    {
        protected override string NombreConnectionString()
        {
            return "ACNet";
        }

        protected override void Completar(IssuanceInformation ObjetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {            
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
    }
}
