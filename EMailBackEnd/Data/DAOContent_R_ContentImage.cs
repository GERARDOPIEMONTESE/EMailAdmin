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
    public class DAOContent_R_ContentImage : DAOObjetoNegocio<Content_R_ContentImage>, IDAOContent_R_ContentImage
    {
        #region IDAOContent_R_ContentImage Members

        public IList<Content_R_ContentImage> Find(int idContent, int order)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdContent", idContent);
            parameters.AgregarParametro("Order", order);

            return Buscar(new Filtro(parameters, "dbo.Content_R_ContentImage_Tx_IdContent_Order"));
        }

        public void DeleteByIdContent(int idContent, int idUser)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdContent", idContent);
            parameters.AgregarParametro("IdUser", idUser);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            Ejecutar(new Filtro(parameters, "Content_R_ContentImage_E_IdContent"));
        }

        public void DeleteByIdContent(int idContent, int idUser, TransactionScope ts)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdContent", idContent);
            parameters.AgregarParametro("IdUser", idUser);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            Ejecutar(new Filtro(parameters, "Content_R_ContentImage_E_IdContent"), ts);
        }

        #endregion

        protected override void Completar(Content_R_ContentImage objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdContent_R_Image"]);
            objetoPersistido.IdContent = Convert.ToInt32(dr["IdContent"]);
            objetoPersistido.ContentImage =
                DAOLocator.Instance().GetDaoContentImage().Get(Convert.ToInt32(dr["IdImage"]));
            objetoPersistido.Order = Convert.ToInt32(dr["Order"]);
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
        }

        protected override Parametros ParametrosCrear(Content_R_ContentImage objetoNegocio)
        {
            objetoNegocio.IdEstado = ObjetoNegocio.Creado();
            var parameters = new Parametros();

            parameters.AgregarParametro("IdContent", objetoNegocio.IdContent);
            parameters.AgregarParametro("IdContentImage", objetoNegocio.ContentImage.Id);
            parameters.AgregarParametro("Order", objetoNegocio.Order);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override Parametros ParametrosModificar(Content_R_ContentImage objetoNegocio)
        {
            objetoNegocio.IdEstado = ObjetoNegocio.Modificado();
            var parameters = new Parametros();

            parameters.AgregarParametro("IdContent_R_ContentImage", objetoNegocio.Id);
            parameters.AgregarParametro("IdContent", objetoNegocio.IdContent);
            parameters.AgregarParametro("IdContentImage", objetoNegocio.ContentImage.Id);
            parameters.AgregarParametro("Order", objetoNegocio.Order);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);

            return parameters;
        }

        protected override Parametros ParametrosEliminar(Content_R_ContentImage objetoNegocio)
        {
            objetoNegocio.IdEstado = ObjetoNegocio.Eliminado();
            var parameters = new Parametros();

            parameters.AgregarParametro("IdContent_R_ContentImage", objetoNegocio.Id);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override Parametros ParametrosGrabarLog(Content_R_ContentImage objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdContent_R_ContentImage", objetoNegocio.Id);
            parameters.AgregarParametro("IdContent", objetoNegocio.IdContent);
            parameters.AgregarParametro("IdContentImage", objetoNegocio.ContentImage.Id);
            parameters.AgregarParametro("Order", objetoNegocio.Order);
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