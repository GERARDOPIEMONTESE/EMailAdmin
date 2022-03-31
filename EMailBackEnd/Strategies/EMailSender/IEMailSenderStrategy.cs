using System.Collections.Generic;
using AssistCard.ServerMSG.Message;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.BackEnd.Home;
using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using EMailAdmin.BackEnd.Utils;
using System.Linq;
using CapaNegocioDatos.Servicios;
using System.Diagnostics;

namespace EMailAdmin.BackEnd.Strategies.EMailSender
{
    public abstract class IEMailSenderStrategy
    {
        protected abstract AbstractEMailDTO GetDto(string xml);

        protected abstract void CompleteDto(AbstractEMailDTO dto);
        /**
         * Returns the template that will be proccesed and sent by mail.
         **/
        protected abstract Template GetTemplate(AbstractEMailDTO dto);

        protected abstract IList<Domain.Attachment> FindAttachemnts(AbstractEMailDTO dto, Template template);
                
        public virtual void SendEMail(string xml)
        {
            AbstractEMailDTO dto = GetDto(xml);
            SendEMail(dto);
        }

        public virtual void SendEMail(AbstractEMailDTO dto)
        {

            EMailLog log = ServiceLocator.Instance().GetEMailLogService().InitLog(dto);
            try
            {
                dto.Log = log;
                CompleteDto(dto);
                if (AgencySendEmails(dto))
                {
                    ServiceLocator.Instance().GetEMailLogService().UpdateLog(dto, log);
                    SendAndProcessEMail(dto, log);
                }
                else
                    SaveErrorLog_AgencyExclude(dto, log);

            }
            catch (Exception ex)
            {
                SaveErrorLog(ex, dto, log);   
            }
        }

        public virtual AbstractEMailDTO GetDynamicTemplate(AbstractEMailDTO dto)
        {

            EMailLog log = ServiceLocator.Instance().GetEMailLogService().InitLog(dto);
            AbstractEMailDTO result = null;
            try
            {
                dto.Log = log;
                CompleteDto(dto);

                if (AgencySendEmails(dto))
                {
                    ServiceLocator.Instance().GetEMailLogService().UpdateLog(dto, log);
                    result = SendAndProcessTemplate(dto, log);
                }
            }
            catch (Exception ex)
            {
                SaveErrorLog(ex, dto, log);
                result = null;
            }

            return result;
        }

        public byte[] getDynamicPDF(AbstractEMailDTO dto, bool IsTag)
        {

            EMailLog log = ServiceLocator.Instance().GetEMailLogService().InitLog(dto);
            try
            {
                CompleteDto(dto);
                dto.EMailBody = ProccessContextData(dto, ProccessData(dto, GetTemplate(dto)));
                var rst = PdfUtils.GetPdfEtiquetas(dto.EMailBody, IsTag);

                return rst.Data;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public virtual bool AgencySendEmails(AbstractEMailDTO dto)
        {
            return true;
        }

        public virtual byte[] GetAttachments(AbstractEMailDTO dto)
        {
            try
            {
                CompleteDto(dto);
                var template = GetTemplate(dto);
                IList<Domain.Attachment> attachments = FindAttachemnts(dto, template);
                return (byte[])ServiceLocator.Instance().GetAttachmentService().GetItems(attachments[1], dto)[0].Content;
            }
            catch (Exception ex)
            {
                throw ex;
            }             
        }

        public virtual void SaveErrorLog( Exception ex, AbstractEMailDTO dto, EMailLog log )
        {
            log.ErrorMessage = ex.Message + " Stack: " + ex.StackTrace;
            ServiceLocator.Instance().GetEMailLogService().
                SaveLog(dto, log, EMailLog.EXTERNALINFOCOMPLETE);
        }

        public virtual void SaveErrorLog_AgencyExclude(AbstractEMailDTO dto, EMailLog log)
        {
            ServiceLocator.Instance().GetEMailLogService().
               SaveLog(dto, log, EMailLog.AGENCY_EXCLUDE);
        }

        public virtual byte[] FindAttachment(AbstractEMailDTO dto, out string attachName, Nullable<bool> attachMerge = null)
        {
            attachName = "";
            EMailLog log = ServiceLocator.Instance().GetEMailLogService().InitLog(dto);
            try
            {
                dto.Log = log;
                log.InvokeByHandler = true;
                CompleteDto(dto);
                ServiceLocator.Instance().GetEMailLogService().UpdateLog(dto, log);
                return ProcessAndFindAttach(dto, log, out attachName, attachMerge);
            }
            catch (Exception ex)
            {
                SaveErrorLog(ex, dto, log);   
                return new byte[0];
            }
        }

        public virtual byte[] FindAttachment(AbstractEMailDTO dto, FiltersAttachsDTO filtersAttachs)
        {
            EMailLog log = ServiceLocator.Instance().GetEMailLogService().InitLog(dto);
            try
            {
                dto.Log = log;
                log.InvokeByHandler = true;
                CompleteDto(dto);
                ServiceLocator.Instance().GetEMailLogService().UpdateLog(dto, log);
                return ProcessAndFindAttach(dto, log, filtersAttachs);
            }
            catch (Exception ex)
            {
                SaveErrorLog(ex, dto, log);   
                return new byte[0];
            }
        }
        
        protected virtual void SendAndProcessEMail(AbstractEMailDTO dto, EMailLog log)
        {
            Proccess(dto, GetTemplate(dto), log);
        }

        protected virtual AbstractEMailDTO SendAndProcessTemplate(AbstractEMailDTO dto, EMailLog log)
        {
            return ProccessDynamicTemplate(dto, GetTemplate(dto), log);
        }
                
        private byte[] StringEncodeToBytes(string p)
        {
            System.Text.Encoding enc = System.Text.Encoding.ASCII;
            return enc.GetBytes(p);
        }

        public static byte[] MergeFiles(List<byte[]> sourceFiles)
        {
            var document = new Document();
            var output = new MemoryStream();

            try
            {
                // Initialize pdf writer
                var writer = PdfWriter.GetInstance(document, output);

                // Open document to write
                document.Open();
                var content = writer.DirectContent;

                // Iterate through all pdf documents
                for (int fileCounter = 0; fileCounter < sourceFiles.Count; fileCounter++)
                {
                    // Create pdf reader
                    if (sourceFiles[fileCounter].Length > 0)
                    {
                        var reader = new PdfReader(sourceFiles[fileCounter]);
                        int numberOfPages = reader.NumberOfPages;

                        // Iterate through all pages
                        for (int currentPageIndex = 1; currentPageIndex <=
                                           numberOfPages; currentPageIndex++)
                        {
                            // Determine page size for the current page
                            document.SetPageSize(
                               reader.GetPageSizeWithRotation(currentPageIndex));

                            // Create page
                            document.NewPage();
                            var importedPage =
                              writer.GetImportedPage(reader, currentPageIndex);


                            // Determine page orientation
                            var pageOrientation = reader.GetPageRotation(currentPageIndex);
                            if ((pageOrientation == 90) || (pageOrientation == 270))
                            {
                                content.AddTemplate(importedPage, 0, -1f, 1f, 0, 0,
                                   reader.GetPageSizeWithRotation(currentPageIndex).Height);
                            }
                            else
                            {
                                content.AddTemplate(importedPage, 1f, 0, 0, 1f, 0, 0);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("There has an unexpected exception" +
                      " occured during the pdf merging process.", exception);
            }
            finally
            {
                document.Close();
            }
            return output.GetBuffer();
        }

        protected virtual byte[] ProcessAndFindAttach(AbstractEMailDTO dto, EMailLog log, out string attachName, Nullable<bool> attachMerge = null)
        {
            Template template = GetTemplate(dto);

            if (attachMerge.HasValue) //forzado para que mande todos los attachs en un solo archivo
            { 
                template.MergeAttachsWithEKit = attachMerge.Value;
                template.TypeAttachsWithEkit = Template.eTypeAttachsWithEkit.opAttachsWithEkit;
            }

            IList<System.Net.Mail.Attachment> attachments = ProccessAttachment(dto, template, log, out attachName);
            if (attachments != null && attachments.Count > 0)
            {
                //var memoryStream = new MemoryStream();
                //attachments[0].ContentStream.CopyTo(memoryStream);
                //return memoryStream.ToArray();

                attachments[0].ContentStream.Position = 0;
                byte[] buffer = new byte[attachments[0].ContentStream.Length];
                for (int totalBytesCopied = 0; totalBytesCopied < attachments[0].ContentStream.Length; )
                    totalBytesCopied += attachments[0].ContentStream.Read(buffer, totalBytesCopied, Convert.ToInt32(attachments[0].ContentStream.Length) - totalBytesCopied);

                ServiceLocator.Instance().GetEMailLogService().FinishLog(template, log);      

                return buffer;
            }

            return new byte[0];
        }

        protected virtual byte[] ProcessAndFindAttach(AbstractEMailDTO dto, EMailLog log, FiltersAttachsDTO filtersAttach)
        {
            Template template = GetTemplate(dto);

            if (filtersAttach.attachMerge.HasValue) //forzado para que mande todos los attachs en un solo archivo
            {
                template.MergeAttachsWithEKit =filtersAttach.attachMerge.Value;
                template.TypeAttachsWithEkit = Template.eTypeAttachsWithEkit.opAttachsWithEkit;
            }

            IList<System.Net.Mail.Attachment> attachments = ProccessAttachment(dto, template, log, filtersAttach);
            if (attachments != null && attachments.Count > 0)
            {
                //var memoryStream = new MemoryStream();
                //attachments[0].ContentStream.CopyTo(memoryStream);
                //return memoryStream.ToArray();

                attachments[0].ContentStream.Position = 0;
                byte[] buffer = new byte[attachments[0].ContentStream.Length];
                for (int totalBytesCopied = 0; totalBytesCopied < attachments[0].ContentStream.Length; )
                    totalBytesCopied += attachments[0].ContentStream.Read(buffer, totalBytesCopied, Convert.ToInt32(attachments[0].ContentStream.Length) - totalBytesCopied);

                ServiceLocator.Instance().GetEMailLogService().FinishLog(template, log);

                return buffer;
            }

            return new byte[0];
        }

        protected virtual string ProccessContextData(AbstractEMailDTO dto, string body)
        {
            return body;
        }

        protected virtual IList<System.Net.Mail.Attachment> GetMailAttachments(AbstractEMailDTO dto, Template template, EMailLog log, out string ekitName)
        {
            if (ConfigurationValueHome.ValidaHabilitarMailAttachments_Grouped())
                return GetMailAttachmentsGrouped(dto, template, log, out ekitName);
            else
                return GetMailAttachmentsOriginal(dto, template, log, out ekitName);
        }

        protected virtual IList<System.Net.Mail.Attachment> GetMailAttachments(AbstractEMailDTO dto, Template template, EMailLog log, FiltersAttachsDTO filtersAttachs)
        {
                return GetMailAttachmentsFilters(dto, template, log, filtersAttachs);
        }

        protected virtual IList<System.Net.Mail.Attachment> GetMailAttachmentsOriginal(AbstractEMailDTO dto, Template template, EMailLog log, out string ekitName)
        {
            IList<System.Net.Mail.Attachment> mailAttachments = new List<System.Net.Mail.Attachment>();
            IList<Domain.Attachment> attachments = FindAttachemnts(dto, template);
            List<AttachmentItem> items = new List<AttachmentItem>();

            ekitName = "";
            log.AttachmentIds = "";

            foreach (Domain.Attachment attachment in attachments)
            {
                bool bAttachTemplate = false;
                if (attachment.AttachmentTypeDescripcion.ToUpper() == AttachmentType.STRATEGY.ToString())
                {
                    var attachTemplate = ServiceLocator.Instance().GetAttachmentService().GetTemplateAttach(attachment.Id, dto, template.Id);
                    if (attachTemplate != null && attachTemplate.Id > 0)
                    {
                        bAttachTemplate = true;
                        byte[] attachTemplateData = GenerateAttachmentByTemplate(dto, attachTemplate, attachment.Estrategy);

                        AttachmentItem AttachmentTemplateItem = new AttachmentItem()
                        {
                            Name = attachTemplate.GetAttachName(dto.IdLanguage),
                            Content = attachTemplateData
                        };

                        items.Add(AttachmentTemplateItem);
                    }                   
                }

                if (!bAttachTemplate)
                {
                    if (attachment.AttachmentTypeDescripcion.ToUpper() == AttachmentType.STRATEGY.ToString())
                    {
                        attachment.AttachmentContentPDF = FindContentAttachmentPDF(template.Id, attachment.Id, dto);
                    }

                    var itemsAttach = ServiceLocator.Instance().
                        GetAttachmentService().GetItems(attachment, dto);

                    items.AddRange(itemsAttach);

                    foreach (AttachmentItem item in itemsAttach)
                    {
                        log.AttachmentIds += item.Id.ToString() + ",";

                        var stream = new MemoryStream((byte[])item.Content);

                        var mailAttachment = new System.Net.Mail.Attachment(stream, item.Name);
                        mailAttachments.Add(mailAttachment);
                    }
                }
            }

            GenerateAttachmentEvoucher(dto, template, ref mailAttachments, ref items, out ekitName);
            
            return mailAttachments;
        }

        protected virtual IList<System.Net.Mail.Attachment> GetMailAttachmentsFilters(AbstractEMailDTO dto, Template template, EMailLog log, FiltersAttachsDTO filtersAttachs)
        {
            string ekitName = "";

            IList<System.Net.Mail.Attachment> mailAttachments = new List<System.Net.Mail.Attachment>();
            IList<Domain.Attachment> attachments = FindAttachemnts(dto, template);
            List<AttachmentItem> items = new List<AttachmentItem>();

            Dictionary<string, GroupAttachment> dicOrden = new Dictionary<string, GroupAttachment>();

            Dictionary<GroupAttachment, List<Domain.Attachment>> grupoAttachs = new Dictionary<GroupAttachment, List<Domain.Attachment>>();

            OrderGroupsAttachs(attachments, ref dicOrden, ref grupoAttachs, filtersAttachs);

            bool bAgregarEvoucher = false;
            byte[] buffer = null;
            
            foreach (var item in grupoAttachs)
            {
                var lstAttachs = ((List<Domain.Attachment>)item.Value);
                GroupAttachment grupo = ((GroupAttachment)item.Key);
                
                bAgregarEvoucher = (grupo.IsGroupEVoucher && template.TypeAttachsWithEkit == Template.eTypeAttachsWithEkit.opAddEkitAttach);
                string itemAttachName = grupo.AttachName(dto.IdLanguage);
                buffer = ConvertToMailAttachments(lstAttachs, dto, template, log, items, ref itemAttachName);

                var streamEvoucher = new MemoryStream(buffer);

                if (streamEvoucher.Length > 0)
                    mailAttachments.Add(new System.Net.Mail.Attachment(streamEvoucher, itemAttachName));
            }

            if (bAgregarEvoucher) template.TypeAttachsWithEkit = Template.eTypeAttachsWithEkit.opAttachsWithEkit;

            if (filtersAttachs.AttachName != "" || filtersAttachs.GroupAttachName!="") 
                template.TypeAttachsWithEkit = Template.eTypeAttachsWithEkit.opNotAttachEkit;

            GenerateAttachmentEvoucher(dto, template, ref mailAttachments, ref items, out ekitName);

            return mailAttachments;
        }

        private void OrderGroupsAttachs(IList<Domain.Attachment> attachments, ref Dictionary<string, GroupAttachment> dicOrden, ref Dictionary<GroupAttachment, List<Domain.Attachment>> grupoAttachs, FiltersAttachsDTO filtersAttachs=null)
        {           
            var grupos = from x in attachments
                         orderby x.GroupAttachment.AttachOrder descending
                         select x.GroupAttachment;
            string FilterGroupName = (filtersAttachs != null ? filtersAttachs.GroupAttachName : "");
            string FilterAttachName = (filtersAttachs != null ? filtersAttachs.AttachName : "");

            foreach (var item in grupos)
            {
                var GroupName = (string.IsNullOrEmpty(item.GroupName) ? "" : item.GroupName);

                if (FilterGroupName == "" || FilterGroupName == GroupName)
                {
                    if (!dicOrden.ContainsKey(GroupName))
                    {
                        dicOrden.Add(GroupName, item);
                    }
                }
            }

            foreach (var item in dicOrden)
            {
                List<Domain.Attachment> AttachmentItems = new List<Domain.Attachment>();

                if (FilterAttachName != "")
                {
                    AttachmentItems = (from x in attachments
                                       where x.Name == FilterAttachName
                                       orderby x.AttachOrder descending
                                       select x).ToList();
                }
                else
                {
                    AttachmentItems = (from x in attachments
                                       where x.GroupAttachment.GroupName == item.Key
                                       orderby x.AttachOrder descending
                                       select x).ToList();
                }
                
                grupoAttachs.Add(item.Value, AttachmentItems);
            }
        }

        protected virtual IList<System.Net.Mail.Attachment> GetMailAttachmentsGrouped(AbstractEMailDTO dto, Template template, EMailLog log, out string ekitName)
        {
            IList<System.Net.Mail.Attachment> mailAttachments = new List<System.Net.Mail.Attachment>();
            IList<Domain.Attachment> attachments = FindAttachemnts(dto, template);
            List<AttachmentItem> items = new List<AttachmentItem>();

            ekitName = "";
           
            Dictionary<string, GroupAttachment> dicOrden = new Dictionary<string, GroupAttachment>();

            Dictionary<GroupAttachment, List<Domain.Attachment>> grupoAttachs = new Dictionary<GroupAttachment, List<Domain.Attachment>>();

            OrderGroupsAttachs(attachments, ref dicOrden, ref grupoAttachs);

            bool bGroupEVoucher = false;

            if (template.TypeAttachsWithEkit == Template.eTypeAttachsWithEkit.opAttachsWithEkit)
            {
                List<Domain.Attachment> lstAllAttachs = new List<Domain.Attachment>();
                foreach (var item in grupoAttachs)
                {
                    lstAllAttachs.AddRange((List<Domain.Attachment>)item.Value);                    
                }

                List<AttachmentItem> itemsSinGrupo = new List<AttachmentItem>();
                string itemAttachNameSinGrupo = "";
                byte[] buffer = ConvertToMailAttachments(lstAllAttachs, dto, template, log, itemsSinGrupo, ref itemAttachNameSinGrupo);
                GenerateEvoucherWithAttachments(dto, template, buffer, ref mailAttachments, ref items, out ekitName);
            }
            else
            {
                foreach (var item in grupoAttachs)
                {
                    var lstAttachs = ((List<Domain.Attachment>)item.Value);
                    GroupAttachment grupo = ((GroupAttachment)item.Key);

                    if (!bGroupEVoucher) bGroupEVoucher = grupo.IsGroupEVoucher;

                    string itemAttachName = grupo.AttachName(dto.IdLanguage);

                    if (grupo.GroupName == "")
                    {
                        // archivos sueltos sin grupo
                        List<AttachmentItem> itemsSinGrupo = new List<AttachmentItem>();
                        string itemAttachNameSinGrupo = "";
                        byte[] buffer = ConvertToMailAttachments(lstAttachs, dto, template, log, itemsSinGrupo, ref itemAttachNameSinGrupo);

                        foreach (var attachSinGrupo in itemsSinGrupo)
                        {
                            var streamattachs = new MemoryStream((byte[])attachSinGrupo.Content);
                            if (streamattachs.Length > 0)
                            {
                                mailAttachments.Add(new System.Net.Mail.Attachment(streamattachs, attachSinGrupo.Name));
                            }
                        }
                    }
                    else
                    {
                        byte[] buffer = ConvertToMailAttachments(lstAttachs, dto, template, log, items, ref itemAttachName);

                        //archivos agrupados                    
                        if (!grupo.IsGroupEVoucher)
                        {
                            var streamattachs = new MemoryStream(buffer);
                            if (streamattachs.Length > 0)
                            {
                                mailAttachments.Add(new System.Net.Mail.Attachment(streamattachs, itemAttachName));
                            }
                        }
                        else
                        {
                            //grupo evoucher                    
                            GenerateEvoucherWithAttachments(dto, template, buffer, ref mailAttachments, ref items, out ekitName);
                        }
                    }
                }
            }

            return mailAttachments;
        }

        private void GenerateEvoucherWithAttachments(AbstractEMailDTO dto, Template template, byte[] Attachments, ref IList<System.Net.Mail.Attachment> mailAttachments, 
            ref List<AttachmentItem> items, out string ekitName)
        {
            byte[] ekitData = GenerateAttachmentEvoucher(dto, template, out ekitName);

            List<byte[]> attachsBytes = new List<byte[]>();

            attachsBytes.Add(ekitData);
            attachsBytes.Add(Attachments);
                        
            byte[] buffer = MergeFiles(attachsBytes);

            var streamEvoucher = new MemoryStream(buffer);

            mailAttachments.Add(new System.Net.Mail.Attachment(streamEvoucher, ekitName));
        }

        private void GenerateAttachmentEvoucher(AbstractEMailDTO dto, Template template, ref IList<System.Net.Mail.Attachment> mailAttachments, ref List<AttachmentItem> items, out string ekitName)
        {            
            if (template.TypeAttachsWithEkit == Template.eTypeAttachsWithEkit.opAddEkitAttach)
            {
                byte[] ekitData = GenerateAttachmentEvoucher(dto, template, out ekitName);

                var stream = new MemoryStream(ekitData);

                var mailAttachment = new System.Net.Mail.Attachment(stream, ekitName);
                mailAttachments.Insert(0, mailAttachment);
            }
            else if (template.TypeAttachsWithEkit == Template.eTypeAttachsWithEkit.opAttachsWithEkit)
            {
                byte[] ekitData = GenerateAttachmentEvoucher(dto, template, out ekitName);

                List<byte[]> attachsBytes = new List<byte[]>();

                attachsBytes.Add(ekitData);

                foreach (var item in items)
                {
                    attachsBytes.Add((byte[])item.Content);
                }

                byte[] buffer = MergeFiles(attachsBytes);

                var streamEvoucher = new MemoryStream(buffer);

                mailAttachments.Clear();
                mailAttachments.Add(new System.Net.Mail.Attachment(streamEvoucher, ekitName));
            }
            else
                ekitName = "";
        }

        private byte[] ConvertToMailAttachments(IList<Domain.Attachment> lstAttachs, AbstractEMailDTO dto,
            Template template, EMailLog log, List<AttachmentItem> items, ref string attachName)
        {
            byte[] buffer = new byte[0];
            try
            {
                List<byte[]> attachsBytes = new List<byte[]>();

                var ordenadas = from x in lstAttachs orderby x.AttachOrder select x;

                foreach (var attachment in ordenadas)
                {
                    bool bAttachTemplate = false;
                    if (attachment.AttachmentTypeDescripcion.ToUpper() == AttachmentType.STRATEGY.ToString())
                    {            
                        Template attachTemplate=null;
                        if (attachment.AttachmentTemplates != null && attachment.AttachmentTemplates.Count > 0)
                        {
                            //si hay templates asociados a la estrategia es porque son de upgrade
                            var strategyTemplate = attachment.AttachmentTemplates.FirstOrDefault();
                            attachTemplate = TemplateHome.Get(strategyTemplate.IdTemplateAttachment);
                        }
                        else
                        {
                            //busca por producto si hay templates asociados a la estrategia
                            attachTemplate = ServiceLocator.Instance().GetAttachmentService().GetTemplateAttach(attachment.Id, dto, template.Id);
                        }

                        if (attachTemplate != null && attachTemplate.Id > 0)
                        {
                            bAttachTemplate = true;
                            byte[] attachTemplateData = GenerateAttachmentByTemplate(dto, attachTemplate, attachment.Estrategy);

                            AttachmentItem AttachmentTemplateItem = new AttachmentItem()
                            {
                                Name = attachTemplate.GetAttachName(dto.IdLanguage),
                                Content = attachTemplateData
                            };

                            if (string.IsNullOrEmpty(attachName)) attachName = AttachmentTemplateItem.Name;
                            if (string.IsNullOrEmpty(attachName)) attachName = attachTemplate.GetAttachName(dto.IdLanguage);
                            
                            log.AttachmentIds += attachment.Id.ToString() + ",";
                            attachsBytes.Add((byte[])AttachmentTemplateItem.Content);

                            items.Add(AttachmentTemplateItem);
                        }
                    }

                    if (!bAttachTemplate)
                    {
                        if (attachment.AttachmentTypeDescripcion.ToUpper() == AttachmentType.STRATEGY.ToString())
                        {
                            attachment.AttachmentContentPDF = FindContentAttachmentPDF(template.Id, attachment.Id, dto);
                        }

                        var itemsAttach = ServiceLocator.Instance().
                            GetAttachmentService().GetItems(attachment, dto);

                        foreach (AttachmentItem item in itemsAttach)
                        {
                            log.AttachmentIds += item.Id.ToString() + ",";

                            if (string.IsNullOrEmpty(attachName)) attachName = item.Name;

                            attachsBytes.Add((byte[])item.Content);

                            items.Add(item);
                        }
                    }                 
                }
                if (attachsBytes.Count > 0)
                    buffer = MergeFiles(attachsBytes);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return buffer;
        }

        public byte[] GenerateAttachmentEvoucher(AbstractEMailDTO dto, Template template, out string ekitName)
        {
            string sEmailBody = dto.EMailBody;
            int idTemplatePDF = 0;

            var ekitStrategy = new Strategies.Attachment.EKitAttachStrategy();

            int.TryParse(template.IdTemplatePDF.ToString(), out idTemplatePDF);

            if (idTemplatePDF > 0)
            {
                Template templatePDF = TemplateHome.Get(idTemplatePDF);
                sEmailBody = ProccessContextData(dto, ProccessData(dto, templatePDF));
            }
            else
                sEmailBody = ProccessContextData(dto, ProccessData(dto, template));

            ekitName = (!string.IsNullOrEmpty(template.GetContent(dto.IdLanguage).EVoucherName) ? template.GetContent(dto.IdLanguage).EVoucherName : ekitStrategy.GetAttachName());

            return ekitStrategy.GetAttachmentEkit(sEmailBody, dto.IdLanguage);
        }

        public virtual byte[] GenerateAttachmentByTemplate(AbstractEMailDTO dto)
        {
            var ekitStrategy = new Strategies.Attachment.AttachmentTemplate();
            Template template = GetTemplate(dto);
            Estrategy estrategy = EstrategyHome.Get(dto.IdStrategy);
            var dtoAttachTemplate = ekitStrategy.ConvertTo(dto, estrategy);
            string sEmailBody = ProccessContextData(dto, ProccessData(dto, template));
            return ekitStrategy.GetAttachmentEkit(sEmailBody);
        }

        private byte[] GenerateAttachmentByTemplate(AbstractEMailDTO dto, Template attachTemplate, Estrategy estrategy)
        {
            var ekitStrategy = new Strategies.Attachment.AttachmentTemplate();
            string sEmailBody = "";

            AbstractEMailDTO dtoAttachTemplate = dto;
            if (attachTemplate.TemplateType.Id == TemplateTypeHome.GetDynamic().Id)
            {
                dtoAttachTemplate = ekitStrategy.ConvertTo(dto, estrategy);
                
                ((DynamicDTO)dtoAttachTemplate).StrategyData = estrategy.Code;
                ((DynamicDTO)dtoAttachTemplate).IdLanguage = dto.IdLanguage;
                ServiceLocator.Instance().GetInformationDynamicServices().CompleteInformation(dtoAttachTemplate);

                var EMailDynamicSender = EMailSenderStrategyLocator.Instance().GetEMailDynamicSender();
                sEmailBody = EMailDynamicSender.ProccessContextData(dtoAttachTemplate, EMailDynamicSender.ProccessData(dtoAttachTemplate, attachTemplate));
            }
            else
                sEmailBody = ProccessContextData(dtoAttachTemplate, ProccessData(dtoAttachTemplate, attachTemplate));
         
            return ekitStrategy.GetAttachmentEkit(sEmailBody);
        }

        protected virtual IList<ContentAttachment> FindContentAttachmentPDF(int IdTemplate, int IdAttachment, AbstractEMailDTO dto)
        {
            var contentsPDF = AttachmentHome.FindAttachmentContentPDF(IdTemplate, IdAttachment, dto.IdLanguage);
            foreach (var item in contentsPDF)
            {
                item.Body = ProccessContextData(dto, item.Body);
            }

            return contentsPDF;
        }
        
  
        private string Stream(string p)
        {
            throw new NotImplementedException();
        }

        /**
         * Replace the signature, contacts, links and other variable text of 
         * the template's body that are stored in our data base.
         **/
        protected virtual string ProccessData(AbstractEMailDTO dto, Template template)
        {
            return ServiceLocator.Instance().GetTemplateService().ParseBody(dto, template);
        }

        protected virtual void Proccess(AbstractEMailDTO dto, Template template, EMailLog log)
        {
            string attachName = "";
            EMailAddress fromAddress = EMailAddressHome.Get(template.IdEMailFromAddress);
            log.MailFrom = fromAddress.Address != null ? fromAddress.Address : "-";
            log.MailTo = dto.To != null ? dto.To : dto.Bcc;          
            log.ErrorMessage = "";

            try
            {
                if (template.Id != 0)
                {
                    dto.EMailBody = ProccessContextData(dto, ProccessData(dto, template));
                    dto.Header = (byte[])template.GetContent(dto.IdLanguage).Header.Content;
                    dto.Footer = (byte[])template.GetContent(dto.IdLanguage).Footer.Content;
                    dto.HeaderPDF = (byte[])template.GetContent(dto.IdLanguage).HeaderPDF.Content;
                    dto.FooterPDF = (byte[])template.GetContent(dto.IdLanguage).FooterPDF.Content;
                    dto.color = template.GetContent(dto.IdLanguage).Color;
                    log.Subject = template.GetSubject(dto.IdLanguage);
                    log.Body = dto.EMailBody;
                    if (dto.MailFromAppearance==null || dto.MailFromAppearance == "[FROMADDRESSNAME]")
                        dto.MailFromAppearance = (fromAddress.Address!= null ? fromAddress.Name : string.Empty);

                    ServiceLocator.Instance().GetEMailLogService().Tracking(dto, log, template.Id);

                    var rst = MessagingRst.SendMailThread(fromAddress.Address != null ? fromAddress.Address : "-",
                        dto.To, dto.Cc, dto.Bcc, template.GetSubject(dto.IdLanguage),
                        dto.EMailBody, GetMailAttachments(dto, template, log, out attachName), dto.MailFromAppearance != null ? dto.MailFromAppearance : string.Empty);

                    bool sentMail = rst.bSendMail;
                    
                        /*
                    bool sentMail = Messaging.SendMailThread(fromAddress.Address != null ? fromAddress.Address : "-", 
                        dto.To, dto.Cc, dto.Bcc, template.GetSubject(dto.IdLanguage),
                        dto.EMailBody, GetMailAttachments(dto, template, log, out attachName), dto.MailFromAppearance != null ? dto.MailFromAppearance : string.Empty);
                        */

                    log.ErrorMessage = sentMail ? "" : "Error in mail - To: " + dto.To;

                    if (!sentMail && rst.Error != null) log.ErrorMessage += rst.Error.Message + " - " + rst.Error.StackTrace;
                }

                ServiceLocator.Instance().GetEMailLogService().FinishLog(template, log);
                var asd = GetMailAttachments(dto, template, log, out attachName);

                //var rst = PdfUtils.GetPdf();

            }
            catch (Exception ex)
            {
                log.ErrorMessage = ex.Message + " - " + ex.StackTrace;
                ServiceLocator.Instance().GetEMailLogService().FinishLog(template, log);
                throw ex;
            }
        }

        protected virtual AbstractEMailDTO ProccessDynamicTemplate(AbstractEMailDTO dto, Template template, EMailLog log)
        {
            string attachName = "";
            AbstractEMailDTO result = null;
            EMailAddress fromAddress = EMailAddressHome.Get(template.IdEMailFromAddress);
            log.MailFrom = fromAddress.Address != null ? fromAddress.Address : "-";
            log.MailTo = dto.To != null ? dto.To : dto.Bcc;
            log.ErrorMessage = "";

            try
            {
                if (template.Id != 0)
                {
                    dto.EMailBody = ProccessContextData(dto, ProccessData(dto, template));
                    dto.Header = (byte[])template.GetContent(dto.IdLanguage).Header.Content;
                    dto.Footer = (byte[])template.GetContent(dto.IdLanguage).Footer.Content;
                    dto.HeaderPDF = (byte[])template.GetContent(dto.IdLanguage).HeaderPDF.Content;
                    dto.FooterPDF = (byte[])template.GetContent(dto.IdLanguage).FooterPDF.Content;
                    dto.color = template.GetContent(dto.IdLanguage).Color;
                    log.Subject = template.GetSubject(dto.IdLanguage);
                    log.Body = dto.EMailBody;
                    if (dto.MailFromAppearance == null || dto.MailFromAppearance == "[FROMADDRESSNAME]")
                        dto.MailFromAppearance = (fromAddress.Address != null ? fromAddress.Name : string.Empty);

                    ServiceLocator.Instance().GetEMailLogService().Tracking(dto, log, template.Id);
                }

                ServiceLocator.Instance().GetEMailLogService().FinishLog(template, log);

                result = dto;
            }
            catch (Exception ex)
            {
                log.ErrorMessage = ex.Message + " - " + ex.StackTrace;
                ServiceLocator.Instance().GetEMailLogService().FinishLog(template, log);
                result = null;
            }

            return result;
        }

        protected virtual IList<System.Net.Mail.Attachment> ProccessAttachment(AbstractEMailDTO dto, Template template, EMailLog log, out string attachName)
        {
            attachName = "";
            EMailAddress fromAddress = EMailAddressHome.Get(template.IdEMailFromAddress);
            log.MailFrom = fromAddress.Address != null ? fromAddress.Address : "-";
            log.MailTo = dto.To;
            log.ErrorMessage = "";
            try
            {
                if (template.Id != 0)
                {
                    dto.EMailBody = ProccessContextData(dto, ProccessData(dto, template));
                    dto.Header = (byte[])template.GetContent(dto.IdLanguage).Header.Content;
                    dto.Footer = (byte[])template.GetContent(dto.IdLanguage).Footer.Content;
                    dto.color = template.GetContent(dto.IdLanguage).Color;
                    dto.HeaderPDF = (byte[])template.GetContent(dto.IdLanguage).HeaderPDF.Content;
                    dto.FooterPDF = (byte[])template.GetContent(dto.IdLanguage).FooterPDF.Content;
                    log.Subject = template.GetSubject(dto.IdLanguage);
                    log.Body = dto.EMailBody;

                    return GetMailAttachments(dto, template, log, out attachName);
                }
                return new List<System.Net.Mail.Attachment>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected virtual IList<System.Net.Mail.Attachment> ProccessAttachment(AbstractEMailDTO dto, Template template, EMailLog log, FiltersAttachsDTO filtersAttachs)
        {
            EMailAddress fromAddress = EMailAddressHome.Get(template.IdEMailFromAddress);
            log.MailFrom = fromAddress.Address != null ? fromAddress.Address : "-";
            log.MailTo = dto.To;
            log.ErrorMessage = "";
            try
            {
                if (template.Id != 0)
                {
                    dto.EMailBody = ProccessContextData(dto, ProccessData(dto, template));
                    dto.Header = (byte[])template.GetContent(dto.IdLanguage).Header.Content;
                    dto.Footer = (byte[])template.GetContent(dto.IdLanguage).Footer.Content;
                    dto.color = template.GetContent(dto.IdLanguage).Color;
                    dto.HeaderPDF = (byte[])template.GetContent(dto.IdLanguage).HeaderPDF.Content;
                    dto.FooterPDF = (byte[])template.GetContent(dto.IdLanguage).FooterPDF.Content;
                    log.Subject = template.GetSubject(dto.IdLanguage);
                    log.Body = dto.EMailBody;

                    return GetMailAttachments(dto, template, log, filtersAttachs);
                }
                return new List<System.Net.Mail.Attachment>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
