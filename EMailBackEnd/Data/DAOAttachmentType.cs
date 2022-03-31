using System;
using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Data.Interfaces;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOAttachmentType : DAOObjetoCodificado<AttachmentType>, IDAOAttachmentType
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override void Completar(AttachmentType objetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdAttachmentType"]);
            objetoPersistido.Codigo = dr["Code"].ToString();
            objetoPersistido.Descripcion = dr["Description"].ToString();
        }

        public AttachmentType Get(int id)
        {
            return Obtener(id);
        }

        public AttachmentType GetByCode(string code)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("Code", code);

            return Obtener(new Filtro(parameters, "dbo.AttachmentType_Tx_Filters"));
        }

        public IList<AttachmentType> FindAll()
        {
            return Buscar(new Filtro(new Parametros(), "dbo.AttachmentType_Tx_Filters"));
        }
    }
}
