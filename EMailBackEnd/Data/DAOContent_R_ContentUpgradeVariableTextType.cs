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
    public class DAOContent_R_ContentUpgradeVariableTextType : DAOObjetoNegocio<Content_R_ContentUpgradeVariableTextType>,
                                                     IDAOContent_R_ContentUpgradeVariableTextType
    {
        #region IDAOContent_R_ContentUpgradeVariableTextType Members

        public IList<Content_R_ContentUpgradeVariableTextType> Find(int idContent)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdContent", idContent);

            return Buscar(new Filtro(parameters, "dbo.Content_R_ContentUpgradeVariableTextType_Tx_IdContent"));
        }

        public void DeleteByIdContent(int idContent, int idUser)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdContent", idContent);
            parameters.AgregarParametro("IdUser", idUser);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            Ejecutar(new Filtro(parameters, "Content_R_ContentUpgradeVariableTextType_E_IdContent"));
        }

        public void DeleteByIdContent(int idContent, int idUser, TransactionScope ts)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdContent", idContent);
            parameters.AgregarParametro("IdUser", idUser);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            Ejecutar(new Filtro(parameters, "Content_R_ContentUpgradeVariableTextType_E_IdContent"), ts);
        }

        #endregion

        protected override void Completar(Content_R_ContentUpgradeVariableTextType objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdContent_R_UpgradeVariableTextType"]);
            objetoPersistido.IdContent = Convert.ToInt32(dr["IdContent"]);
            objetoPersistido.UpgradeVariableTextType =
                DAOLocator.Instance().GetDaoUpgradeVariableTextType().Get(Convert.ToInt32(dr["IdUpgradeVariableTextType"]));
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
        }

        protected override Parametros ParametrosCrear(Content_R_ContentUpgradeVariableTextType objetoNegocio)
        {
            objetoNegocio.IdEstado = ObjetoNegocio.Creado();

            var parameters = new Parametros();

            parameters.AgregarParametro("IdContent", objetoNegocio.IdContent);
            parameters.AgregarParametro("IdUpgradeVariableTextType", objetoNegocio.UpgradeVariableTextType.Id);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override Parametros ParametrosModificar(Content_R_ContentUpgradeVariableTextType objetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override Parametros ParametrosEliminar(Content_R_ContentUpgradeVariableTextType objetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override Parametros ParametrosGrabarLog(Content_R_ContentUpgradeVariableTextType objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdContent_R_ContentUpgradeVariableTextType", objetoNegocio.Id);
            parameters.AgregarParametro("IdContent", objetoNegocio.IdContent);
            parameters.AgregarParametro("IdUpgradeVariableTextType", objetoNegocio.UpgradeVariableTextType.Id);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }
    }
}