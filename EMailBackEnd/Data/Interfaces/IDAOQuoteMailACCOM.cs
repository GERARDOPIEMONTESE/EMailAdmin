using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOQuoteMailACCOM
    {
        IList<QuoteMailACCOM> GetPendingQuotes();
        IList<QuoteMailACCOM> GetPrePurchaseQuotes();
        QuoteMailACCOM GetPendingQuoteMailById(int IdPendingQuoteMail);
        QuoteMailACCOM GetPrePurchaseQuoteMailById(int IdPrePurchaseQuoteMail);
    }
}
