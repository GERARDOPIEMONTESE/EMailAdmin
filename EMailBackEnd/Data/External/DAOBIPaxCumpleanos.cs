using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Dato;

using EMailAdmin.BackEnd.Domain.External;
using EMailAdmin.BackEnd.Data.Interfaces;
using CapaNegocioDatos.CapaNegocio;
using CapaNegocioDatos.CapaDatos;

namespace EMailAdmin.BackEnd.Data.External
{
    public class DAOBIPaxCumpleanos : DAOObjetoPersistido<PaxCumpleanos>, IDAOBIPaxCumpleanos
    {
        IList<Idioma> idiomas = new List<Idioma>();

        public string debugEmail { get; set; }

        protected override string NombreConnectionString()
        {
            return "BI";
        }

        protected override void Completar(PaxCumpleanos ObjetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            ObjetoPersistido.COUNTRYCODE = Convert.ToInt32(dr["COUNTRYCODE"]);
            ObjetoPersistido.LANGUAGE = dr["LANGUAGE"].ToString();
            ObjetoPersistido.NATIONALID = dr["NATIONALID"].ToString();
            ObjetoPersistido.NAME = dr["NAME"].ToString();
            ObjetoPersistido.SURNAME = dr["SURNAME"].ToString();
            ObjetoPersistido.EMAIL = dr["EMAIL"].ToString();
            ObjetoPersistido.CELLPHONE = dr["CELLPHONE"].ToString();
            ObjetoPersistido.BIRTHDATE = DateTime.Parse(dr["BIRTHDATE"].ToString());
            if (dr.GetSchemaTable().Columns.Contains("IDCLIENTEUNICO") && dr["IDCLIENTEUNICO"] != DBNull.Value)
                ObjetoPersistido.IDCLIENTEUNICO = Convert.ToInt32(dr["IDCLIENTEUNICO"]);

            if (!string.IsNullOrEmpty(debugEmail))
            {
                ObjetoPersistido.EMAIL = debugEmail;
                ObjetoPersistido.debug = true;
            }
        }

        public IList<PaxCumpleanos> Find()
        {
            string filtro = " AND MONTH(BIRTHDATE) = MONTH( GETDATE()) AND DAY(BIRTHDATE) = DAY( GETDATE())";
            string sql = "SELECT * FROM [AC_CAMPAÑA].[dbo].[PaxCumpleanos] WHERE 1=1 {FILTRO}";

            var hbEmail = DAOConfigurationValue.Instance().GetByCode("HB_Email");
            if (hbEmail != null && hbEmail.Id > 0 && !string.IsNullOrEmpty(hbEmail.Value))
            {
                debugEmail = hbEmail.Value;
                sql = @"SELECT TOP 1 * FROM [AC_CAMPAÑA].[dbo].[PaxCumpleanos] WHERE LANGUAGE='es' {FILTRO} union 
SELECT TOP 1 * FROM [AC_CAMPAÑA].[dbo].[PaxCumpleanos] WHERE LANGUAGE='en' {FILTRO} union 
SELECT TOP 1 * FROM [AC_CAMPAÑA].[dbo].[PaxCumpleanos] WHERE LANGUAGE='pt' {FILTRO}";
            }

            sql = sql.Replace("{FILTRO}", filtro);

            return Buscar(sql, true);
        }
    }
}
