using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data.Interfaces;
using FrameworkDAC.Parametro;
using System.Data.SqlClient;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOEMailListExclude : DAOObjetoNegocio<EMailListExclude>, IDAOEMailListExclude
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override Parametros ParametrosCrear(EMailListExclude objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("AgencyCode", objetoNegocio.AgencyCode);
            parameters.AgregarParametro("BranchNumber", objetoNegocio.Branch);
            parameters.AgregarParametro("CountryCode", objetoNegocio.CountryCode);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.ObtenerCreado());

            return parameters;
        }

        protected override Parametros ParametrosModificar(EMailListExclude objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdEMailListExclude", objetoNegocio.Id);
            parameters.AgregarParametro("AgencyCode", objetoNegocio.AgencyCode);
            parameters.AgregarParametro("BranchNumber", objetoNegocio.Branch);
            parameters.AgregarParametro("CountryCode", objetoNegocio.CountryCode);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.ObtenerCreado());

            return parameters;
        }

        protected override Parametros ParametrosEliminar(EMailListExclude objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdEMailListExclude", objetoNegocio.Id);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.ObtenerEliminado());

            return parameters;
        }

        protected override Parametros ParametrosGrabarLog(EMailListExclude objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdEMailListExclude", objetoNegocio.Id);
            parameters.AgregarParametro("AgencyCode", objetoNegocio.AgencyCode);
            parameters.AgregarParametro("BranchNumber", objetoNegocio.Branch);
            parameters.AgregarParametro("CountryCode", objetoNegocio.CountryCode);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override void Completar(EMailListExclude objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdEMailListExclude"]);
            objetoPersistido.AgencyCode = dr["AgencyCode"].ToString();
            objetoPersistido.Branch = int.Parse(dr["BranchNumber"].ToString());
            objetoPersistido.CountryCode = int.Parse(dr["CountryCode"].ToString());
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);

            try
            {
                objetoPersistido.RazonSocial = dr["RazonSocial"].ToString();
                objetoPersistido.Denominacion = dr["Denominacion"].ToString();
                objetoPersistido.Pais = dr["Pais"].ToString();
            }
            catch { }
        }

        public IList<EMailListExclude> FindAll()
        {
            return Buscar(new Filtro(new Parametros(), "dbo.EMailListExclude_Tx_Filters"));
        }

        public EMailListExclude Get(int id)
        {
            return Obtener(id);
        }

        public EMailListExclude GetByAccount(string codeAccount, int Branch, int CountryCode)
        {
            Parametros parametros = new Parametros();

            parametros.AgregarParametro("AgencyCode", codeAccount);
            parametros.AgregarParametro("BranchNumber", Branch);
            parametros.AgregarParametro("CountryCode", CountryCode);

            return Obtener(new Filtro(parametros, "dbo.EMailListExclude_Tx_Filters"));
        }

        public EMailListExclude GetExcludeAccount(string codeAccount, int Branch, int CountryCode)
        {
            Parametros parametros = new Parametros();

            parametros.AgregarParametro("AgencyCode", codeAccount);
            parametros.AgregarParametro("BranchNumber", Branch);
            parametros.AgregarParametro("CountryCode", CountryCode);
            parametros.AgregarParametro("IdStatus", EMailListExclude.Creado());

            return Obtener(new Filtro(parametros, "dbo.EMailListExclude_Tx_Filters"));
        }


        public IList<EMailListExclude> GetByFilters(string AccountCode, int Branch, int countryCode)
        {
            Parametros parametros = new Parametros();

            if (AccountCode!="") parametros.AgregarParametro("AgencyCode", AccountCode);
            if (Branch>-1) parametros.AgregarParametro("BranchNumber", Branch);
            if (countryCode>-1) parametros.AgregarParametro("CountryCode", countryCode);

            parametros.AgregarParametro("IdStatus", EMailListExclude.Creado());

            return Buscar(new Filtro(parametros, "dbo.EMailListExclude_Tx_Filters"));
        }
    }
}
