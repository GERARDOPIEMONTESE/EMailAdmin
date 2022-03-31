using System;
using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Data.Interfaces;
using FrameworkDAC.Parametro;
using CapaNegocioDatos.CapaHome;
using System.Transactions;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOCountryVisibleTextContent : DAOObjetoNegocio<CountryVisibleTextContent>, IDAOCountryVisibleTextContent
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override Parametros ParametrosCrear(CountryVisibleTextContent objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdCountryVisibleText", objetoNegocio.IdCountryVisibleText);
            parameters.AgregarParametro("IdLanguage", objetoNegocio.Language.Id);
            parameters.AgregarParametro("ContentText", objetoNegocio.Content);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", FrameworkDAC.Negocio.ObjetoNegocio.Creado());

            return parameters;
        }

        protected override Parametros ParametrosModificar(CountryVisibleTextContent objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdCountryVisibleText", objetoNegocio.IdCountryVisibleText);
            parameters.AgregarParametro("IdLanguage", objetoNegocio.Language.Id);
            parameters.AgregarParametro("ContentText", objetoNegocio.Content);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);

            return parameters;
        }

        protected override Parametros ParametrosEliminar(CountryVisibleTextContent objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdCountryVisibleTextContent", objetoNegocio.Id);
            parameters.AgregarParametro("IdStatus", FrameworkDAC.Negocio.ObjetoNegocio.Eliminado());

            return parameters;
        }

        protected override Parametros ParametrosGrabarLog(CountryVisibleTextContent objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdCountryVisibleTextContent", objetoNegocio.Id);
            parameters.AgregarParametro("IdCountryVisibleText", objetoNegocio.IdCountryVisibleText);
            parameters.AgregarParametro("IdLanguage", objetoNegocio.Language.Id);
            parameters.AgregarParametro("ContentText", objetoNegocio.Content);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", FrameworkDAC.Negocio.ObjetoNegocio.Creado());

            return parameters;
        }

        protected override void Completar(CountryVisibleTextContent objetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdCountryVisibleTextContent"]);
            objetoPersistido.IdCountryVisibleText = Convert.ToInt32(dr["IdCountryVisibleText"]);
            objetoPersistido.Language = IdiomaHome.Obtener(Convert.ToInt32(dr["IdLanguage"]));
            objetoPersistido.Content = dr["ContentText"].ToString();
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);            
        }

        #region IDAOCountryVisibleTextContent Members

        public IList<CountryVisibleTextContent> GetByIdCountryVisibleText(int idCountryVisibleText)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdCountryVisibleText", idCountryVisibleText);

            return Buscar(new Filtro(parameters, "dbo.CountryVisibleTextContent_Tx_Filters"));
        }

        public CountryVisibleTextContent Get(int id)
        {
            return Obtener(id);
        }

        public void DeleteByIdCountryVisibleText(int idCountryVisibleText)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdCountryVisibleText", idCountryVisibleText);
            parameters.AgregarParametro("IdStatus", FrameworkDAC.Negocio.ObjetoNegocio.Eliminado());

            Ejecutar(new Filtro(parameters, "dbo.CountryVisibleTextContent_E_IdCountryVisibleText"));
        }

        public void DeleteByIdCountryVisibleText(int idCountryVisibleText, TransactionScope ts)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdCountryVisibleText", idCountryVisibleText);
            parameters.AgregarParametro("IdStatus", FrameworkDAC.Negocio.ObjetoNegocio.Eliminado());

            Ejecutar(new Filtro(parameters, "dbo.CountryVisibleTextContent_E_IdCountryVisibleText"), ts);
        }

        #endregion
    }
}
