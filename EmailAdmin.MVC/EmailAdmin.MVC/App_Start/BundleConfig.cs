using Admin.Common.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace EmailAdmin.MVC.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            CommonBundleConfig.RegisterBundles(bundles);
        }
    }
}