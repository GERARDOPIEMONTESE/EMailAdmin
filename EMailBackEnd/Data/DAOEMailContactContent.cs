using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Transactions;
using CapaNegocioDatos.CapaDatos;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOEMailContactContent : DAOObjetoNegocio<EMailContactContent>, IDAOEMailContactContent
    {
        #region IDAOEMailContactContent Members

        public void DeleteAll(int idEMailContact, TransactionScope ts)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdEMailContact", idEMailContact);

            Ejecutar(new Filtro(parameters, "dbo.EMailContactContent_E_All"), ts);
        }

        public IList<EMailContactContent> FindAll()
        {
            return Buscar(new Filtro(new Parametros(), "dbo.EMailContactContent_tx_IdEMailContact"));
        }

        public IList<EMailContactContent> FindByIdEMailContact(int idEMailContact)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdEMailContact", idEMailContact);

            return Buscar(new Filtro(parameters, "dbo.EMailContactContent_tx_IdEMailContact"));
        }

        public EMailContactContent Get(int id)
        {
            return Obtener(id);
        }

        #endregion

        protected override Parametros ParametrosCrear(EMailContactContent objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdEMailContact", objetoNegocio.IdEMailContact);
            parameters.AgregarParametro("IdLanguage", objetoNegocio.Language.Id);
            parameters.AgregarParametro("ContentText", objetoNegocio.ContentText);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override Parametros ParametrosModificar(EMailContactContent objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdEMailContactContent", objetoNegocio.Id);
            parameters.AgregarParametro("IdEMailContact", objetoNegocio.IdEMailContact);
            parameters.AgregarParametro("IdLanguage", objetoNegocio.Language.Id);
            parameters.AgregarParametro("ContentText", objetoNegocio.ContentText);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);

            return parameters;
        }

        protected override Parametros ParametrosEliminar(EMailContactContent objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdEMailContactContent", objetoNegocio.Id);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override Parametros ParametrosGrabarLog(EMailContactContent objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdEMailContactContent", objetoNegocio.Id);
            parameters.AgregarParametro("IdEMailContact", objetoNegocio.IdEMailContact);
            parameters.AgregarParametro("IdLanguage", objetoNegocio.Language.Id);
            parameters.AgregarParametro("ContentText", objetoNegocio.ContentText);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override void Completar(EMailContactContent objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdEMailContactContent"]);
            objetoPersistido.IdEMailContact = Convert.ToInt32(dr["IdEMailContact"]);
            objetoPersistido.Language = DAOIdioma.Instancia().Obtener(Convert.ToInt32(dr["IdLanguage"]));
            objetoPersistido.ContentText = dr["ContentText"].ToString();
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
        }

        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }
    }
}