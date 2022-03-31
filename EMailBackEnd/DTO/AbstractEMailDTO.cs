using EMailAdmin.BackEnd.Domain;
using CapaNegocioDatos.CapaNegocio;
using System.Collections.Generic;
using System;
using System.Xml.Serialization;
namespace EMailAdmin.BackEnd.DTO
{
    public abstract class AbstractEMailDTO
    {  
        #region Properties

        public string Date { get; set; }

        public string To { get; set; }

        public string Cc { get; set; }

        public string Bcc { get; set; }

        public string ModuleCode { get; set; }

        public int IdLanguage { get; set; }

        public int IdStrategy { get; set; }

        public int CountryCode { get; set; }

        public EMailLog Log { get; set; }

        public int IdAttachmentType { get; set; }
        
        #region Transient properties (MUST be filled by each strategy)

        public AssociationGroupDTO AssociationGroupDto { get; set; }

        public TemplateType TemplateType { get; set; }

        public Modulo Module { get; set; }

        public string EMailBody { get; set; }

        public byte[] Header { get; set; }

        public byte[] Footer { get; set; }

        public byte[] HeaderPDF { get; set; }

        public byte[] FooterPDF { get; set; }

        public string color { get; set; }

        public string CurrentDate { get; set; }

        public bool IsPreview { get; set; }

        public string XmlContextInformation { get; set; }

        public bool GivenToAddress { get; set; }

        public string MailFromAppearance { get; set; }

        public string DatosAdicionales { get; set; }

        public string XMLContentAttachment { get; set; }

        #endregion

        #region Common properties

        public string RecipientName { get; set; }

        public string RecipientSurname { get; set; }

        public string RecipientFullName { get; set; }

        public string RecipientDocumentNumber { get; set; }

        public string CountryName { get; set; }

        public string ApplicationUrl { get; set; }

        public string CompleteVoucherCode { get; set; }

        public Nullable<int> IdLote { get; set; }

        public Nullable<int> IdClienteUnico { get; set; }

        [XmlIgnore] 
        public List<TrackEmail> ListTrackEmail { get; set; }
        [XmlIgnore] 
        public List<TrackLink> ListLinkTrack { get; set; }

        #endregion

        #endregion

        #region Virtual Methods

        public virtual string GetLinks()
        {
            return "";
        }

        #endregion

        public System.Drawing.Color ColorRGB
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(color))
                        color = CapaNegocioDatos.CapaHome.CodigoActivadorHome.ObtenerColorDefault();
                    return System.Drawing.ColorTranslator.FromHtml("#" + color);
                }
                catch
                {
                    return System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                }
            }
        }

        internal void AddTrackEmail(TrackEmail trackEmail)
        {
            if (ListTrackEmail == null)
                ListTrackEmail = new List<TrackEmail>();

            ListTrackEmail.Add(trackEmail);
        }

        internal void AddLinkTrack(TrackLink linkTrack)
        {
            if (ListLinkTrack == null)
                ListLinkTrack = new List<TrackLink>();

            ListLinkTrack.Add(linkTrack);
        }
    }
}
