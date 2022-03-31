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
    public class DAOSignature_R_SignatureType : DAOObjetoNegocio<Signature_R_SignatureType>, IDAOSignature_R_SignatureType
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override void Completar(Signature_R_SignatureType objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdSignature_R_Type"]);
            objetoPersistido.SignatureType =
                DAOLocator.Instance().GetDaoSignatureType().Get(Convert.ToInt32(dr["IdSignatureType"]));
            objetoPersistido.SignatureId = Convert.ToInt32(dr["IdSignature"]);
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
        }

        protected override Parametros ParametrosCrear(Signature_R_SignatureType objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdSignature", objetoNegocio.SignatureId);
            parameters.AgregarParametro("IdSignatureType", objetoNegocio.SignatureType.Id);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }

        protected override Parametros ParametrosModificar(Signature_R_SignatureType objetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override Parametros ParametrosEliminar(Signature_R_SignatureType objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdSignature_R_SignatureType", objetoNegocio.Id);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            return parameters;
        }

        protected override Parametros ParametrosGrabarLog(Signature_R_SignatureType objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdSignature_R_Type", objetoNegocio.Id);
            parameters.AgregarParametro("IdSignature", objetoNegocio.SignatureId);
            parameters.AgregarParametro("IdSignatureType", objetoNegocio.SignatureType.Id);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }

        public IList<Signature_R_SignatureType> FindBySignatureId(int idSignature)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdSignature", idSignature);

            return Buscar(new Filtro(parameters, "Signature_R_SignatureType_Tx_Filters"));
        }

        public void DeleteByIdSignature(int idSignature)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdSignature", idSignature);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            Ejecutar(new Filtro(parameters, "Signature_R_SignatureType_E_IdSignature"));
        }

        public void DeleteByIdSignature(int idSignature, TransactionScope ts)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdSignature", idSignature);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            Ejecutar(new Filtro(parameters, "Signature_R_SignatureType_E_IdSignature"), ts);
        }
    }
}
