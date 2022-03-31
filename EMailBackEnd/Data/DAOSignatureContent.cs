using System;
using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Data.Interfaces;
using FrameworkDAC.Parametro;
using CapaNegocioDatos.CapaHome;
using System.Transactions;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOSignatureContent : DAOObjetoNegocio<SignatureContent>, IDAOSignatureContent
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override Parametros ParametrosCrear(SignatureContent objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdSignature", objetoNegocio.IdSignature);
            parameters.AgregarParametro("IdLanguage", objetoNegocio.Language.Id);
            parameters.AgregarParametro("ContentText", objetoNegocio.Content);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", FrameworkDAC.Negocio.ObjetoNegocio.Creado());

            return parameters;
        }

        protected override Parametros ParametrosModificar(SignatureContent objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdSignature", objetoNegocio.IdSignature);
            parameters.AgregarParametro("IdLanguage", objetoNegocio.Language.Id);
            parameters.AgregarParametro("ContentText", objetoNegocio.Content);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);

            return parameters;
        }

        protected override Parametros ParametrosEliminar(SignatureContent objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdSignatureContent", objetoNegocio.Id);
            parameters.AgregarParametro("IdStatus", FrameworkDAC.Negocio.ObjetoNegocio.Eliminado());

            return parameters;
        }

        protected override Parametros ParametrosGrabarLog(SignatureContent objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdSignatureContent", objetoNegocio.Id);
            parameters.AgregarParametro("IdSignature", objetoNegocio.IdSignature);
            parameters.AgregarParametro("IdLanguage", objetoNegocio.Language.Id);
            parameters.AgregarParametro("ContentText", objetoNegocio.Content);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", FrameworkDAC.Negocio.ObjetoNegocio.Creado());

            return parameters;
        }

        protected override void Completar(SignatureContent objetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdSignatureContent"]);
            objetoPersistido.IdSignature = Convert.ToInt32(dr["IdSignature"]);
            objetoPersistido.Language = IdiomaHome.Obtener(Convert.ToInt32(dr["IdLanguage"]));
            objetoPersistido.Content = dr["ContentText"].ToString();
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);            
        }

        #region IDAOSignatureContent Members

        public IList<SignatureContent> GetByIdSignature(int idSignature)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdSignature", idSignature);

            return Buscar(new Filtro(parameters, "dbo.SignatureContent_Tx_Filters"));
        }

        public SignatureContent Get(int id)
        {
            return Obtener(id);
        }

        public void DeleteByIdSignature(int idSignature)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdSignature", idSignature);
            parameters.AgregarParametro("IdStatus", FrameworkDAC.Negocio.ObjetoNegocio.Eliminado());

            Ejecutar(new Filtro(parameters, "dbo.SignatureContent_E_IdSignature"));
        }

        public void DeleteByIdSignature(int idSignature, TransactionScope ts)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdSignature", idSignature);
            parameters.AgregarParametro("IdStatus", FrameworkDAC.Negocio.ObjetoNegocio.Eliminado());

            Ejecutar(new Filtro(parameters, "dbo.SignatureContent_E_IdSignature"), ts);
        }

        #endregion
    }
}
