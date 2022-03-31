using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data.Interfaces;
using FrameworkDAC.Parametro;
using EMailAdmin.BackEnd.Domain.External;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOEmailLogCheck : DAOObjetoPersistido<EMailLog>, IDAOEMailLogCheck
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override void Completar(EMailLog objetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdEMailLog"]);
            objetoPersistido.IdTemplateType = DBNull.Value.Equals(dr["IdTemplateType"]) ? 0 : Convert.ToInt32(dr["IdTemplateType"]);
            objetoPersistido.TemplateName = DBNull.Value.Equals(dr["TemplateName"]) ? "" : dr["TemplateName"].ToString();
            objetoPersistido.Fecha = Convert.ToDateTime(dr["StartDate"]);
            objetoPersistido.EndDate = DBNull.Value.Equals(dr["EndDate"]) ? DateTime.MinValue : Convert.ToDateTime(dr["EndDate"]);
            objetoPersistido.ErrorMessage = DBNull.Value.Equals(dr["ErrorMessage"]) ? "" : dr["ErrorMessage"].ToString();
            objetoPersistido.ProcessStatus = Convert.ToInt32(dr["ProcessStatus"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
            objetoPersistido.Receive = Convert.ToBoolean(dr["Receive"]);
            objetoPersistido.ReceiveDate = DBNull.Value.Equals(dr["ReceiveDate"]) ? DateTime.MinValue : Convert.ToDateTime(dr["ReceiveDate"]);            
        }

        public EMailLog CheckSendEmailHappyBirth(PaxCumpleanos pax, int IdTemplateType)
        {
            Parametros parameters = new Parametros();
            parameters.AgregarParametro("IdTemplateType", IdTemplateType);
            parameters.AgregarParametro("email", pax.EMAIL);
            parameters.AgregarParametro("PaxName", pax.NAME);
            parameters.AgregarParametro("PaxSurname", pax.SURNAME);
            parameters.AgregarParametro("EndDate", new DateTime(DateTime.Now.Year, 1, 1));

            return Obtener(new Filtro(parameters, "dbo.EMailLog_Tx_Exist"));
        }

        public EMailLog CheckSendEmailContinuaCompra(PaxContinuaCompra pax, int IdTemplateType)
        {
            throw new NotImplementedException();
        }
    }
}
