using System;
using System.Collections.Generic;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Strategies.EMailSender
{
    public abstract class EMailSenderDefault : IEMailSenderStrategy
    {
        public IDAOTemplate DaoTemplate;

        protected override AbstractEMailDTO GetDto(string xml)
        {
            throw new Exception("");
        }

        protected override Template GetTemplate(AbstractEMailDTO dto)
        {
            var iTemplate = DaoTemplate.Find(dto.TemplateType.Id);

            return GetMaxHierarchyTemplate(iTemplate);
        }

        protected virtual Template GetMaxHierarchyTemplate(IEnumerable<Template> iMaxTemplate)
        {
            var maxHierarchy = 0;
            var maxHierarchyTemplate = new Template();

            foreach (var template in iMaxTemplate)
            {
                if (template.Hierarchy >= maxHierarchy)
                {
                    maxHierarchy = template.Hierarchy;
                    maxHierarchyTemplate = template;
                }
            }

            return maxHierarchyTemplate;
        }

        protected override IList<System.Net.Mail.Attachment> GetMailAttachments(AbstractEMailDTO dto, Template template, EMailLog log, out string attachName)
        {
            attachName = "";
            return new List<System.Net.Mail.Attachment>();
        }

        protected override IList<Domain.Attachment> FindAttachemnts(AbstractEMailDTO dto, Template template)
        {
            return new List<Domain.Attachment>();
        }

        protected override void CompleteDto(AbstractEMailDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}