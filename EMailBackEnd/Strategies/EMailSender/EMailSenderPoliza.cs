using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Service;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using EMailAdmin.BackEnd.Properties;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Strategies.EMailSender
{
    public class EMailSenderPoliza : EMailSenderDefault
    {
        protected override void CompleteDto(AbstractEMailDTO dto)
        {
            ServiceLocator.Instance().GetSendMailPolizaServices().CompleteInformation(dto);            
        }

        protected override Template GetTemplate(AbstractEMailDTO dto)
        {
            IList<Group_R_Template> iGroupsRTemplate = DAOLocator.Instance().GetDaoGroup_R_Template().Find(
                dto.TemplateType.Id, dto.AssociationGroupDto.IdLocation, dto.AssociationGroupDto.IdAccount,
                dto.AssociationGroupDto.IdProduct, dto.AssociationGroupDto.IdRate, dto.AssociationGroupDto.IdDistributionType,
                dto.Date == null ? DateTime.Now : Convert.ToDateTime(dto.Date), dto.Module.Id,
                GroupTypeHome.TemplateGroup().Id);

            return GetMaxHierarchyTemplate(GetMaxWeightTemplates(iGroupsRTemplate));
        }

        /**
         * Returns list, of the relationship between the group and template, filtered by the max weight. 
         **/
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

        private String SerializeObject(Object pObject)
        {
            try
            {
                var memoryStream = new MemoryStream();
                var xs = new XmlSerializer(pObject.GetType());
                var xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                xs.Serialize(xmlTextWriter, pObject);
                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
                string xmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
                return xmlizedString;
            }
            catch (Exception e) { Console.WriteLine(e); return null; }
        }

        private static String UTF8ByteArrayToString(Byte[] characters)
        {
            var encoding = new UTF8Encoding();
            var constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        protected override string ProccessContextData(AbstractEMailDTO dto, string body)
        {
            string xml = SerializeObject((EmailPolizaDTO)dto);

            var xDoc = new XmlDocument();
            xDoc.LoadXml(xml.Substring(xml.IndexOf("?>") + 2));

            var variableInitTag = Settings.Default["VariableInitTag"].ToString();
            var variableEndTag = Settings.Default["VariableEndTag"].ToString();

            IList<string> textsToReplace = ServiceLocator.Instance().GetTemplateService().GetVariableTags(body);

            foreach (string textToReplace in textsToReplace)
            {
                string tag = textToReplace.Replace(variableInitTag, "").Replace(variableEndTag, "");
                XmlNodeList nodes = xDoc.GetElementsByTagName(tag.Trim());

                body = body.Replace(textToReplace, nodes.Count == 0 ? textToReplace : nodes[0].InnerText);
            }

            return body;
        }

        public override bool AgencySendEmails(AbstractEMailDTO dto)
        {
            EmailPolizaDTO ekitDto = (EmailPolizaDTO)dto;
            bool isExclude = EMailListExcludeHome.IsExclude(ekitDto.CountryCode, ekitDto.AgencyCode, ekitDto.BranchNumber);
            return !isExclude;
        }
    }
}