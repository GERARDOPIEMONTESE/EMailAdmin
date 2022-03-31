using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOAttachment : DAOObjetoNegocio<Attachment>, IDAOAttachment
    {
        #region IDAOAttachment Members

        public IList<Attachment> FindAll()
        {
            return Buscar(new Filtro(new Parametros(), "dbo.Attachment_Tx_Filters"), true);
        }

        public IList<Attachment> FindByFilters(int idType, int idEstrategy, string name, bool lazy)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdAttachmentType", idType);
            parameters.AgregarParametro("IdEstrategy", idEstrategy);
            parameters.AgregarParametro("Name", name);

            return Buscar(new Filtro(parameters, "dbo.Attachment_Tx_AttachmentType_Country"), lazy);
        }

        public IList<Attachment> FindByFilters(int idType, int idEstrategy, string name)
        {
            return FindByFilters(idType, idEstrategy, name, false);
        }

        public IList<Attachment> FindWithAssociations(int idType, int idEstrategy, string name)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdAttachmentType", idType);
            parameters.AgregarParametro("IdEstrategy", idEstrategy);
            parameters.AgregarParametro("Name", name);

            return Buscar(new Filtro(parameters, "dbo.Attachment_Tx_Associations"));
        } 

        public IList<Attachment> FindByName(string name)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("Name", name);

            return Buscar(new Filtro(parameters, "dbo.Attachment_Tx_Filters"));
        }

        public IList<Attachment> FindByNameType(string name, int type)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("Name", name);
            parameters.AgregarParametro("IdAttachmentType", type);

            return Buscar(new Filtro(parameters, "dbo.Attachment_Tx_Filters"), true);
        }

        public IList<Attachment> FindByType( int type)
        {
            return FindByType(type, false);
        }

        public IList<Attachment> FindByType(int type, bool lazy)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdAttachmentType", type);

            return Buscar(new Filtro(parameters, "dbo.Attachment_Tx_Filters"), lazy);
        }

        public Attachment Get(int id)
        {
            return Obtener(id);
        }

        public IList<Attachment> FindByTemplateId(int idTemplate)
        {
            return
                DAOLocator.Instance().GetDaoTemplate_R_Attachment().FindByTemplateId(idTemplate).Select(
                    templateRAttachment => templateRAttachment.Attachment).ToList();
        }

        public IList<Attachment> Find(int idTemplate, int idModule, int idGroupType,
            int idLocation, int idProduct, int idRate, int IdAccount)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdTemplate", idTemplate);
            parameters.AgregarParametro("IdModule", idModule);
            parameters.AgregarParametro("IdGroupType", idGroupType);
            parameters.AgregarParametro("IdLocation", idLocation);
            parameters.AgregarParametro("IdProduct", idProduct);
            parameters.AgregarParametro("IdRate", idRate);
            parameters.AgregarParametro("IdAccount", IdAccount);

            return Buscar(new Filtro(parameters, "dbo.Attachment_Tx_Group_Filters"));
        }

        public IList<Attachment> FindByTypeWithoutAssociations(int idAttachmentType)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdAttachmentType", idAttachmentType);
            return Buscar(new Filtro(parameters, "dbo.Attachment_Tx_IdAttachmentType_NoAssociations"));
        }

        #endregion

        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override Parametros ParametrosCrear(Attachment objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("Name", objetoNegocio.Name);
            parameters.AgregarParametro("IdAttachmentType", objetoNegocio.AttachmentType.Id);
            parameters.AgregarParametro("IdEstrategy", objetoNegocio.Estrategy.Id);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }

        protected override Parametros ParametrosModificar(Attachment objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdAttachment", objetoNegocio.Id);
            parameters.AgregarParametro("Name", objetoNegocio.Name);
            parameters.AgregarParametro("IdAttachmentType", objetoNegocio.AttachmentType.Id);
            parameters.AgregarParametro("IdEstrategy", objetoNegocio.Estrategy.Id);

            return parameters;
        }

        protected override Parametros ParametrosEliminar(Attachment objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdAttachment", objetoNegocio.Id);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            return parameters;
        }

        protected override Parametros ParametrosGrabarLog(Attachment objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdAttachment", objetoNegocio.Id);
            parameters.AgregarParametro("Name", objetoNegocio.Name);
            parameters.AgregarParametro("IdAttachmentType", objetoNegocio.AttachmentType.Id);
            parameters.AgregarParametro("IdEstrategy", objetoNegocio.Estrategy.Id);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }

        protected override void Completar(Attachment objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdAttachment"]);
            objetoPersistido.Name = dr["Name"].ToString();
            objetoPersistido.AttachmentType =
                DAOLocator.Instance().GetDaoAttachmentType().Get(Convert.ToInt32(dr["IdAttachmentType"]));
            objetoPersistido.Estrategy =
                DAOLocator.Instance().GetDaoEstrategy().Get(Convert.ToInt32(dr["IdEstrategy"]));
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
        }

        protected override void CompletarComposicion(Attachment objetoPersistido)
        {
            //ITEMS
            objetoPersistido.AttachmentItems =
                DAOLocator.Instance().GetDaoAttachmentItem().FindByAttachmentId(objetoPersistido.Id);

            objetoPersistido.IGroups =
                DAOLocator.Instance().GetDaoAttachment_R_Group().FindByAttachmentId(objetoPersistido.Id);


        }

        protected override void CrearComposicion(ObjetoNegocio objetoNegocio, TransactionScope ts)
        {
            var attachment = (Attachment)objetoNegocio;

            DAOLocator.Instance().GetDaoAttachmentItem().DeleteByAttachmentId(attachment.Id, ts);
            if (attachment.AttachmentItems != null)
            {
                foreach (AttachmentItem attachmentItem in attachment.AttachmentItems)
                {
                    attachmentItem.IdAttachment = attachment.Id;
                    attachmentItem.IdEstado = ObjetoNegocio.Creado();
                    attachmentItem.IdUsuario = attachment.IdUsuario;
                    attachmentItem.Persistir(ts);
                }
            }

            DAOLocator.Instance().GetDaoAttachment_R_Group().DeleteByIdAttachment(attachment.Id);
            foreach (var attachmentRGroup in attachment.IGroups)
            {
                attachmentRGroup.Id = 0;
                attachmentRGroup.Attachment = attachment;
                attachmentRGroup.IdEstado = ObjetoNegocio.Creado();
                attachmentRGroup.IdUsuario = attachment.IdUsuario;
                DAOLocator.Instance().GetDaoAttachment_R_Group().Persistir(attachmentRGroup, ts);
            }
        }

        protected override void ModificarComposicion(ObjetoNegocio objetoNegocio, TransactionScope ts)
        {
            CrearComposicion(objetoNegocio, ts);
        }

        protected override void EliminarComposicion(ObjetoNegocio objetoNegocio, TransactionScope ts)
        {
            DAOLocator.Instance().GetDaoAttachmentItem().DeleteByAttachmentId(objetoNegocio.Id, ts);
            DAOLocator.Instance().GetDaoAttachment_R_Group().DeleteByIdAttachment(objetoNegocio.Id, ts);
        }
        
        public AttachmentItem FindAttachItemByNameAndLang(string name, int IdLanguage)
        {
            var attach = DAOLocator.Instance().GetDaoAttachment().FindByName(name).FirstOrDefault();
            if (attach != null && attach.Id > 0)
            {
                return attach.AttachmentItems.FirstOrDefault(x => x.Language.Id == IdLanguage);
            }
            return null;
        }        
    }
}