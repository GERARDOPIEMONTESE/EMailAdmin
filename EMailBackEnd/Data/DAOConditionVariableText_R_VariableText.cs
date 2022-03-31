using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data.Interfaces;
using FrameworkDAC.Parametro;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOConditionVariableText_R_VariableText : DAOObjetoNegocio<ConditionVariableText_R_VariableText>, IDAOConditionVariableText_R_VariableText
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override Parametros ParametrosCrear(ConditionVariableText_R_VariableText ObjetoNegocio)
        {
            ObjetoNegocio.IdEstado = ObjetoNegocio.ObtenerCreado();
            var parameters = new Parametros();

            parameters.AgregarParametro("IdConditionVariableText", ObjetoNegocio.ConditionVariableTextId);
            parameters.AgregarParametro("IdVariableText", ObjetoNegocio.VariableText.Id);
            parameters.AgregarParametro("Condition", ObjetoNegocio.Condition);
            parameters.AgregarParametro("DynamicName", ObjetoNegocio.DynamicName);
            parameters.AgregarParametro("IdUser", ObjetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.IdEstado);

            return parameters;
        }

        protected override Parametros ParametrosGrabarLog(ConditionVariableText_R_VariableText ObjetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdConditionVariableText_R_VariableText", ObjetoNegocio.Id);
            parameters.AgregarParametro("IdConditionVariableText", ObjetoNegocio.ConditionVariableTextId);
            parameters.AgregarParametro("IdVariableText", ObjetoNegocio.VariableText.Id);
            parameters.AgregarParametro("Condition", ObjetoNegocio.Condition);
            parameters.AgregarParametro("DynamicName", ObjetoNegocio.DynamicName);
            parameters.AgregarParametro("IdUser", ObjetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.IdEstado);

            return parameters;
        }

        protected override Parametros ParametrosModificar(ConditionVariableText_R_VariableText ObjetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override Parametros ParametrosEliminar(ConditionVariableText_R_VariableText ObjetoNegocio)
        {
            ObjetoNegocio.IdEstado = ObjetoNegocio.ObtenerEliminado();
            var parameters = new Parametros();

            parameters.AgregarParametro("IdConditionVariableText_R_VariableText", ObjetoNegocio.Id);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.IdEstado);

            return parameters;
        }

        protected override void Completar(ConditionVariableText_R_VariableText ObjetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            ObjetoPersistido.Id = Convert.ToInt32(dr["IdConditionVariableText_R_VariableText"]);
            ObjetoPersistido.ConditionVariableTextId = Convert.ToInt32(dr["IdConditionVariableText"]);
            ObjetoPersistido.VariableText = DAOLocator.Instance().GetDaoVariableText().Get(Convert.ToInt32(dr["IdVariableText"]));
            ObjetoPersistido.Condition = dr["Condition"].ToString();
            ObjetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            ObjetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
            ObjetoPersistido.DynamicName = dr["DynamicName"].ToString();
        }

        public IList<ConditionVariableText_R_VariableText> FindByConditionVariableTextId(int idConditionVariableText)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdConditionVariableText", idConditionVariableText);

            return Buscar(new Filtro(parameters, "ConditionVariableText_R_VariableText_Tx_Filters"));
        }

        public void DeleteByIdConditionVariableText(int idConditionVariableText)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdConditionVariableText", idConditionVariableText);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            Ejecutar(new Filtro(parameters, "ConditionVariableText_R_VariableText_E_IdConditionVariableText"));
        }

        public void DeleteByIdConditionVariableText(int idConditionVariableText, System.Transactions.TransactionScope ts)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdConditionVariableText", idConditionVariableText);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            Ejecutar(new Filtro(parameters, "ConditionVariableText_R_VariableText_E_IdConditionVariableText"), ts);
        }
    }
}
