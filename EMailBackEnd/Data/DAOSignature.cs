using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Transactions;
using CapaNegocioDatos.CapaNegocio;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOSignature : DAOObjetoNegocio<Signature>, IDAOSignature
    {
        #region IDAOSignature Members

        public IList<Signature> FindAll()
        {
            return Buscar(new Filtro(new Parametros(), "dbo.Signature_Tx_Filters"), true);
        }

        public IList<Signature> FindByFilters(int idType, int idCountry, string name)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdCountry", idCountry);
            parameters.AgregarParametro("IdSignatureType", idType);
            parameters.AgregarParametro("Name", name);

            return Buscar(new Filtro(parameters, "dbo.Signature_Tx_SignatureType_Country"));
        }

        public IList<Signature> FindByFilters(int idType, int idCountry)
        {
            return FindByFilters(idType, idCountry, "");
        }

        public IList<Signature> FindByName(string name)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("Name", name);

            return Buscar(new Filtro(parameters, "dbo.Signature_Tx_Filters"));
        }

        public Signature Get(int id)
        {
            return Obtener(id);
        }

        #endregion

        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override Parametros ParametrosCrear(Signature objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("Name", objetoNegocio.Name);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }

        protected override Parametros ParametrosModificar(Signature objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdSignature", objetoNegocio.Id);
            parameters.AgregarParametro("Name", objetoNegocio.Name);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);

            return parameters;
        }

        protected override Parametros ParametrosEliminar(Signature objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdSignature", objetoNegocio.Id);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);

            return parameters;
        }

        protected override Parametros ParametrosGrabarLog(Signature objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdSignature", objetoNegocio.Id);
            parameters.AgregarParametro("Name", objetoNegocio.Name);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }

        protected override void Completar(Signature objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdSignature"]);
            objetoPersistido.Name = dr["Name"].ToString();
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
        }

        protected override void CompletarComposicion(Signature objetoPersistido)
        {
            //CONTENT
            objetoPersistido.Content =
                DAOLocator.Instance().GetDaoSignatureContent().GetByIdSignature(objetoPersistido.Id);
            //COUNTRIES
            if (objetoPersistido.Countries == null)
            {
                objetoPersistido.Countries = new List<Locacion>();
            }
            foreach (
                Signature_R_Country signatureRCountry in
                    DAOLocator.Instance().GetDaoSignature_R_Country().FindBySignatureId(objetoPersistido.Id))
            {
                objetoPersistido.Countries.Add(signatureRCountry.Country);
            }
            //TYPES
            if (objetoPersistido.SignatureTypes == null)
            {
                objetoPersistido.SignatureTypes = new List<SignatureType>();
            }
            foreach (
                Signature_R_SignatureType signatureRSignatureType in
                    DAOLocator.Instance().GetDaoSignature_R_SignatureType().FindBySignatureId(objetoPersistido.Id))
            {
                objetoPersistido.SignatureTypes.Add(signatureRSignatureType.SignatureType);
            }
        }

        protected override void CrearComposicion(ObjetoNegocio objetoNegocio, TransactionScope ts)
        {
            var signature = (Signature) objetoNegocio;

            foreach (SignatureType signatureType in signature.SignatureTypes)
            {
                var signatureTypeRelationship = new Signature_R_SignatureType
                                                    {
                                                        SignatureType = signatureType,
                                                        SignatureId = signature.Id,
                                                        IdUsuario = signature.IdUsuario
                                                    };
                DAOLocator.Instance().GetDaoSignature_R_SignatureType().Crear(signatureTypeRelationship);
            }

            foreach (Locacion country in signature.Countries)
            {
                var signatureCountryRelationship = new Signature_R_Country
                                                       {
                                                           Country = country,
                                                           SignatureId = signature.Id,
                                                           IdUsuario = signature.IdUsuario
                                                       };
                DAOLocator.Instance().GetDaoSignature_R_Country().Crear(signatureCountryRelationship);
            }

            foreach (SignatureContent content in signature.Content)
            {
                var signatureContent = new SignatureContent
                                           {
                                               IdSignature = signature.Id,
                                               Content = content.Content,
                                               Language = content.Language,
                                               IdUsuario = signature.IdUsuario
                                           };
                DAOLocator.Instance().GetDaoSignatureContent().Crear(signatureContent);
            }
        }

        protected override void ModificarComposicion(ObjetoNegocio objetoNegocio, TransactionScope ts)
        {
            var signature = (Signature) objetoNegocio;

            DAOLocator.Instance().GetDaoSignature_R_SignatureType().DeleteByIdSignature(objetoNegocio.Id);
            foreach (SignatureType signatureType in signature.SignatureTypes)
            {
                var signatureTypeRelationship = new Signature_R_SignatureType
                                                    {
                                                        SignatureType = signatureType,
                                                        SignatureId = signature.Id,
                                                        IdUsuario = signature.IdUsuario
                                                    };
                DAOLocator.Instance().GetDaoSignature_R_SignatureType().Crear(signatureTypeRelationship);
            }

            DAOLocator.Instance().GetDaoSignature_R_Country().DeleteByIdSignature(objetoNegocio.Id);
            foreach (Locacion country in signature.Countries)
            {
                var signatureCountryRelationship = new Signature_R_Country
                                                       {
                                                           Country = country,
                                                           SignatureId = signature.Id,
                                                           IdUsuario = signature.IdUsuario
                                                       };
                DAOLocator.Instance().GetDaoSignature_R_Country().Crear(signatureCountryRelationship);
            }

            foreach (SignatureContent content in signature.Content)
            {
                var signatureContent = new SignatureContent
                                           {
                                               IdSignature = signature.Id,
                                               Content = content.Content,
                                               Language = content.Language,
                                               IdUsuario = signature.IdUsuario
                                           };
                DAOLocator.Instance().GetDaoSignatureContent().Modificar(signatureContent);
            }
        }

        protected override void EliminarComposicion(ObjetoNegocio objetoNegocio, TransactionScope ts)
        {
            DAOLocator.Instance().GetDaoSignatureContent().DeleteByIdSignature(objetoNegocio.Id, ts);
            DAOLocator.Instance().GetDaoSignature_R_Country().DeleteByIdSignature(objetoNegocio.Id, ts);
            DAOLocator.Instance().GetDaoSignature_R_SignatureType().DeleteByIdSignature(objetoNegocio.Id, ts);
        }

    }
}