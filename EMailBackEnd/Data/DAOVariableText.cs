using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOVariableText : DAOObjetoNegocio<VariableText>, IDAOVariableText
    {
        #region IDAOVariableText Members

        public VariableText Get(int id)
        {
            return Obtener(id);
        }

        public VariableText Get(string name)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("Name", name);

            return Obtener(new Filtro(parameters, "dbo.VariableText_Tx_Name"));
        }

        public IList<VariableText> FindAll()
        {
            return Buscar(new Filtro(new Parametros(), "dbo.VariableText_Tx_Name"));
        }

        public IList<VariableText> FindByType(int idType)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("idType", idType);

            return Buscar(new Filtro(parameters, "dbo.VariableText_Tx_IdVariableTextType"));
        }

        public IList<VariableText> FindByType(string code)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("Code", code);

            return Buscar(new Filtro(parameters, "dbo.VariableText_Tx_CodeVariableTextType"));
        }

        #endregion

        protected override Parametros ParametrosCrear(VariableText objetoNegocio)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("Name", objetoNegocio.Name);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override Parametros ParametrosModificar(VariableText objetoNegocio)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdVariableText", objetoNegocio.Id);
            parameters.AgregarParametro("Name", objetoNegocio.Name);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override Parametros ParametrosEliminar(VariableText objetoNegocio)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdVariableText", objetoNegocio.Id);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override void Completar(VariableText objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdVariableText"]);
            objetoPersistido.Name = dr["Name"].ToString();
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
        }

        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }
    }
}