using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Exceptions;
using EMailAdmin.BackEnd.Service.Interfaces;
using EMailAdmin.BackEnd.Domain;
using System.Collections.Generic;
using EMailAdmin.BackEnd.DTO;
using System.Linq;
using System;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.BackEnd.Service
{
    public class AttachmentService : IAttachmentService
    {
        public IDAOAttachment DaoAttachment { get; set; }

        public void Save(Attachment attachment)
        {
            if (attachment.Name != "")
            {
                DaoAttachment.Persistir(attachment);
            }
            else
            {
                throw new NonSavedObjectException("Attachment not saved");
            }
        }

        public void Delete(Attachment attachment)
        {
            if (attachment.Id != 0)
            {
                DaoAttachment.Eliminar(attachment);
            }
            else
            {
                throw new NonEliminatedObjectException("Attachment not deleted");
            }
        }

        public void SaveAssociations(IList<Attachment_R_Group> items)
        {
            if (items != null && items.Count > 0)
            {
                DAOLocator.Instance().GetDaoAttachment_R_Group().DeleteByIdAttachment(items[0].IdAttachment);
                foreach (Attachment_R_Group attachmentRGroup in items)
                {
                    DAOLocator.Instance().GetDaoAttachment_R_Group().Crear(attachmentRGroup);
                }
            }
        }

        public Template GetTemplateAttach(int IdAttachment, AbstractEMailDTO dto, int IdMainTemplate)
        {
            var template = GetAttachmentTemplates(IdMainTemplate, IdAttachment, dto);
            return template;
        }

        public Template GetTemplateAttach(int IdAttachment, AbstractEMailDTO dto, Template MainTemplate)
        {
            if (MainTemplate.IsDynamic)
                return GetAttachmentTemplatesDynamic(MainTemplate.Id, IdAttachment, ((DynamicDTO) dto));
            else
                return GetTemplateAttach(IdAttachment, dto, MainTemplate.Id);
        }

        public IList<AttachmentItem> GetItems(Attachment attachment, AbstractEMailDTO dto)
        {           
            if (attachment.AttachmentType.Codigo == AttachmentType.FIXED)
            {
                IList<AttachmentItem> items = new List<AttachmentItem>();

                foreach (AttachmentItem item in attachment.AttachmentItems)
                {
                    if (item.Language.Id == dto.IdLanguage)
                    {
                        items.Add(item);
                    }
                }

                return items;
            }

            dto.IdStrategy = attachment.Estrategy.Id;

            if (attachment.AttachmentContentPDF != null && attachment.AttachmentContentPDF.Count>0)
            {
                dto.XMLContentAttachment = attachment.GetXMLAttachmentContentPDF();
            }
            return attachment.Estrategy.GetStrategy().GetAttachmentItems(dto);
        }

        #region AttachTemplates
        /// <summary>
        /// el attach puede tener templates asociados
        /// busca el que corresponde a los datos segun los grupos asociados a cada uno
        /// </summary>
        /// <param name="IdTemplate"></param>
        /// <param name="IdAttachment"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        ///
        protected Template GetAttachmentTemplates(int IdTemplate, int IdAttachment, AbstractEMailDTO dto)
        {
            IList<Group_R_Template> iGroupsRTemplate = DAOLocator.Instance().GetDaoGroup_R_Template().Find(IdTemplate, IdAttachment,
                dto.AssociationGroupDto.IdLocation, dto.AssociationGroupDto.IdAccount,
                dto.AssociationGroupDto.IdProduct, dto.AssociationGroupDto.IdRate, dto.AssociationGroupDto.IdDistributionType,
                dto.Date == null ? DateTime.Now : Convert.ToDateTime(dto.Date), dto.Module.Id,
                GroupTypeHome.TemplateGroup().Id, true);

            return GetMaxHierarchyTemplate(GetMaxWeightTemplates(iGroupsRTemplate));
        }

        protected Template GetAttachmentTemplatesDynamic(int IdTemplate, int IdAttachment, DynamicDTO dto)
        {
            IList<Group_R_Template> iGroupsRTemplate = new List<Group_R_Template>();
            
            var lst =  DAOLocator.Instance().GetDaoEstrategyAttachmentTemplate().FindAttachmentTemplates(IdTemplate, IdAttachment);
            foreach (var item in lst)
            {
                IList<Group_R_Template> iGroupsRTemplateAttachment = DAOLocator.Instance().GetDaoGroup_R_Template().FindByTemplateId(item.IdTemplateAttachment);
                foreach (var itemGroupTA in iGroupsRTemplateAttachment)
                {
                    if (AttachmentTemplateDynamic_ValidateGroups(itemGroupTA, dto))
                    {
                        iGroupsRTemplate.Add(itemGroupTA);
                    }
                }
            }

            return GetMaxHierarchyTemplate(GetMaxWeightTemplates(iGroupsRTemplate));

        }

        private bool AttachmentTemplateDynamic_ValidateGroups(Group_R_Template itemGroup, DynamicDTO dto)
        {
            IList<GroupCondition> lstGroupConditions = GroupConditionsHome.FindByIdGroupWithValues(itemGroup.IdGroup);
            return dto.ValidGroupConditions(lstGroupConditions);
        }


        protected virtual IList<Group_R_Template> GetMaxWeightTemplates(IList<Group_R_Template> iGroupsRTemplate)
        {
            int maxWeight = 0;
            IList<Group_R_Template> iMaxGroupsRTemplate = new List<Group_R_Template>();
            foreach (Group_R_Template groupRTemplate in iGroupsRTemplate)
            {
                Group group = DAOLocator.Instance().GetDaoGroup().Get(groupRTemplate.IdGroup, true);
                if (group.TotalWeight > maxWeight)
                {
                    maxWeight = group.TotalWeight;
                    iMaxGroupsRTemplate.Clear();
                    iMaxGroupsRTemplate.Add(groupRTemplate);
                }
                else
                {
                    if (group.TotalWeight == maxWeight)
                    {
                        iMaxGroupsRTemplate.Add(groupRTemplate);
                    }
                }
            }
            return iMaxGroupsRTemplate;
        }

        /**
         * Looks for the template with the maximun hierarchy of the list. 
         **/
        protected virtual Template GetMaxHierarchyTemplate(IList<Group_R_Template> iMaxGroupsRTemplate)
        {
            int IdTemplateResult = 0;
            int maxHierarchy = 0;
            Template maxHierarchyTemplate = new Template();

            foreach (Group_R_Template groupRTemplate in iMaxGroupsRTemplate)
            {
                groupRTemplate.Template = DAOLocator.Instance().GetDaoTemplate().GetHierarchy(groupRTemplate.IdTemplate);
                if (groupRTemplate.Template.Hierarchy >= maxHierarchy)
                {
                    maxHierarchy = groupRTemplate.Template.Hierarchy;
                    IdTemplateResult = groupRTemplate.IdTemplate;
                }
            }
            if (IdTemplateResult > 0)
                maxHierarchyTemplate = DAOLocator.Instance().GetDaoTemplate().Get(IdTemplateResult);
            return maxHierarchyTemplate;
        }
        #endregion    
    

     
    }
}
