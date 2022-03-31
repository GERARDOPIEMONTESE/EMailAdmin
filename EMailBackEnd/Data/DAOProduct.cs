using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOProduct : DAOObjetoCodificado<Product>, IDAOProduct
    {
         #region IDAOProduct Members

        public IList<Product> FindAllByCountry(string countryCode)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("CodigoPais", countryCode);

            return Buscar(new Filtro(parameters, "dbo.Producto_Tx_CodigoPais_Ordenado"));
        }

        public Product Get(string countryCode, string code, int idType)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("CodigoPais", countryCode);
            parameters.AgregarParametro("Codigo", code);
            parameters.AgregarParametro("IdTipoGrupoClausula", idType);

            return Obtener(new Filtro(parameters, "dbo.Producto_Tx_Codigo"));
        }

        public Product Get(int id)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdProducto", id);

            return Obtener(new Filtro(parameters, "dbo.Producto_Tx_IdProducto"));
        }

        public IList<Product> FindAllUpgradesByCountry(string countryCode)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("CodigoPais", countryCode);
            parameters.AgregarParametro("IdTipoGrupoClausula", Product.UPGRADE);

            return Buscar(new Filtro(parameters, "dbo.Producto_Tx_CodigoPais_Ordenado"));
        }

        #endregion

        protected override string NombreConnectionString()
        {
            return "Condiciones";
        }

        protected override void Completar(Product objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdProducto"]);
            objetoPersistido.CountryCode = dr["CodigoPais"].ToString();
            objetoPersistido.Code = dr["Codigo"].ToString();
            objetoPersistido.Descripcion = dr["Nombre"].ToString();
        }
    }
}
