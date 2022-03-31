using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Context;
using Spring.Context.Support;
using EMailAdmin.ExternalServices.Service.Interface;

namespace EMailAdmin.ExternalServices.Service
{
    public class ExternalServiceLocator
    {
       #region Singleton

        private static ExternalServiceLocator _instance;

        private IApplicationContext _context;

        private ExternalServiceLocator()
        {
            _context = ContextRegistry.GetContext();
        }

        public static ExternalServiceLocator Instance()
        {
            return _instance ?? (_instance = new ExternalServiceLocator());
        }

        #endregion

        public IExternalInformationService GetSignatureService()
        {
            return (IExternalInformationService)_context.GetObject("ExternalInformationService");
        }
    }
}
