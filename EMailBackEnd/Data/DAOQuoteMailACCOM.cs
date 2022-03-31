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
    public class DAOQuoteMailACCOM : DAOObjetoPersistido<QuoteMailACCOM>, IDAOQuoteMailACCOM
    {
        protected override string NombreConnectionString()
        {
            return "EmailAdmin";
        }

        protected override void Completar(QuoteMailACCOM objetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            objetoPersistido.IdQuoteLog = Convert.ToInt32(dr["IdQuoteLog"]);
            objetoPersistido.PurchaseProcessCode = dr["PurchaseProcessCode"].ToString();
            objetoPersistido.HTMLBody = dr["Body"].ToString();
            objetoPersistido.Email = dr["Email"].ToString();

            if (dr["PrePurchase"] != DBNull.Value)
            {
                objetoPersistido.Modality = dr["Modality"].ToString();
                objetoPersistido.Destination = dr["Destination"].ToString();
                objetoPersistido.Phone = dr["Phone"].ToString();
                objetoPersistido.AuxPhone = dr["AuxPhone"].ToString();
                objetoPersistido.Product = dr["Product"].ToString();
                objetoPersistido.FullName = dr["FullName"].ToString();
                objetoPersistido.DaysQuantity = dr["DaysQuantity"].ToString();
                objetoPersistido.CountryCode = Convert.ToInt32(dr["CountryCode"]);
            }
        }

        #region IDAOQuoteMailACCOM Members

        public IList<QuoteMailACCOM> GetPendingQuotes()
        {
            return Buscar(new Filtro(new Parametros(), "dbo.QuoteMailACCOM_Tx_Pending"));
        }

        public IList<QuoteMailACCOM> GetPrePurchaseQuotes()
        {
            return Buscar(new Filtro(new Parametros(), "dbo.QuoteMailACCOM_Tx_PrePurchaseQuoteInformation"));
        }

        public QuoteMailACCOM GetPendingQuoteMailById(int IdPendingQuoteMail)
        {
            Parametros parametros = new Parametros();
            parametros.AgregarParametro("IdQuoteLog", IdPendingQuoteMail);

            return Obtener(new Filtro(parametros, "dbo.QuoteMailACCOM_Tx_Body_Mail"));
        }

        public QuoteMailACCOM GetPrePurchaseQuoteMailById(int IdPrePurchaseQuoteMail)
        {
            Parametros parametros = new Parametros();
            parametros.AgregarParametro("IdQuoteLog", IdPrePurchaseQuoteMail);

            return Obtener(new Filtro(parametros, "dbo.QuoteMailACCOM_Tx_PrePurchase_Body_Mail"));
        }

        #endregion
    }
}
