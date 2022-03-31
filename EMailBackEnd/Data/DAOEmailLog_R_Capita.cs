using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Parametro;
using EMailAdmin.BackEnd.Data.Interfaces;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOEmailLog_R_Capita : DAOObjetoNegocio<EmailLog_R_Capita>, IDAOEmailLog_R_Capita
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override FrameworkDAC.Parametro.Parametros ParametrosCrear(EmailLog_R_Capita objetoNegocio)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdEmailLog", objetoNegocio.EmailLog.Id);
            parameters.AgregarParametro("Nombre", objetoNegocio.Nombre);
            parameters.AgregarParametro("Apellido", objetoNegocio.Apellido);
            if (objetoNegocio.TipoDocumento != null) parameters.AgregarParametro("CodigoTipoDocumento", objetoNegocio.TipoDocumento.Codigo);
            if (objetoNegocio.Documento != "") parameters.AgregarParametro("Documento", objetoNegocio.Documento);
            parameters.AgregarParametro("CodigoPais", objetoNegocio.Pais.Codigo);
            parameters.AgregarParametro("CapitaCode", objetoNegocio.ProductCode);
            parameters.AgregarParametro("Capita", objetoNegocio.ProductName);
            parameters.AgregarParametro("PlanCode", objetoNegocio.RateCode);
            parameters.AgregarParametro("Plan", objetoNegocio.RateName);
            parameters.AgregarParametro("EnvioLinks", objetoNegocio.bEnvioLinks);
            return parameters;
        }

        protected override FrameworkDAC.Parametro.Parametros ParametrosModificar(EmailLog_R_Capita objetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override FrameworkDAC.Parametro.Parametros ParametrosEliminar(EmailLog_R_Capita ObjetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override void Completar(EmailLog_R_Capita ObjetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            ObjetoPersistido.Apellido = dr["Apellido"].ToString();
            ObjetoPersistido.Nombre = dr["Nombre"].ToString();
            ObjetoPersistido.Documento = dr["Documento"].ToString();
            ObjetoPersistido.ProductName = dr["Capita"].ToString();
            ObjetoPersistido.RateName= dr["Plan"].ToString();
            ObjetoPersistido.EmailLog = new EMailLog() { 
                Fecha = Convert.ToDateTime(dr["StartDate"].ToString()) ,
                MailTo = dr["MailTo"].ToString(),
                ProcessStatus = int.Parse(dr["ProcessStatus"].ToString())
            };
            ObjetoPersistido.TipoDocumento = new CapaNegocioDatos.CapaNegocio.TipoDocumento() { Codigo = dr["CodigoTipoDocumento"].ToString() };
            ObjetoPersistido.Pais = CapaNegocioDatos.CapaDatos.DAOPais.Instancia().ObtenerPorCodigo(dr["CodigoPais"].ToString());            
        }

        public EmailLog_R_Capita Find(int idEmailLog_R_Capita)
        {
            throw new NotImplementedException();
        }

        public IList<EmailLog_R_Capita> Find(string Nombre, string Apellido, string documento, string capita,
            string plan, int CountryCode, int envioLinks, Nullable<DateTime> fechaDesde, Nullable<DateTime> fechaHasta)
        {
            var parameters = new Parametros();
            if (Nombre!="") parameters.AgregarParametro("Nombre", Nombre);
            if (Apellido != "") parameters.AgregarParametro("Apellido", Apellido);
            if (documento != "") parameters.AgregarParametro("Documento", documento);
            if (CountryCode>0) parameters.AgregarParametro("CodigoPais", CountryCode);
            if (capita != "") parameters.AgregarParametro("Capita", capita);
            if (plan != "") parameters.AgregarParametro("Plan", plan);
            if (envioLinks!=-1) parameters.AgregarParametro("EnvioLinks", envioLinks);
            if (fechaDesde.HasValue) parameters.AgregarParametro("FechaDesde", fechaDesde.Value);
            if (fechaHasta.HasValue) parameters.AgregarParametro("FechaHasta", fechaHasta.Value);
            return Buscar(new Filtro() { Parametros = parameters, NombreStoredProcedure = "EmailLog_R_Capita_Tx_Filtros" });
        }
    }
}
