using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;
using CapaNegocioDatos.CapaNegocio;

namespace EMailAdmin.BackEnd.Strategies.Attachment
{
   public class AttachStrategy
    {
        public string ATTACHTYPE { get; set; }
        public string ATTACHNAME { get; set; }
        public int IdLanguage { get; set; }
        public int IdStrategy { get; set; }

        private Estrategy estrategy = null;
       
        public Estrategy _estrategy
        {
            get
            {
                if (IdStrategy > 0 && (estrategy==null || estrategy.Id != IdStrategy))
                    estrategy = EstrategyHome.Get(IdStrategy);
                
                if (estrategy==null || estrategy.Id==0) 
                    estrategy = EstrategyHome.GetByClass(this.GetType().FullName);

                return estrategy;
            }
        }
       
        public virtual string GetAttachType()
        {
            return (_estrategy.AttachType != "" ? _estrategy.AttachType : ATTACHTYPE);
        }

        public virtual string GetAttachName()
        {
            string _attchName = "";
            string keyattchName = ConfigurationValueHome.GetByCode("ATTACH_NAME").Value;

            if (!string.IsNullOrEmpty(keyattchName))
                ATTACHNAME = keyattchName;

            if (IdLanguage == 0)
                _attchName= (_estrategy.AttachName != "" ? _estrategy.AttachName : ATTACHNAME);
            else
            {
                switch (IdLanguage.ToString())
                {
                    case Idioma.ESPANOL: _attchName = _estrategy.AttachName; break;
                    case Idioma.INGLES: _attchName = _estrategy.AttachName_EN; break;
                    case Idioma.PORTUGUES: _attchName = _estrategy.AttachName_PT; break;
                    default: _attchName = ATTACHNAME; break;
                }
            }

            if (string.IsNullOrEmpty(_attchName))
                _attchName = ATTACHNAME;

            return _attchName;
        }

        public virtual string GetUrlDownload()
        {
            return _estrategy.UrlDownload;
        }

        public virtual string GetKeyError()
        {
            return _estrategy.KeyError;
        }
    }
}
