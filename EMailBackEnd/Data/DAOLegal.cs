using System;
using System.Collections.Generic;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOLegal : DAOObjetoPersistido<Legal>, IDAOLegal
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override void Completar(Legal objetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdEMailLog"]);
            objetoPersistido.CountryCode = Convert.ToInt32(dr["CountryCode"]);
            objetoPersistido.VoucherCode = dr["VoucherCode"].ToString();
            objetoPersistido.Email = DBNull.Value.Equals(dr["Email"]) ? "" : dr["Email"].ToString();
            objetoPersistido.PaxName = DBNull.Value.Equals(dr["PaxName"]) ? "" : dr["PaxName"].ToString();
            objetoPersistido.TemplateName = DBNull.Value.Equals(dr["TemplateName"]) ? "" : dr["TemplateName"].ToString();
            objetoPersistido.EmissionDate = DBNull.Value.Equals(dr["EmissionDate"]) || dr["EmissionDate"].ToString().Length == 0 ?
                "" : dr["EmissionDate"].ToString();
            objetoPersistido.SentDate = DBNull.Value.Equals(dr["SentDate"]) ? DateTime.MinValue : Convert.ToDateTime(dr["SentDate"]);
            objetoPersistido.ErrorDate = DBNull.Value.Equals(dr["errorDate"]) ? DateTime.MinValue : Convert.ToDateTime(dr["errorDate"]);
            objetoPersistido.ErrorMessage = DBNull.Value.Equals(dr["ErrorMessage"]) ? "" : dr["ErrorMessage"].ToString();
            objetoPersistido.ProcessStatus = Convert.ToInt32(dr["process"]);
            if (dr["IdLote"]!=DBNull.Value) 
                objetoPersistido.IdLote = Convert.ToInt32( dr["IdLote"]);
        }

        public IList<Legal> Find(int countryCode, string voucherCode, string email, string templateName)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("CountryCode", countryCode);
            parameters.AgregarParametro("VoucherCode", voucherCode);
            parameters.AgregarParametro("Email", email);
            parameters.AgregarParametro("TemplateName", templateName);

            return Buscar(new Filtro(parameters, "dbo.EMailLogLegal_Tx_Filters"));
        }
    }
}
