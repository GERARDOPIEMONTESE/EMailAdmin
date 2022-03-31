using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Admin.Common.Helpers;
using DTOMapper;
using Newtonsoft.Json;

namespace EmailAdmin.MVC.Helpers
{
    public class ContextHelper
    {
        private static HttpContext Context = HttpContext.Current;
     
        public static dynamic InitContext(int IdModulo, int IdUsuario, int IdPerfil)
        {
            try
            {
                string url;
                url = "Context/GetContext";
                DTOFilter filter = new DTOFilter { Parameters = new Dictionary<string, object>() };

                filter.Parameters.Add("IdUsuario", IdUsuario);
                filter.Parameters.Add("IdModulo", IdModulo);
                filter.Parameters.Add("IdPerfil", IdPerfil);

                var jsonContext = JsonConvert.SerializeObject(filter);

                var obj = ApiHelper.Post(url,"", jsonContext);

                var rst = JsonConvert.DeserializeObject<dynamic>(obj);

                return rst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ChangeModule(int IdModulo)
        {
            try
            {
                string url;
                url = "Modulo/ChangeModulo";
                DTOFilter filter = new DTOFilter { Parameters = new Dictionary<string, object>() };

                filter.Parameters.Add("IdUsuario", SessionManager.IdUsuario);
                filter.Parameters.Add("IdModulo", IdModulo);

                var jsonContext = JsonConvert.SerializeObject(filter);

                url = JsonConvert.DeserializeObject<string>(ApiHelper.Post(url,"", jsonContext));
                return url;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}