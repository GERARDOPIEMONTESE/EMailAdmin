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
    public class DAOGroup_R_Template : DAOObjetoNegocio<Group_R_Template>, IDAOGroup_R_Template
    {
        #region IDAOGroup_R_Template Members

        public IList<Group_R_Template> FindByGroupId(int idGroup)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdGroup", idGroup);

            return Buscar(new Filtro(parameters, "Group_R_Template_Tx_Filters"));
        }

        public IList<Group_R_Template> FindByTemplateId(int idTemplate)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdTemplate", idTemplate);

            return Buscar(new Filtro(parameters, "Group_R_Template_Tx_Filters"));
        }

        public IList<Group_R_Template> Find(int idTemplateType, int idLocation, int idAccount, int idProduct,
                                        int idRate, int idDistributionType, DateTime effectiveDate, int idModule, int idGroupType)
        {
            return Find(idTemplateType, idLocation, idAccount, idProduct, idRate, idDistributionType, effectiveDate, idModule, idGroupType, false);
        }
        
        public IList<Group_R_Template> Find(int idTemplateType, int idLocation, int idAccount, int idProduct,
                                        int idRate, int idDistributionType, DateTime effectiveDate, int idModule, int idGroupType, bool lazy)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdTemplateType", idTemplateType);
            parameters.AgregarParametro("IdGroupType", idGroupType);
            parameters.AgregarParametro("IdLocation", idLocation);
            parameters.AgregarParametro("IdAccount", idAccount);
            parameters.AgregarParametro("IdProduct", idProduct);
            parameters.AgregarParametro("IdRate", idRate);
            parameters.AgregarParametro("IdDistributionType", idDistributionType);
            parameters.AgregarParametro("EffectiveDate", effectiveDate);
            parameters.AgregarParametro("IdModule", idModule);

            return Buscar(new Filtro(parameters, "dbo.Group_R_Template_Tx_TemplateType_Filters"), lazy);
        }

        public IList<Group_R_Template> Find(int idTemplate, int idAttachment, int idLocation, int idAccount, int idProduct,
                                        int idRate, int idDistributionType, DateTime effectiveDate, int idModule, int idGroupType, bool lazy)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdTemplate", idTemplate);
            parameters.AgregarParametro("IdAttachment", idAttachment);
            parameters.AgregarParametro("IdGroupType", idGroupType);
            parameters.AgregarParametro("IdLocation", idLocation);
            parameters.AgregarParametro("IdAccount", idAccount);
            parameters.AgregarParametro("IdProduct", idProduct);
            parameters.AgregarParametro("IdRate", idRate);
            parameters.AgregarParametro("IdDistributionType", idDistributionType);
            parameters.AgregarParametro("EffectiveDate", effectiveDate);
            parameters.AgregarParametro("IdModule", idModule);

            return Buscar(new Filtro(parameters, "dbo.Group_R_Template_Tx_Attachment_Filters"), lazy);
        }

        public IList<Group_R_Template> Find(int idTemplateType, string countryCode, string accountCode, int idProduct,
                                string rateCode, DateTime effectiveDate, int idGroupType)
        {
            return Find(idTemplateType, countryCode, accountCode, idProduct, rateCode, effectiveDate, idGroupType, false);
        }

        public IList<Group_R_Template> Find(int idTemplateType, string countryCode, string accountCode, int idProduct,
                                string rateCode, DateTime effectiveDate, int idGroupType, bool lazy)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdTemplateType", idTemplateType);
            parameters.AgregarParametro("IdGroupType", idGroupType);
            parameters.AgregarParametro("CountryCode", countryCode);
            parameters.AgregarParametro("AccountCode", accountCode);
            parameters.AgregarParametro("IdProduct", idProduct);
            parameters.AgregarParametro("RateCode", rateCode);
            parameters.AgregarParametro("EffectiveDate", effectiveDate);

            return Buscar(new Filtro(parameters, "dbo.Group_R_Template_Tx_TemplateType_FiltersCodes"), lazy);
        }
      
        public void DeleteByIdTemplate(int idTemplate, int idUser)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("idTemplate", idTemplate);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());
            parameters.AgregarParametro("IdUser", idUser);

            Ejecutar(new Filtro(parameters, "dbo.Group_R_Template_E_IdTemplate"));
        }

        public void DeleteByIdTemplate(int idTemplate, int idUser, TransactionScope ts)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("idTemplate", idTemplate);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());
            parameters.AgregarParametro("IdUser", idUser);

            Ejecutar(new Filtro(parameters, "dbo.Group_R_Template_E_IdTemplate"), ts);
        }

        #endregion

        protected override void Completar(Group_R_Template objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdGroup_R_Template"]);
            objetoPersistido.IdTemplate = Convert.ToInt32(dr["IdTemplate"].ToString());
            objetoPersistido.IdGroup = Convert.ToInt32(dr["IdGroup"]);
            objetoPersistido.Module = DAOModulo.Instancia().Obtener(Convert.ToInt32(dr["IdModule"]), true);
            objetoPersistido.Receive = Convert.ToBoolean(dr["Receive"]);
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
        }

        protected override void CompletarComposicion(Group_R_Template objetoPersistido)
        {
            objetoPersistido.Group = DAOLocator.Instance().GetDaoGroup().Get(objetoPersistido.IdGroup, true);
        }

        protected override Parametros ParametrosCrear(Group_R_Template objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdTemplate", objetoNegocio.Template != null ? objetoNegocio.Template.Id : objetoNegocio.IdTemplate);
            parameters.AgregarParametro("IdGroup", objetoNegocio.Group!=null ? objetoNegocio.Group.Id : objetoNegocio.IdGroup);
            parameters.AgregarParametro("IdModule", objetoNegocio.Module.Id);
            parameters.AgregarParametro("Receive", objetoNegocio.Receive);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }

        protected override Parametros ParametrosModificar(Group_R_Template objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdGroup_R_Template", objetoNegocio.Id);
            parameters.AgregarParametro("IdTemplate", objetoNegocio.Template != null ? objetoNegocio.Template.Id : objetoNegocio.IdTemplate);
            parameters.AgregarParametro("IdGroup", objetoNegocio.Group != null ? objetoNegocio.Group.Id : objetoNegocio.IdGroup);
            parameters.AgregarParametro("IdModule", objetoNegocio.Module.Id);
            parameters.AgregarParametro("Receive", objetoNegocio.Receive);

            return parameters;
        }

        protected override Parametros ParametrosEliminar(Group_R_Template objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdGroup_R_Template", objetoNegocio.Id);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            return parameters;
        }

        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override Parametros ParametrosGrabarLog(Group_R_Template objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdGroup_R_Template", objetoNegocio.Id);
            parameters.AgregarParametro("IdTemplate", objetoNegocio.Template != null ? objetoNegocio.Template.Id : objetoNegocio.IdTemplate);
            parameters.AgregarParametro("IdGroup", objetoNegocio.Group != null ? objetoNegocio.Group.Id : objetoNegocio.IdGroup);
            parameters.AgregarParametro("IdModule", objetoNegocio.Module.Id);
            parameters.AgregarParametro("Receive", objetoNegocio.Receive);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }

    }
}