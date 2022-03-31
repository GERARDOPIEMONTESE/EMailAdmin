using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain.External;
using CapaNegocioDatos.CapaNegocio;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data.External
{
    public class DAOBIPaxContinuaCompra : DAOObjetoPersistido<PaxContinuaCompra>, IDAOBIPaxContinuaCompra
    {
        IList<Idioma> idiomas = new List<Idioma>();

        public string debugEmail { get; set; }

        protected override string NombreConnectionString()
        {
            return "BI";
        }

        protected override void Completar(PaxContinuaCompra ObjetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            ObjetoPersistido.LISTA_ID = Convert.ToInt32(dr["LISTA_ID"]);
            ObjetoPersistido.LANGUAGE = dr["LANGUAGE"].ToString();
            ObjetoPersistido.IDQUOTELOG = Convert.ToInt32(dr["IDQUOTELOG"]);
            ObjetoPersistido.ISO2CODE = dr["ISO2CODE"].ToString().ToLower();
            ObjetoPersistido.FULLNAME = dr["FULLNAME"].ToString();
            ObjetoPersistido.EMAIL = dr["EMAIL"].ToString();
            ObjetoPersistido.URLBASE64 = dr["URLBASE64"].ToString();
            if (dr.GetSchemaTable().Columns.Contains("IDCLIENTEUNICO") && dr["IDCLIENTEUNICO"] != DBNull.Value)
                ObjetoPersistido.IDCLIENTEUNICO = Convert.ToInt32(dr["IDCLIENTEUNICO"]);

            if (!string.IsNullOrEmpty(debugEmail))
            {
                ObjetoPersistido.EMAIL = debugEmail;
            }
        }

        public IList<PaxContinuaCompra> Find()
        {
            string filtro = "";
            string sql = "SELECT * FROM [AC_CAMPAÑA].[dbo].[Continuar_Con_Tu_Compra] WHERE 1=1 {FILTRO}";

            var hbEmail = DAOConfigurationValue.Instance().GetByCode("KEEPBUY_Email");
            if (hbEmail != null && hbEmail.Id > 0 && !string.IsNullOrEmpty(hbEmail.Value))
            {
                debugEmail = hbEmail.Value;
                sql = @"SELECT TOP 1 * FROM [AC_CAMPAÑA].[dbo].[Continuar_Con_Tu_Compra] WHERE LANGUAGE='es' {FILTRO} 
union SELECT TOP 1 * FROM [AC_CAMPAÑA].[dbo].[Continuar_Con_Tu_Compra] WHERE LANGUAGE='en' {FILTRO} 
union SELECT TOP 1 * FROM [AC_CAMPAÑA].[dbo].[Continuar_Con_Tu_Compra] WHERE LANGUAGE='pt' {FILTRO}";
            }

            sql = sql.Replace("{FILTRO}", filtro);

            return Buscar(sql, true);
        }

        public Nullable<int> GetIdLote()
        {
            string sql = "SELECT TOP 1 * FROM [AC_CAMPAÑA].[dbo].[Continuar_Con_Tu_Compra]";

            var dato = Obtener(sql, true);
            if (dato != null)
                return dato.LISTA_ID;
            else
                return null;
        }
    }
}