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
    public class DAOAttachmentItem : DAOObjetoNegocio<AttachmentItem>, IDAOAttachmentItem
    {
        #region IDAOAttachmentItem Members

        public IList<AttachmentItem> FindByAttachmentId(int idAttachment)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdAttachment", idAttachment);

            return Buscar(new Filtro(parameters, "AttachmentItem_Tx_Filters"));
        }

        public void DeleteByAttachmentId(int idAttachment)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdAttachment", idAttachment);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            Ejecutar(new Filtro(parameters, "AttachmentItem_E_IdAttachment"));
        }

        public void DeleteByAttachmentId(int idAttachment, TransactionScope ts)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdAttachment", idAttachment);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            Ejecutar(new Filtro(parameters, "AttachmentItem_E_IdAttachment"), ts);
        }

        #endregion

        protected override void Completar(AttachmentItem objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdAttachmentItem"]);
            objetoPersistido.IdAttachment = Convert.ToInt32(dr["IdAttachment"]);
            objetoPersistido.Language = DAOIdioma.Instancia().Obtener(Convert.ToInt32(dr["IdLanguage"]));
            objetoPersistido.Name = dr["Name"].ToString();
            objetoPersistido.Description = dr["Description"].ToString();
            objetoPersistido.Content = dr["Content"];
            objetoPersistido.Type = dr["Type"].ToString();
            objetoPersistido.Dimenssion = Convert.ToDecimal(dr["Dimenssion"]);
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
        }

        protected override Parametros ParametrosCrear(AttachmentItem objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdAttachment", objetoNegocio.IdAttachment);
            parameters.AgregarParametro("IdLanguage", objetoNegocio.Language.Id);
            parameters.AgregarParametro("Name", objetoNegocio.Name);
            parameters.AgregarParametro("Description", objetoNegocio.Description);
            parameters.AgregarParametro("Content", objetoNegocio.Content);
            parameters.AgregarParametro("Type", objetoNegocio.Type);
            parameters.AgregarParametro("Dimenssion", objetoNegocio.Dimenssion);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }

        protected override Parametros ParametrosModificar(AttachmentItem objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdAttachmentItem", objetoNegocio.Id);
            parameters.AgregarParametro("IdAttachment", objetoNegocio.IdAttachment);
            parameters.AgregarParametro("IdLanguage", objetoNegocio.Language.Id);
            parameters.AgregarParametro("Name", objetoNegocio.Name);
            parameters.AgregarParametro("Description", objetoNegocio.Description);
            parameters.AgregarParametro("Content", objetoNegocio.Content);
            parameters.AgregarParametro("Type", objetoNegocio.Type);
            parameters.AgregarParametro("Dimenssion", objetoNegocio.Dimenssion);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }

        protected override Parametros ParametrosEliminar(AttachmentItem objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdAttachmentItem", objetoNegocio.Id);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            return parameters;
        }

        protected override Parametros ParametrosGrabarLog(AttachmentItem objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdAttachmentItem", objetoNegocio.Id);
            parameters.AgregarParametro("IdAttachment", objetoNegocio.IdAttachment);
            parameters.AgregarParametro("IdLanguage", objetoNegocio.Language.Id);
            parameters.AgregarParametro("Name", objetoNegocio.Name);
            parameters.AgregarParametro("Description", objetoNegocio.Description);
            parameters.AgregarParametro("Content", objetoNegocio.Content);
            parameters.AgregarParametro("Type", objetoNegocio.Type);
            parameters.AgregarParametro("Dimenssion", objetoNegocio.Dimenssion);
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