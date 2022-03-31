using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOConfigurationValue : DAOObjetoNegocio<ConfigurationValue>
    {
        private const string CONNECTIONSTRING = "EMailAdmin";

        #region Singleton

        private static DAOConfigurationValue _instance;

        private DAOConfigurationValue()
        {
        }

        public static DAOConfigurationValue Instance()
        {
            return _instance ?? (_instance = new DAOConfigurationValue());
        }

        #endregion

        protected override string NombreConnectionString()
        {
            return CONNECTIONSTRING;
        }

        protected override Parametros ParametrosCrear(ConfigurationValue ObjetoNegocio)
        {
            var parametros = new Parametros();

            parametros.AgregarParametro("Code", ObjetoNegocio.Code);
            parametros.AgregarParametro("Description", ObjetoNegocio.Description);
            parametros.AgregarParametro("Value", ObjetoNegocio.Value);
            parametros.AgregarParametro("IdUser", ObjetoNegocio.IdUsuario);
            parametros.AgregarParametro("IdStatus", ObjetoNegocio.ObtenerCreado());

            return parametros;
        }

        protected override Parametros ParametrosModificar(ConfigurationValue ObjetoNegocio)
        {
            var parametros = new Parametros();
            parametros.AgregarParametro("IdConfigurationValue", ObjetoNegocio.Id);
            parametros.AgregarParametro("Code", ObjetoNegocio.Code);
            parametros.AgregarParametro("Description", ObjetoNegocio.Description);
            parametros.AgregarParametro("Value", ObjetoNegocio.Value);
            parametros.AgregarParametro("IdUser", ObjetoNegocio.IdUsuario);
            parametros.AgregarParametro("ModificationDate", DateTime.Now);
            parametros.AgregarParametro("IdStatus", ObjetoNegocio.ObtenerModificado());

            return parametros;
        }

        protected override Parametros ParametrosEliminar(ConfigurationValue ObjetoNegocio)
        {
            var parametros = new Parametros();
            parametros.AgregarParametro("IdConfigurationValue", ObjetoNegocio.Id);
            parametros.AgregarParametro("ModificationDate", DateTime.Now);
            parametros.AgregarParametro("IdStatus", ObjetoNegocio.ObtenerEliminado());
            parametros.AgregarParametro("IdUser", ObjetoNegocio.IdUsuario);

            return parametros;
        }

        protected override void Completar(ConfigurationValue ObjetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            ObjetoPersistido.Id = Convert.ToInt32(dr["IdConfigurationValue"]);
            ObjetoPersistido.Code = dr["Code"].ToString();
            ObjetoPersistido.Description = dr["Description"].ToString();
            ObjetoPersistido.Value = dr["Value"].ToString();
        }

        public ConfigurationValue GetById(int IdConfigurationValue)
        {
            var p = new Parametros();
            p.AgregarParametro("IdConfigurationValue", IdConfigurationValue);
            var filter = new Filtro(p, "dbo.ConfigurationValue_Tx_IdConfigurationValue");

            return Obtener(filter);
        }

        public ConfigurationValue GetByCode(string code)
        {
            var p = new Parametros();
            p.AgregarParametro("Code", code);
            var filter = new Filtro(p, "dbo.ConfigurationValue_Tx_Code");

            return Obtener(filter);
        }

        public IList<ConfigurationValue> FindAll()
        {
            var p = new Parametros();
            var filter = new Filtro(p, "dbo.ConfigurationValue_TT");

            return Buscar(filter);
        }
    }
}
