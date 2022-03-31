using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace EmailAdmin.MVC.Helpers
{
    public static class SessionManager
    {
        private const string CONTEXT = "Context";
        private const string IDUSUARIO = "IdUsuario";
        private const string IDIOMA = "Idioma";
        private const string MENU = "Menu";
        private const string CULTUREUI = "CultureUI";
        private const string LABELSCOLORS = "LabelsColors";

        //compatibilidad portal viejo
        private const string USUARIO = "Usuario";
        private const string SUFIJOIDIOMAUSU = "SufijoIdiomaUsu";

        public static Dictionary<string, string> views;
      

        public static HttpSessionState Session
        {
            get { return HttpContext.Current.Session; }

        }

        public static void InitContexto(int _IdUsuario, int _IdModulo, int _IdPerfil)
        {
            try
            {
                views = new Dictionary<string, string>();
                Context = ContextHelper.InitContext(_IdModulo, _IdUsuario, _IdPerfil);
                //Usuario = Context.Usuario;
                IdUsuario = _IdUsuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region CONTEXT
        public static dynamic Context
        {
            set
            {
                Session[CONTEXT] = value;
            }

            get
            {
                if (Session[CONTEXT] == null)
                    return null;
                return (dynamic)Session[CONTEXT];
            }
        }
        #endregion

        #region ID_USUARIO

        public static int IdUsuario
        {
            set
            {
                Session[IDUSUARIO] = value;
            }

            get
            {
                if (Session[IDUSUARIO] == null)
                    return 0;
                return (int)Session[IDUSUARIO];
            }
        }

        #endregion

        public static dynamic Usuario
        {
            set
            {
                Session[USUARIO] = value;
                Session[SUFIJOIDIOMAUSU] = "es";
            }

            get
            {
                if (Session[USUARIO] == null)
                    return null;
                return (dynamic)Session[USUARIO];
            }
        }
        //public static IList<DTOModulo> Modulos
        //{           
        //    get
        //    {              
        //        return (IList<DTOModulo>)Context.Modulos;
        //    }
        //}

        //#region SufijoIdioma
        //public static string SufijoIdioma
        //{
        //    get
        //    {
        //        return Session[SUFIJOIDIOMAUSU].ToString();
        //    }
        //}
        //#endregion
        
        public static string CurrentUICulture
        {
            set
            {
                Session[CULTUREUI] = value;
            }

            get
            {
                if (Session[CULTUREUI] == null)
                    return null;
                return Session[CULTUREUI].ToString();
            }
        }

        public static Dictionary<string, Color> LabelsColors
        {         
            set
            {
                Session[LABELSCOLORS] = value;
            }

            get
            {
                if (Session[LABELSCOLORS] == null)
                    return new Dictionary<string, Color>();

                return ((Dictionary<string, Color>)Session[LABELSCOLORS]);
            }
        }
         

        #region VIEWS

        internal static string GetView(string virtualPath)
        {
            if (views.ContainsKey(virtualPath))
                return views[virtualPath];
            else
                return null;
        }

        internal static void SetView(string virtualPath, string content)
        {
            if (!views.ContainsKey(virtualPath))
                views.Add(virtualPath, content);
            else
                views[virtualPath] = content;
        }

        #endregion
    }
}