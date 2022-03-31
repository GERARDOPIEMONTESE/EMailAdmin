using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;
using FrameworkDAC.Parametro;
using System.Transactions;
using System.Diagnostics;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOTemplate_R_Attachment : DAOObjetoNegocio<Template_R_Attachment>, IDAOTemplate_R_Attachment
    {
        #region IDAOTemplate_R_Attachment Members

        public IList<Template_R_Attachment> FindByTemplateId(int idTemplate)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("idTemplate", idTemplate);

            return Buscar(new Filtro(parameters, "Template_R_Attachment_Tx_Filters"));
        }

        public Template_R_Attachment FindByTemplateAttach(int IdTemplate, int IdAttachment)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdTemplate", IdTemplate);
            parameters.AgregarParametro("IdAttachment", IdAttachment);

            return Obtener(new Filtro(parameters, "Template_R_Attachment_Tx_Filters"));
        }

        public void DeleteByIdTemplate(int idTemplate,int idUser)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("idTemplate", idTemplate);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());
            parameters.AgregarParametro("IdUser", idUser);

            Ejecutar(new Filtro(parameters, "Template_R_Attachment_E_IdTemplate"));
        }

        public void DeleteByIdTemplate(int idTemplate, int idUser, TransactionScope ts)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("idTemplate", idTemplate);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());
            parameters.AgregarParametro("IdUser", idUser);

            Ejecutar(new Filtro(parameters, "Template_R_Attachment_E_IdTemplate"), ts);
        }

        #endregion

        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override void Completar(Template_R_Attachment objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdTemplate_R_Attachment"]);
            objetoPersistido.Attachment =
                DAOLocator.Instance().GetDaoAttachment().Get(Convert.ToInt32(dr["IdAttachment"]));
            objetoPersistido.IdTemplate = Convert.ToInt32(dr["IdTemplate"]);            
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
            if (!string.IsNullOrEmpty(dr["IdGroupAttachment"].ToString()))
            {
                objetoPersistido.Attachment.GroupAttachment = new GroupAttachment()
                {
                    Id = Convert.ToInt32(dr["IdGroupAttachment"]),
                    GroupName = dr["GroupName"].ToString(),
                    AttachName_ES = dr["GroupNameES"].ToString(),
                    AttachName_EN = dr["GroupNameEN"].ToString(),
                    AttachName_PT = dr["GroupNamePT"].ToString(),
                    AttachOrder = int.Parse(dr["GroupAttachOrder"].ToString())
                };
            }
            if (!string.IsNullOrEmpty(dr["AttachOrder"].ToString()))
                objetoPersistido.Attachment.AttachOrder = Convert.ToInt32(dr["AttachOrder"]);
        }

        protected override void CompletarComposicion(Template_R_Attachment objetoPersistido)
        {
            objetoPersistido.Attachment.AttachmentTemplates = DAOLocator.Instance().GetDaoEstrategyAttachmentTemplate().FindAttachmentTemplates(objetoPersistido.IdTemplate, objetoPersistido.Attachment.Id);
        }

        protected override Parametros ParametrosCrear(Template_R_Attachment objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdTemplate", objetoNegocio.IdTemplate);
            parameters.AgregarParametro("IdAttachment", objetoNegocio.Attachment.Id);
            if (objetoNegocio.Attachment.GroupAttachment.Id > 0)
                parameters.AgregarParametro("IdGroupAttachment", objetoNegocio.Attachment.GroupAttachment.Id);
            if (objetoNegocio.Attachment.AttachOrder > 0)
                parameters.AgregarParametro("AttachOrder", objetoNegocio.Attachment.AttachOrder);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }

        protected override void CrearComposicion(ObjetoNegocio objetoNegocio, TransactionScope ts)
        {
            Template_R_Attachment obj = ((Template_R_Attachment)objetoNegocio);
            if (obj.Attachment.AttachmentContentPDF != null)
            {
                foreach (var item in obj.Attachment.AttachmentContentPDF)
                {
                    item.IdTemplate = obj.IdTemplate;
                    item.IdUsuario = obj.IdUsuario;
                    if (item.Body.Length > 0)
                    {
                        item.Persistir(ts);
                    }
                    else
                    {
                        if (item.Id > 0)
                            item.Eliminar(ts);
                    }
                }
            }

            if (obj.Attachment.AttachmentTemplates != null)
            {
                foreach (var item in obj.Attachment.AttachmentTemplates)
                {
                    if (item.Id == 0)
                    {
                        item.IdUsuario = obj.IdUsuario;
                        item.Persistir(ts);
                    }
                    else if (item.IdEstado == item.ObtenerEliminado())
                    {
                        item.IdUsuario = obj.IdUsuario;
                        item.Eliminar(ts);
                    }
                    // si tiene id y no esta eliminado ya esta guardado. No hay modificacion
                }
            }
        }

        protected override Parametros ParametrosModificar(Template_R_Attachment objetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override Parametros ParametrosEliminar(Template_R_Attachment objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdTemplate_R_Attachment", objetoNegocio.Id);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            return parameters;
        }

        protected override Parametros ParametrosGrabarLog(Template_R_Attachment objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdTemplate_R_Attachment", objetoNegocio.Id);
            parameters.AgregarParametro("IdTemplate", objetoNegocio.IdTemplate);
            parameters.AgregarParametro("IdAttachment", objetoNegocio.Attachment.Id);
            parameters.AgregarParametro("IdGroupAttachment", objetoNegocio.Attachment.GroupAttachment.Id);
            parameters.AgregarParametro("AttachOrder", objetoNegocio.Attachment.AttachOrder);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }
    }
}