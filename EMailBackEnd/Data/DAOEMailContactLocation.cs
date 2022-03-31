using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Transactions;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOEMailContactLocation : DAOObjetoNegocio<EMailContactLocation>, IDAOEMailContactLocation
    {
        #region IDAOEMailContactLocation Members

        public void DeleteAll(int idEMailContact, TransactionScope ts)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdEMailContact", idEMailContact);

            Ejecutar(new Filtro(parameters, "dbo.EMailContact_R_Location_E_All"), ts);
        }

        public IList<EMailContactLocation> FindAll()
        {
            return Buscar(new Filtro(new Parametros(), "dbo.EMailContact_R_Location_Tx_IdEMailContact"), true);
        }

        public IList<EMailContactLocation> FindByIdEMailContact(int idEMailContact)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdEMailContact", idEMailContact);

            return Buscar(new Filtro(parameters, "dbo.EMailContact_R_Location_Tx_IdEMailContact"));
        }

        #endregion

        protected override Parametros ParametrosCrear(EMailContactLocation objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdEMailContact", objetoNegocio.IdEMailContact);
            parameters.AgregarParametro("IdLocation", objetoNegocio.IdLocation);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override Parametros ParametrosEliminar(EMailContactLocation objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdEMailContact_R_Location", objetoNegocio.Id);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override Parametros ParametrosModificar(EMailContactLocation objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdEMailContact_R_Location", objetoNegocio.Id);
            parameters.AgregarParametro("IdEMailContact", objetoNegocio.IdEMailContact);
            parameters.AgregarParametro("IdLocation", objetoNegocio.IdLocation);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override Parametros ParametrosGrabarLog(EMailContactLocation objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdEMailContact_R_Location", objetoNegocio.Id);
            parameters.AgregarParametro("IdEMailContact", objetoNegocio.IdEMailContact);
            parameters.AgregarParametro("IdLocation", objetoNegocio.IdLocation);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override void Completar(EMailContactLocation objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdEMailContact_R_Location"]);
            objetoPersistido.IdEMailContact = Convert.ToInt32(dr["IdEMailContact"]);
            objetoPersistido.IdLocation = Convert.ToInt32(dr["IdLocation"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
            try
            {    
                objetoPersistido.Name = dr["Name"].ToString();
            }catch{}
        }

        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }
    }
}