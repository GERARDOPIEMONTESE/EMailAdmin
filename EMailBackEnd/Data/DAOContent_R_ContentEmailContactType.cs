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
    public class DAOContent_R_ContentEmailContactType : DAOObjetoNegocio<Content_R_ContentEmailContactType>,
                                                        IDAOContent_R_ContentEmailContactType
    {
        #region IDAOContent_R_ContentEmailContactType Members

        public IList<Content_R_ContentEmailContactType> Find(int idContent)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdContent", idContent);

            return Buscar(new Filtro(parameters, "dbo.Content_R_ContentEmailContactType_Tx_IdContent"));
        }

        public void DeleteByIdContent(int idContent, int idUser)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdContent", idContent);
            parameters.AgregarParametro("IdUser", idUser);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            Ejecutar(new Filtro(parameters, "Content_R_ContentEmailContactType_E_IdContent"));
        }

        public void DeleteByIdContent(int idContent, int idUser, TransactionScope ts)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdContent", idContent);
            parameters.AgregarParametro("IdUser", idUser);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            Ejecutar(new Filtro(parameters, "Content_R_ContentEmailContactType_E_IdContent"), ts);
        }

        #endregion

        protected override void Completar(Content_R_ContentEmailContactType objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdContent_R_EmailContactType"]);
            objetoPersistido.IdContent = Convert.ToInt32(dr["IdContent"]);
            objetoPersistido.EMailContactType =
                DAOLocator.Instance().GetDaoEMailContactType().Get(Convert.ToInt32(dr["IdEmailContactType"]));
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
        }

        protected override Parametros ParametrosCrear(Content_R_ContentEmailContactType objetoNegocio)
        {
            objetoNegocio.IdEstado = ObjetoNegocio.Creado();

            var parameters = new Parametros();

            parameters.AgregarParametro("IdContent", objetoNegocio.IdContent);
            parameters.AgregarParametro("IdEmailContactType", objetoNegocio.EMailContactType.Id);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override Parametros ParametrosModificar(Content_R_ContentEmailContactType objetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override Parametros ParametrosEliminar(Content_R_ContentEmailContactType objetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override Parametros ParametrosGrabarLog(Content_R_ContentEmailContactType objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdContent_R_ContentEmailContactType", objetoNegocio.Id);
            parameters.AgregarParametro("IdContent", objetoNegocio.IdContent);
            parameters.AgregarParametro("IdEmailContactType", objetoNegocio.EMailContactType.Id);
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