using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Data.Interfaces;
using FrameworkDAC.Parametro;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOACCOMNotIssue : DAOObjetoPersistido<ACCOMNotIssue>, IDAOACCOMNotIssue
    {
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override void Completar(ACCOMNotIssue ObjetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {          
            try
            {
                ObjetoPersistido.CountryCode = int.Parse(dr["CountryCode"].ToString());
                ObjetoPersistido.CountryName = dr["CountryName"].ToString();
                ObjetoPersistido.Gateway = dr["Gateway"].ToString();
                ObjetoPersistido.PurchaseProcessTypeDesc = dr["PurchaseProcessTypeDesc"].ToString();

                DateTime lastQuoteLog;
                if (DateTime.TryParse(dr["QuoteLogDate"].ToString(), out lastQuoteLog))
                    ObjetoPersistido.QuoteLogDate = lastQuoteLog;
                
                DateTime LastConfirmationDate;
                if (DateTime.TryParse(dr["LastConfirmationDate"].ToString(), out LastConfirmationDate))
                    ObjetoPersistido.LastConfirmationDate = LastConfirmationDate;
            }
            catch { }
        }

        public IList<ACCOMNotIssue> GetACCOMNotIssue()
        {
            var parameters = new Parametros();            
            return Buscar(new Filtro(parameters, "ACCOM_not_issue"));
        }
    }
}
