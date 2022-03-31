using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Home
{
    public class QuoteExchangeBodyHome
    {
        public static QuoteExchangeBody GetBody()
        {
            return DAOLocator.Instance().GetDaoQuoteExchangeBody().GetBody();
        }
    }
}
