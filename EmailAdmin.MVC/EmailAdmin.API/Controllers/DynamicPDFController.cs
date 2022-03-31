using System;
using System.Threading;
using System.Web.Http;
using CapaNegocioDatos.Servicios;
using DTOMapper.Helpers;
using DTOMapper;
using EmailAdmin.Dto;
using EMailAdmin.BackEnd.Utils;
using EMailAdmin.BackEnd.Home;
using EmailAdmin.Api.Utilities;
using System.Collections.Generic;
using EMailAdmin.BackEnd.Service;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Net.Http.Headers;
using EMailAdmin.BackEnd.DTO;
using CapaNegocioDatos.CapaNegocio;
using CapaNegocioDatos.CapaHome;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data;
//using System.Web.Http.Cors;


namespace EmailAdmin.Api.Controllers
{
    public class DynamicPDFController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage  GetPDFDynamicByTemplete(DTOFilter filter)
        {
            try
            {
                byte[] pdf = null;

                APISecurityHelper.CheckApiAuthentication(filter);

                int countryCode = RequestParameterHelper.GetGenericParameter<int>(filter, "CountryCode");
                string moduleCode = RequestParameterHelper.GetGenericParameter<string>(filter, "ModuleCode");
                
                
                string strategyCode = RequestParameterHelper.GetGenericOptionalParameter<string>(filter, "StrategyCode");
                string uiCulture = RequestParameterHelper.GetGenericParameter<string>(filter, "UICulture");
                Dictionary<string, object> data = RequestParameterHelper.GetGenericOptionalParameter<Dictionary<string, object>>(filter, "Data");


                if (!string.IsNullOrEmpty(RequestParameterHelper.GetGenericParameter<string>(filter, "TemplateCode")))
                {
                    string templateCode = RequestParameterHelper.GetGenericParameter<string>(filter, "TemplateCode");
                 
                    SendEmailDto sendEmailDto = new Dto.SendEmailDto();
                    sendEmailDto.user = RequestParameterHelper.GetGenericParameter<string>(filter, "User"); ;
                    sendEmailDto.password = RequestParameterHelper.GetGenericParameter<string>(filter, "Password"); ;
                    sendEmailDto.CountryCode = countryCode;
                    sendEmailDto.ModuleCode = moduleCode;
                    sendEmailDto.TemplateCode = templateCode;
                    sendEmailDto.StrategyCode = strategyCode;
                    sendEmailDto.UICulture = uiCulture;
                    sendEmailDto.data = data;

                    EMailDynamicExecution execution = new EMailDynamicExecution() { dtoSendEmail = sendEmailDto };
                    pdf = execution.GetDynamicPDF(true);

                    return GetPdfHttpResponse(templateCode, pdf);
                }
                else if (string.IsNullOrEmpty(RequestParameterHelper.GetGenericParameter<string>(filter, "TemplateCode")) && data.Count > 0)
                {
                    string AgencyCode = "";
                    int BranchNumber = 0;
                    string ProductCode="";
                    string RateCode = "";
                    foreach (var info in data)
                    {
                        switch (info.Key)
                        {
                            case "AgencyCode":
                                AgencyCode = info.Value.ToString();
                                break;
                            case "BranchNumber":
                                BranchNumber = Convert.ToInt32(info.Value.ToString());
                                break;
                            case "ProductCode":
                                ProductCode = info.Value.ToString();
                                break;
                            case "RateCode":
                                RateCode = info.Value.ToString();
                                break;
                            default:
                                break;
                        }
                    }
                    EMailEkitDTO ekitDto = new EMailEkitDTO();
                    ekitDto.TemplateType = TemplateTypeHome.GetDynamic();

                    ekitDto.AssociationGroupDto = new AssociationGroupDTO();
                    Pais country = PaisHome.ObtenerPorCodigo(countryCode.ToString());
                    ekitDto.AssociationGroupDto.IdLocation = country.IdLocacion;

                    Sucursal branch = SucursalHome.Obtener(countryCode.ToString(), AgencyCode, BranchNumber);
                    ekitDto.Sucursal = new SucursalDTO(branch);
                    ekitDto.AssociationGroupDto.IdAccount = branch.Id;

                    ekitDto.AssociationGroupDto.IdProduct = ProductHome.Get(countryCode.ToString(), ProductCode, Product.PRODUCT).Id;

                    if (RateCode != "")
                    {
                        Rate rate = RateHome.GetByProductCode(
                            ekitDto.AssociationGroupDto.IdProduct, RateCode);
                        ekitDto.AssociationGroupDto.IdRate = rate.Id;
                        ekitDto.RateModality = rate.Modality;
                    }
                    
                    ekitDto.AssociationGroupDto.IdDistributionType = DistributionTypeHome.Get("-").Id;

                    ekitDto.Module = ModuloHome.ObtenerPorNombre(moduleCode);

                    IList<Group_R_Template> iGroupsRTemplate = DAOLocator.Instance().GetDaoGroup_R_Template().Find(
                        ekitDto.TemplateType.Id, ekitDto.AssociationGroupDto.IdLocation, ekitDto.AssociationGroupDto.IdAccount,
                        ekitDto.AssociationGroupDto.IdProduct, ekitDto.AssociationGroupDto.IdRate, ekitDto.AssociationGroupDto.IdDistributionType,
                        DateTime.Now, ekitDto.Module.Id,
                        GroupTypeHome.TemplateGroup().Id);

                    var template = GetMaxHierarchyTemplate(GetMaxWeightTemplates(iGroupsRTemplate));

                    SendEmailDto sendEmailDto = new Dto.SendEmailDto();
                    sendEmailDto.user = RequestParameterHelper.GetGenericParameter<string>(filter, "User"); ;
                    sendEmailDto.password = RequestParameterHelper.GetGenericParameter<string>(filter, "Password"); ;
                    sendEmailDto.CountryCode = countryCode;
                    sendEmailDto.ModuleCode = moduleCode;
                    sendEmailDto.TemplateCode = template.Name;
                    sendEmailDto.StrategyCode = strategyCode;
                    sendEmailDto.UICulture = uiCulture;
                    sendEmailDto.data = data;

                    EMailDynamicExecution execution = new EMailDynamicExecution() { dtoSendEmail = sendEmailDto };
                    pdf = execution.GetDynamicPDF(false);
                    return GetPdfHttpResponse(template.Name, pdf);
                }
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return response;
            }
            catch (HttpErrorCustomizedException ex)
            {
                DTOApplicationErrorData dtoApplicationErrorData = ex.ApplicationErrorData;
                ApplicationEventLogHelper.LogApplicationErrorDataCatched(ref dtoApplicationErrorData);
                return null;
            }
            catch (Exception ex)
            {
                DTOApplicationErrorData applicationErrorData = ApplicationEventLogHelper.LogErrorCatched(ex, filter);
                return null;
            }
        }

        public HttpResponseMessage GetPdfHttpResponse(string documentName, byte[] byteArray)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            int contentLength = byteArray.Length;
            response.Content = new StreamContent(new MemoryStream(byteArray));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentLength = contentLength;

            ContentDispositionHeaderValue contentDisposition = null;
            if (ContentDispositionHeaderValue.TryParse("inline; filename=" + documentName + ".pdf", out contentDisposition))
                response.Content.Headers.ContentDisposition = contentDisposition;
            return response;
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
    }
}