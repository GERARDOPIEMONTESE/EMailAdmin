using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOEstrategy : DAOObjetoCodificado<Estrategy>, IDAOEstrategy
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override void Completar(Estrategy objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdEstrategy"]);
            objetoPersistido.Code = dr["Code"].ToString();
            objetoPersistido.Descripcion = dr["Description"].ToString();
            objetoPersistido.Class = dr["Class"].ToString();
            try
            {
                objetoPersistido.AttachName = dr["AttachName"].ToString();
                objetoPersistido.AttachType = dr["AttachType"].ToString();
                objetoPersistido.UrlDownload = dr["UrlDowload"].ToString();
                objetoPersistido.KeyError = dr["KeyError"].ToString();
                objetoPersistido.AttachName_EN = (!string.IsNullOrEmpty(dr["AttachName_EN"].ToString()) ? dr["AttachName_EN"].ToString() : objetoPersistido.AttachName);
                objetoPersistido.AttachName_PT = (!string.IsNullOrEmpty(dr["AttachName_PT"].ToString()) ? dr["AttachName_PT"].ToString() : objetoPersistido.AttachName);
                objetoPersistido.JsonParams = dr["JsonParams"].ToString();
                if (!string.IsNullOrEmpty(dr["IdTemplate"].ToString()))
                    objetoPersistido.IdTemplate = Convert.ToInt32(dr["IdTemplate"]);
            }
            catch { }
        }

        public Estrategy Get(int id)
        {
            return Obtener(id);
        }

        public Estrategy GetByCode(string code)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("Code", code);

            return Obtener(new Filtro(parameters, "dbo.Estrategy_Tx_Filters"));
        }

        public IList<Estrategy> FindAll()
        {
            return Buscar(new Filtro(new Parametros(), "dbo.Estrategy_Tx_Filters"));
        }

        public Estrategy GetByClass(string className)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("Class", className);

            return Obtener(new Filtro(parameters, "dbo.Estrategy_Tx_Filters"));
        }
    }
}
