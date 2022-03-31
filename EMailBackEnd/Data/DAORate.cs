using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data
{
    public class DAORate : DAOObjetoCodificado<Rate>, IDAORate
    {
        #region IDAORate Members

        public IList<Rate> FindAllByCountryAndProduct(string countryCode, int idProduct)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdProducto", idProduct);
            parameters.AgregarParametro("CodigoPais", countryCode);
            return Buscar(new Filtro(parameters, "dbo.Tarifa_Tx_IdProducto"));
        }

        #endregion

        protected override string NombreConnectionString()
        {
            return "Condiciones";
        }

        protected override void Completar(Rate objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdTarifa"]);
            objetoPersistido.Code = dr["Codigo"].ToString();
            objetoPersistido.Descripcion = dr["Nombre"].ToString();
            objetoPersistido.Annual = Convert.ToBoolean(dr["Anual"]);
            try
            {
                objetoPersistido.Modality = dr["Modalidad"].ToString();
            }
            catch (Exception) { }
        }

        #region IDAORate Members


        public Rate GetByProductCode(int idProduct, string code)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdProducto", idProduct);
            parameters.AgregarParametro("Codigo", code);

            return Obtener(new Filtro(parameters, "dbo.Tarifa_Tx_IdProducto_Codigo_TipoModalidad"));
        }

        #endregion
    }
}
