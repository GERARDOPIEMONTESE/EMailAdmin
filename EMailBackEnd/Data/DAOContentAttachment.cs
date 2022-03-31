using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data.Interfaces;
using FrameworkDAC.Parametro;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOContentAttachment : DAOObjetoNegocio<ContentAttachment>, IDAOContentAttachment
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override FrameworkDAC.Parametro.Parametros ParametrosCrear(ContentAttachment objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdTemplate", objetoNegocio.IdTemplate);
            parameters.AgregarParametro("IdAttachment", objetoNegocio.IdAttachment);
            parameters.AgregarParametro("IdLanguage", objetoNegocio.IdLanguage);
            parameters.AgregarParametro("CodeRPT", objetoNegocio.CodeRPT);
            parameters.AgregarParametro("Body", objetoNegocio.Body);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }

        protected override FrameworkDAC.Parametro.Parametros ParametrosModificar(ContentAttachment objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdContentAttachment", objetoNegocio.Id);            
            parameters.AgregarParametro("CodeRPT", objetoNegocio.CodeRPT);
            parameters.AgregarParametro("Body", objetoNegocio.Body);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Creado());

            return parameters;
        }

        protected override FrameworkDAC.Parametro.Parametros ParametrosEliminar(ContentAttachment objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdContentAttachment", objetoNegocio.Id);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", ObjetoNegocio.Eliminado());

            return parameters;
        }

        protected override void Completar(ContentAttachment objetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdContentAttachment"]);
            objetoPersistido.IdAttachment = Convert.ToInt32(dr["IdAttachment"]);
            objetoPersistido.IdTemplate = Convert.ToInt32(dr["IdTemplate"]);
            objetoPersistido.IdLanguage = Convert.ToInt32(dr["IdLanguage"]);
            objetoPersistido.CodeRPT = dr["CodeRPT"].ToString();
            objetoPersistido.Body = dr["Body"].ToString();
        }

        public ContentAttachment Get(int Id)
        {
            throw new NotImplementedException();
        }

        public IList<ContentAttachment> Find(int IdTemplate, int IdAttachment, int IdLanguage)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdTemplate", IdTemplate);
            parameters.AgregarParametro("IdAttachment", IdAttachment);
            parameters.AgregarParametro("IdLanguage", IdLanguage);

            return Buscar(new Filtro(parameters, "ContentAttachment_By_TemplateAttach"));
        }


        public IList<ContentAttachment> Find(int IdTemplate, int IdAttachment)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdTemplate", IdTemplate);
            parameters.AgregarParametro("IdAttachment", IdAttachment);

            return Buscar(new Filtro(parameters, "ContentAttachment_By_TemplateAttach"));
        }


        public IList<ContentAttachment> Find(int IdTemplate, int IdAttachment, string Type)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdTemplate", IdTemplate);
            parameters.AgregarParametro("IdAttachment", IdAttachment);
            if (!string.IsNullOrEmpty(Type))
                parameters.AgregarParametro("CodeRPT", Type);

            return Buscar(new Filtro(parameters, "ContentAttachment_By_TemplateAttach"));
        }
    }
}
