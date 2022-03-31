using AssistCard.ServerMSG.Message;
using System.Collections.Generic;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data;
using CapaNegocioDatos.Utilitarios;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Properties;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.BackEnd.Service.Interfaces;
using System.Xml;
using System;
using CapaNegocioDatos.CapaHome;
using CapaNegocioDatos.CapaNegocio;
using EMailAdmin.BackEnd.Utils;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using System.Configuration;

namespace EMailAdmin.BackEnd.Strategies.EMailSender
{
    public class EMailSenderPoints : EMailSenderDefault
    {
        #region Points Strategy Methods

        protected override void CompleteDto(AbstractEMailDTO dto)
        {
            dto.To = ConfigurationManager.AppSettings["VoucherPointsEmailTo"].ToString();
            dto.MailFromAppearance = ConfigurationManager.AppSettings["VoucherPointsMailFromAppearance"].ToString();
            dto.TemplateType = TemplateTypeHome.Get("VPR"); // "Voucher Points Report"
        }

        protected override IList<Domain.Attachment> FindAttachemnts(AbstractEMailDTO dto, Template template)
        {
            IList<Domain.Attachment> attachments = DAOLocator.Instance().GetDaoAttachment().
                FindByTemplateId(template.Id);
            return attachments;
        }

        public override byte[] GetAttachments(AbstractEMailDTO dto)
        {
            try
            {
                CompleteDto(dto);
                var template = GetTemplate(dto);
                IList<Domain.Attachment> attachments = FindAttachemnts(dto, template);
                IAttachmentService service = ServiceLocator.Instance().GetAttachmentService();
                IList<AttachmentItem> items = service.GetItems(attachments[0], dto);
                return (byte[])items[0].Content;
            }
            catch (Exception ex)
            {
            }
            return new byte[] { };
        }

        protected virtual IList<System.Net.Mail.Attachment> GetMailAttachments(AbstractEMailDTO dto, Template template, EMailLog log)
        {
            IList<System.Net.Mail.Attachment> mailAttachments = new List<System.Net.Mail.Attachment>();
            IList<Domain.Attachment> attachments = FindAttachemnts(dto, template);
            log.AttachmentIds = "";
            try
            {
                foreach (Domain.Attachment attachment in attachments)
                {
                    IList<AttachmentItem> items = ServiceLocator.Instance().
                        GetAttachmentService().GetItems(attachment, dto);

                    foreach (AttachmentItem item in items)
                    {
                        log.AttachmentIds += item.Id.ToString() + ",";
                        var stream = new MemoryStream((byte[])item.Content);
                        var mailAttachment = new System.Net.Mail.Attachment(stream, item.Name);
                        mailAttachment.NameEncoding = Encoding.UTF8;
                        mailAttachments.Add(mailAttachment);
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return mailAttachments;
        }

        protected override void Proccess(AbstractEMailDTO dto, Template template, EMailLog log)
        {
            EMailAddress fromAddress = EMailAddressHome.Get(template.IdEMailFromAddress);
            log.MailFrom = fromAddress.Address != null ? fromAddress.Address : "-";
            log.MailTo = dto.To != null ? dto.To : dto.Bcc;
            log.ErrorMessage = "";

            try
            {
                if (template.Id != 0)
                {
                    dto.EMailBody = ProccessContextData(dto, ProccessData(dto, template));
                    //dto.Header = (byte[])template.GetContent(dto.IdLanguage).Header.Content;
                    //dto.Footer = (byte[])template.GetContent(dto.IdLanguage).Footer.Content;
                    //dto.HeaderPDF = (byte[])template.GetContent(dto.IdLanguage).HeaderPDF.Content;
                    //dto.FooterPDF = (byte[])template.GetContent(dto.IdLanguage).FooterPDF.Content;
                    log.Subject = template.GetSubject(dto.IdLanguage);
                    log.Body = dto.EMailBody;
                    if (dto.MailFromAppearance == "[FROMADDRESSNAME]")
                        dto.MailFromAppearance = (fromAddress.Address != null ? fromAddress.Name : string.Empty);

                    bool sentMail = Messaging.SendMailThread(fromAddress.Address != null ? fromAddress.Address : "-",
                        dto.To, dto.Cc, dto.Bcc, template.GetSubject(dto.IdLanguage),
                        dto.EMailBody, GetMailAttachments(dto, template, log), dto.MailFromAppearance != null ? dto.MailFromAppearance : string.Empty);

                    if (sentMail)
                    {
                        var reportHistory = new PointsReportHistory()
                        {
                            ReportDate = DateTime.Now
                        };
                        DAOLocator.Instance().GetDAOPointsReportHistory().Crear(reportHistory);
                    }

                    log.ErrorMessage = sentMail ? "" : "Error in mail - To: " + dto.To;
                }

                ServiceLocator.Instance().GetEMailLogService().FinishLog(template, log);
            }
            catch (Exception ex)
            {
                log.ErrorMessage = ex.Message + " - " + ex.StackTrace;
                ServiceLocator.Instance().GetEMailLogService().FinishLog(template, log);
                throw ex;
            }
        }

        #endregion
    }
}
