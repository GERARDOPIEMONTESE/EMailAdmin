using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Parametro;
using EMailAdmin.BackEnd.Data.Interfaces;
using CapaNegocioDatos.CapaDatos;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOConditionVariableTextContent : DAOObjetoNegocio<ConditionVariableTextContent>, IDAOConditionVariableTextContent
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }
        protected override Parametros ParametrosCrear(ConditionVariableTextContent objetoNegocio)
        {
            objetoNegocio.IdEstado = objetoNegocio.ObtenerCreado();
            var parameters = new Parametros();

            parameters.AgregarParametro("IdConditionVariableText", objetoNegocio.IdConditionVariableText);
            parameters.AgregarParametro("IdLanguage", objetoNegocio.Language.Id);
            parameters.AgregarParametro("ContentText", objetoNegocio.Content);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override Parametros ParametrosModificar(ConditionVariableTextContent objetoNegocio)
        {
            objetoNegocio.IdEstado = objetoNegocio.ObtenerCreado();

            var parameters = new Parametros();

            parameters.AgregarParametro("IdConditionVariableText", objetoNegocio.IdConditionVariableText);
            parameters.AgregarParametro("IdLanguage", objetoNegocio.Language.Id);
            parameters.AgregarParametro("ContentText", objetoNegocio.Content);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);

            return parameters;
        }

        protected override Parametros ParametrosEliminar(ConditionVariableTextContent objetoNegocio)
        {
            objetoNegocio.IdEstado = objetoNegocio.ObtenerEliminado();
            var parameters = new Parametros();

            parameters.AgregarParametro("IdConditionVariableText", objetoNegocio.Id);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override void Completar(ConditionVariableTextContent objetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdConditionVariableTextContent"]);
            objetoPersistido.IdConditionVariableText = Convert.ToInt32(dr["IdConditionVariableText"]);
            objetoPersistido.Language = DAOIdioma.Instancia().Obtener(Convert.ToInt32(dr["IdLanguage"]));
            objetoPersistido.Content = dr["ContentText"].ToString();
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);            
        }

        protected override Parametros ParametrosGrabarLog(ConditionVariableTextContent objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdConditionVariableTextContent", objetoNegocio.Id);
            parameters.AgregarParametro("IdConditionVariableText", objetoNegocio.IdConditionVariableText);
            parameters.AgregarParametro("IdLanguage", objetoNegocio.Language.Id);
            parameters.AgregarParametro("ContentText", objetoNegocio.Content);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", FrameworkDAC.Negocio.ObjetoNegocio.Creado());

            return parameters;
        }

        public IList<ConditionVariableTextContent> FindByIdConditionVariableText(int IdConditionVariableText)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdConditionVariableText", IdConditionVariableText);

            return Buscar(new Filtro(parameters, "dbo.ConditionVariableText_Tx_IdConditionVariableText"));
        }

        public ConditionVariableTextContent Get(int id)
        {
            return Obtener(id);
        }

        public IList<ConditionVariableTextContent> GetByIdConditionVariableText(int idConditionVariableText)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdConditionVariableText", idConditionVariableText);

            return Buscar(new Filtro(parameters, "dbo.ConditionVariableTextContent_Tx_IdConditionVariableText"));
        }

        public void DeleteByIdConditionVariableText(int idConditionVariableText)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdConditionVariableText", idConditionVariableText);
            parameters.AgregarParametro("IdStatus", FrameworkDAC.Negocio.ObjetoNegocio.Eliminado());

            Ejecutar(new Filtro(parameters, "dbo.ConditionVariableTextContent_E"));
        }

        public void DeleteByIdConditionVariableText(int idConditionVariableText, System.Transactions.TransactionScope ts)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdConditionVariableText", idConditionVariableText);
            parameters.AgregarParametro("IdStatus", FrameworkDAC.Negocio.ObjetoNegocio.Eliminado());

            Ejecutar(new Filtro(parameters, "dbo.ConditionVariableTextContent_E"), ts);
        }
    }
}