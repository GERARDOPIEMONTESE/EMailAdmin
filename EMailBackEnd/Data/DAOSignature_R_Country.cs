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
    public class DAOSignature_R_Country : DAOObjetoNegocio<Signature_R_Country>, IDAOSignature_R_Country
    {
        #region IDAOSignature_R_Country Members

        public IList<Signature_R_Country> FindBySignatureId(int idSignature)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdSignature", idSignature);

            return Buscar(new Filtro(parameters, "Signature_R_Country_Tx_Filters"));
        }

        public void DeleteByIdSignature(int idSignature)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdSignature", idSignature);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            Ejecutar(new Filtro(parameters, "Signature_R_Country_E_IdSignature"));
        }

        public void DeleteByIdSignature(int idSignature, TransactionScope ts)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdSignature", idSignature);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            Ejecutar(new Filtro(parameters, "Signature_R_Country_E_IdSignature"), ts);
        }

        #endregion

        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override void Completar(Signature_R_Country objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdSignature_R_Country"]);
            objetoPersistido.Country = DAOLocacion.Instancia().Obtener(Convert.ToInt32(dr["IdCountry"].ToString()));
            objetoPersistido.SignatureId = Convert.ToInt32(dr["IdSignature"]);
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
        }

        protected override Parametros ParametrosCrear(Signature_R_Country objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdSignature", objetoNegocio.SignatureId);
            parameters.AgregarParametro("IdCountry", objetoNegocio.Country.Id);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }

        protected override Parametros ParametrosModificar(Signature_R_Country objetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override Parametros ParametrosEliminar(Signature_R_Country objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdSignature_R_Country", objetoNegocio.Id);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            return parameters;
        }

        protected override Parametros ParametrosGrabarLog(Signature_R_Country objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdSignature_R_Country", objetoNegocio.Id);
            parameters.AgregarParametro("IdSignature", objetoNegocio.SignatureId);
            parameters.AgregarParametro("IdCountry", objetoNegocio.Country.Id);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }
    }
}