using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOEMailAddress : DAOObjetoNegocio<EMailAddress>, IDAOEMailAddress
    {
        #region IDAOEMailAddress Members

        public EMailAddress Get(int id)
        {
            return Obtener(id);
        }

        public IList<EMailAddress> FindAll()
        {
            return Buscar(new Filtro(new Parametros(), "dbo.EMailAddress_Tx_Name"));
        }

        public IList<EMailAddress> FindByFilters(string name, string address)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("Name", name);
            parameters.AgregarParametro("Address", address);

            return Buscar(new Filtro(parameters, "dbo.EMailAddress_Tx_Name"));
        }

        #endregion

        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override Parametros ParametrosCrear(EMailAddress objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("Name", objetoNegocio.Name);
            parameters.AgregarParametro("Address", objetoNegocio.Address);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override Parametros ParametrosModificar(EMailAddress objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdEMailAddress", objetoNegocio.Id);
            parameters.AgregarParametro("Name", objetoNegocio.Name);
            parameters.AgregarParametro("Address", objetoNegocio.Address);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override Parametros ParametrosEliminar(EMailAddress objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdEMailAddress", objetoNegocio.Id);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override void Completar(EMailAddress objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdEMailAddress"]);
            objetoPersistido.Name = dr["Name"].ToString();
            objetoPersistido.Address = dr["Address"].ToString();
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
        }
    }
}