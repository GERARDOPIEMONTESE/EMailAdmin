using System;
using System.Collections.Generic;
using CapaNegocioDatos.CapaNegocio;
using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.BackEnd.Domain
{
    public class Template : ObjetoNegocio
    {
        #region Constants
        private const string NAME = "Template";
        #endregion

        public enum eTypeAttachsWithEkit //la descripcion esta en el resource (mismo nombre)
        {
            opNotAttachEkit=0,
            opAttachsWithEkit=1,
            opAddEkitAttach=2            
        }

        #region Properties

        public string Name { get; set; }

        public DateTime EffectiveStartDate { get; set; }

        public DateTime EffectiveEndDate { get; set; }

        public int Hierarchy { get; set; }

        public TemplateType TemplateType { get; set; }

        public Modulo Module { get; set; }

        public IList<Content> IContent { get; set; }

        public IList<Attachment> IAttachments { get; set; }

        public IList<Group_R_Template> IGroups { get; set; }

        public int IdEMailFromAddress { get; set; }

        public string TemplateTypeDescription
        {
            get { return TemplateType.Descripcion; }
        }

        public string EffectiveStartDateDescription
        {
            get { return EffectiveStartDate.ToShortDateString(); }
        }

        public string EffectiveEndDateDescription
        {
            get { return EffectiveEndDate.ToShortDateString(); }
        }

        public string HierarchyDescription
        {
            get { return Hierarchy.ToString(); }
        }

        public bool MergeAttachsWithEKit { get; set; }

        public eTypeAttachsWithEkit TypeAttachsWithEkit { get; set; }

        public int IdTemplatePDF { get; set; }

        #endregion

        #region Constructor

        public Template()
        {
            IContent = new List<Content>();
            IAttachments = new List<Attachment>();
            IGroups = new List<Group_R_Template>();
            MergeAttachsWithEKit = true;
        }

        #endregion Constructor

        #region Methods
        public override IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoTemplate();
        }

        public override string ObtenerNombre()
        {
            return NAME;
        }
                
        public string GetSubject(int idLanguage)
        {
            foreach (Content content in IContent)
            {
                if (content.Language.Id == idLanguage)
                {
                    return content.Subject;
                }
            }
            return "";
        }

        public string GetBody(int idLanguage)
        {
            foreach (Content content in IContent)
            {
                if (content.Language.Id == idLanguage)
                {
                    return content.Body;
                }
            }
            return "";
        }

        public Content GetContent(int idLanguage)
        {
            foreach (Content content in IContent)
            {
                if (content.Language.Id == idLanguage)
                {
                    return content;
                }
            }
            return new Content();
        }

        public string GetAttachName(int idLanguage)
        {
            foreach (Content content in IContent)
            {
                if (content.Language.Id == idLanguage)
                {
                    return content.EVoucherName;
                }
            }
            return "";
        }

        public Template Clone()
        {
            Template clone = new Template();

            clone.Name = Name;
            clone.EffectiveStartDate = EffectiveStartDate;
            clone.EffectiveEndDate = EffectiveEndDate;
            clone.Hierarchy = Hierarchy;
            clone.TemplateType = TemplateType;
            clone.Module = Module;
            clone.IContent = new List<Content>();
            clone.IAttachments = IAttachments;
            clone.IGroups = new List<Group_R_Template>();
            clone.IdEMailFromAddress = IdEMailFromAddress;
            clone.MergeAttachsWithEKit = MergeAttachsWithEKit;

            foreach (Content content in IContent)
            {
                clone.IContent.Add(content.Clone());
            }

            foreach (Group_R_Template groupRTemplate in IGroups)
            {
                clone.IGroups.Add(groupRTemplate.Clone());
            }

            return clone;
        }

        public static int GetTypeAttachsWithEkitValue(string op)
        {
            return Convert.ToInt32(Enum.Parse(typeof(eTypeAttachsWithEkit), op));
        }
        
        #endregion Methods

        public static eTypeAttachsWithEkit GetTypeAttachsWithEkit(string op)
        {
            eTypeAttachsWithEkit valor;
            Enum.TryParse(op, out valor);
            return valor;
        }

        private Nullable<bool> _isDynamic = null;
        public bool IsDynamic
        {
            get
            {
                if (!_isDynamic.HasValue)
                {
                    TemplateType ttDynamic = TemplateTypeHome.GetDynamic();
                    _isDynamic = (ttDynamic.Id > 0 && ttDynamic.Id == TemplateType.Id);
                }

                return _isDynamic.Value;
            }
        }
    }
}