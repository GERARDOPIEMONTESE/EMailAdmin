using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Dato;

namespace EMailAdmin.BackEnd.Domain
{
    public class Link : VariableText
    {
        private const string NAME = "Link";

        #region Attributes

        private LinkType _LinkType;

        private string _Name;

        private string _Url;

        #endregion

        #region Properties

        public LinkType LinkType
        {
            get
            {
                return _LinkType;
            }
            set
            {
                _LinkType = value;
            }
        }

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        public string Url
        {
            get
            {
                return _Url;
            }
            set
            {
                _Url = value;
            }
        }

        public string UrlDeepLink
        {
            get
            {
                if (Url.Contains("url="))
                {
                    var variableInitTag = "url=";

                    var indexInit = Url.ToLower().IndexOf(variableInitTag);
                    indexInit += variableInitTag.Length;

                    string _urlLink = Url.Substring(indexInit, Url.Length - indexInit);

                    return _urlLink;
                }
                else
                    return Url;
            }
        }

        public string DisplayText_ES { get; set; }
        public string DisplayText_EN { get; set; }
        public string DisplayText_PT { get; set; }
        public string Style { get; set; }
        public string ImageName { get; set; }
        public bool EnabledDeepLink { get; set; }

        #endregion

        protected override IDAOObjetoNegocio GetConcreteDao()
        {
            return DAOLocator.Instance().GetDaoLink();
        }

        protected override string GetConcreteName()
        {
            return NAME;
        }

        public string ImageStyle
        {
            get
            {
                if (!string.IsNullOrEmpty(ImageName))
                {
                    string[] imageConfig = ImageName.Split('|');
                    if (imageConfig.Length > 0)
                    {
                        foreach (var item in imageConfig)
                        {
                            if (item.ToUpper().Contains("STYLE="))
                                return item;
                        }
                    }
                }

                return "";
            }
        }

        public string ImageURL
        {
            get
            {
                if (!string.IsNullOrEmpty(ImageName) &&  !string.IsNullOrEmpty(ImageStyle))
                {
                    return ImageName.Replace(ImageStyle, "").Replace("|","");
                }
                return "";
            }
        }

        public bool ImageEncode
        {
            get
            {
                return (!string.IsNullOrEmpty(ImageName) && ImageName.Contains("$"));
            }
        }

        internal string GetDisplayText(int IdLenguaje)
        {
            string texto = "";

            switch (IdLenguaje.ToString())
            {
                case CapaNegocioDatos.CapaNegocio.Idioma.INGLES:
                    { texto = DisplayText_EN; break; }
                case CapaNegocioDatos.CapaNegocio.Idioma.ESPANOL:
                    { texto = DisplayText_ES; break; }
                case CapaNegocioDatos.CapaNegocio.Idioma.PORTUGUES:
                    { texto = DisplayText_PT; break; }
                default:
                    { texto = Name; break; }
            }

            return texto;
        }
    }
}
