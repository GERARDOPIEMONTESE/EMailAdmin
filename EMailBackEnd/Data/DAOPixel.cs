using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data.Interfaces;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOPixel : DAOObjetoNegocio<Pixel>, IDAOPixel
    {
        
        protected override FrameworkDAC.Parametro.Parametros ParametrosCrear(Pixel objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("Name", objetoNegocio.Name);
            parameters.AgregarParametro("jsonContenido", Pixel.GetJsonContenido(objetoNegocio.pixel));
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.ObtenerCreado());

            return parameters;
        }

        protected override FrameworkDAC.Parametro.Parametros ParametrosModificar(Pixel objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdPixel", objetoNegocio.Id);
            parameters.AgregarParametro("Name", objetoNegocio.Name);
            parameters.AgregarParametro("jsonContenido", Pixel.GetJsonContenido(objetoNegocio.pixel));
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.ObtenerModificado());

            return parameters;
        }

        protected override FrameworkDAC.Parametro.Parametros ParametrosEliminar(Pixel objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdPixel", objetoNegocio.Id);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.ObtenerEliminado());

            return parameters;
        }

        protected override void Completar(Pixel objetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdPixel"]);
            objetoPersistido.Name = dr["Name"].ToString();
            if (!string.IsNullOrEmpty(dr["jsonContenido"].ToString()))
                objetoPersistido.pixel = Pixel.GetPixel(dr["jsonContenido"].ToString());

            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
        }

        public Pixel Get(int id)
        {
            return Obtener(id);
        }

        public Pixel Get(string name)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("Name", name);

            return Obtener(new Filtro(parameters, "dbo.Pixel_Tx_Name"));
        }

        public IList<Pixel> BuscarPixels(string nombre)
        {
            Parametros parametros = new Parametros();
            if (!string.IsNullOrEmpty(nombre))
                parametros.AgregarParametro("Name", nombre);
            return Buscar(new Filtro(parametros, "dbo.Pixel_Tx_Name"));
        }

        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }
    }
}
