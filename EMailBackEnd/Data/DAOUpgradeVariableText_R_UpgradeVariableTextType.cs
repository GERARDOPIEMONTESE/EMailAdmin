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
    public class DAOUpgradeVariableText_R_UpgradeVariableTextType : DAOObjetoNegocio<UpgradeVariableText_R_UpgradeVariableTextType>, IDAOUpgradeVariableText_R_UpgradeVariableTextType
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override void Completar(UpgradeVariableText_R_UpgradeVariableTextType objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdUpgradeVariableText_R_UpgradeVariableTextType"]);
            objetoPersistido.UpgradeVariableTextType =
                DAOLocator.Instance().GetDaoUpgradeVariableTextType().Get(Convert.ToInt32(dr["IdUpgradeVariableTextType"]));
            objetoPersistido.UpgradeVariableTextId = Convert.ToInt32(dr["IdUpgradeVariableText"]);
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
        }

        protected override Parametros ParametrosCrear(UpgradeVariableText_R_UpgradeVariableTextType objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdUpgradeVariableText", objetoNegocio.UpgradeVariableTextId);
            parameters.AgregarParametro("IdUpgradeVariableTextType", objetoNegocio.UpgradeVariableTextType.Id);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }

        protected override Parametros ParametrosModificar(UpgradeVariableText_R_UpgradeVariableTextType objetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override Parametros ParametrosEliminar(UpgradeVariableText_R_UpgradeVariableTextType objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdUpgradeVariableText_R_UpgradeVariableTextType", objetoNegocio.Id);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            return parameters;
        }

        protected override Parametros ParametrosGrabarLog(UpgradeVariableText_R_UpgradeVariableTextType objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdUpgradeVariableText_R_UpgradeVariableTextType", objetoNegocio.Id);
            parameters.AgregarParametro("IdUpgradeVariableText", objetoNegocio.UpgradeVariableTextId);
            parameters.AgregarParametro("IdUpgradeVariableTextType", objetoNegocio.UpgradeVariableTextType.Id);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }

        public IList<UpgradeVariableText_R_UpgradeVariableTextType> FindByUpgradeVariableTextId(int idUpgradeVariableText)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdUpgradeVariableText", idUpgradeVariableText);

            return Buscar(new Filtro(parameters, "UpgradeVariableText_R_UpgradeVariableTextType_Tx_Filters"));
        }

        public void DeleteByIdUpgradeVariableText(int idUpgradeVariableText)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdUpgradeVariableText", idUpgradeVariableText);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            Ejecutar(new Filtro(parameters, "UpgradeVariableText_R_UpgradeVariableTextType_E_IdUpgradeVariableText"));
        }

        public void DeleteByIdUpgradeVariableText(int idUpgradeVariableText, TransactionScope ts)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdUpgradeVariableText", idUpgradeVariableText);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            Ejecutar(new Filtro(parameters, "UpgradeVariableText_R_UpgradeVariableTextType_E_IdUpgradeVariableText"), ts);
        }
    }
}
