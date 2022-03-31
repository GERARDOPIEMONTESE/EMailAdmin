using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using ConsultasGenerales.Dto;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;
using Admin.Common;
using Admin.Common.Helpers;

namespace EmailAdmin.MVC.Helpers
{
    public class ObjectManager : ApplicationObjectManager
    {
        public static List<Template> GetTemplateListForSelectors()
        {
            if (HttpContext.Current.Application[MethodBase.GetCurrentMethod().Name] == null)
                HttpContext.Current.Application[MethodBase.GetCurrentMethod().Name] = TemplateHome.FindAllList();

            return (List<Template>)HttpContext.Current.Application[MethodBase.GetCurrentMethod().Name];
        }

        //public static IList<Link> GetLinkListForSelectors()
        //{
        //    if (HttpContext.Current.Application[MethodBase.GetCurrentMethod().Name] == null)
        //        HttpContext.Current.Application[MethodBase.GetCurrentMethod().Name] = TemplateHome.FindAllList();

        //    List<Link> linkListReturn = new List<Link>();
        //    List<Link> templateList = (List<Link>)HttpContext.Current.Application[MethodBase.GetCurrentMethod().Name];
        //    foreach (Link template in templateList)
        //        linkListReturn.Add(template);
        //    return linkListReturn;
        //}
    }
}