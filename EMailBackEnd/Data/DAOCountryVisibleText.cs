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
    public class DAOCountryVisibleText : DAOObjetoNegocio<CountryVisibleText>, IDAOCountryVisibleText
    {
        #region IDAOCountryVisibleText Members

        public IList<CountryVisibleText> FindAll()
        {
            return Buscar(new Filtro(new Parametros(), "dbo.CountryVisibleText_Tx_Filters"), true);
        }

        public IList<CountryVisibleText> FindByFilters(int idType, int idCountry, string name)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdCountry", idCountry);
            parameters.AgregarParametro("IdCountryVisibleTextType", idType);
            parameters.AgregarParametro("Name", name);

            return Buscar(new Filtro(parameters, "dbo.CountryVisibleText_Tx_CountryVisibleTextType_Country"));
        }

        public IList<CountryVisibleText> FindByFilters(int idType, int idCountry)
        {
            return FindByFilters(idType, idCountry, "");
        }

        public IList<CountryVisibleText> FindByName(string name)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("Name", name);

            return Buscar(new Filtro(parameters, "dbo.CountryVisibleText_Tx_Filters"));
        }

        public CountryVisibleText Get(int id)
        {
            return Obtener(id);
        }

        #endregion

        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override Parametros ParametrosCrear(CountryVisibleText objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("Name", objetoNegocio.Name);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }

        protected override Parametros ParametrosModificar(CountryVisibleText objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdCountryVisibleText", objetoNegocio.Id);
            parameters.AgregarParametro("Name", objetoNegocio.Name);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);

            return parameters;
        }

        protected override Parametros ParametrosEliminar(CountryVisibleText objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdCountryVisibleText", objetoNegocio.Id);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);

            return parameters;
        }

        protected override Parametros ParametrosGrabarLog(CountryVisibleText objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdCountryVisibleText", objetoNegocio.Id);
            parameters.AgregarParametro("Name", objetoNegocio.Name);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }

        protected override void Completar(CountryVisibleText objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdCountryVisibleText"]);
            objetoPersistido.Name = dr["Name"].ToString();
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
        }

        protected override void CompletarComposicion(CountryVisibleText objetoPersistido)
        {
            //CONTENT
            objetoPersistido.Content =
                DAOLocator.Instance().GetDaoCountryVisibleTextContent().GetByIdCountryVisibleText(objetoPersistido.Id);
            //COUNTRIES
            if (objetoPersistido.Countries == null)
            {
                objetoPersistido.Countries = new List<Locacion>();
            }
            foreach (
                CountryVisibleText_R_Country CountryVisibleTextRCountry in
                    DAOLocator.Instance().GetDaoCountryVisibleText_R_Country().FindByCountryVisibleTextId(objetoPersistido.Id))
            {
                objetoPersistido.Countries.Add(CountryVisibleTextRCountry.Country);
            }
            //TYPES
            if (objetoPersistido.CountryVisibleTextTypes == null)
            {
                objetoPersistido.CountryVisibleTextTypes = new List<CountryVisibleTextType>();
            }
            foreach (
                CountryVisibleText_R_CountryVisibleTextType CountryVisibleTextRCountryVisibleTextType in
                    DAOLocator.Instance().GetDaoCountryVisibleText_R_CountryVisibleTextType().FindByCountryVisibleTextId(objetoPersistido.Id))
            {
                objetoPersistido.CountryVisibleTextTypes.Add(CountryVisibleTextRCountryVisibleTextType.CountryVisibleTextType);
            }
        }

        protected override void CrearComposicion(ObjetoNegocio objetoNegocio, TransactionScope ts)
        {
            var CountryVisibleText = (CountryVisibleText) objetoNegocio;

            foreach (CountryVisibleTextType CountryVisibleTextType in CountryVisibleText.CountryVisibleTextTypes)
            {
                var CountryVisibleTextTypeRelationship = new CountryVisibleText_R_CountryVisibleTextType
                                                    {
                                                        CountryVisibleTextType = CountryVisibleTextType,
                                                        CountryVisibleTextId = CountryVisibleText.Id,
                                                        IdUsuario = CountryVisibleText.IdUsuario
                                                    };
                DAOLocator.Instance().GetDaoCountryVisibleText_R_CountryVisibleTextType().Crear(CountryVisibleTextTypeRelationship);
            }

            foreach (Locacion country in CountryVisibleText.Countries)
            {
                var CountryVisibleTextCountryRelationship = new CountryVisibleText_R_Country
                                                       {
                                                           Country = country,
                                                           CountryVisibleTextId = CountryVisibleText.Id,
                                                           IdUsuario = CountryVisibleText.IdUsuario
                                                       };
                DAOLocator.Instance().GetDaoCountryVisibleText_R_Country().Crear(CountryVisibleTextCountryRelationship);
            }

            foreach (CountryVisibleTextContent content in CountryVisibleText.Content)
            {
                var CountryVisibleTextContent = new CountryVisibleTextContent
                                           {
                                               IdCountryVisibleText = CountryVisibleText.Id,
                                               Content = content.Content,
                                               Language = content.Language,
                                               IdUsuario = CountryVisibleText.IdUsuario
                                           };
                DAOLocator.Instance().GetDaoCountryVisibleTextContent().Crear(CountryVisibleTextContent);
            }
        }

        protected override void ModificarComposicion(ObjetoNegocio objetoNegocio, TransactionScope ts)
        {
            var CountryVisibleText = (CountryVisibleText) objetoNegocio;

            DAOLocator.Instance().GetDaoCountryVisibleText_R_CountryVisibleTextType().DeleteByIdCountryVisibleText(objetoNegocio.Id);
            foreach (CountryVisibleTextType CountryVisibleTextType in CountryVisibleText.CountryVisibleTextTypes)
            {
                var CountryVisibleTextTypeRelationship = new CountryVisibleText_R_CountryVisibleTextType
                                                    {
                                                        CountryVisibleTextType = CountryVisibleTextType,
                                                        CountryVisibleTextId = CountryVisibleText.Id,
                                                        IdUsuario = CountryVisibleText.IdUsuario
                                                    };
                DAOLocator.Instance().GetDaoCountryVisibleText_R_CountryVisibleTextType().Crear(CountryVisibleTextTypeRelationship);
            }

            DAOLocator.Instance().GetDaoCountryVisibleText_R_Country().DeleteByIdCountryVisibleText(objetoNegocio.Id);
            foreach (Locacion country in CountryVisibleText.Countries)
            {
                var CountryVisibleTextCountryRelationship = new CountryVisibleText_R_Country
                                                       {
                                                           Country = country,
                                                           CountryVisibleTextId = CountryVisibleText.Id,
                                                           IdUsuario = CountryVisibleText.IdUsuario
                                                       };
                DAOLocator.Instance().GetDaoCountryVisibleText_R_Country().Crear(CountryVisibleTextCountryRelationship);
            }

            foreach (CountryVisibleTextContent content in CountryVisibleText.Content)
            {
                var CountryVisibleTextContent = new CountryVisibleTextContent
                                           {
                                               IdCountryVisibleText = CountryVisibleText.Id,
                                               Content = content.Content,
                                               Language = content.Language,
                                               IdUsuario = CountryVisibleText.IdUsuario
                                           };
                DAOLocator.Instance().GetDaoCountryVisibleTextContent().Modificar(CountryVisibleTextContent);
            }
        }

        protected override void EliminarComposicion(ObjetoNegocio objetoNegocio, TransactionScope ts)
        {
            DAOLocator.Instance().GetDaoCountryVisibleTextContent().DeleteByIdCountryVisibleText(objetoNegocio.Id, ts);
            DAOLocator.Instance().GetDaoCountryVisibleText_R_Country().DeleteByIdCountryVisibleText(objetoNegocio.Id, ts);
            DAOLocator.Instance().GetDaoCountryVisibleText_R_CountryVisibleTextType().DeleteByIdCountryVisibleText(objetoNegocio.Id, ts);
        }

    }
}