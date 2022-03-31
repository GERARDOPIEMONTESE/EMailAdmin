using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Service.Interfaces;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Negocio;
using CapaNegocioDatos.Utilitarios;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.BackEnd.Service
{
    public class EMailLogService : IEMailLogService
    {
        public IDAOEMailLog DaoEMailLog { get; set; }

        public EMailLog InitLog(AbstractEMailDTO dto)
        {
            EMailLog log = new EMailLog();

            log.InvokeInformation = ServicioConversionXml.Instancia().SerializeObject(dto);
            log.InvokeInformation = log.InvokeInformation == null ? "-" : log.InvokeInformation;
            log.CountryCode = dto.CountryCode;
            log.ModuleCode = dto.ModuleCode;
            log.IdLote = dto.IdLote;
            log.IdClienteUnico = dto.IdClienteUnico;
            log.Fecha = DateTime.Now;            
            log.ProcessStatus = EMailLog.INITIAL;
            log.IdEstado = ObjetoNegocio.Creado();
            log.ContextInformation = dto.XmlContextInformation == null ?
                new byte[] { } : ServicioCompresionXml.ZipXml(dto.XmlContextInformation);

            if (typeof(DefaultEMailDTO).IsAssignableFrom(dto.GetType()))
                log.VoucherCode = ((DefaultEMailDTO)dto).VoucherCode;
            else
                log.VoucherCode = "-";
            DaoEMailLog.Crear(log);
            return log;
        }

        public void UpdateLog(AbstractEMailDTO dto, EMailLog log)
        {
            log.IdTemplateType = 0;
            log.TemplateName = log.TemplateName == null || log.TemplateName.Length == 0 
                ? "NO TEMPLATE" : log.TemplateName;
            log.EndDate = DateTime.MinValue.AddYears(1900);
            log.Body = log.Body == null || log.Body.Length == 0 ? "NO BODY" : log.Body;
            log.Subject = log.Subject == null || log.Subject.Length == 0 ? "NO SUBJECT" : log.Subject;
            log.MailTo = log.MailTo == null || log.MailTo.Length == 0 ? "NO MAIL TO" : log.MailTo;
            log.MailFrom = log.MailFrom == null || log.MailFrom.Length == 0 ? "NO MAIL FROM" : log.MailFrom;
            log.AttachmentIds = log.MailFrom == null || log.MailFrom.Length == 0 ? "NO ATTACHMENTS" : log.MailFrom;
            log.ErrorMessage = log.ErrorMessage == null || log.ErrorMessage.Length == 0 ? "" : log.ErrorMessage;
            log.ProcessStatus = EMailLog.INPROGRESS;
            log.ContextInformation = dto.XmlContextInformation != null ? 
                ServicioCompresionXml.ZipXml(dto.XmlContextInformation) : new byte[] {};
            log.PaxName = dto.RecipientName;
            log.PaxSurname = dto.RecipientSurname;

            try
            {
                log.IssuanceDate = ((EMailEkitDTO)dto).IssuanceDateShortFormat;
            }
            catch (Exception)
            {
                log.IssuanceDate = "-";
            }

            DaoEMailLog.Persistir(log);
        }

        public void SaveLog(AbstractEMailDTO dto, EMailLog log, int processStatus)
        {
            log.IdTemplateType = 0;
            log.TemplateName = log.TemplateName == null || log.TemplateName.Length == 0 
                ? "NO TEMPLATE" : log.TemplateName;
            log.EndDate = DateTime.MinValue.AddYears(1900);
            log.Body = log.Body == null || log.Body.Length == 0 ? "NO BODY" : log.Body;
            log.Subject = log.Subject == null || log.Subject.Length == 0 ? "NO SUBJECT" : log.Subject;
            log.MailTo = log.MailTo == null || log.MailTo.Length == 0 ? "NO MAIL TO" : log.MailTo;
            log.MailFrom = log.MailFrom == null || log.MailFrom.Length == 0 ? "NO MAIL FROM" : log.MailFrom;
            log.AttachmentIds = log.MailFrom == null || log.MailFrom.Length == 0 ? "NO ATTACHMENTS" : log.MailFrom;
            log.ErrorMessage = log.ErrorMessage == null || log.ErrorMessage.Length == 0 ? "" : log.ErrorMessage;
            log.ProcessStatus = processStatus;
            log.ContextInformation = dto.XmlContextInformation != null ? 
                ServicioCompresionXml.ZipXml(dto.XmlContextInformation) : new byte[] { };

            DaoEMailLog.Persistir(log);
        }

        public void FinishLog(Template template, EMailLog log)
        {
            log.IdTemplateType = template.TemplateType == null ? 0 : template.TemplateType.Id;
            log.IdTemplate = template.Id;
            log.TemplateName = template.Name == null ? "NO TEMPLATE" : template.Name;
            log.EndDate = DateTime.Now;

            if (log.InvokeByHandler)
            {
                log.ProcessStatus = log.ErrorMessage == null || log.ErrorMessage.Length == 0
                ? EMailLog.OK_HANDLER : EMailLog.ERRORINHANDLER;
            }
            else
            {
                log.ProcessStatus = log.ErrorMessage == null || log.ErrorMessage.Length == 0
                    ? EMailLog.OK :
                    log.ErrorMessage.Contains("Error in mail - To:") ? EMailLog.ERRORINMAIL : EMailLog.ERROR;
            }
            log.Body = log.Body == null ? "NO BODY" : log.Body;
            log.Subject = log.Subject == null ? "NO SUBJECT" : log.Subject;
            log.MailTo = log.MailTo == null ? "NO MAIL TO" : log.MailTo;
            log.MailFrom = log.MailFrom == null ? "NO MAIL FROM" : log.MailFrom;
            log.AttachmentIds = log.AttachmentIds == null ? "NO ATTACHMENTS" : log.AttachmentIds;

            DaoEMailLog.Persistir(log);
        }

        public void RegisterEMailReception(int countryCode, string voucherCode, int idTemplateType)
        {
            IList<EMailLog> logs = DaoEMailLog.Find(countryCode, voucherCode, idTemplateType);

            foreach (EMailLog log in logs)
            {
                log.Receive = true;
                log.ReceiveDate = DateTime.Now;

                DaoEMailLog.Persistir(log);
            }
        }

        #region IEMailLogService Members

        public void SaveLog(EMailLog log)
        {
            DaoEMailLog.Persistir(log);
        }

        public void ZipContextInformation()
        {
            IList<EMailLog> logs = DaoEMailLog.FindZipPending();

            foreach (EMailLog log in logs)
            {
                log.ContextInformation = ServicioCompresionXml.ZipXml(log.ContextInformationZipPending);
                DaoEMailLog.Persistir(log);
            }
        }
        
        public int ViewZipContextInformation()
        {
            IList<EMailLog> logs = DaoEMailLog.FindZipPending();

            return logs.Count();
        }

        public string GetZipContextInformation(int countryCode, string vouchercode, string templateCode )
        {
            int IdTemplateType= TemplateHome.FindByName(templateCode).FirstOrDefault().TemplateType.Id;
            IList<EMailLog> lst = DaoEMailLog.FindLog(countryCode, vouchercode, IdTemplateType);
            string info = "";
            foreach (var item in lst)
            {
                info+=ServicioCompresionXml.UnzipXml(item.ContextInformation);
            }
            return info;
        }

        public void Tracking(AbstractEMailDTO dto, EMailLog log, int IdTemplate)
        {            
            if (dto.ListTrackEmail != null)
            {
                foreach (var track in dto.ListTrackEmail)
                {
                    track.CastInfoPax(dto);
                    track.IdEmailLog = log.Id;
                    track.IdTemplate = IdTemplate;
                    track.IdClienteUnico = dto.IdClienteUnico;
                    track.CountryCode = dto.CountryCode;
                    int voucher = 0;
                    if (int.TryParse(log.VoucherCode, out voucher))
                        track.VoucherCode = voucher;
                    track.Persistir();
                }
            }

            if (dto.ListLinkTrack != null)
            {
                foreach (var tlink in dto.ListLinkTrack)
                {
                    tlink.IdEmailLog = log.Id;
                    tlink.IdTemplate = IdTemplate;
                    tlink.IdClienteUnico = dto.IdClienteUnico;
                    tlink.CountryCode = dto.CountryCode;
                    int voucher = 0;
                    if (int.TryParse(log.VoucherCode, out voucher))
                        tlink.VoucherCode = voucher;
                    tlink.Persistir();
                }
            }
        }
        #endregion

    }
}
