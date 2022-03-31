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
    public class DAOLink : DAOObjetoNegocio<Link>, IDAOLink
    {
        #region IDAOLink Members

        public Link Get(int id)
        {
            return Obtener(id);
        }

        public Link Get(string name)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("Name", name);

            return Obtener(new Filtro(parameters, "dbo.Link_Tx_Name"));
        }

        public IList<Link> BuscarLinks(string nombre, string url)
        {
            Parametros parametros = new Parametros();
        
            parametros.AgregarParametro("Name", nombre);
            parametros.AgregarParametro("Url", url);
            parametros.AgregarParametro("IdDeleteStatus", ObjetoNegocio.Eliminado());

           return Buscar(new Filtro(parametros, "dbo.Link_Tx_NameUrl"));
        }


        public IList<Link> LinksFixed()
        {
            Parametros parametros = new Parametros();
            return Buscar(new Filtro(parametros, "dbo.Link_Tx_NameUrl"));
        }

        public IList<Link> LinksFixed(string linkType)
        {
            int IdLinkType = new DAOLinkType().Get(linkType).Id;
            Parametros parametros = new Parametros();
            parametros.AgregarParametro("IdLinkType", IdLinkType);
            return Buscar(new Filtro(parametros, "dbo.Link_Tx_NameUrl"));
        }

        #endregion

        protected override Parametros ParametrosCrear(Link objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdLinkType", objetoNegocio.LinkType.Id);
            parameters.AgregarParametro("Name", objetoNegocio.Name);
            parameters.AgregarParametro("DisplayText_ES", objetoNegocio.DisplayText_ES);
            parameters.AgregarParametro("DisplayText_EN", objetoNegocio.DisplayText_EN);
            parameters.AgregarParametro("DisplayText_PT", objetoNegocio.DisplayText_PT);
            parameters.AgregarParametro("Style", objetoNegocio.Style);
            parameters.AgregarParametro("ImageName", objetoNegocio.ImageName);
            parameters.AgregarParametro("Url", objetoNegocio.Url);
            parameters.AgregarParametro("EnabledDeepLink", objetoNegocio.EnabledDeepLink);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }

        protected override Parametros ParametrosModificar(Link objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdLink", objetoNegocio.Id);
            parameters.AgregarParametro("IdLinkType", objetoNegocio.LinkType.Id);
            parameters.AgregarParametro("Name", objetoNegocio.Name);
            parameters.AgregarParametro("DisplayText_ES", objetoNegocio.DisplayText_ES);
            parameters.AgregarParametro("DisplayText_EN", objetoNegocio.DisplayText_EN);
            parameters.AgregarParametro("DisplayText_PT", objetoNegocio.DisplayText_PT);
            parameters.AgregarParametro("Style", objetoNegocio.Style);
            parameters.AgregarParametro("ImageName", objetoNegocio.ImageName);
            parameters.AgregarParametro("Url", objetoNegocio.Url);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("EnabledDeepLink", objetoNegocio.EnabledDeepLink);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }

        protected override Parametros ParametrosEliminar(Link objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdLink", objetoNegocio.Id);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            return parameters;
        }

        protected override void Completar(Link objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdLink"]);
            objetoPersistido.LinkType = DAOLocator.Instance().GetDaoLinkType().Get(
                Convert.ToInt32(dr["IdLinkType"]));
            objetoPersistido.Name = dr["Name"].ToString();
            objetoPersistido.Url = dr["Url"].ToString();
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
            
            if (dr["EnabledDeepLink"] != DBNull.Value)
                objetoPersistido.EnabledDeepLink = Convert.ToBoolean(dr["EnabledDeepLink"]);
            else
                objetoPersistido.EnabledDeepLink = false;
            try
            {
                objetoPersistido.DisplayText_ES = dr["DisplayText_ES"].ToString();
                objetoPersistido.DisplayText_EN = dr["DisplayText_EN"].ToString();
                objetoPersistido.DisplayText_PT = dr["DisplayText_PT"].ToString();
                objetoPersistido.Style = dr["Style"].ToString();
                objetoPersistido.ImageName = dr["ImageName"].ToString();
            }
            catch { }
        }

        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }
    }
}