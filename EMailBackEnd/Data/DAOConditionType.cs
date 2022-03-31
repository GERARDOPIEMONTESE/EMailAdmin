using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOConditionType : DAOObjetoCodificado<ConditionType>, IDAOConditionType
    {
        #region IDAOConditionType Members

        public IList<ConditionType> FindAll()
        {
            return Buscar(new Filtro(new Parametros(), "ConditionType_TT"));
        }

        #endregion

        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override void Completar(ConditionType objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdConditionType"]);
            objetoPersistido.Code = dr["Code"].ToString();
            objetoPersistido.Descripcion = dr["Description"].ToString();
        }

        public ConditionType Get(int id)
        {
            return Obtener(id);
        }
    }
}