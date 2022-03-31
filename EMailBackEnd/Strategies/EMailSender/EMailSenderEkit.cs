using System.Collections.Generic;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data;
using CapaNegocioDatos.Utilitarios;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Properties;
using EMailAdmin.BackEnd.Service;
using System.Xml;
using System;
using CapaNegocioDatos.CapaHome;
using CapaNegocioDatos.CapaNegocio;
using EMailAdmin.BackEnd.Utils;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using System.Configuration;
using LibreriaUtilitarios;
using System.Linq;

namespace EMailAdmin.BackEnd.Strategies.EMailSender
{
    public class EMailSenderEkit : IEMailSenderStrategy
    {
        public IDAOTemplate DaoTemplate { get; set; }

        #region EKit Strategy Methods

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

        protected virtual bool ExistsAttachment(Domain.Attachment attachment, IList<Domain.Attachment> attachments)
        {
            bool exists = false;

            foreach (Domain.Attachment oldAttach in attachments)
            {
                if (attachment.AttachmentType.Id == AttachmentTypeHome.Fixed().Id)
                {
                    exists |= oldAttach.Id == attachment.Id;
                }
                else
                {
                    exists |= (oldAttach.Estrategy != null && attachment.Estrategy != null && oldAttach.Estrategy.Id == attachment.Estrategy.Id);
                }
            }

            return exists;
        }

        protected virtual void AddUpgradeAttachments(AbstractEMailDTO dto, Template template, 
            EMailEKitUpgradeDTO upgradeDto, IList<Domain.Attachment> attachments)
        {
            IList<Domain.Attachment> upgradeAttachments = DAOLocator.Instance().GetDaoAttachment().Find(
                    template.Id, dto.Module.Id, GroupTypeHome.AttachmentGroup().Id,
                    upgradeDto.IdLocation, upgradeDto.IdUpgrade, upgradeDto.IdUpgradeRate, dto.AssociationGroupDto.IdAccount);

            foreach (Domain.Attachment attach in upgradeAttachments)
            {
                if (!ExistsAttachment(attach, attachments))
                {
                    //busca si hay templates configurados para el html del adjunto segun los datos del upgrade
                    attach.AttachmentTemplates = DAOLocator.Instance().GetDaoEstrategyAttachmentTemplate().
                        FindAttachmentTemplates(template.Id, attach.Id);

                    attachments.Add(attach);
                }
            }
        }

        //protected override IList<Domain.Attachment> FindAttachemnts(AbstractEMailDTO dto, Template template, string findAttach)
        //{
        //    var lst = FindAttachemnts(dto, template);

        //    var lstAttachFilter = lst.FirstOrDefault(x => x.Name == findAttach);

        //    var _lst = new List<Domain.Attachment>();
        //    if (lstAttachFilter != null)
        //        _lst.Add(lstAttachFilter);
        //    return _lst;
        //}

        protected override IList<Domain.Attachment> FindAttachemnts(AbstractEMailDTO dto, Template template)
        {          
            IList<Domain.Attachment> attachments = DAOLocator.Instance().GetDaoAttachment().Find(
                template.Id, dto.Module.Id, GroupTypeHome.AttachmentGroup().Id,
                dto.AssociationGroupDto.IdLocation, dto.AssociationGroupDto.IdProduct, dto.AssociationGroupDto.IdRate, dto.AssociationGroupDto.IdAccount);

            foreach (var item in attachments)
            {
                var Template_R_Attachment = DAOLocator.Instance().GetDaoTemplate_R_Attachment().FindByTemplateAttach(template.Id, item.Id);
                if (Template_R_Attachment.Id > 0)
                {
                    item.AttachOrder = Template_R_Attachment.Attachment.AttachOrder;
                    item.GroupAttachment = Template_R_Attachment.Attachment.GroupAttachment;
                }
            }

            if (dto.IdAttachmentType != 0)
            {
                IList<Domain.Attachment> myAttachments = new List<Domain.Attachment>();
                foreach (Domain.Attachment att in attachments)
                {
                    if (att.AttachmentType.Id == dto.IdAttachmentType)
                    {                        
                        myAttachments.Add(att);
                    }
                }

                attachments = myAttachments;
            }

            IList<EMailEKitUpgradeDTO> upgrades = ((EMailEkitDTO)dto).Upgrades != null ? 
                ((EMailEkitDTO)dto).Upgrades : new EMailEKitUpgradeDTO[1];

            foreach (EMailEKitUpgradeDTO upgrade in upgrades)
            {
                AddUpgradeAttachments(dto, template, upgrade, attachments);                
            }            

            return attachments;
        }

        #endregion

        #region IEMailSenderStrategy Members

        private static String UTF8ByteArrayToString(Byte[] characters)
        {
            var encoding = new UTF8Encoding();
            var constructedString = encoding.GetString(characters);
            return (constructedString);
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

        protected override string ProccessContextData(AbstractEMailDTO dto, string body)
        {
            string xml = SerializeObject((EMailEkitDTO) dto);

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

        private void ValidateCodes(EMailEkitDTO ekitDto)
        {
            int voucherCode = 0;
            Int32.TryParse(ekitDto.VoucherCode, out voucherCode);

            if (ekitDto.CountryCode == 0 || voucherCode == 0)
            {
                throw new Exception("Code error for: " + ekitDto.CountryCode + " " + ekitDto.VoucherCode);
            }
        }

        protected override void CompleteDto(AbstractEMailDTO dto)
        {
            EMailEkitDTO ekitDto = (EMailEkitDTO)dto;

            ValidateCodes(ekitDto);
            if (dto.To != null && dto.To.Length != 0)
            {
                ekitDto.GivenToAddress = true;
                ekitDto.To = dto.To;
            }
            //ServiceLocator.Instance().GetInformationService().CompleteInformation(dto);
            ServiceLocator.Instance().GetInformationService().CompleteInformationMore(dto);

            if (ekitDto.Log != null)
            {
                ServiceLocator.Instance().GetEMailLogService().
                    SaveLog(dto, ekitDto.Log, EMailLog.EXTERNALINFOCOMPLETE);
            }

            ekitDto.TemplateType = TemplateTypeHome.GetEkit();
            ekitDto.Module = ModuloHome.ObtenerPorNombre(dto.ModuleCode);
            if (ConfigurationManager.AppSettings["EkitMailFromAppearance"]!=null) ekitDto.MailFromAppearance = ConfigurationManager.AppSettings["EkitMailFromAppearance"].ToString();
            ekitDto.AssociationGroupDto = new AssociationGroupDTO();
            Pais country = PaisHome.ObtenerPorCodigo(ekitDto.CountryCode.ToString());
            ekitDto.AssociationGroupDto.IdLocation = country.IdLocacion;

            Sucursal branch = SucursalHome.Obtener(dto.CountryCode.ToString(), ekitDto.AgencyCode, ekitDto.BranchNumber);
            ekitDto.Sucursal = new SucursalDTO(branch);
            ekitDto.AssociationGroupDto.IdAccount = branch.Id;
            if (dto.IdLanguage != 0)
                ekitDto.IdLanguage = dto.IdLanguage;
            else
                ekitDto.IdLanguage = branch.IdIdioma == 0 ? 1 : branch.IdIdioma;
            ekitDto.AssociationGroupDto.IdProduct = ProductHome.Get(ekitDto.CountryCode.ToString(), ekitDto.ProductCode, Product.PRODUCT).Id;
            try
            {
                ekitDto.Underwritter = InsuranceCompanyHome.Get(ekitDto.CountryCode, ekitDto.ProductCode).Name;
            }
            catch (Exception) { }
            if (ekitDto.RateCode != null)
            {
                Rate rate = RateHome.GetByProductCode(
                    ekitDto.AssociationGroupDto.IdProduct, ekitDto.RateCode);
                ekitDto.AssociationGroupDto.IdRate = rate.Id;
                ekitDto.RateModality = rate.Modality;
            }
            ekitDto.AssociationGroupDto.IdDistributionType = DistributionTypeHome.Get(ekitDto.DistributionTypeCode).Id;

            //Other attributes
            ekitDto.RecipientFullName = ekitDto.RecipientName + " " + ekitDto.RecipientSurname;
            ekitDto.CountryName = country.Nombre;
            ekitDto.RecipientDocumentNumber = ekitDto.PaxPassport;
            //Este dato se recibe desde el servicio de la net, ya no se arma mas asi
            //ekitDto.CompleteVoucherCode = ekitDto.CountryCode.ToString() + " " + ekitDto.VoucherCode + " " + ekitDto.PaxType + ekitDto.ProductCode;
            ekitDto.EffectiveStartDateFormat = DateUtil.FormatToCompleteDate(
                DateUtil.ConvertToDate(ekitDto.EffectiveStartDate), dto.IdLanguage);
            ekitDto.EffectiveEndDateFormat = DateUtil.FormatToCompleteDate(
                DateUtil.ConvertToDate(ekitDto.EffectiveEndDate), dto.IdLanguage);
            ekitDto.IssuanceDateFormat = DateUtil.FormatToCompleteDate(
                DateUtil.ConvertToDate(ekitDto.IssuanceDate), dto.IdLanguage);
            ekitDto.IssuanceDateShortFormat = DateUtil.FormatToShortDate(
                DateUtil.ConvertToDate(ekitDto.IssuanceDate), dto.IdLanguage);
            ekitDto.EffectiveStartDateShortFormat = DateUtil.FormatToShortDate(
                DateUtil.ConvertToDate(ekitDto.EffectiveStartDate), dto.IdLanguage);
            ekitDto.EffectiveEndDateShortFormat = DateUtil.FormatToShortDate(
                DateUtil.ConvertToDate(ekitDto.EffectiveEndDate), dto.IdLanguage);
            if (!String.IsNullOrWhiteSpace(ekitDto.TransactionDate))
            {
                ekitDto.TransactionDateFormat = DateUtil.FormatToCompleteDate(
                    DateUtil.ConvertToDate(ekitDto.TransactionDate), dto.IdLanguage);
                ekitDto.TransactionDate = DateUtil.FormatToShortDate(
                    DateUtil.ConvertToDate(ekitDto.TransactionDate), dto.IdLanguage);
            }
            if (!String.IsNullOrWhiteSpace(ekitDto.ServiceStartDate))
            {
                ekitDto.ServiceStartDateFormat = DateUtil.FormatToCompleteDate(
                    DateUtil.ConvertToDate(ekitDto.ServiceStartDate), dto.IdLanguage);
                ekitDto.ServiceStartDate = DateUtil.FormatToShortDate(
                    DateUtil.ConvertToDate(ekitDto.ServiceStartDate), dto.IdLanguage);
            }
            if (!String.IsNullOrWhiteSpace(ekitDto.ServiceEndDate))
            {
                ekitDto.ServiceEndDateFormat = DateUtil.FormatToCompleteDate(
                    DateUtil.ConvertToDate(ekitDto.ServiceEndDate), dto.IdLanguage);
                ekitDto.ServiceEndDate = DateUtil.FormatToShortDate(
                    DateUtil.ConvertToDate(ekitDto.ServiceEndDate), dto.IdLanguage);
            }
            ekitDto.CurrentDate = DateUtil.FormatToCompleteDate(DateTime.Now, ekitDto.IdLanguage);
            ekitDto.ConsecutiveDays = ekitDto.GetConsecutiveDays();
            ekitDto.RCIExchangeAditional = ekitDto.GetRCIExchangeAditional();
            ekitDto.AnnualText = ekitDto.GetAnnualText();
            ekitDto.PersonalInformationSecurity = ekitDto.GetPersonalInformationSecurity();
            ekitDto.ApplicationUrl = ConfigurationValueHome.GetApplicationUrl();
            ekitDto.CardType = ekitDto.ProductName;
            ekitDto.BenefitsText = ekitDto.GrupoClausulaDTO == null || ekitDto.GrupoClausulaDTO.Texto == null ?
                "" : ekitDto.GrupoClausulaDTO.Texto.Replace("<![CDATA[", "").Replace("]]>", "");
            ekitDto.AnualTextEEUU = ekitDto.Days == "365" ? " up to 365 days per trip along" : "";
            ekitDto.ClauseC422 = ekitDto.GrupoClausulaDTO != null ? 
                ekitDto.GrupoClausulaDTO.GetBenefitContent("C.4.22", ekitDto.IdLanguage) : "" ;
            ekitDto.ClauseC4211 = ekitDto.GrupoClausulaDTO != null ?
                ekitDto.GrupoClausulaDTO.GetBenefitContent("C.4.21.1", ekitDto.IdLanguage) : "";
            ekitDto.ClauseC4 = ekitDto.GrupoClausulaDTO != null ?
                ekitDto.GrupoClausulaDTO.GetBenefit("C.4") != null ? ekitDto.GrupoClausulaDTO.GetBenefit("C.4").GetLeyend(ekitDto.IdLanguage) :
                ekitDto.GrupoClausulaDTO.GetBenefit("B0001") != null ? ekitDto.GrupoClausulaDTO.GetBenefit("B0001").GetLeyend(ekitDto.IdLanguage) :
                ekitDto.GrupoClausulaDTO.GetBenefit("B0004") != null ? ekitDto.GrupoClausulaDTO.GetBenefit("B0004").GetLeyend(ekitDto.IdLanguage) :
                ekitDto.GrupoClausulaDTO.GetBenefit("B0008") != null ? ekitDto.GrupoClausulaDTO.GetBenefit("B0008").GetLeyend(ekitDto.IdLanguage) : "" : "";
            ekitDto.PaxCountry = ekitDto.PaxCountry;

            foreach (EMailEKitUpgradeDTO upgradeDTO in ekitDto.Upgrades)
            {
                upgradeDTO.IdLocation = country.IdLocacion;
                upgradeDTO.IdUpgrade = ProductHome.Get(upgradeDTO.CountryCode.ToString(), upgradeDTO.Upgrade, Product.UPGRADE).Id;
                upgradeDTO.IdUpgradeRate = RateHome.GetByProductCode(upgradeDTO.IdUpgrade, upgradeDTO.UpgradeRateCode).Id;
            }
        }

        protected override AbstractEMailDTO GetDto(string xml)
        {
            try
            {
                EMailEkitDTO dto = (EMailEkitDTO)ServicioConversionXml.Instancia().
                    DeserializeObject(xml, (new EMailEkitDTO()).GetType());
                dto.TemplateType = TemplateTypeHome.GetEkit();
                dto.Module = ModuloHome.ObtenerPorNombre(dto.ModuleCode);

                dto.AssociationGroupDto = new AssociationGroupDTO();
                dto.AssociationGroupDto.IdLocation = PaisHome.ObtenerPorCodigo(
                    dto.CountryCode.ToString()).IdLocacion;
                dto.AssociationGroupDto.IdAccount = SucursalHome.Obtener(
                    dto.CountryCode.ToString(), dto.AgencyCode, dto.BranchNumber).Id;
                dto.AssociationGroupDto.IdProduct = ProductHome.Get(dto.CountryCode.ToString(), 
                    dto.ProductCode != null ? dto.ProductCode : "-", Product.PRODUCT).Id;
                dto.AssociationGroupDto.IdRate = RateHome.GetByProductCode(dto.AssociationGroupDto.IdProduct, 
                    dto.RateCode != null ? dto.RateCode : "-").Id;

                return dto;
            }
            catch (Exception)
            {
            }

            return new EMailEkitDTO();
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

        public override bool AgencySendEmails(AbstractEMailDTO dto)
        {
            EMailEkitDTO ekitDto = (EMailEkitDTO)dto;
            bool isExclude = EMailListExcludeHome.IsExclude(ekitDto.CountryCode, ekitDto.AgencyCode, ekitDto.BranchNumber);
            return !isExclude;
        }

        #endregion     
    }
}
