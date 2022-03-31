using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Context;
using Spring.Context.Support;
using EMailAdmin.ExternalServices.Data.Interfaces;
using EMailAdmin.BackEnd.Data.Interfaces;

namespace EMailAdmin.ExternalServices.Data
{
    public class ExternalDAOLocator
    {
        #region Singleton

        private IApplicationContext _context;

        private static ExternalDAOLocator _instance;

        private ExternalDAOLocator()
        {
            _context = ContextRegistry.GetContext();
        }

        public static ExternalDAOLocator Instance()
        {
            return _instance ?? (_instance = new ExternalDAOLocator());
        }

        #endregion

        public IDAOIssuanceInformation GetDaoIssuanceInformation()
        {
            return (IDAOIssuanceInformation)_context.GetObject("DaoIssuanceInformation");
        }

        public IDAORateInformation GetDaoRateInformation()
        {
            return (IDAORateInformation)_context.GetObject("DaoRateInformation");
        }

        public IDAOExternalVoucher GetDatoExternalVoucher()
        {
            return (IDAOExternalVoucher)_context.GetObject("DaoExternalVoucher");
        }

        public IDAOPrepurchase GetDaoPrepurchase()
        {
            return (IDAOPrepurchase)_context.GetObject("DaoPrepurchase");
        }
        
    }
}
