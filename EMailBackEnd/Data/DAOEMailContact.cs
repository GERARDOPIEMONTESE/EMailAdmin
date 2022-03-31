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
    public class DAOEMailContact : DAOObjetoNegocio<EMailContact>, IDAOEMailContact
    {
        #region Parameters

        protected override Parametros ParametrosCrear(EMailContact objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("Name", objetoNegocio.Name);
            parameters.AgregarParametro("IdEMailContactType", objetoNegocio.EMailContactType.Id);
            parameters.AgregarParametro("EMail", objetoNegocio.EMail);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override Parametros ParametrosModificar(EMailContact objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdEMailContact", objetoNegocio.Id);
            parameters.AgregarParametro("Name", objetoNegocio.Name);
            parameters.AgregarParametro("IdEMailContactType", objetoNegocio.EMailContactType.Id);
            parameters.AgregarParametro("EMail", objetoNegocio.EMail);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override Parametros ParametrosEliminar(EMailContact objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdEMailContact", objetoNegocio.Id);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override Parametros ParametrosGrabarLog(EMailContact objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdEMailContact", objetoNegocio.Id);
            parameters.AgregarParametro("Name", objetoNegocio.Name);
            parameters.AgregarParametro("IdEMailContactType", objetoNegocio.EMailContactType.Id);
            parameters.AgregarParametro("EMail", objetoNegocio.EMail);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override void Completar(EMailContact objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdEMailContact"]);
            objetoPersistido.Name = dr["Name"].ToString();
            objetoPersistido.EMailContactType = DAOLocator.Instance().GetDaoEMailContactType().Get(
                Convert.ToInt32(dr["IdEMailContactType"]));
            objetoPersistido.EMail = dr["EMail"].ToString();
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
        }

        #endregion

        #region Composition

        protected override void CrearComposicion(ObjetoNegocio objetoNegocio,
                                                 TransactionScope ts)
        {
            var contact = (EMailContact) objetoNegocio;

            DAOLocator.Instance().GetDaoEMailContactContent().DeleteAll(contact.Id, ts);
            foreach (EMailContactContent content in contact.Content)
            {
                content.IdEMailContact = contact.Id;
                content.IdUsuario = contact.IdUsuario;
                content.IdEstado = ObjetoNegocio.Creado();
                DAOLocator.Instance().GetDaoEMailContactContent().Persistir(content, ts);
            }

            DAOLocator.Instance().GetDaoEMailContactLocation().DeleteAll(contact.Id, ts);
            foreach (EMailContactLocation location in contact.Location)
            {
                location.IdEMailContact = contact.Id;
                location.IdUsuario = contact.IdUsuario;
                location.IdEstado = ObjetoNegocio.Creado();
                DAOLocator.Instance().GetDaoEMailContactLocation().Persistir(location, ts);
            }
        }

        protected override void EliminarComposicion(ObjetoNegocio objetoNegocio, TransactionScope ts)
        {
            var contact = (EMailContact) objetoNegocio;

            DAOLocator.Instance().GetDaoEMailContactContent().DeleteAll(contact.Id, ts);
            DAOLocator.Instance().GetDaoEMailContactLocation().DeleteAll(contact.Id, ts);
        }

        protected override void ModificarComposicion(ObjetoNegocio objetoNegocio,
                                                     TransactionScope ts)
        {
            CrearComposicion(objetoNegocio, ts);
        }

        protected override void CompletarComposicion(EMailContact objetoPersistido)
        {
            objetoPersistido.Content = DAOLocator.Instance().GetDaoEMailContactContent().FindByIdEMailContact(
                objetoPersistido.Id);
            objetoPersistido.Location = DAOLocator.Instance().GetDaoEMailContactLocation().FindByIdEMailContact(
                objetoPersistido.Id);
        }

        #endregion

        #region IDAOEMailContact Members

        public IList<EMailContact> FindAll()
        {
            return Buscar(new Filtro(new Parametros(), "dbo.EMailContact_Tx_Name"), true);
        }

        public IList<EMailContact> Find(string name)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("Name", name);

            return Buscar(new Filtro(parameters, "dbo.EMailContact_Tx_Name"), true);
        }

        public IList<EMailContact> Find(string name, int idLocation)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("Name", name);
            parameters.AgregarParametro("IdLocation", idLocation);

            return Buscar(new Filtro(parameters, "dbo.EMailContact_Tx_Name_IdLocation"));
        }

        public IList<EMailContact> Find(string name, int idLocation, int idEMailContactType)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("Name", name);
            parameters.AgregarParametro("IdLocation", idLocation);
            parameters.AgregarParametro("IdEMailContactType", idEMailContactType);

            return Buscar(new Filtro(parameters, "dbo.EMailContact_Tx_Name_IdLocation"));
        }

        public EMailContact Get(int id)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdEmailContact", id);

            return Obtener(new Filtro(parameters, "dbo.EMailContact_Tx_IdEMailContact"));
        }

        public IList<EMailContact> Find(int idEMailContactType, int idLocation)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdEMailContactType", idEMailContactType);
            parameters.AgregarParametro("IdLocation", idLocation);

            return Buscar(new Filtro(parameters, "dbo.EMailContact_Tx_IdEMailContactType_IdLocation"));
        }

        #endregion

        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }
    }
}