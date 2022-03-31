using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Transactions;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using FrameworkDAC.Parametro;
using System.Linq;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOGroupCondition : DAOObjetoNegocio<GroupCondition>, IDAOGroupCondition
    {
        private bool _completo;
        
        private IList<ConditionType> _listConditionsTypes;
        private IList<ConditionType> listConditionsTypes
        {
            get { return _listConditionsTypes ?? (_listConditionsTypes = DAOLocator.Instance().GetDaoConditionType().FindAll()); }
            set { _listConditionsTypes = value; }
        }

        #region IDAOGroupCondition Members

        public GroupCondition Get(int id)
        {
            _completo = false;
            return Obtener(id);
        }

        public IList<GroupCondition> Find(int idGroup)
        {
            return Find(idGroup, false);
        }

        public IList<GroupCondition> Find(int idGroup, bool complete)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdGroup", idGroup);
            var query = "dbo.GroupCondition_Tx_IdGroup";
            if(complete)
            {
                query = "dbo.GroupCondition_Tx_IdGroupComplete";
            }
            _completo = complete;
            return Buscar(new Filtro(parameters, query));
        }

        public IList<GroupCondition> FindWithValues(int idGroup)
        {
            return FindWithValues(idGroup, false);
        }

        public IList<GroupCondition> FindWithValues(int idGroup, bool complete)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdGroup", idGroup);
            var query = "dbo.GroupCondition_Tx_IdGroupValuesComplete";
            _completo = true;
            return Buscar(new Filtro(parameters, query));
        }

        public void DeleteAll(int idGroup, int idUser, TransactionScope ts)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdGroup", idGroup);
            parameters.AgregarParametro("IdUser", idUser);

            Ejecutar(new Filtro(parameters, "dbo.GroupCondition_E_All"), ts);
        }

        #endregion

        protected override void Completar(GroupCondition objetoPersistido, SqlDataReader dr)
        {
            var fieldNames = Enumerable.Range(0, dr.FieldCount).Select(i => dr.GetName(i)).ToArray();

            objetoPersistido.Id = Convert.ToInt32(dr["IdGroupCondition"]);
            objetoPersistido.IdGroup = Convert.ToInt32(dr["IdGroup"]);
            objetoPersistido.ConditionType = listConditionsTypes.First(p => p.Id == int.Parse(dr["IdConditionType"].ToString()));
            objetoPersistido.Value = dr["Value"].ToString();
            
            if (fieldNames.Contains("VisibleValue"))
                objetoPersistido.VisibleValue = dr["VisibleValue"].ToString();

            if (fieldNames.Contains("VisibleCode"))
                objetoPersistido.VisibleCode = dr["VisibleCode"].ToString();

            if (fieldNames.Contains("DynamicKey"))
                objetoPersistido.DynamicKey = dr["DynamicKey"].ToString();
            else
            {
                if (objetoPersistido.ConditionType.Code == ConditionType.DynamicValueCode && fieldNames.Contains("VisibleCode"))
                    objetoPersistido.DynamicKey = dr["VisibleCode"].ToString();
            }

            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
        }

        protected override Parametros ParametrosGrabarLog(GroupCondition objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdGroupCondition", objetoNegocio.Id);
            parameters.AgregarParametro("IdGroup", objetoNegocio.IdGroup);
            parameters.AgregarParametro("IdConditionType", objetoNegocio.ConditionType.Id);
            parameters.AgregarParametro("Value", objetoNegocio.Value);
            parameters.AgregarParametro("DynamicKey", objetoNegocio.DynamicKey);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override Parametros ParametrosCrear(GroupCondition objetoNegocio)
        {
            objetoNegocio.IdEstado = objetoNegocio.ObtenerCreado();
            var parameters = new Parametros();

            parameters.AgregarParametro("IdGroup", objetoNegocio.IdGroup);
            parameters.AgregarParametro("IdConditionType", objetoNegocio.ConditionType.Id);
            parameters.AgregarParametro("Value", objetoNegocio.Value);
            parameters.AgregarParametro("DynamicKey", objetoNegocio.DynamicKey);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override Parametros ParametrosModificar(GroupCondition objetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override Parametros ParametrosEliminar(GroupCondition objetoNegocio)
        {
            objetoNegocio.IdEstado = objetoNegocio.ObtenerEliminado();

            var parameters = new Parametros();

            parameters.AgregarParametro("IdGroupCondition", objetoNegocio.Id);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }
    }
}