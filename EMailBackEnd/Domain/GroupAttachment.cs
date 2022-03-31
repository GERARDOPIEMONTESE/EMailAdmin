using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;
using CapaNegocioDatos.CapaNegocio;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Data.Interfaces;

namespace EMailAdmin.BackEnd.Domain
{
    public class GroupAttachment : ObjetoNegocio
    {
        #region Constants

        private const string NAME = "GroupAttachment";

        #endregion

        #region Constructor

        public GroupAttachment()
        {
            GroupName = "";
        }

        #endregion Constructor

        #region Properties

        public string GroupName { get; set; }
        public string AttachName_ES { get; set; }
        public string AttachName_EN { get; set; }
        public string AttachName_PT { get; set; }
        public int AttachOrder { get; set; }

        #endregion

        public override FrameworkDAC.Dato.IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoGroupAttachment(); 
        }

        public string AttachName(int IdLanguage)
        {
            switch (IdLanguage.ToString())
            {
                case Idioma.ESPANOL: return AttachName_ES;
                case Idioma.INGLES: return AttachName_EN;
                case Idioma.PORTUGUES: return AttachName_PT;
                default:
                    return AttachName_ES;
            }
        }

        public override string ObtenerNombre()
        {
            return NAME;
        }

        public bool IsGroupEVoucher
        {
            get
            {
                bool bOK = false;
                bOK = (!string.IsNullOrEmpty(GroupName) && GroupName == ConfigurationValueHome.GetByCode("GroupEVoucher").Value);
                return bOK;
            }
        }

        public string InUseTemplates { get; set; }
        private Nullable<bool> _canDeleted = null;
        public bool CanDeleted
        {
            get
            {
                if (!_canDeleted.HasValue)
                {
                    if (!IsGroupEVoucher)
                    {
                        string _InUseTemplates = "";
                        _canDeleted = ((IDAOGroupAttachment)ObtenerDAO()).CanDelete(Id, out _InUseTemplates);
                        InUseTemplates = _InUseTemplates;
                    }
                    else
                    {
                        _canDeleted = false;
                        InUseTemplates = "---EVOUCHER---";
                    }
                }

               return _canDeleted.Value;
            }
        }
    }

    public class GroupAttachmentUseTemplate : ObjetoPersistido
    {
        public string TemplateName { get; set; }

        public override string ObtenerNombre()
        {
            return "";
        }
    }
}
