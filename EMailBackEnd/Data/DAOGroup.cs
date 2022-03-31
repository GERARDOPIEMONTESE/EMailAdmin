using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOGroup : DAOObjetoNegocio<Group>, IDAOGroup
    {
        #region IDAOGroup Members

        public IList<Group> FindAll()
        {
            return Buscar(new Filtro(new Parametros(), "dbo.Group_Tx_Filters"));
        }

        public Group Get(int id)
        {
            return Obtener(id);
        }

        public IList<Group> FindByName(string name)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("Name", name);
            return Buscar(new Filtro(parameters, "dbo.Group_Tx_Filters"));
        }

        public IList<Group> FindByGroupType(int idGroupType)
        {
            return FindByGroupType(idGroupType, false);
        }

        public IList<Group> FindByGroupType(int idGroupType, bool lazy)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdGroupType", idGroupType);
            return Buscar(new Filtro(parameters, "dbo.Group_Tx_Filters"), lazy);
        }
        
        
        public bool Exist(string name)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("Name", name);
            return Existe(new Filtro(parameters, "dbo.Group_Tx_Filters"));
        }

        public IList<Group> FindByFilters(string name, int idGroupType)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("Name", name);
            parameters.AgregarParametro("IdGroupType", idGroupType);
            return Buscar(new Filtro(parameters, "dbo.Group_Tx_Filters"));
        }

        public IList<Group> FindByFilters(string name, int idGroupType, bool lazy)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("Name", name);
            parameters.AgregarParametro("IdGroupType", idGroupType);
            return Buscar(new Filtro(parameters, "dbo.Group_Tx_Filters"), lazy);
        }

        public Group FindByFilters(int IdGroup)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdGroup", IdGroup);
            return Obtener(new Filtro(parameters, "dbo.Group_Tx_Filters"), true);
        }

        public bool CanDelete(int id)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdGroup", id);
            var groups = Buscar(new Filtro(parameters, "dbo.Group_Tx_CanDelete"), true);
            return groups.Count <= 0;
        }

        #endregion

        #region Override Methods

        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override void Completar(Group objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdGroup"]);
            objetoPersistido.Name = dr["Name"].ToString();
            objetoPersistido.GroupType = DAOLocator.Instance().GetDaoGroupType().Get(Convert.ToInt32(dr["IdGroupType"]));
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
            try
            {
                objetoPersistido.TotalWeight = Convert.ToInt32(dr["TotalWeight"]);
            }
            catch (Exception) { }
        }

        protected override void CompletarComposicion(Group objetoPersistido)
        {
            objetoPersistido.Conditions = DAOLocator.Instance().GetDaoGroupCondition().Find(objetoPersistido.Id);
        }

        protected override Parametros ParametrosCrear(Group objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("Name", objetoNegocio.Name);
            parameters.AgregarParametro("IdGroupType", objetoNegocio.GroupType.Id);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }

        protected override Parametros ParametrosModificar(Group objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdGroup", objetoNegocio.Id);
            parameters.AgregarParametro("Name", objetoNegocio.Name);
            parameters.AgregarParametro("IdGroupType", objetoNegocio.GroupType.Id);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Modificado());

            return parameters;
        }

        protected override Parametros ParametrosEliminar(Group objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdGroup", objetoNegocio.Id);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            return parameters;
        }

        protected override Parametros ParametrosGrabarLog(Group objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdGroup", objetoNegocio.Id);
            parameters.AgregarParametro("Name", objetoNegocio.Name);
            parameters.AgregarParametro("IdGroupType", objetoNegocio.GroupType != null ? objetoNegocio.GroupType.Id : 0);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }

        protected override void CrearComposicion(ObjetoNegocio objetoNegocio, System.Transactions.TransactionScope ts)
        {
            var group = (Group) objetoNegocio;
            
            DAOLocator.Instance().GetDaoGroupCondition().DeleteAll(group.Id, group.IdUsuario, ts);

            foreach (GroupCondition condition in group.Conditions)
            {
                condition.IdGroup = group.Id;
                condition.IdEstado = objetoNegocio.ObtenerCreado();
                condition.IdUsuario = group.IdUsuario;

                DAOLocator.Instance().GetDaoGroupCondition().Crear(condition, ts);
            }
        }

        protected override void ModificarComposicion(ObjetoNegocio objetoNegocio, System.Transactions.TransactionScope ts)
        {
            CrearComposicion(objetoNegocio, ts);
        }

        protected override void EliminarComposicionPredecesor(ObjetoNegocio ObjetoNegocio, System.Transactions.TransactionScope ts)
        {
            DAOLocator.Instance().GetDaoGroupCondition().DeleteAll(ObjetoNegocio.Id, ObjetoNegocio.IdUsuario, ts);
        }

        protected override void EliminarComposicion(ObjetoNegocio objetoNegocio, System.Transactions.TransactionScope ts)
        {
            
        }

        #endregion

        #region IDAOGroup Members


        public Group Get(int id, bool lazy)
        {
            return Obtener(id, lazy);
        }

        #endregion

    }
}