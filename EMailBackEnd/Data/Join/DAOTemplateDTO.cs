using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.DTO;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Data.Interfaces;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data.Join
{
    public class DAOTemplateDTO : DAOObjetoPersistido<TemplateDTO>, IDAOTemplateDTO
    {
        protected override string NombreConnectionString()
        {
            return "EmailAdmin";
        }

        protected override void Completar(TemplateDTO objetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdTemplate"]);
            objetoPersistido.Name = dr["Name"].ToString();
            objetoPersistido.TemplateTypeDescription = dr["TemplateType"].ToString();
            objetoPersistido.Hierarchy = Convert.ToInt32(dr["Hierarchy"]);
            objetoPersistido.EffectiveStartDate = Convert.ToDateTime(dr["EffectiveStartDate"]);
            objetoPersistido.EffectiveEndDate = Convert.ToDateTime(dr["EffectiveEndDate"]);
        }

        #region IDAOTemplateDTO Members

        public IList<TemplateDTO> Find(int idTemplateType, string name)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdTemplateType", idTemplateType);
            parameters.AgregarParametro("Name", name);
            return Buscar(new Filtro(parameters, "dbo.Template_Tx_IdTemplateType_Name"), true);
        }
        #endregion
    }
}
