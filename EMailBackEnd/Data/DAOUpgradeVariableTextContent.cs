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
    public class DAOUpgradeVariableTextContent : DAOObjetoNegocio<UpgradeVariableTextContent>, IDAOUpgradeVariableTextContent
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override Parametros ParametrosCrear(UpgradeVariableTextContent objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdUpgradeVariableText", objetoNegocio.IdUpgradeVariableText);
            parameters.AgregarParametro("IdLanguage", objetoNegocio.Language.Id);
            parameters.AgregarParametro("ContentText", objetoNegocio.Content);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", FrameworkDAC.Negocio.ObjetoNegocio.Creado());

            return parameters;
        }

        protected override Parametros ParametrosModificar(UpgradeVariableTextContent objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdUpgradeVariableText", objetoNegocio.IdUpgradeVariableText);
            parameters.AgregarParametro("IdLanguage", objetoNegocio.Language.Id);
            parameters.AgregarParametro("ContentText", objetoNegocio.Content);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);

            return parameters;
        }

        protected override Parametros ParametrosEliminar(UpgradeVariableTextContent objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdUpgradeVariableTextContent", objetoNegocio.Id);
            parameters.AgregarParametro("IdStatus", FrameworkDAC.Negocio.ObjetoNegocio.Eliminado());

            return parameters;
        }

        protected override Parametros ParametrosGrabarLog(UpgradeVariableTextContent objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdUpgradeVariableTextContent", objetoNegocio.Id);
            parameters.AgregarParametro("IdUpgradeVariableText", objetoNegocio.IdUpgradeVariableText);
            parameters.AgregarParametro("IdLanguage", objetoNegocio.Language.Id);
            parameters.AgregarParametro("ContentText", objetoNegocio.Content);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", FrameworkDAC.Negocio.ObjetoNegocio.Creado());

            return parameters;
        }

        protected override void Completar(UpgradeVariableTextContent objetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdUpgradeVariableTextContent"]);
            objetoPersistido.IdUpgradeVariableText = Convert.ToInt32(dr["IdUpgradeVariableText"]);
            objetoPersistido.Language = IdiomaHome.Obtener(Convert.ToInt32(dr["IdLanguage"]));
            objetoPersistido.Content = dr["ContentText"].ToString();
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);            
        }

        #region IDAOUpgradeVariableTextContent Members

        public IList<UpgradeVariableTextContent> GetByIdUpgradeVariableText(int idUpgradeVariableText)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdUpgradeVariableText", idUpgradeVariableText);

            return Buscar(new Filtro(parameters, "dbo.UpgradeVariableTextContent_Tx_Filters"));
        }

        public UpgradeVariableTextContent Get(int id)
        {
            return Obtener(id);
        }

        public void DeleteByIdUpgradeVariableText(int idUpgradeVariableText)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdUpgradeVariableText", idUpgradeVariableText);
            parameters.AgregarParametro("IdStatus", FrameworkDAC.Negocio.ObjetoNegocio.Eliminado());

            Ejecutar(new Filtro(parameters, "dbo.UpgradeVariableTextContent_E_IdUpgradeVariableText"));
        }

        public void DeleteByIdUpgradeVariableText(int idUpgradeVariableText, TransactionScope ts)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdUpgradeVariableText", idUpgradeVariableText);
            parameters.AgregarParametro("IdStatus", FrameworkDAC.Negocio.ObjetoNegocio.Eliminado());

            Ejecutar(new Filtro(parameters, "dbo.UpgradeVariableTextContent_E_IdUpgradeVariableText"), ts);
        }

        #endregion
    }
}
