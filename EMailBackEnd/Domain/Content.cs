using System.Collections.Generic;
using CapaNegocioDatos.CapaNegocio;
using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;

namespace EMailAdmin.BackEnd.Domain
{
    public class Content : ObjetoNegocio
    {
        #region Constant

        private const string NAME = "Content";

        #endregion Constant

        #region Properties

        public int IdTemplate { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public Idioma Language { get; set; }

        public ContentImage Header { get; set; }

        public ContentImage Footer { get; set; }

        public ContentImage HeaderPDF { get; set; }

        public ContentImage FooterPDF { get; set; }

        public string Color { get; set; }

        public string EVoucherName { get; set; }

        public int IdHeader { get; set; }

        public int IdFooter { get; set; }

        public int IdHeaderPDF { get; set; }

        public int IdFooterPDF { get; set; }

        public IList<ContentImage> Images { get; set; }

        public IList<Link> Links { get; set; }

        public IList<EMailContactType> Contacts { get; set; }

        public IList<VariableText> VarableTexts { get; set; }

        public IList<SignatureType> Signatures { get; set; }

        public IList<CountryVisibleTextType> CountryVisibleTexts { get; set; }

        public IList<UpgradeVariableTextType> UpgradeVariableTexts { get; set; }

        #endregion

        #region Methods

        public override IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoContent();
        }

        public override string ObtenerNombre()
        {
            return NAME;
        }

        public Content Clone()
        {
            var clone = new Content
                            {
                                Subject = Subject,
                                Body = Body,
                                Language = Language,
                                Header = Header,
                                Footer = Footer,
                                HeaderPDF = HeaderPDF,
                                FooterPDF = FooterPDF,
                                IdHeader = IdHeader,
                                IdFooter = IdFooter,
                                IdHeaderPDF = IdHeaderPDF,
                                IdFooterPDF = IdFooterPDF,
                                Images = Images,
                                Links = Links,
                                Contacts = Contacts,
                                VarableTexts = VarableTexts,
                                Signatures = Signatures
                            };



            return clone;
        }

        public ContentImage GetContentImage(string name)
        {
            if (Images != null)
            {
                foreach (ContentImage image in Images)
                {
                    if (image.Name != null && image.Name.ToLower().Equals(name.ToLower()))
                    {
                        return image;
                    }
                }
            }

            return null;
        }

        #endregion Methods
    }
}