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
    public class DAOGroup_R_Template_Association : DAOObjetoPersistido<ReportAssociation>, IDAOGroup_R_Template_Association
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        public IList<ReportAssociation> Find(int idTemplateType, string groupName, int IdLocacion, string accountCode, int idProduct, string rateCode, DateTime effectiveDate, int idGroupType, int asociados)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdTemplateType", idTemplateType);
            parameters.AgregarParametro("GroupName", groupName);
            parameters.AgregarParametro("IdGroupType", idGroupType);
            parameters.AgregarParametro("IdLocacion", IdLocacion);
            parameters.AgregarParametro("AccountCode", accountCode);
            parameters.AgregarParametro("IdProduct", idProduct);
            parameters.AgregarParametro("RateCode", rateCode);
            parameters.AgregarParametro("EffectiveDate", effectiveDate);
            parameters.AgregarParametro("Asociados", asociados);

            return Buscar(new Filtro(parameters, "dbo.Group_R_Template_Tx_TemplateType_FiltersCodes"));
        }

        protected override void Completar(ReportAssociation objetoPersistido, SqlDataReader dr)
        {
            
            objetoPersistido.HierarchyDescription = dr["GroupDescription"].ToString();
            objetoPersistido.GroupDescription = dr["GroupDescription"].ToString();
            objetoPersistido.TemplateType = dr["TemplateType"].ToString();
            objetoPersistido.TemplateName = dr["TemplateName"].ToString();
            if (dr["EffectiveStartDate"].ToString()!="") objetoPersistido.EffectiveStartDate = DateTime.Parse(dr["EffectiveStartDate"].ToString()).ToShortDateString();
            if (dr["EffectiveEndDate"].ToString()!="") objetoPersistido.EffectiveEndDate = DateTime.Parse(dr["EffectiveEndDate"].ToString()).ToShortDateString();
        }
    }
}
