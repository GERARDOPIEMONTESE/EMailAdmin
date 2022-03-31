using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Parametro;
using EMailAdmin.BackEnd.Data.Interfaces;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOHappyBirthBody : DAOObjetoPersistido<HappyBirthBody>, IDAOHappyBirthBody
    {
        protected override string NombreConnectionString()
        {
            return "EmailAdmin";
        }

        protected override void Completar(HappyBirthBody objetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            //objetoPersistido.Id = Convert.ToInt32(dr["IdTemplate"]);
            objetoPersistido.Body = dr["Body"].ToString();
            objetoPersistido.Locations = dr["Locations"].ToString();
        }

        #region IDAOHappyBirthBody Members

        public HappyBirthBody GetBody()
        {
            return Obtener(new Filtro(new Parametros(), "dbo.HappyBirthDate_Tx_Body"));
        }
        #endregion
    }
}
