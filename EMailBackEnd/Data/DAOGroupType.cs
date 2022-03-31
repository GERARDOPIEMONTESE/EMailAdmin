using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOGroupType : DAOObjetoCodificado<GroupType>, IDAOGroupType
    {
        #region IDAOGroupType Members

        public GroupType Get(int id)
        {
            return Obtener(id);
        }

        public GroupType GetByCode(string code)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("Code", code);

            return Obtener(new Filtro(parameters, "dbo.GroupType_Tx_Code"));
        }

        public IList<GroupType> FindAll()
        {
            return Buscar(new Filtro(new Parametros(), "dbo.GroupType_TT"));
        }

        #endregion

        protected override void Completar(GroupType objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdGroupType"]);
            objetoPersistido.Codigo = dr["Code"].ToString();
            objetoPersistido.Descripcion = dr["Description"].ToString();
        }

        protected override string NombreConnectionString()
        {
            return "EmailAdmin";
        }
    }
}