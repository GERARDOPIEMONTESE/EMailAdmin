using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using FrameworkDAC.Parametro;
using EMailAdmin.BackEnd.Data.Interfaces;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOQuoteExchangeBody : DAOObjetoPersistido<QuoteExchangeBody>, IDAOQuoteExchangeBody
    {
        protected override string NombreConnectionString()
        {
            return "EmailAdmin";
        }

        protected override void Completar(QuoteExchangeBody objetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            //objetoPersistido.Id = Convert.ToInt32(dr["IdTemplate"]);
            objetoPersistido.Body = dr["Body"].ToString();
        }

        #region IDAOQuoteExchangeBody Members

        public QuoteExchangeBody GetBody()
        {
            return Obtener(new Filtro(new Parametros(), "dbo.QuoteExchange_Tx_Body"));
        }
        #endregion
    }
}
