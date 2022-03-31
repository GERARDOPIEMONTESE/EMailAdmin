using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using FrameworkDAC.Parametro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOEstrategyAttachmentTemplate : DAOObjetoNegocio<EstrategyAttachmentTemplate>, IDAOEstrategyAttachmentTemplate
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        public IList<EstrategyAttachmentTemplate> FindAttachmentTemplates(int IdTemplate, int IdAttachment)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdTemplate", IdTemplate);
            parameters.AgregarParametro("IdAttachment", IdAttachment);
            return Buscar(new Filtro(parameters, "dbo.EstrategyAttachmentTemplate_Tx_Filters"));
        }

        protected override FrameworkDAC.Parametro.Parametros ParametrosCrear(EstrategyAttachmentTemplate objetoNegocio)
        {
            objetoNegocio.IdEstado = objetoNegocio.ObtenerCreado();

            Parametros parameters = new Parametros();
            parameters.AgregarParametro("IdAttachment", objetoNegocio.IdAttachment);
            parameters.AgregarParametro("IdTemplate", objetoNegocio.IdTemplate);
            parameters.AgregarParametro("IdTemplateAttachment", objetoNegocio.IdTemplateAttachment);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus",objetoNegocio.IdEstado );

            return parameters;
        }

        protected override FrameworkDAC.Parametro.Parametros ParametrosModificar(EstrategyAttachmentTemplate objetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override FrameworkDAC.Parametro.Parametros ParametrosEliminar(EstrategyAttachmentTemplate ObjetoNegocio)
        {
            ObjetoNegocio.IdEstado = ObjetoNegocio.ObtenerEliminado();
            var parameters = new Parametros();

            parameters.AgregarParametro("IdEstrategyAttachmentTemplate", ObjetoNegocio.Id);

            return parameters;
        }

        protected override Parametros ParametrosGrabarLog(EstrategyAttachmentTemplate objetoNegocio)
        {            
            Parametros parameters = new Parametros();
            parameters.AgregarParametro("IdEstrategyAttachmentTemplate", objetoNegocio.Id);
            parameters.AgregarParametro("IdAttachment", objetoNegocio.IdAttachment);
            parameters.AgregarParametro("IdTemplate", objetoNegocio.IdTemplate);
            parameters.AgregarParametro("IdTemplateAttachment", objetoNegocio.IdTemplateAttachment);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override void Completar(EstrategyAttachmentTemplate ObjetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            ObjetoPersistido.Id = Convert.ToInt32(dr["IdEstrategyAttachmentTemplate"]);
            ObjetoPersistido.IdAttachment = Convert.ToInt32(dr["IdAttachment"]);
            ObjetoPersistido.IdTemplate= Convert.ToInt32(dr["IdTemplate"]);
            ObjetoPersistido.IdTemplateAttachment = Convert.ToInt32(dr["IdTemplateAttachment"]);
            ObjetoPersistido.TemplateName = dr["TemplateName"].ToString();      
        }
    }
}
