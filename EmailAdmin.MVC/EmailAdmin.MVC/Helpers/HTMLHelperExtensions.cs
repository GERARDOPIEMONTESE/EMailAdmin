using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Optimization;
using DTOMapper;
using Newtonsoft.Json;

namespace EmailAdmin.MVC.Helpers
{
    public static class HTMLHelperExtensions
    {
        public static IHtmlString LocalStyleRender(string paths)
        {
            return Styles.RenderFormat(@"<link href='{0}' rel='stylesheet'>", paths + "?v=" + GetVersion(paths));
        }

        public static IHtmlString LocalScriptRender(string paths)
        {
            return Scripts.RenderFormat(@"<script src='{0}'></script>", paths + "?v=" + GetVersion(paths));
        }

        public static string GetVersion(string file)
        {
            return File.GetLastWriteTimeUtc(AppDomain.CurrentDomain.BaseDirectory + file).ToString("yyyyMMddhhmmss");
        }

        public static IEnumerable<SelectListItem> StatusOpen()
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            dic.Add("Todos", -1);
            dic.Add("Cerrado", 0);
            dic.Add("Abierto", 1);
            return new SelectList(dic, "value", "key");
        }
    }
}