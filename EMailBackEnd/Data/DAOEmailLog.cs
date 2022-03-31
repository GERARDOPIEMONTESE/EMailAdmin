using System;
using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Data.Interfaces;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOEmailLog : DAOObjetoNegocio<EMailLog>, IDAOEMailLog
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override Parametros ParametrosCrear(EMailLog objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("CountryCode", objetoNegocio.CountryCode);
            parameters.AgregarParametro("ModuleCode", objetoNegocio.ModuleCode);
            parameters.AgregarParametro("VoucherCode", objetoNegocio.VoucherCode);
            parameters.AgregarParametro("InvokeInformation", objetoNegocio.InvokeInformation);
            parameters.AgregarParametro("ZipContextInformation", objetoNegocio.ContextInformation);
            parameters.AgregarParametro("StartDate", objetoNegocio.Fecha);
            parameters.AgregarParametro("ProcessStatus", objetoNegocio.ProcessStatus);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            if (objetoNegocio.EndDate != DateTime.MinValue &&
                objetoNegocio.ErrorMessage != null)
            {
                parameters.AgregarParametro("EndDate", objetoNegocio.EndDate);
                parameters.AgregarParametro("ErrorMessage", objetoNegocio.ErrorMessage);
            }

            parameters.AgregarParametro("PaxName", objetoNegocio.PaxName);
            parameters.AgregarParametro("PaxSurname", objetoNegocio.PaxSurname);
            parameters.AgregarParametro("IssuanceDate", objetoNegocio.IssuanceDate);
            if (objetoNegocio.IdLote.HasValue)
                parameters.AgregarParametro("IdLote", objetoNegocio.IdLote);
            return parameters;
        }

        protected override Parametros ParametrosModificar(EMailLog objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdEMailLog", objetoNegocio.Id);
            parameters.AgregarParametro("IdTemplate", objetoNegocio.IdTemplate);
            parameters.AgregarParametro("IdTemplateType", objetoNegocio.IdTemplateType);
            parameters.AgregarParametro("TemplateName", objetoNegocio.TemplateName);
            parameters.AgregarParametro("CountryCode", objetoNegocio.CountryCode);
            parameters.AgregarParametro("ModuleCode", objetoNegocio.ModuleCode);
            parameters.AgregarParametro("VoucherCode", objetoNegocio.VoucherCode);
            parameters.AgregarParametro("InvokeInformation", objetoNegocio.InvokeInformation);
            parameters.AgregarParametro("ZipContextInformation", objetoNegocio.ContextInformation);
            parameters.AgregarParametro("MailFrom", objetoNegocio.MailFrom);
            parameters.AgregarParametro("MailTo", objetoNegocio.MailTo);
            parameters.AgregarParametro("Subject", objetoNegocio.Subject);
            parameters.AgregarParametro("Body", objetoNegocio.Body);
            parameters.AgregarParametro("AttachmentIds", objetoNegocio.AttachmentIds);
            parameters.AgregarParametro("StartDate", objetoNegocio.Fecha);
            parameters.AgregarParametro("EndDate", objetoNegocio.EndDate);
            parameters.AgregarParametro("ErrorMessage", objetoNegocio.ErrorMessage);
            parameters.AgregarParametro("ProcessStatus", objetoNegocio.ProcessStatus);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);
            parameters.AgregarParametro("Receive", objetoNegocio.Receive);
            if (objetoNegocio.ReceiveDate != null && !DateTime.MinValue.Equals(objetoNegocio.ReceiveDate))
            {
                parameters.AgregarParametro("ReceiveDate", objetoNegocio.ReceiveDate);
            }
            parameters.AgregarParametro("PaxName", objetoNegocio.PaxName);
            parameters.AgregarParametro("PaxSurname", objetoNegocio.PaxSurname);
            parameters.AgregarParametro("IssuanceDate", objetoNegocio.IssuanceDate);
            return parameters;
        }

        protected override Parametros ParametrosEliminar(EMailLog ObjetoNegocio)
        {
            throw new NotImplementedException();
        }

        //protected override Parametros ParametrosGrabarLog(EMailLog objetoNegocio)
        //{
        //    var parameters = new Parametros();

        //    parameters.AgregarParametro("IdEMailLog", objetoNegocio.Id);
        //    parameters.AgregarParametro("IdTemplate", objetoNegocio.IdTemplate);
        //    parameters.AgregarParametro("IdTemplateType", objetoNegocio.IdTemplateType);
        //    parameters.AgregarParametro("TemplateName", objetoNegocio.TemplateName);
        //    parameters.AgregarParametro("CountryCode", objetoNegocio.CountryCode);
        //    parameters.AgregarParametro("ModuleCode", objetoNegocio.ModuleCode);
        //    parameters.AgregarParametro("VoucherCode", objetoNegocio.VoucherCode);
        //    parameters.AgregarParametro("InvokeInformation", objetoNegocio.InvokeInformation);
        //    parameters.AgregarParametro("ZipContextInformation", objetoNegocio.ContextInformation);
        //    parameters.AgregarParametro("StartDate", objetoNegocio.Fecha);
        //    parameters.AgregarParametro("ProcessStatus", objetoNegocio.ProcessStatus);
        //    parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);
        //    parameters.AgregarParametro("MailFrom", objetoNegocio.MailFrom);
        //    parameters.AgregarParametro("MailTo", objetoNegocio.MailTo);
        //    parameters.AgregarParametro("Subject", objetoNegocio.Subject);
        //    parameters.AgregarParametro("Body", objetoNegocio.Body);
        //    parameters.AgregarParametro("AttachmentIds", objetoNegocio.AttachmentIds);
        //    parameters.AgregarParametro("EndDate", objetoNegocio.EndDate == null 
        //        || objetoNegocio.EndDate.CompareTo(DateTime.MinValue) <= 0
        //        ? DateTime.MinValue.AddYears(1900) : objetoNegocio.EndDate);
        //    parameters.AgregarParametro("ErrorMessage", objetoNegocio.ErrorMessage);
        //    parameters.AgregarParametro("Receive", objetoNegocio.Receive);
        //    if (objetoNegocio.ReceiveDate != null && !DateTime.MinValue.Equals(objetoNegocio.ReceiveDate))
        //    {
        //        parameters.AgregarParametro("ReceiveDate", objetoNegocio.ReceiveDate);
        //    }
        //    parameters.AgregarParametro("PaxName", objetoNegocio.PaxName);
        //    parameters.AgregarParametro("PaxSurname", objetoNegocio.PaxSurname);
        //    parameters.AgregarParametro("IssuanceDate", objetoNegocio.IssuanceDate);

        //    return parameters;
        //}

        protected override void Completar(EMailLog objetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdEMailLog"]);
            objetoPersistido.IdTemplateType = DBNull.Value.Equals(dr["IdTemplateType"]) ? 0 : Convert.ToInt32(dr["IdTemplateType"]);
            objetoPersistido.TemplateName = DBNull.Value.Equals(dr["TemplateName"]) ? "" : dr["TemplateName"].ToString();
            objetoPersistido.CountryCode = Convert.ToInt32(dr["CountryCode"]);
            objetoPersistido.ModuleCode = dr["ModuleCode"].ToString();
            objetoPersistido.VoucherCode = dr["VoucherCode"].ToString();
            objetoPersistido.InvokeInformation = DBNull.Value.Equals(dr["InvokeInformation"]) ? "" : dr["InvokeInformation"].ToString();
            objetoPersistido.ContextInformation = DBNull.Value.Equals(dr["ZipContextInformation"]) ?
                new byte[] { } : (byte[])dr["ZipContextInformation"];
            objetoPersistido.MailFrom = DBNull.Value.Equals(dr["MailFrom"]) ? "" : dr["MailFrom"].ToString();
            objetoPersistido.MailTo = DBNull.Value.Equals(dr["MailTo"]) ? "" : dr["MailTo"].ToString();
            objetoPersistido.Subject = DBNull.Value.Equals(dr["Subject"]) ? "" : dr["Subject"].ToString();
            objetoPersistido.Body = DBNull.Value.Equals(dr["Body"]) ? "" : dr["Body"].ToString();
            objetoPersistido.AttachmentIds = DBNull.Value.Equals(dr["AttachmentIds"])
                ? "" : dr["AttachmentIds"].ToString();
            objetoPersistido.Fecha = Convert.ToDateTime(dr["StartDate"]);
            objetoPersistido.EndDate = DBNull.Value.Equals(dr["EndDate"])
                ? DateTime.MinValue : Convert.ToDateTime(dr["EndDate"]);
            objetoPersistido.ErrorMessage = DBNull.Value.Equals(dr["ErrorMessage"])
                ? "" : dr["ErrorMessage"].ToString();
            objetoPersistido.ProcessStatus = Convert.ToInt32(dr["ProcessStatus"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
            objetoPersistido.Receive = Convert.ToBoolean(dr["Receive"]);
            objetoPersistido.ReceiveDate = DBNull.Value.Equals(dr["ReceiveDate"])
                ? DateTime.MinValue : Convert.ToDateTime(dr["ReceiveDate"]);
            objetoPersistido.PaxName = dr["PaxName"].ToString();
            objetoPersistido.PaxSurname = dr["PaxSurname"].ToString();
            objetoPersistido.IssuanceDate = dr["IssuanceDate"].ToString();
            objetoPersistido.ContextInformationZipPending = "";
            //objetoPersistido.ContextInformationZipPending = DBNull.Value.Equals(dr["ContextInformation"]) ?
            //    "" : dr["ContextInformation"].ToString();
            if (dr["IdLote"] != DBNull.Value)
                objetoPersistido.IdLote = Convert.ToInt32(dr["IdLote"]);
        }

        public void CompletarLazy(EMailLog objetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            Completar(objetoPersistido, dr);
        }

        public IList<EMailLog> FindAllPendings()
        {
            return Buscar(new Filtro(new Parametros(), "dbo.EMailLog_Tx_Reprocess"));
        }

        public IList<EMailLog> FindAllPendings(string voucherCode, int idLocation, int idStatus, string fromDate, string toDate)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("voucherCode", voucherCode);
            parameters.AgregarParametro("idLocation", idLocation);
            parameters.AgregarParametro("idStatus", idStatus);
            parameters.AgregarParametro("fromDate", fromDate);
            parameters.AgregarParametro("toDate", toDate);

            return Buscar(new Filtro(parameters, "dbo.EMailLog_Tx_ReprocessFilters"));
        }

        public IList<EMailLog> Find(int countryCode, string voucherCode, int idTemplateType)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("CountryCode", countryCode);
            parameters.AgregarParametro("VoucherCode", voucherCode);
            parameters.AgregarParametro("IdTemplateType", idTemplateType);

            return Buscar(new Filtro(parameters, "dbo.EMailLog_Tx_Filters"));
        }

        public IList<EMailLog> Find(int id)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdEMailLog", id);

            return Buscar(new Filtro(parameters, "dbo.EMailLog_Tx_IdEMailLog"));
        }

        public override EMailLog  Obtener(int id)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdEMailLog", id);

            return Obtener(new Filtro(parameters, "dbo.EMailLog_Tx_IdEMailLog"));
        }

        public EMailLog GetLastByProcessStatus(int processStatus)
        {
            Parametros parameters = new Parametros();
            parameters.AgregarParametro("ProcessStatus", processStatus);

            return Obtener(new Filtro(parameters, "dbo.EMailLog_Tx_ProcessStatus_Last"));
        }

        public bool IsVoucherValid(string countryCode, string voucherCode)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("countryCode", countryCode);
            parameters.AgregarParametro("voucherCode", voucherCode);

            var existe = Cantidad(new Filtro(parameters, "dbo.Voucher_Tx_Exists", "Exist"));
            return existe != 0;
        }

        public IList<EMailLog> FindZipPending()
        {
            return Buscar(new Filtro(new Parametros(), "dbo.EMailLog_Tx_Zip"));
        }

        public IList<EMailLog> FindLog(int countryCode, string voucherCode, int idTemplateType)
        {
            Parametros parameters = new Parametros();
            parameters.AgregarParametro("countryCode", countryCode);
            parameters.AgregarParametro("voucherCode", voucherCode);
            parameters.AgregarParametro("IdTemplateType", idTemplateType);

            return Buscar(new Filtro(parameters, "dbo.EMailLog_Tx_Exist"));
        }
    }
}

