using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Context;
using Spring.Context.Support;

namespace EMailAdmin.BackEnd.Home
{
    public class HomeLocator
    {
        #region Singleton

        private IApplicationContext _Context;

        private static HomeLocator _instance;

        private HomeLocator()
        {
            _Context = ContextRegistry.GetContext();
        }

        public static HomeLocator Instance()
        {
            return _instance ?? (_instance = new HomeLocator());
        }

        #endregion

        public SignatureHome GetSignatureHome()
        {
           return (SignatureHome)_Context.GetObject("SignatureHome");
        }

    }
}
