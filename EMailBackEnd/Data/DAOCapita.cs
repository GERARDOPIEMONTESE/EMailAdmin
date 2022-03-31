using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Parametro;
using System.Xml;
using LibreriaUtilitarios;
using CapaNegocioDatos.Utilitarios;
using CapaNegocioDatos.CapaNegocio;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOCapita : DAOObjetoPersistido<Capita>, IDAOCapita    
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override void Completar(Capita ObjetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            ObjetoPersistido.Id = Convert.ToInt32(dr["IdProducto"]);
            ObjetoPersistido.Descripcion = dr["CapitaNombre"].ToString();
            ObjetoPersistido.Codigo = dr["CapitaCodigo"].ToString();
            ObjetoPersistido.Plan.Id = Convert.ToInt32(dr["IdTarifa"]);
            ObjetoPersistido.Plan.Descripcion = dr["PlanNombre"].ToString();
            ObjetoPersistido.Plan.Codigo = dr["PlanCodigo"].ToString();
        }

        public IList<Capita> FindAll(int countryCode, string capita,string plan )
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("CodigoPais", countryCode);
            parameters.AgregarParametro("CapitaNombre", capita);
            parameters.AgregarParametro("PlanNombre", plan);

            return Buscar(new Filtro(parameters, "dbo.Capitas_Tx_Filters"));
        }

        public Dictionary<int, string> GetCondicionesTipoDocumento()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            string xml = ObtenerXml(new Filtro(new Parametros(), "dbo.CondicionesTipoDocumento_Tx_All"));
            
            XmlDocument xmlDocument = XmlParser.GetDocument(xml);            
            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
                dic.Add(int.Parse(xmlNode.Attributes["IdTipoDocumento"].Value), xmlNode.Attributes["Nombre"].Value);
            }
            return dic;
        }
    }    
}
