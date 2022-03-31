using System.Collections.Generic;
using EMailAdmin.BackEnd.Data;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;
using CapaNegocioDatos.Utilitarios;
using EMailAdmin.BackEnd.Reports.Objects;
using System.IO;
using LibreriaUtilitarios;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace EMailAdmin.BackEnd.Domain
{
    public class Attachment : ObjetoNegocio
    {
        #region Constants

        private const string NAME = "Attachment";

        #endregion

        #region Constructor

        public Attachment()
        {
            AttachmentItems = new List<AttachmentItem>();
            GroupAttachment = new GroupAttachment();
            AttachmentTemplates = new List<EstrategyAttachmentTemplate>();
        }

        #endregion Constructor

        #region Properties

        public string Name { get; set; }
        public AttachmentType AttachmentType { get; set; }
        public Estrategy Estrategy { get; set; }
        public IList<AttachmentItem> AttachmentItems { get; set; }
        public IList<Attachment_R_Group> IGroups { get; set; }
        public IList<ContentAttachment> AttachmentContentPDF { get; set; }
        public GroupAttachment GroupAttachment { get; set; }
        public IList<EstrategyAttachmentTemplate> AttachmentTemplates { get; set; }
        public int AttachOrder { get; set; }

        public string AttachmentTypeDescripcion
        {
            get { return AttachmentType.Description; }
        }

        public string EstrategyDescription
        {
            get { return Estrategy.Description; }
        }

        public bool EsSTRATEGY
        {
            get { return AttachmentTypeDescripcion.ToUpper() == AttachmentType.STRATEGY.ToString(); }
        }
        
        public string GetXMLAttachmentContentPDF()
        {
            return ServicioConversionXml.Instancia().SerializeObject(AttachmentContentPDF);
        }

        private static Dictionary<string, string> GetDicContentAttachment(string XMLContentAttachment)
        {            
            IList<ContentAttachment> lst = new List<ContentAttachment>();
            lst = (IList<ContentAttachment>)ServicioConversionXml.Instancia().DeserializeObject(XMLContentAttachment, lst.GetType());

            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var item in lst)
            {
                dic.Add(item.CodeRPT, item.Body);
            }
            return dic;
        }

        public static void SetContentAttachmentInfo(VoucherInformationReportObject voucher, string XMLContentAttachment)
        {
            if (XMLContentAttachment != null)
            {
                Dictionary<string, string> dicContentAttachment = GetDicContentAttachment(XMLContentAttachment);

                if (dicContentAttachment != null)
                {
                    if (dicContentAttachment.ContainsKey(ContentAttachment.HEADER))
                    {
                        voucher.SeccionHeaderInfo = dicContentAttachment[ContentAttachment.HEADER];
                    }
                    if (dicContentAttachment.ContainsKey(ContentAttachment.FOOTER))
                    {
                        voucher.SeccionFooterInfo = dicContentAttachment[ContentAttachment.FOOTER];
                    }
                }
            }
        }

        #endregion

        #region Methods

        public override IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoAttachment();
        }

        public override string ObtenerNombre()
        {
            return NAME;
        }
        
        #endregion Methods

        public AttachmentItem GetItem(int idLanguage)
        {
            foreach (AttachmentItem item in AttachmentItems)
            {
                if (item.Language.Id == idLanguage)
                {
                    return item;
                }
            }

            return new AttachmentItem();
        }

    }
}