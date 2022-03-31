using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOTemplateType : DAOObjetoCodificado<TemplateType>, IDAOTemplateType
    {
        #region IDAOTemplateType Members

        public IList<TemplateType> FindAll()
        {
            return Buscar(new Filtro(new Parametros(), "dbo.TemplateType_Tx_Code"));
        }

        public TemplateType GetByDescription(string description)
        {
            var parametros = new Parametros();
            parametros.AgregarParametro("Description", description);

            return Obtener(new Filtro(parametros, "dbo.TemplateType_Tx_Description"));
        }

        public TemplateType Get(string code)
        {
            var parametros = new Parametros();
            parametros.AgregarParametro("Code", code);

            return Obtener(new Filtro(parametros, "dbo.TemplateType_Tx_Code"));
        }

        public IList<TemplateType> Find(string Prefijo)
        {
            var parametros = new Parametros();
            parametros.AgregarParametro("Prefijo", Prefijo);

            return Buscar(new Filtro(parametros, "dbo.TemplateType_Tx_Code"));
        }

        public TemplateType Get(int id)
        {
            return Obtener(id);
        }

        #endregion

        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override void Completar(TemplateType ObjetoPersistido, SqlDataReader dr)
        {
            ObjetoPersistido.Id = Convert.ToInt32(dr["IdTemplateType"]);
            ObjetoPersistido.Codigo = dr["Code"].ToString();
            ObjetoPersistido.Descripcion = dr["Description"].ToString();
        }


   
    }
}