using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CapaNegocioDatos.CapaDatos;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;
using FrameworkDAC.Parametro;
using System.Transactions;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOAttachment_R_Group : DAOObjetoNegocio<Attachment_R_Group>, IDAOAttachment_R_Group
    {
        #region IDAOAttachment_R_Group Members

        public IList<Attachment_R_Group> FindByGroupId(int idGroup)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdGroup", idGroup);

            return Buscar(new Filtro(parameters, "Attachment_R_Group_Tx_Filters"));
        }

        public IList<Attachment_R_Group> FindByAttachmentId(int idAttachment)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdAttachment", idAttachment);

            return Buscar(new Filtro(parameters, "Attachment_R_Group_Tx_Filters"));
        }

        public void DeleteByIdAttachment(int idAttachment)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("idAttachment", idAttachment);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            Ejecutar(new Filtro(parameters, "dbo.Attachment_R_Group_E_IdAttachment"));
        }

        public void DeleteByIdAttachment(int idAttachment, TransactionScope ts)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("idAttachment", idAttachment);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            Ejecutar(new Filtro(parameters, "dbo.Attachment_R_Group_E_IdAttachment"), ts);
        }

        #endregion

        protected override void Completar(Attachment_R_Group objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdAttachment_R_Group"]);
            objetoPersistido.IdAttachment = Convert.ToInt32(dr["IdAttachment"].ToString());
            objetoPersistido.IdGroup = Convert.ToInt32(dr["IdGroup"]);
            objetoPersistido.Module = DAOModulo.Instancia().Obtener(Convert.ToInt32(dr["IdModule"]), true);
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
        }

        protected override void CompletarComposicion(Attachment_R_Group objetoPersistido)
        {
            objetoPersistido.Group = DAOLocator.Instance().GetDaoGroup().Get(objetoPersistido.IdGroup);
        }

        protected override Parametros ParametrosCrear(Attachment_R_Group objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdAttachment", objetoNegocio.Attachment!= null ? objetoNegocio.Attachment.Id : objetoNegocio.IdAttachment);
            parameters.AgregarParametro("IdGroup", objetoNegocio.IdGroup);
            parameters.AgregarParametro("IdModule", objetoNegocio.Module.Id);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }

        protected override Parametros ParametrosModificar(Attachment_R_Group objetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override Parametros ParametrosEliminar(Attachment_R_Group objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdAttachment_R_Group", objetoNegocio.Id);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            return parameters;
        }

        protected override Parametros ParametrosGrabarLog(Attachment_R_Group objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdAttachment_R_Group", objetoNegocio.Id);
            parameters.AgregarParametro("IdAttachment", objetoNegocio.Attachment != null ? objetoNegocio.Attachment.Id : objetoNegocio.IdAttachment);
            parameters.AgregarParametro("IdGroup", objetoNegocio.IdGroup);
            parameters.AgregarParametro("IdModule", objetoNegocio.Module.Id);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }

        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }
    }
}