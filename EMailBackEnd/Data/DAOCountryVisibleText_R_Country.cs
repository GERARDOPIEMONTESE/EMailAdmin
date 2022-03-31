using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CapaNegocioDatos.CapaDatos;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;
using FrameworkDAC.Parametro;
using System.Transactions;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOCountryVisibleText_R_Country : DAOObjetoNegocio<CountryVisibleText_R_Country>, IDAOCountryVisibleText_R_Country
    {
        #region IDAOCountryVisibleText_R_Country Members

        public IList<CountryVisibleText_R_Country> FindByCountryVisibleTextId(int idCountryVisibleText)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdCountryVisibleText", idCountryVisibleText);

            return Buscar(new Filtro(parameters, "CountryVisibleText_R_Country_Tx_Filters"));
        }

        public void DeleteByIdCountryVisibleText(int idCountryVisibleText)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdCountryVisibleText", idCountryVisibleText);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            Ejecutar(new Filtro(parameters, "CountryVisibleText_R_Country_E_IdCountryVisibleText"));
        }

        public void DeleteByIdCountryVisibleText(int idCountryVisibleText, TransactionScope ts)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdCountryVisibleText", idCountryVisibleText);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            Ejecutar(new Filtro(parameters, "CountryVisibleText_R_Country_E_IdCountryVisibleText"), ts);
        }

        #endregion

        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override void Completar(CountryVisibleText_R_Country objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdCountryVisibleText_R_Country"]);
            objetoPersistido.Country = DAOLocacion.Instancia().Obtener(Convert.ToInt32(dr["IdCountry"].ToString()));
            objetoPersistido.CountryVisibleTextId = Convert.ToInt32(dr["IdCountryVisibleText"]);
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
        }

        protected override Parametros ParametrosCrear(CountryVisibleText_R_Country objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdCountryVisibleText", objetoNegocio.CountryVisibleTextId);
            parameters.AgregarParametro("IdCountry", objetoNegocio.Country.Id);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }

        protected override Parametros ParametrosModificar(CountryVisibleText_R_Country objetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override Parametros ParametrosEliminar(CountryVisibleText_R_Country objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdCountryVisibleText_R_Country", objetoNegocio.Id);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            return parameters;
        }

        protected override Parametros ParametrosGrabarLog(CountryVisibleText_R_Country objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdCountryVisibleText_R_Country", objetoNegocio.Id);
            parameters.AgregarParametro("IdCountryVisibleText", objetoNegocio.CountryVisibleTextId);
            parameters.AgregarParametro("IdCountry", objetoNegocio.Country.Id);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }
    }
}