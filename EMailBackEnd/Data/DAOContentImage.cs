using System;
using System.Data.SqlClient;
using System.Runtime.Caching;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOContentImage : DAOObjetoNegocio<ContentImage>, IDAOContentImage
    {
        const string CACHE_KEY_PREFIX = "ContentImage-";
        ObjectCache cache = new MemoryCache("DAOContentImageCache");

        #region IDAOContentImage Members

        public ContentImage Get(int idContentImage)
        {
            // Buscar en cache
            string key = CACHE_KEY_PREFIX + idContentImage;
            ContentImage val = (ContentImage)cache.Get(key);
            if (val == null)
            {
                val = Obtener(idContentImage);

                // Agregar a la cache
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(3.0);

                cache.Add(key, val, policy);
            }
            return val;
        }

        #endregion

        protected override void Completar(ContentImage objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdImage"]);
            objetoPersistido.Name = dr["Name"].ToString();
            objetoPersistido.Content = dr["Content"];
            objetoPersistido.Type = dr["Type"].ToString();
            objetoPersistido.Dimenssion = Convert.ToDecimal(dr["Dimenssion"]);
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
        }

        protected override Parametros ParametrosCrear(ContentImage objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("Name", objetoNegocio.Name);
            parameters.AgregarParametro("Content", objetoNegocio.Content);
            parameters.AgregarParametro("Type", objetoNegocio.Type);
            parameters.AgregarParametro("Dimenssion", objetoNegocio.Dimenssion);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);

            return parameters;
        }

        protected override Parametros ParametrosModificar(ContentImage objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdContentImage", objetoNegocio.Id);
            parameters.AgregarParametro("Name", objetoNegocio.Name);
            parameters.AgregarParametro("Content", objetoNegocio.Content);
            parameters.AgregarParametro("Type", objetoNegocio.Type);
            parameters.AgregarParametro("Dimenssion", objetoNegocio.Dimenssion);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);

            return parameters;
        }

        protected override Parametros ParametrosEliminar(ContentImage objetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        public ContentImage Get(int idContent, int idLanguage, string name)
        {
            // Buscar en cache
            string key = CACHE_KEY_PREFIX + idContent + "-" + idLanguage + "-" + name;
            ContentImage val = (ContentImage)cache.Get(key);
            if (val == null)
            {
                Parametros parameters = new Parametros();
                parameters.AgregarParametro("IdContent", idContent);
                parameters.AgregarParametro("IdLanguage", idLanguage);
                parameters.AgregarParametro("Name", name);

                val = Obtener(new Filtro(parameters, "dbo.ContentImage_Tx_IdContent"));

                // Agregar a la cache
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(3.0);

                cache.Add(key, val, policy);
            }
            return val;
        }
    }
}