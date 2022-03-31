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
    public class DAOUpgradeVariableText : DAOObjetoNegocio<UpgradeVariableText>, IDAOUpgradeVariableText
    {
        #region IDAOUpgradeVariableText Members

        public IList<UpgradeVariableText> FindAll()
        {
            return Buscar(new Filtro(new Parametros(), "dbo.UpgradeVariableText_Tx_Filters"), true);
        }

        public IList<UpgradeVariableText> FindByFilters(int idType, int idProduct, string name)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdProduct", idProduct);
            parameters.AgregarParametro("IdUpgradeVariableTextType", idType);
            parameters.AgregarParametro("Name", name);

            return Buscar(new Filtro(parameters, "dbo.UpgradeVariableText_Tx_UpgradeVariableTextType_Product"));
        }

        public IList<UpgradeVariableText> FindByFilters(int idType, int idProduct)
        {
            return FindByFilters(idType, idProduct, "");
        }

        public IList<UpgradeVariableText> FindByName(string name)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("Name", name);

            return Buscar(new Filtro(parameters, "dbo.UpgradeVariableText_Tx_Filters"));
        }

        public UpgradeVariableText Get(int id)
        {
            return Obtener(id);
        }

        #endregion

        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override Parametros ParametrosCrear(UpgradeVariableText objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("Name", objetoNegocio.Name);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }

        protected override Parametros ParametrosModificar(UpgradeVariableText objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdUpgradeVariableText", objetoNegocio.Id);
            parameters.AgregarParametro("Name", objetoNegocio.Name);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);

            return parameters;
        }

        protected override Parametros ParametrosEliminar(UpgradeVariableText objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdUpgradeVariableText", objetoNegocio.Id);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);

            return parameters;
        }

        protected override Parametros ParametrosGrabarLog(UpgradeVariableText objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdUpgradeVariableText", objetoNegocio.Id);
            parameters.AgregarParametro("Name", objetoNegocio.Name);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }

        protected override void Completar(UpgradeVariableText objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdUpgradeVariableText"]);
            objetoPersistido.Name = dr["Name"].ToString();
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
        }

        protected override void CompletarComposicion(UpgradeVariableText objetoPersistido)
        {
            //CONTENT
            objetoPersistido.Content =
                DAOLocator.Instance().GetDaoUpgradeVariableTextContent().GetByIdUpgradeVariableText(objetoPersistido.Id);
            //COUNTRIES
            if (objetoPersistido.Upgrades == null)
            {
                objetoPersistido.Upgrades = new List<Product>();
            }
            foreach (
                UpgradeVariableText_R_Upgrade upgradeVariableTextRUpgrade in
                    DAOLocator.Instance().GetDaoUpgradeVariableText_R_Upgrade().FindByUpgradeVariableTextId(objetoPersistido.Id))
            {
                objetoPersistido.Upgrades.Add(upgradeVariableTextRUpgrade.Upgrade);
            }
            //TYPES
            if (objetoPersistido.UpgradeVariableTextTypes == null)
            {
                objetoPersistido.UpgradeVariableTextTypes = new List<UpgradeVariableTextType>();
            }
            foreach (
                UpgradeVariableText_R_UpgradeVariableTextType upgradeVariableTextRUpgradeVariableTextType in
                    DAOLocator.Instance().GetDaoUpgradeVariableText_R_UpgradeVariableTextType().FindByUpgradeVariableTextId(objetoPersistido.Id))
            {
                objetoPersistido.UpgradeVariableTextTypes.Add(upgradeVariableTextRUpgradeVariableTextType.UpgradeVariableTextType);
            }
        }

        protected override void CrearComposicion(ObjetoNegocio objetoNegocio, TransactionScope ts)
        {
            var upgradeVariableText = (UpgradeVariableText) objetoNegocio;

            foreach (UpgradeVariableTextType upgradeVariableTextType in upgradeVariableText.UpgradeVariableTextTypes)
            {
                var upgradeVariableTextTypeRelationship = new UpgradeVariableText_R_UpgradeVariableTextType
                                                    {
                                                        UpgradeVariableTextType = upgradeVariableTextType,
                                                        UpgradeVariableTextId = upgradeVariableText.Id,
                                                        IdUsuario = upgradeVariableText.IdUsuario
                                                    };
                DAOLocator.Instance().GetDaoUpgradeVariableText_R_UpgradeVariableTextType().Crear(upgradeVariableTextTypeRelationship);
            }

            foreach (Product upgrade in upgradeVariableText.Upgrades)
            {
                var upgradeVariableTextCountryRelationship = new UpgradeVariableText_R_Upgrade
                                                       {
                                                           Upgrade = upgrade,
                                                           UpgradeVariableTextId = upgradeVariableText.Id,
                                                           IdUsuario = upgradeVariableText.IdUsuario
                                                       };
                DAOLocator.Instance().GetDaoUpgradeVariableText_R_Upgrade().Crear(upgradeVariableTextCountryRelationship);
            }

            foreach (UpgradeVariableTextContent content in upgradeVariableText.Content)
            {
                var upgradeVariableTextContent = new UpgradeVariableTextContent
                                           {
                                               IdUpgradeVariableText = upgradeVariableText.Id,
                                               Content = content.Content,
                                               Language = content.Language,
                                               IdUsuario = upgradeVariableText.IdUsuario
                                           };
                DAOLocator.Instance().GetDaoUpgradeVariableTextContent().Crear(upgradeVariableTextContent);
            }
        }

        protected override void ModificarComposicion(ObjetoNegocio objetoNegocio, TransactionScope ts)
        {
            var upgradeVariableText = (UpgradeVariableText) objetoNegocio;

            DAOLocator.Instance().GetDaoUpgradeVariableText_R_UpgradeVariableTextType().DeleteByIdUpgradeVariableText(objetoNegocio.Id);
            foreach (UpgradeVariableTextType upgradeVariableTextType in upgradeVariableText.UpgradeVariableTextTypes)
            {
                var upgradeVariableTextTypeRelationship = new UpgradeVariableText_R_UpgradeVariableTextType
                                                    {
                                                        UpgradeVariableTextType = upgradeVariableTextType,
                                                        UpgradeVariableTextId = upgradeVariableText.Id,
                                                        IdUsuario = upgradeVariableText.IdUsuario
                                                    };
                DAOLocator.Instance().GetDaoUpgradeVariableText_R_UpgradeVariableTextType().Crear(upgradeVariableTextTypeRelationship);
            }

            DAOLocator.Instance().GetDaoUpgradeVariableText_R_Upgrade().DeleteByIdUpgradeVariableText(objetoNegocio.Id);
            foreach (Product upgrade in upgradeVariableText.Upgrades)
            {
                var upgradeVariableTextCountryRelationship = new UpgradeVariableText_R_Upgrade
                                                       {
                                                           Upgrade = upgrade,
                                                           UpgradeVariableTextId = upgradeVariableText.Id,
                                                           IdUsuario = upgradeVariableText.IdUsuario
                                                       };
                DAOLocator.Instance().GetDaoUpgradeVariableText_R_Upgrade().Crear(upgradeVariableTextCountryRelationship);
            }

            foreach (UpgradeVariableTextContent content in upgradeVariableText.Content)
            {
                var upgradeVariableTextContent = new UpgradeVariableTextContent
                                           {
                                               IdUpgradeVariableText = upgradeVariableText.Id,
                                               Content = content.Content,
                                               Language = content.Language,
                                               IdUsuario = upgradeVariableText.IdUsuario
                                           };
                DAOLocator.Instance().GetDaoUpgradeVariableTextContent().Modificar(upgradeVariableTextContent);
            }
        }

        protected override void EliminarComposicion(ObjetoNegocio objetoNegocio, TransactionScope ts)
        {
            DAOLocator.Instance().GetDaoUpgradeVariableTextContent().DeleteByIdUpgradeVariableText(objetoNegocio.Id, ts);
            DAOLocator.Instance().GetDaoUpgradeVariableText_R_Upgrade().DeleteByIdUpgradeVariableText(objetoNegocio.Id, ts);
            DAOLocator.Instance().GetDaoUpgradeVariableText_R_UpgradeVariableTextType().DeleteByIdUpgradeVariableText(objetoNegocio.Id, ts);
        }

    }
}