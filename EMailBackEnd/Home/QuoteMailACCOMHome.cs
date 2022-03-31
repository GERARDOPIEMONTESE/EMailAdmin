using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Home
{
    public class QuoteMailACCOMHome
    {
        public static IList<QuoteMailACCOM> GetPendingQuotes()
        {
            return DAOLocator.Instance().GetDaoQuoteMailACCOM().GetPendingQuotes();
        }

        public static IList<QuoteMailACCOM> GetPrePurchaseQuotes()
        {
            return DAOLocator.Instance().GetDaoQuoteMailACCOM().GetPrePurchaseQuotes();
        }

        public static QuoteMailACCOM GetPendingQuoteMailById(int IdPendingQuoteMail)
        {
            return DAOLocator.Instance().GetDaoQuoteMailACCOM().GetPendingQuoteMailById(IdPendingQuoteMail);
        }

        public static QuoteMailACCOM GetPrePurchaseQuoteMailById(int IdPrePurchaseQuoteMail)
        {
            return DAOLocator.Instance().GetDaoQuoteMailACCOM().GetPrePurchaseQuoteMailById(IdPrePurchaseQuoteMail);
        }
    }
}
