using CapaNegocioDatos.CapaNegocio;
using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class AttachmentItem : ObjetoNegocio
    {
        #region Constants

        private const string NAME = "AttachmentItem";

        #endregion

        #region Properties

        public bool Added { get; set; }
        public int IdAttachment { get; set; }
        public Idioma Language { get; set; }
        public object Content { get; set; }
        public string Type { get; set; }
        public decimal Dimenssion { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LanguageDescriptionSpanish
        {
            get { return Language.Descripcion; }
        }
        public string LanguageDescriptionEnglish
        {
            get { return Language.DescripcionIngles; }
        }
        public string LanguageDescriptionPortuguese
        {
            get { return Language.DescripcionPortugues; }
        }

        public string NameToShowOnGrid
        {
            get { return Name.Length > 12 ? Name.Substring(0, 12) + @"..." : Name; }
        }

        public string DescriptionToShowOnGrid
        {
            get { return Description.Length > 12 ? Description.Substring(0, 12) + @"..." : Description; }
        }
        #endregion

        #region Methods

        public override string ObtenerNombre()
        {
            return NAME;
        }

        public override IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoAttachmentItem();
        }

        public AttachmentItem Clone()
        {
            AttachmentItem clone = new AttachmentItem();

            clone.Language = Language;
            clone.Content = Content;
            clone.Type = Type;
            clone.Dimenssion = Dimenssion;
            clone.Name = Name;
            clone.Description = Description;

            return clone;
        }

        #endregion Methods
    }
}
