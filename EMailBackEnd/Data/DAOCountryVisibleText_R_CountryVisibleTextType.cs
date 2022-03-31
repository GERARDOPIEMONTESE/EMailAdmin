using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;
using FrameworkDAC.Parametro;
using System.Transactions;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOCountryVisibleText_R_CountryVisibleTextType : DAOObjetoNegocio<CountryVisibleText_R_CountryVisibleTextType>, IDAOCountryVisibleText_R_CountryVisibleTextType
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override void Completar(CountryVisibleText_R_CountryVisibleTextType objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdCountryVisibleText_R_CountryVisibleTextType"]);
            objetoPersistido.CountryVisibleTextType =
                DAOLocator.Instance().GetDaoCountryVisibleTextType().Get(Convert.ToInt32(dr["IdCountryVisibleTextType"]));
            objetoPersistido.CountryVisibleTextId = Convert.ToInt32(dr["IdCountryVisibleText"]);
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
        }

        protected override Parametros ParametrosCrear(CountryVisibleText_R_CountryVisibleTextType objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdCountryVisibleText", objetoNegocio.CountryVisibleTextId);
            parameters.AgregarParametro("IdCountryVisibleTextType", objetoNegocio.CountryVisibleTextType.Id);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }

        protected override Parametros ParametrosModificar(CountryVisibleText_R_CountryVisibleTextType objetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override Parametros ParametrosEliminar(CountryVisibleText_R_CountryVisibleTextType objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdCountryVisibleText_R_CountryVisibleTextType", objetoNegocio.Id);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            return parameters;
        }

        protected override Parametros ParametrosGrabarLog(CountryVisibleText_R_CountryVisibleTextType objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdCountryVisibleText_R_CountryVisibleTextType", objetoNegocio.Id);
            parameters.AgregarParametro("IdCountryVisibleText", objetoNegocio.CountryVisibleTextId);
            parameters.AgregarParametro("IdCountryVisibleTextType", objetoNegocio.CountryVisibleTextType.Id);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }

        public IList<CountryVisibleText_R_CountryVisibleTextType> FindByCountryVisibleTextId(int idCountryVisibleText)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdCountryVisibleText", idCountryVisibleText);

            return Buscar(new Filtro(parameters, "CountryVisibleText_R_CountryVisibleTextType_Tx_Filters"));
        }

        public void DeleteByIdCountryVisibleText(int idCountryVisibleText)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdCountryVisibleText", idCountryVisibleText);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            Ejecutar(new Filtro(parameters, "CountryVisibleText_R_CountryVisibleTextType_E_IdCountryVisibleText"));
        }

        public void DeleteByIdCountryVisibleText(int idCountryVisibleText, TransactionScope ts)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdCountryVisibleText", idCountryVisibleText);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            Ejecutar(new Filtro(parameters, "CountryVisibleText_R_CountryVisibleTextType_E_IdCountryVisibleText"), ts);
        }
    }
}
