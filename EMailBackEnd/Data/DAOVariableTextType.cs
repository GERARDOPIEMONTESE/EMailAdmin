using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOVariableTextType : DAOObjetoCodificado<VariableTextType>, IDAOVariableTextType
    {
        #region IDAOGroupType Members

        public VariableTextType Get(int id)
        {
            return Obtener(id);
        }

        public VariableTextType GetByCode(string code)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("Code", code);

            return Obtener(new Filtro(parameters, "dbo.VariableTextType_Tx_Code"));
        }

        public IList<VariableTextType> FindAll()
        {
            return Buscar(new Filtro(new Parametros(), "dbo.VariableTextType_TT"));
        }

        #endregion

        protected override void Completar(VariableTextType objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdVariableTextType"]);
            objetoPersistido.Codigo = dr["Code"].ToString();
            objetoPersistido.Descripcion = dr["Description"].ToString();
        }

        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }
    }
}