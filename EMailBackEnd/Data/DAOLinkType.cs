using System;
using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Data.Interfaces;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOLinkType : DAOObjetoCodificado<LinkType>, IDAOLinkType
    {
        protected override void Completar(LinkType objetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdLinkType"]);
            objetoPersistido.Codigo = dr["Code"].ToString();
            objetoPersistido.Descripcion = dr["Description"].ToString();
        }

        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        #region IDAOLinkType Members

        public LinkType Get(int id)
        {
            return Obtener(id);
        }

        public LinkType Get(string code)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("Code", code);

            return Obtener(new Filtro(parameters, "dbo.LinkType_Tx_Code"));
        }

        public IList<LinkType> FindAll()
        {
            return Buscar(new Filtro(new Parametros(), "dbo.LinkType_Tx_Code"));
        }

        #endregion
    }
}
