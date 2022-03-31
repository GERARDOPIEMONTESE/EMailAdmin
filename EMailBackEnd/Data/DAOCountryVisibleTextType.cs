using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOCountryVisibleTextType : DAOObjetoNegocio<CountryVisibleTextType>, IDAOCountryVisibleTextType
    {
        #region IDAOCountryVisibleTextType Members

        public CountryVisibleTextType Get(int id)
        {
            return Obtener(id);
        }

        public CountryVisibleTextType GetByCode(string code)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("Code", code);

            return Obtener(new Filtro(parameters, "dbo.CountryVisibleTextType_Tx_Filters"));
        }

        public IList<CountryVisibleTextType> FindAll()
        {
            return Buscar(new Filtro(new Parametros(), "dbo.CountryVisibleTextType_Tx_Filters"));
        }

        public IList<CountryVisibleTextType> FindByFilters(string description)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("Description", description);

            return Buscar(new Filtro(parameters, "dbo.CountryVisibleTextType_Tx_Filters"));
        }

        #endregion

        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override void Completar(CountryVisibleTextType objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdCountryVisibleTextType"]);
            objetoPersistido.Code = dr["Code"].ToString();
            objetoPersistido.Description = dr["Description"].ToString();
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
        }

        protected override Parametros ParametrosCrear(CountryVisibleTextType objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("Code", objetoNegocio.Code);
            parameters.AgregarParametro("Description", objetoNegocio.Description);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }

        protected override Parametros ParametrosModificar(CountryVisibleTextType objetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override Parametros ParametrosEliminar(CountryVisibleTextType objetoNegocio)
        {
            throw new NotImplementedException();
        }
    }
}