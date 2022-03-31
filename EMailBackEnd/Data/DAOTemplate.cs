using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Transactions;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOTemplate : DAOObjetoNegocio<Template>, IDAOTemplate
    {
        #region IDAOTemplate Members

        public IList<Template> FindAll()
        {
            var parameters = new Parametros();
            return Buscar(new Filtro(parameters, "dbo.Template_Tx_IdTemplateType_Name"), true);
        }

        public IList<Template> FindAllList()
        {
            var parameters = new Parametros();
            return Buscar_KeyValue(new Filtro(parameters, "dbo.Template_Tx_IdTemplateType_Name"), true);
        }

        public IList<Template> Find(int idTemplateType, bool lazy)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdTemplateType", idTemplateType);
            return Buscar(new Filtro(parameters, "dbo.Template_Tx_IdTemplateType_Name"), lazy);
        }

        public IList<Template> Find(int idTemplateType)
        {
            return Find(idTemplateType, false);
        }

        public IList<Template> FindByTypeWithoutAssociations(int idTemplateType)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdTemplateType", idTemplateType);
            return Buscar(new Filtro(parameters, "dbo.Template_Tx_IdTemplateType_NoAssociations"), true);
        }

        public IList<Template> Find(int idTemplateType, string name, bool lazy = true)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdTemplateType", idTemplateType);
            parameters.AgregarParametro("Name", name);
            return Buscar(new Filtro(parameters, "dbo.Template_Tx_IdTemplateType_Name"), lazy);
        }

        public IList<Template> Find(string name)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("Name", name);
            return Buscar(new Filtro(parameters, "dbo.Template_Tx_Name"), true);
        }

        public IList<Template> Find(int idTemplateType, string name, DateTime date)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdTemplateType", idTemplateType);
            parameters.AgregarParametro("Name", name);
            parameters.AgregarParametro("Date", date);

            return Buscar(new Filtro(parameters, "dbo.Template_Tx_IdTemplateType_Name_Date"));
        }

        public IList<Template> FindWithAssociations(int idTemplateType, string name)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdTemplateType", idTemplateType);
            parameters.AgregarParametro("Name", name);
            return Buscar(new Filtro(parameters, "dbo.Template_Tx_IdTemplateType_Name_Associations"), true);
        }

        public Template Get(int id)
        {
            return Obtener(id);
        }

        public Template Get(int idTemplateType, string name, DateTime date)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdTemplateType", idTemplateType);
            parameters.AgregarParametro("Name", name);
            parameters.AgregarParametro("Date", date);

            return Obtener(new Filtro(parameters, "dbo.Template_Tx_IdTemplateType_Name_Date"));
        }

        public Template Get(int idTemplateType, string name)
        {
            return Get(idTemplateType, name, DateTime.Now);
        }

        public IList<Template> Find(int idTemplateType, int country, string accountCode, string productCode, DateTime effectiveDate)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdTemplateType", idTemplateType);
            parameters.AgregarParametro("EffectiveDate", effectiveDate);
            parameters.AgregarParametro("Country", country);
            parameters.AgregarParametro("AccountCode", accountCode);
            parameters.AgregarParametro("ProductCode", productCode);

            return Buscar(new Filtro(parameters, "dbo.Template_Tx_TemplateType_Group"));
        }


        public Template GetHierarchy(int IdTemplate)
        {
            Template template = new Template();
            var parametros = new Parametros();

            parametros.AgregarParametro("IdTemplate", IdTemplate);

            IDictionary<string, object> dicDatos = new Dictionary<string, object>();
            dicDatos.Add("IdTemplate", 0);
            dicDatos.Add("Hierarchy",0);

            IDictionary<string, object> rstDatos = Valores(new Filtro(parametros, "dbo.Template_Tx_IdTemplate", dicDatos));

            if (rstDatos["IdTemplate"] != DBNull.Value)
            {
                template.Id = Convert.ToInt32(rstDatos["IdTemplate"]);
                template.Hierarchy = Convert.ToInt32(rstDatos["Hierarchy"]);
            }

            return template;
        }

        #endregion

        protected override string NombreConnectionString()
        {
            return "EmailAdmin";
        }

        protected override Parametros ParametrosCrear(Template objetoNegocio)
        {
            var parametros = new Parametros();

            parametros.AgregarParametro("Name", objetoNegocio.Name);
            parametros.AgregarParametro("IdTemplateType", objetoNegocio.TemplateType.Id);
            parametros.AgregarParametro("Hierarchy", objetoNegocio.Hierarchy);
            //parametros.AgregarParametro("IdModule", objetoNegocio.Module.Id);
            parametros.AgregarParametro("IdModule", 0);
            parametros.AgregarParametro("EffectiveStartDate", objetoNegocio.EffectiveStartDate);
            parametros.AgregarParametro("EffectiveEndDate", objetoNegocio.EffectiveEndDate);
            parametros.AgregarParametro("IdEMailFromAddress", objetoNegocio.IdEMailFromAddress);
            parametros.AgregarParametro("MergeAttachsWithEKit", objetoNegocio.MergeAttachsWithEKit);
            parametros.AgregarParametro("TypeAttachsWithEkit", Template.GetTypeAttachsWithEkitValue(objetoNegocio.TypeAttachsWithEkit.ToString()));
            parametros.AgregarParametro("IdTemplatePDF", objetoNegocio.IdTemplatePDF);
            parametros.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parametros.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parametros;
        }

        protected override Parametros ParametrosModificar(Template objetoNegocio)
        {
            var parametros = new Parametros();

            parametros.AgregarParametro("IdTemplate", objetoNegocio.Id);
            parametros.AgregarParametro("Name", objetoNegocio.Name);
            parametros.AgregarParametro("IdTemplateType", objetoNegocio.TemplateType.Id);
            parametros.AgregarParametro("Hierarchy", objetoNegocio.Hierarchy);
            //parametros.AgregarParametro("IdModule", objetoNegocio.Module.Id);
            parametros.AgregarParametro("IdModule", 0);
            parametros.AgregarParametro("EffectiveStartDate", objetoNegocio.EffectiveStartDate);
            parametros.AgregarParametro("EffectiveEndDate", objetoNegocio.EffectiveEndDate);
            parametros.AgregarParametro("IdEMailFromAddress", objetoNegocio.IdEMailFromAddress);
            parametros.AgregarParametro("MergeAttachsWithEKit", objetoNegocio.MergeAttachsWithEKit);
            parametros.AgregarParametro("TypeAttachsWithEkit", Template.GetTypeAttachsWithEkitValue(objetoNegocio.TypeAttachsWithEkit.ToString()));
            parametros.AgregarParametro("IdTemplatePDF", objetoNegocio.IdTemplatePDF);
            parametros.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parametros.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parametros;
        }

        protected override Parametros ParametrosEliminar(Template objetoNegocio)
        {
            var parametros = new Parametros();

            parametros.AgregarParametro("IdTemplate", objetoNegocio.Id);
            parametros.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parametros.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            return parametros;
        }
        
        protected override Parametros ParametrosGrabarLog(Template objetoNegocio)
        {
            var parametros = new Parametros();

            parametros.AgregarParametro("IdTemplate", objetoNegocio.Id);
            parametros.AgregarParametro("Name", objetoNegocio.Name);
            parametros.AgregarParametro("IdTemplateType", objetoNegocio.TemplateType.Id);
            parametros.AgregarParametro("Hierarchy", objetoNegocio.Hierarchy);
            //parametros.AgregarParametro("IdModule", objetoNegocio.Module.Id);
            parametros.AgregarParametro("IdModule", 0);
            parametros.AgregarParametro("EffectiveStartDate", objetoNegocio.EffectiveStartDate);
            parametros.AgregarParametro("EffectiveEndDate", objetoNegocio.EffectiveEndDate);
            parametros.AgregarParametro("IdEMailFromAddress", objetoNegocio.IdEMailFromAddress);
            parametros.AgregarParametro("MergeAttachsWithEKit", objetoNegocio.MergeAttachsWithEKit);
            parametros.AgregarParametro("TypeAttachsWithEkit", Template.GetTypeAttachsWithEkitValue(objetoNegocio.TypeAttachsWithEkit.ToString()));
            parametros.AgregarParametro("IdTemplatePDF", objetoNegocio.IdTemplatePDF);
            parametros.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parametros.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parametros;
        }

        protected override void Completar(Template objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdTemplate"]);
            objetoPersistido.Name = dr["Name"].ToString();
            objetoPersistido.TemplateType = DAOLocator.Instance().GetDaoTemplateType().Get(
                Convert.ToInt32(dr["IdTemplateType"]));
            objetoPersistido.Hierarchy = Convert.ToInt32(dr["Hierarchy"]);
            objetoPersistido.EffectiveStartDate = Convert.ToDateTime(dr["EffectiveStartDate"]);
            objetoPersistido.EffectiveEndDate = Convert.ToDateTime(dr["EffectiveEndDate"]);
            objetoPersistido.IdEMailFromAddress = Convert.ToInt32(dr["IdEMailFromAddress"]);
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);

            bool merge = false;
            bool.TryParse(dr["MergeAttachsWithEKit"].ToString(), out merge);
            objetoPersistido.MergeAttachsWithEKit = merge;

            if (!string.IsNullOrEmpty(dr["TypeAttachsWithEkit"].ToString()))
                objetoPersistido.TypeAttachsWithEkit = ((Template.eTypeAttachsWithEkit)dr["TypeAttachsWithEkit"]);

            if (!string.IsNullOrEmpty(dr["IdTemplatePDF"].ToString()))
                objetoPersistido.IdTemplatePDF = Convert.ToInt32(dr["IdTemplatePDF"]);
        }

        protected override void CompletarComposicion(Template objetoPersistido)
        {
            objetoPersistido.IContent = DAOLocator.Instance().GetDaoContent().Find(objetoPersistido.Id);
            objetoPersistido.IAttachments = DAOLocator.Instance().GetDaoAttachment().FindByTemplateId(objetoPersistido.Id);
            objetoPersistido.IGroups =
                DAOLocator.Instance().GetDaoGroup_R_Template().FindByTemplateId(objetoPersistido.Id);
        }

        protected override void CompletarKeyValue(Template objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdTemplate"]);
            objetoPersistido.Name = dr["Name"].ToString();
        }

        protected override void CrearComposicion(ObjetoNegocio objetoNegocio, TransactionScope ts)
        {
            var template = (Template) objetoNegocio;

            //DAOLocator.Instance().GetDaoContent().DeleteAll(template.Id, ts);
            foreach (var content in template.IContent)
            {
                content.IdUsuario = template.IdUsuario;
                content.IdTemplate = template.Id;
                content.IdEstado = objetoNegocio.ObtenerCreado();
                DAOLocator.Instance().GetDaoContent().Persistir(content, ts);
            }
            
            DAOLocator.Instance().GetDaoTemplate_R_Attachment().DeleteByIdTemplate(template.Id, template.IdUsuario, ts);
            foreach (var attachment in template.IAttachments)
            {
                var templateRAttachment = new Template_R_Attachment
                                              {
                                                  Attachment = attachment,
                                                  IdTemplate = template.Id,
                                                  IdUsuario = template.IdUsuario,
                                                  IdEstado = ObjetoNegocio.Creado()
                                              };
                DAOLocator.Instance().GetDaoTemplate_R_Attachment().Persistir(templateRAttachment, ts);
            }

            DAOLocator.Instance().GetDaoGroup_R_Template().DeleteByIdTemplate(template.Id,template.IdUsuario, ts);
            foreach (var groupRTemplate in template.IGroups)
            {
                groupRTemplate.Id = 0;
                groupRTemplate.Template = template;
                groupRTemplate.IdEstado = ObjetoNegocio.Creado();
                groupRTemplate.IdUsuario = template.IdUsuario;
                DAOLocator.Instance().GetDaoGroup_R_Template().Persistir(groupRTemplate, ts);
            }            
        }

        protected override void ModificarComposicion(ObjetoNegocio objetoNegocio, TransactionScope ts)
        {
            CrearComposicion(objetoNegocio, ts); 
        }

        protected override void EliminarComposicionPredecesor(ObjetoNegocio ObjetoNegocio, TransactionScope ts)
        {
        
        //protected override void EliminarComposicion(ObjetoNegocio objetoNegocio, TransactionScope ts)
        
            var template = (Template)ObjetoNegocio;
            DAOLocator.Instance().GetDaoTemplate_R_Attachment().DeleteByIdTemplate(template.Id, template.IdUsuario, ts);
            DAOLocator.Instance().GetDaoGroup_R_Template().DeleteByIdTemplate(template.Id, template.IdUsuario, ts);
            foreach (var content in template.IContent)
            {
                DAOLocator.Instance().GetDaoContent().Eliminar(content,ts);
            }
        }

        #region IDAOTemplate Members


        public Template Get(int id, bool lazy)
        {
            return Obtener(id, lazy);
        }

        #endregion


    }
}