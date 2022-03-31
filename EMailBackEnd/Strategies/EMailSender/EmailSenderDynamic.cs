using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;
using CapaNegocioDatos.CapaHome;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Properties;
using EMailAdmin.BackEnd.Service;
using System.Diagnostics;
using EMailAdmin.BackEnd.Domain;
using System.IO;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Strategies.EMailSender
{
    public class EmailSenderDynamic : EMailSenderDefault
    {
        protected override void CompleteDto(DTO.AbstractEMailDTO dto)
        {
            DynamicDTO dtoDy = ((DynamicDTO)dto);
            if (dtoDy.CultureUI != null)
            {
                CultureInfo cuInfo = CultureInfo.GetCultureInfo(dtoDy.CultureUI);
                dto.IdLanguage = IdiomaHome.ObtenerPorCultura(cuInfo.TwoLetterISOLanguageName).IdIdioma;
            }

            if (!string.IsNullOrEmpty(dtoDy.EmailListCode))
                dto.To = GetEMails(dtoDy.EmailListCode);

            if (!string.IsNullOrEmpty(dtoDy.StrategyData))
                ServiceLocator.Instance().GetInformationDynamicServices().CompleteInformationMore(dto);
        }

        protected override Domain.Template GetTemplate(AbstractEMailDTO dto)
        {
             DynamicDTO dtoDy = ((DynamicDTO)dto);
             if (!string.IsNullOrEmpty(dtoDy.StrategyData))
             {
                 var estrategy = EstrategyHome.GetByCode(dtoDy.StrategyData);

                 return TemplateHome.Get(estrategy.IdTemplate);
             }

             if (!string.IsNullOrEmpty(dtoDy.TemplateCode))
             {
                 var iTemplate = TemplateHome.Find(dtoDy.TemplateType.Id, dtoDy.TemplateCode, false);
                 return GetMaxHierarchyTemplate(iTemplate);
             }

             return null;
        }

        private string GetEMails(string EmailListCode)
        {
            var usus = EMailListHome.FindUsersMailList(-1, EmailListCode);
            string emails = "";

            foreach (var emailUsu in usus)
            {
                if (emails != "") emails += ",";
                emails += emailUsu.CorreoElectronico;
            }
            return emails;
        }

        /*
        protected override string ProccessContextData(AbstractEMailDTO dto, string body)
        {
            DynamicDTO dtoDynamic = ((DynamicDTO)dto);
            
            var variableInitTag = ConfigurationValueHome.GetByCode("VarDicInitTag").Value;
            var variableEndTag = ConfigurationValueHome.GetByCode("VarDicEndTag").Value;

            IList<string> textsToReplace = dtoDynamic.dicValues.Select(x=>x.Key).ToList<string>();

            foreach (var kv in dtoDynamic.dicValues)
            {
                var tag = variableInitTag + kv.Key + variableEndTag;
                body = body.Replace(tag, kv.Value.ToString());
            }

            return body;
        }
         */
        
        protected override string ProccessContextData(AbstractEMailDTO dto, string body)
        {
            try
            {
                if (dto.GetType() == typeof(DynamicDTO))
                {
                    ProcessContextData(dto, ConfigurationValueHome.GetByCode("VarDicInitTag").Value, ConfigurationValueHome.GetByCode("VarDicEndTag").Value, ref body);

                    ProcessContextData(dto, Settings.Default["VariableInitTag"].ToString(), Settings.Default["VariableEndTag"].ToString(), ref body);
                }
            }
            catch (Exception ex)
            {
                {
                    Debug.Write(ex.Message);
                }
            }
            return body;
        }

        private void ProcessContextData(AbstractEMailDTO dto, string variableInitTag, string variableEndTag, ref string body)
        {
            try
            {
                DynamicDTO dtoDynamic = ((DynamicDTO)dto);

                foreach (var kv in dtoDynamic.dicValues)
                {
                    if (!string.IsNullOrEmpty(kv.Key))
                    {
                        var tag = variableInitTag + kv.Key + variableEndTag;
                        
                        body = body.Replace(tag, (kv.Value == null ? "" : kv.Value.ToString()));

                        //si lo puso con el configurador trae un espacio
                        tag = variableInitTag + kv.Key +" "+ variableEndTag.Trim();
                        body = body.Replace(tag, (kv.Value == null ? "" : kv.Value.ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                {
                    Debug.Write(ex.Message);
                }
            }
        }

        protected override IList<Domain.Attachment> FindAttachemnts(AbstractEMailDTO dto, Template template)
        {
            IList<Domain.Attachment> attachments = DAOLocator.Instance().GetDaoAttachment().
                FindByTemplateId(template.Id);
            return attachments;
        }

        protected override IList<System.Net.Mail.Attachment> GetMailAttachments(AbstractEMailDTO dto, Template template, EMailLog log, out string attachName)
        {
            if (ConfigurationValueHome.ValidaHabilitarMailAttachments_Grouped())
                return GetMailAttachmentsGrouped(dto, template, log, out attachName);
            else
                return GetMailAttachmentsOriginal(dto, template, log, out attachName);           
        }

        // attachName = "";
        //    IList<System.Net.Mail.Attachment> mailAttachments = new List<System.Net.Mail.Attachment>();
        //    IList<Domain.Attachment> attachments = FindAttachemnts(dto, template);
        //    log.AttachmentIds = "";

        //    try
        //    {
        //        foreach (Domain.Attachment attachment in attachments)
        //        {
        //            IList<AttachmentItem> items= new List<AttachmentItem>();
        //            if (attachment.AttachmentTypeDescripcion.ToUpper() == AttachmentType.STRATEGY.ToString())
        //            {
        //                Template attachTemplate = null;
        //                if (attachment.AttachmentTemplates != null && attachment.AttachmentTemplates.Count > 0)
        //                {
        //                    var strategyTemplate = attachment.AttachmentTemplates.FirstOrDefault();
        //                    attachTemplate = TemplateHome.Get(strategyTemplate.IdTemplateAttachment);

        //                    if (attachTemplate != null && attachTemplate.Id > 0)
        //                    {
        //                        byte[] attachTemplateData = GenerateAttachmentByTemplate(dto, attachTemplate, attachment.Estrategy);

        //                        AttachmentItem AttachmentTemplateItem = new AttachmentItem()
        //                        {
        //                            Name = attachTemplate.GetAttachName(dto.IdLanguage),
        //                            Content = attachTemplateData
        //                        };

        //                        if (string.IsNullOrEmpty(attachName)) attachName = AttachmentTemplateItem.Name;
        //                        if (string.IsNullOrEmpty(attachName)) attachName = attachTemplate.GetAttachName(dto.IdLanguage);

        //                        log.AttachmentIds += attachment.Id.ToString() + ",";
        //                        items.Add(AttachmentTemplateItem);
        //                    }
        //                }
        //                else
        //                    items = attachment.Estrategy.GetStrategy().GetAttachmentItems(dto);
        //            }
        //            else
        //            {
        //                items = ServiceLocator.Instance().GetAttachmentService().GetItems(attachment, dto);
        //            }

        //            foreach (AttachmentItem item in items)
        //            {
        //                log.AttachmentIds += item.Id.ToString() + ",";
        //                var stream = new MemoryStream((byte[])item.Content);
        //                var mailAttachment = new System.Net.Mail.Attachment(stream, item.Name);
        //                mailAttachment.NameEncoding = Encoding.UTF8;
        //                mailAttachments.Add(mailAttachment);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return mailAttachments;
        //}

        private byte[] GenerateAttachmentByTemplate(AbstractEMailDTO dto, Template attachTemplate, Estrategy estrategy)
        {
            var ekitStrategy = new Strategies.Attachment.AttachmentTemplate();
            string sEmailBody = "";

            AbstractEMailDTO dtoAttachTemplate = dto;
            
            sEmailBody = ProccessContextData(dtoAttachTemplate, ProccessData(dtoAttachTemplate, attachTemplate));

            return ekitStrategy.GetAttachmentEkit(sEmailBody);
        }

    }
}
