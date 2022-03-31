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
    public class DAOGroupAttachmentValid : DAOObjetoPersistido<GroupAttachmentUseTemplate>, IDAOGroupAttachmentValid
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override void Completar(GroupAttachmentUseTemplate ObjetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            ObjetoPersistido.TemplateName = dr["TemplateName"].ToString();
        }

        public IList<GroupAttachmentUseTemplate> InUseTemplates(int IdGroupAttachment)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdGroupAttachment", IdGroupAttachment); 
            return Buscar(new Filtro(parameters, "dbo.GroupAttachment_Tx_InUseTemplates"), true);
        }
    }

    public class DAOGroupAttachment : DAOObjetoNegocio<GroupAttachment>, IDAOGroupAttachment
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        public IList<GroupAttachment> Find(GroupAttachment filter = null)
        {
            var parameters = new Parametros();
            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.GroupName)) parameters.AgregarParametro("GroupName", filter.GroupName);
                if (filter.Id > 0) parameters.AgregarParametro("IdGroupAttachment", filter.Id);
            }
            return Buscar(new Filtro(parameters, "dbo.GroupAttachment_Filters"));
        }

        public override GroupAttachment Obtener(int Id)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdGroupAttachment", Id);

            return Obtener(new Filtro(parameters, "dbo.GroupAttachment_Filters"));
        }

        protected override FrameworkDAC.Parametro.Parametros ParametrosCrear(GroupAttachment objetoNegocio)
        {
            objetoNegocio.IdEstado = objetoNegocio.ObtenerCreado();
            var parameters = new Parametros();

            parameters.AgregarParametro("GroupName", objetoNegocio.GroupName);
            parameters.AgregarParametro("AttachName_EN", objetoNegocio.AttachName_EN);
            parameters.AgregarParametro("AttachName_ES", objetoNegocio.AttachName_ES);
            parameters.AgregarParametro("AttachName_PT", objetoNegocio.AttachName_PT);
            parameters.AgregarParametro("AttachOrder", objetoNegocio.AttachOrder);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override FrameworkDAC.Parametro.Parametros ParametrosModificar(GroupAttachment objetoNegocio)
        {
            objetoNegocio.IdEstado = objetoNegocio.ObtenerCreado();
            var parameters = new Parametros();

            parameters.AgregarParametro("IdGroupAttachment", objetoNegocio.Id);
            parameters.AgregarParametro("GroupName", objetoNegocio.GroupName);
            parameters.AgregarParametro("AttachName_EN", objetoNegocio.AttachName_EN);
            parameters.AgregarParametro("AttachName_ES", objetoNegocio.AttachName_ES);
            parameters.AgregarParametro("AttachName_PT", objetoNegocio.AttachName_PT);
            parameters.AgregarParametro("AttachOrder", objetoNegocio.AttachOrder);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override FrameworkDAC.Parametro.Parametros ParametrosEliminar(GroupAttachment ObjetoNegocio)
        {
            ObjetoNegocio.IdEstado = ObjetoNegocio.ObtenerEliminado();
            var parameters = new Parametros();

            parameters.AgregarParametro("IdGroupAttachment", ObjetoNegocio.Id);
            //parameters.AgregarParametro("IdUser", ObjetoNegocio.IdUsuario);
            //parameters.AgregarParametro("IdStatus", ObjetoNegocio.IdEstado);

            return parameters;
        }

        protected override void Completar(GroupAttachment ObjetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            ObjetoPersistido.Id = Convert.ToInt32(dr["IdGroupAttachment"]);
            ObjetoPersistido.GroupName = dr["GroupName"].ToString();
            ObjetoPersistido.AttachName_EN = dr["AttachName_EN"].ToString();
            ObjetoPersistido.AttachName_ES = dr["AttachName_ES"].ToString();
            ObjetoPersistido.AttachName_PT = dr["AttachName_PT"].ToString();
            int AttachOrder = 0;
            int.TryParse(dr["AttachOrder"].ToString(), out AttachOrder);
            ObjetoPersistido.AttachOrder = AttachOrder;
            ObjetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            ObjetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
        }

        protected override Parametros ParametrosGrabarLog(GroupAttachment objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdGroupAttachment", objetoNegocio.Id);
            parameters.AgregarParametro("GroupName", objetoNegocio.GroupName);
            parameters.AgregarParametro("AttachName_EN", objetoNegocio.AttachName_EN);
            parameters.AgregarParametro("AttachName_ES", objetoNegocio.AttachName_ES);
            parameters.AgregarParametro("AttachName_PT", objetoNegocio.AttachName_PT);
            parameters.AgregarParametro("AttachOrder", objetoNegocio.AttachOrder);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }
        
        public bool CanDelete(int Id , out string InUse_Templates)
        {
            DAOGroupAttachmentValid daoGAV = new DAOGroupAttachmentValid();
            IList<GroupAttachmentUseTemplate> templates = daoGAV.InUseTemplates(Id);
            InUse_Templates = "";
            foreach (var item in templates)
            {
                if (!string.IsNullOrEmpty(InUse_Templates)) InUse_Templates += ",";
                InUse_Templates += item.TemplateName;
            }

            return (templates.Count == 0);
        }
    }
}
