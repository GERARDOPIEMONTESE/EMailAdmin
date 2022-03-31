using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Web.UI.WebControls;
using ControlMenu;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Exceptions;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.Utils;
using MenuPortalLibrary.CapaDTO;
using Content = EMailAdmin.BackEnd.Domain.Content;

namespace EMailAdmin.Template
{
    public partial class TemplateInformation : CustomPage
    {
        #region Constructor

        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClientScript.RegisterOnSubmitStatement(GetType(), String.Concat(ClientID, "_OnSubmit"), "javascript: return OvrdSubmit();");
                SessionManager.SetIsDeleteFromPopUp(false, Session);
                SessionManager.RemoveTemplateFooter(Session);
                SessionManager.RemoveTemplateHeader(Session);
                SessionManager.RemoveTemplateFooterPDF(Session);
                SessionManager.RemoveTemplateHeaderPDF(Session);
                SessionManager.RemoveTemplateColor(Session);
                SessionManager.RemoveContentAttachment(Session);
                SessionManager.RemoveContentItems(Session);
                SessionManager.RemoveAttachmentTemplates(Session);

                LoadControls();

                if (Request.QueryString["IdTemplate"] == null && Request.QueryString["IdCopyTemplate"] == null)
                {
                    SessionManager.SetIsCopy(false, Session);
                    SessionManager.SetTemplate(new BackEnd.Domain.Template(), Session);
                    btnDelete.Visible = false;
                }
                else
                {
                    int idTemplate;
                    Int32.TryParse(Request.QueryString["IdTemplate"], out idTemplate);
                    SessionManager.SetIsCopy(false, Session);

                    if (idTemplate != 0)
                    {
                        SessionManager.SetTemplate(TemplateHome.Get(idTemplate), Session);
                    }
                    else
                    {
                        SessionManager.SetIsCopy(true, Session);
                        Int32.TryParse(Request.QueryString["IdCopyTemplate"], out idTemplate);
                        SessionManager.SetTemplate(ServiceLocator.Instance().GetTemplateService().
                            Copy(idTemplate), Session);
                    }

                    btnDelete.Visible = true;
                    CompleteControls();
                }
                
                SessionManager.SetGroupType(GroupTypeHome.Find(GroupType.TEMPLATEGROUP), Session);
            }
        }

        #endregion Constructor

        #region Propiedades

        protected override void ChequearSession()
        {
            UsuarioDTO usuario = base.UsuarioLogueadoDTO();
            if (usuario == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings["ExpireSessionRedirect"]);
            }
        }

        #endregion Propiedades

        #region Methods

        protected void BtnCancelDelete(object sender, EventArgs e)
        {
            mpeDelete.Hide();
        }

        protected void BtnDeleteOnClick(object sender, EventArgs e)
        {
            mpeDelete.Show();
        }

        protected void LinkUploadButtonPressed(object sender, EventArgs e)
        {
            mpeLink.Show();
        }

        protected void ImageUploadButtonPressed(object sender, EventArgs e)
        {
            mpeImage.Show();
        }

        protected void VariableTextUploadButtonPressed(object sender, EventArgs e)
        {
            mpeVariableText.Show();
        }

        protected void SignatureUploadButtonPressed(object sender, EventArgs e)
        {
            mpeSignature.Show();
        }

        protected void CountryVisibleTextUploadButtonPressed(object sender, EventArgs e)
        {
            mpeCountryVisibleText.Show();
        }

        protected void UpgradeVariableTextUploadButtonPressed(object sender, EventArgs e)
        {
            mpeUpgradeVariableText.Show();
        }

        protected void ContactUploadButtonPressed(object sender, EventArgs e)
        {
            mpeContact.Show();
        }

        protected void BtnAttachmentOnClick(object sender, EventArgs e)
        {
            mpeAttachment.Show();
        }

        protected void BtnAssociationsOnClick(object sender, EventArgs e)
        {
            mpeAssociation.Show();
        }

        protected void BtnPreviewOnClick(object sender, EventArgs e)
        {
            SessionManager.SetPreviewTemplate(GetTemplate(), Session);
            mpePreview.Show();
        }

        protected void TableUploadButtonPressed(object sender, EventArgs e)
        {
            mpeTable.Show();
        }

        protected void ConditionVariableTextButtonPressed(object sender, EventArgs e)
        {
            mpeConditionVariableText.Show();
        }

        protected void PixelButtonPressed(object sender, EventArgs e)
        {
            mpePixelSelector.Show();
        }

        protected void ClausuleButtonPressed(object sender, EventArgs e)
        {
            mpeClausuleSelector.Show();
        }

        protected void BtnCancelOnClick(object sender, EventArgs e)
        {
            Response.Redirect("TemplateList.aspx");
        }

        protected void BtnAcceptDeleteOnClick(object sender, EventArgs e)
        {
            if (!SessionManager.GetIsDeleteFromPopUp(Session))
            {
                if (IsValid)
                {
                    try
                    {
                        ServiceLocator.Instance().GetTemplateService().Delete(SessionManager.GetTemplate(Session));
                        Response.Redirect("TemplateList.aspx");
                    }
                    catch (NonEliminatedObjectException ex)
                    {
                        //ACA SE AVISA QUE NO PUDO ELIMINAR.
                    }
                }
            }
            else
            {
                ascAssociation.DeleteAssociation();
                SessionManager.SetIsDeleteFromPopUp(false, Session);
                mpeAssociation.Show();
            }
        }

        protected void BtnAcceptOnClick(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    ServiceLocator.Instance().GetTemplateService().Save(GetTemplate());
                    Response.Redirect("TemplateList.aspx");
                }
                catch (NonSavedObjectException ex)
                {
                    //ACA SE AVISA DE DATOS INCOMPLETOS.
                }
            }
        }

        protected void ImageUploadedCompleted(object sender, EventArgs e)
        {
            mpeImage.Hide();
            int idLanguage = SessionManager.GetSelectedGridLanguage(Session);
            ContentImage image = imgSelector.Image;
            IDictionary<int, IList<ContentImage>> imagesDic = SessionManager.GetContentImages(Session) ??
                                                              new Dictionary<int, IList<ContentImage>>();
            if (imagesDic.ContainsKey(idLanguage))
            {
                IList<ContentImage> images = imagesDic[idLanguage];
                images.Add(image);
            }
            else
            {
                var images = new List<ContentImage> { image };
                imagesDic.Add(idLanguage, images);
            }
            SessionManager.SetContentImages(imagesDic, Session);
            tclTemplates.AddTag(TagUtils.GenerateImageTag(image.Name));
        }

        protected void ImageCanceled(object sender, EventArgs e)
        {
            mpeImage.Hide();
        }

        protected void LinkUploadedCompleted(object sender, EventArgs e)
        {
            mpeLink.Hide();
            int idLanguage = SessionManager.GetSelectedGridLanguage(Session);
            Link link = lnkSelector.Link;
            IDictionary<int, IList<Link>> linkDic = SessionManager.GetContentLinks(Session) ??
                                                    new Dictionary<int, IList<Link>>();
            if (linkDic.ContainsKey(idLanguage))
            {
                IList<Link> links = linkDic[idLanguage];
                links.Add(link);
            }
            else
            {
                var links = new List<Link> { link };
                linkDic.Add(idLanguage, links);
            }
            SessionManager.SetContentLinks(linkDic, Session);
            tclTemplates.AddTag(TagUtils.GenerateLinkTag(link.Name));
        }

        protected void LinkCanceled(object sender, EventArgs e)
        {
            mpeLink.Hide();
        }

        protected void VariableTextCompleted(object sender, EventArgs e)
        {
            mpeVariableText.Hide();
            int idLanguage = SessionManager.GetSelectedGridLanguage(Session);
            VariableText variableText = vtxVariableText.VariableText;
            IDictionary<int, IList<VariableText>> variableTextDic = SessionManager.GetContentVariableTexts(Session) ??
                                                                    new Dictionary<int, IList<VariableText>>();
            if (variableTextDic.ContainsKey(idLanguage))
            {
                IList<VariableText> variableTexts = variableTextDic[idLanguage];
                variableTexts.Add(variableText);
            }
            else
            {
                var variableTexts = new List<VariableText> { variableText };
                variableTextDic.Add(idLanguage, variableTexts);
            }
            SessionManager.SetContentVariableTexts(variableTextDic, Session);
            tclTemplates.AddTag(TagUtils.GenerateVariableTextTag(variableText.Name));
        }

        protected void VariableTextCanceled(object sender, EventArgs e)
        {
            mpeVariableText.Hide();
        }

        protected void SignatureCompleted(object sender, EventArgs e)
        {
            mpeSignature.Hide();
            int idLanguage = SessionManager.GetSelectedGridLanguage(Session);
            SignatureType signature = sgrSignature.Signature;
            IDictionary<int, IList<SignatureType>> signatureDic = SessionManager.GetContentSignatures(Session) ??
                                                                  new Dictionary<int, IList<SignatureType>>();
            if (signatureDic.ContainsKey(idLanguage))
            {
                IList<SignatureType> signatures = signatureDic[idLanguage];
                signatures.Add(signature);
            }
            else
            {
                var signatures = new List<SignatureType> { signature };
                signatureDic.Add(idLanguage, signatures);
            }
            SessionManager.SetContentSignatures(signatureDic, Session);
            tclTemplates.AddTag(TagUtils.GenerateSignatureTag(signature.Code));
        }

        protected void SignatureCanceled(object sender, EventArgs e)
        {
            mpeSignature.Hide();
        }

        protected void CountryVisibleTextCompleted(object sender, EventArgs e)
        {
            mpeCountryVisibleText.Hide();
            int idLanguage = SessionManager.GetSelectedGridLanguage(Session);
            CountryVisibleTextType countryVisibleText = cvtCountryVisibleText.CountryVisibleText;
            IDictionary<int, IList<CountryVisibleTextType>> countryVisibleTextDic = SessionManager.GetContentCountryVisibleText(Session) ??
                                                                  new Dictionary<int, IList<CountryVisibleTextType>>();
            if (countryVisibleTextDic.ContainsKey(idLanguage))
            {
                IList<CountryVisibleTextType> countryVisibleTextTypes = countryVisibleTextDic[idLanguage];
                countryVisibleTextTypes.Add(countryVisibleText);
            }
            else
            {
                var countryVisibleTextTypes = new List<CountryVisibleTextType> { countryVisibleText };
                countryVisibleTextDic.Add(idLanguage, countryVisibleTextTypes);
            }
            SessionManager.SetContentCountryVisibleText(countryVisibleTextDic, Session);
            tclTemplates.AddTag(TagUtils.GenerateCountryVisibleTextTag(countryVisibleText.Code));
        }

        protected void CountryVisibleTextCanceled(object sender, EventArgs e)
        {
            mpeCountryVisibleText.Hide();
        }

        protected void UpgradeVariableTextCompleted(object sender, EventArgs e)
        {
            mpeUpgradeVariableText.Hide();
            int idLanguage = SessionManager.GetSelectedGridLanguage(Session);
            UpgradeVariableTextType upgradeVariableText = uvtUpgradeVariableText.UpgradeVariableText;
            IDictionary<int, IList<UpgradeVariableTextType>> UpgradeVariableTextDic = SessionManager.GetContentUpgradeVariableText(Session) ??
                                                                  new Dictionary<int, IList<UpgradeVariableTextType>>();
            if (UpgradeVariableTextDic.ContainsKey(idLanguage))
            {
                IList<UpgradeVariableTextType> upgradeVariableTextTypes = UpgradeVariableTextDic[idLanguage];
                upgradeVariableTextTypes.Add(upgradeVariableText);
            }
            else
            {
                var upgradeVariableTextTypes = new List<UpgradeVariableTextType> { upgradeVariableText };
                UpgradeVariableTextDic.Add(idLanguage, upgradeVariableTextTypes);
            }
            SessionManager.SetContentUpgradeVariableText(UpgradeVariableTextDic, Session);
            tclTemplates.AddTag(TagUtils.GenerateUpgradeVariableTextTag(upgradeVariableText.Code));
        }

        protected void UpgradeVariableTextCanceled(object sender, EventArgs e)
        {
            mpeUpgradeVariableText.Hide();
        }

        protected void ContactCompleted(object sender, EventArgs e)
        {
            mpeContact.Hide();
            int idLanguage = SessionManager.GetSelectedGridLanguage(Session);
            EMailContactType contact = ctcContact.Contact;
            IDictionary<int, IList<EMailContactType>> contactDic = SessionManager.GetContentContacts(Session) ??
                                                                   new Dictionary<int, IList<EMailContactType>>();
            if (contactDic.ContainsKey(idLanguage))
            {
                IList<EMailContactType> contacts = contactDic[idLanguage];
                contacts.Add(contact);
            }
            else
            {
                var contacts = new List<EMailContactType> { contact };
                contactDic.Add(idLanguage, contacts);
            }
            SessionManager.SetContentContacts(contactDic, Session);
            tclTemplates.AddTag(TagUtils.GenerateContactTag(contact.Code));
        }

        protected void TableVariableTextCompleted(object sender, EventArgs e)
        {
            mpeTable.Hide();
            int idLanguage = SessionManager.GetSelectedGridLanguage(Session);
            TableVariableText tableVariableText = TableVariableSelector1.TableVariableText;
            IDictionary<int, IList<TableVariableText>> TableVariableTextDic = SessionManager.GetContentTableVariableText(Session) ??
                                                                  new Dictionary<int, IList<TableVariableText>>();
            if (TableVariableTextDic.ContainsKey(idLanguage))
            {
                IList<TableVariableText> tableVariableTextTypes = TableVariableTextDic[idLanguage];
                tableVariableTextTypes.Add(tableVariableText);
            }
            else
            {
                var tableVariableTextTypes = new List<TableVariableText> { tableVariableText };
                TableVariableTextDic.Add(idLanguage, tableVariableTextTypes);
            }
            SessionManager.SetContentTableVariableText(TableVariableTextDic, Session);
            tclTemplates.AddTag(TagUtils.GenerateTableVariableTextTag(tableVariableText.Name));
        }

        protected void TableVariableTextCanceled(object sender, EventArgs e)
        {
            mpeTable.Hide();
        }

        protected void ConditionVariableTextCompleted(object sender, EventArgs e)
        {
            mpeConditionVariableText.Hide();
            int idLanguage = SessionManager.GetSelectedGridLanguage(Session);
            EMailAdmin.BackEnd.Domain.ConditionVariableText conditionVariableText = ConditionVariableTextSelector1.ConditionVariableText;
            IDictionary<int, IList<EMailAdmin.BackEnd.Domain.ConditionVariableText>> ConditionVariableTextDic = SessionManager.GetContentConditionVariableText(Session) ??
                                                                  new Dictionary<int, IList<EMailAdmin.BackEnd.Domain.ConditionVariableText>>();
            if (ConditionVariableTextDic.ContainsKey(idLanguage))
            {
                IList<EMailAdmin.BackEnd.Domain.ConditionVariableText> conditionVariablesText = ConditionVariableTextDic[idLanguage];
                conditionVariablesText.Add(conditionVariableText);
            }
            else
            {
                var conditionVariablesText = new List<EMailAdmin.BackEnd.Domain.ConditionVariableText> { conditionVariableText };
                ConditionVariableTextDic.Add(idLanguage, conditionVariablesText);
            }
            SessionManager.SetContentConditionVariableText(ConditionVariableTextDic, Session);
            tclTemplates.AddTag(TagUtils.GenerateConditionVariableTextTag(conditionVariableText.Name));
        }

        protected void PixelCompleted(object sender, EventArgs e)
        {
            tclTemplates.AddTag(TagUtils.GeneratePixelTag(PixelSelector1.Pixel.Name));
            mpePixelSelector.Hide();
        }

        protected void PixelCanceled(object sender, EventArgs e)
        {
            mpePixelSelector.Hide();
        }

        protected void ClausuleCompleted(object sender, EventArgs e)
        {
            tclTemplates.AddTag(TagUtils.GenerateClausuleTag(ClausuleSelector1.ClausuleSelectorText));
            mpeClausuleSelector.Hide();
        }

        protected void ClausuleCanceled(object sender, EventArgs e)
        {
            mpeClausuleSelector.Hide();
        }

        protected void ConditionVariableTextCanceled(object sender, EventArgs e)
        {
            mpeConditionVariableText.Hide();
        }

        protected void ContactCanceled(object sender, EventArgs e)
        {
            mpeContact.Hide();
        }

        protected void AttachmentClose(object sender, EventArgs e)
        {
            mpeAttachment.Hide();
        }

        protected void AttachmentSearched(object sender, EventArgs e)
        {
            mpeAttachment.Show();
        }

        protected void AttachmentEditedContent(int IdAttachment, string CodeRPT)
        {
            ContentAttachment1.IdAttachment = IdAttachment;
            ContentAttachment1.AddCodeRPT(CodeRPT);
            ContentAttachment1.InitControl();
            mpeContentAttachments.Show();
        }
        
        protected void ContentAttachmentClose(object sender, EventArgs e)
        {
            mpeContentAttachments.Hide();
        }

        protected void AttachmentPageIndexChanged(object sender, EventArgs e)
        {
            mpeAttachment.Show();
        }
        
        protected void AttachmentEditedGroup(int IdAttachment)
        {
            AttachmentGroup1.IdAttachment = IdAttachment;
            AttachmentGroup1.InitControl();
            mpeAttachmentGroup.Show();
        }

        protected void AttachmentTemplatesEdited(int IdAttachment)
        {
            AttachmentTemplatesSelector1.IdAttachment = IdAttachment;
            AttachmentTemplatesSelector1.initControls();
            mpeAttachmentTemplatesSelector.Show();
        }

        protected void AttachmentClosedGroup(object sender, EventArgs e)
        {
            attAttachment.Refresh();
            mpeAttachment.Show();
        }

        protected void PrvPreviewOnClosed(object sender, EventArgs e)
        {
            mpePreview.Hide();
        }

        protected void PrvPreviewOnShow(object sender, EventArgs e)
        {
            mpePreview.Show();
        }

        protected void BtnAcceptGroupViewerClick(object sender, EventArgs e)
        {
            mpeGroup.Hide();
            /*
            var templates = SessionManager.GetTemplateAssociations(Session);
            foreach (var group in grpGroup.TemplateGroups)
            {
                templates.Add(group);
            }
            SessionManager.SetTemplateAssociations(templates, Session);
            */
            ascAssociation.Bind();
            grpGroup.ClearControls();
            mpeAssociation.Show();
        }

        protected void BtnCancelGroupViewerClick(object sender, EventArgs e)
        {
            mpeGroup.Hide();
            mpeAssociation.Show();
        }

        protected void GrpGroupAddPressed(object sender, EventArgs e)
        {
            mpeGroupControl.Show();
        }

        protected void ShowGroupAssociations(object sender, EventArgs e)
        {
            mpeGroup.Show();
        }

        protected void AddGroupAssociations(object sender, Group_R_Template grt)
        {
            var templates = SessionManager.GetTemplateAssociations(Session);
            templates.Add(grt);
            SessionManager.SetTemplateAssociations(templates, Session);
            mpeGroup.Show();
        }

        protected void DelGroupAssociations(object sender, Group_R_Template grt)
        {
            var templates = SessionManager.GetTemplateAssociations(Session);
            templates.Remove(grt);
            SessionManager.SetTemplateAssociations(templates, Session);
            mpeGroup.Show();
        }

        protected void BtnAcceptGroupClick(object sender, EventArgs e)
        {
            if (IsValid && grcGroup.CanSave())
            {
                ServiceLocator.Instance().GetGroupService().Save(grcGroup.Group);
                SessionManager.SetGroupConditions(new List<GroupCondition>(), Session);
                grpGroup.ReloadGroups();
                mpeGroup.Show();
            }
            else
            {
                mpeGroupControl.Show();
            }
        }

        protected void BtnCancelGroupClick(object sender, EventArgs e)
        {
            mpeGroupControl.Hide();
            grcGroup.ClearControls();
            mpeGroup.Show();
        }

        protected void GrcGroupFiltersClosed(object sender, EventArgs e)
        {
            mpeGroupControl.Show();
        }

        protected void GrcGroupFiltersAdded(object sender, EventArgs e)
        {
            mpeGroupControl.Show();
        }

        protected void GrcGroupAccountSearchPressed(object sender, EventArgs e)
        {
            mpeGroupControl.Show();
        }

        protected void GrcGroupProductSearchPressed(object sender, EventArgs e)
        {
            mpeGroupControl.Show();
        }

        protected void GrcGroupRateSearchPressed(object sender, EventArgs e)
        {
            mpeGroupControl.Show();
        }

        protected void GrcGroupDeletePressed(object sender, EventArgs e)
        {
            mpeGroupControl.Show();
        }

        protected void CtmNameValidator(object sender, ServerValidateEventArgs validation)
        {
            validation.IsValid = ValidateName();
            return;
        }

        protected void CvDatesValidator(object sender, ServerValidateEventArgs validation)
        {
            validation.IsValid = ValidateDates();
            return;
        }

        protected void AscAssociationDeletePressed(object sender, EventArgs e)
        {
            mpeAssociation.Hide();
            SessionManager.SetIsDeleteFromPopUp(true, Session);
            mpeDelete.Show();
        }

        protected void AscAssociationAddPressed(object sender, EventArgs e)
        {
            mpeAssociation.Hide();
            grpGroup.BindGridGrupos();
            mpeGroup.Show();
        }

        protected void AccAccountSelectorOnSearch(object sender, EventArgs e)
        {
            mpeAccount.Show();
        }

        protected void AccAccountSelectorOnClose(object sender, EventArgs e)
        {
            grcGroup.SetAccountText(accAccount.GetSelectedItemsText());
            mpeGroupControl.Show();
        }

        protected void CtrCountryOnClose(object sender, EventArgs e)
        {
            grcGroup.SetCountryText(ctrCountry.GetSelectedItemsText());
            mpeGroupControl.Show();
        }

        protected void CtrCountryOnChkPressed(object sender, EventArgs e)
        {
            mpeCountry.Show();
        }

        protected void ProProductOnChkPressed(object sender, EventArgs e)
        {
            mpeProduct.Show();
        }

        protected void ProProductSelectorOnSearch(object sender, EventArgs e)
        {
            mpeProduct.Show();
        }

        protected void ProProductSelectorOnClose(object sender, EventArgs e)
        {
            grcGroup.SetProductText(proProduct.GetSelectedItemsText());
            mpeGroupControl.Show();
        }

        protected void RteRateSelectorOnSearch(object sender, EventArgs e)
        {
            mpeRate.Show();
        }

        protected void RteRateSelectorOnClose(object sender, EventArgs e)
        {
            grcGroup.SetRateText(rteRate.GetSelectedItemsText());
            mpeGroupControl.Show();
        }

        protected void RteRateSelectorOnCountryChanged(object sender, EventArgs e)
        {
            mpeRate.Show();
        }

        protected void RteRateOnChkPressed(object sender, EventArgs e)
        {
            mpeRate.Show();
        }

        protected void ClearFilters(object sender, EventArgs e)
        {
            accAccount.UnSelectAll();
            ctrCountry.UnSelectAll();
            proProduct.UnSelectAll();
            rteRate.UnSelectAll();
        }

        #endregion Methods

        #region Private Methods

        private BackEnd.Domain.Template GetTemplate()
        {
            BackEnd.Domain.Template templateSession = SessionManager.GetTemplate(Session);
            templateSession.Name = txtName.Text.Trim();
            templateSession.TemplateType = TemplateTypeHome.Get(Convert.ToInt32(ddlType.SelectedValue));
            templateSession.Hierarchy = Convert.ToInt32(txtHierarchy.Text);
            templateSession.IAttachments = attAttachment.Attachments;
            templateSession.IContent = GetContents();
            templateSession.IGroups = ascAssociation.TemplateGroups;
            templateSession.EffectiveStartDate = Convert.ToDateTime(txtFromDate.Text, CultureInfo.CurrentCulture);
            templateSession.EffectiveEndDate = Convert.ToDateTime(txtToDate.Text, CultureInfo.CurrentCulture);
            templateSession.IdEMailFromAddress = Convert.ToInt32(ddlFromAddress.SelectedValue);
            templateSession.IdUsuario = SessionManager.GetLoguedUser(Session).Id;
            //templateSession.MergeAttachsWithEKit = chkMergeAttachs.Checked;
            templateSession.TypeAttachsWithEkit = BackEnd.Domain.Template.GetTypeAttachsWithEkit(ddlTypeAttachsWithEkit.SelectedValue);
            if (!string.IsNullOrEmpty(ddlTemplatePDF.SelectedValue))
                templateSession.IdTemplatePDF = Convert.ToInt32(ddlTemplatePDF.SelectedValue);

            return templateSession;
        }

        private IList<Content> GetContents()
        {
            return tclTemplates.GetValuesHtml();
        }

        private void CompleteControls()
        {
            BackEnd.Domain.Template template = SessionManager.GetTemplate(Session);
            txtName.Text = template.Name;
            txtFromDate.Text = template.EffectiveStartDate.ToShortDateString();
            txtToDate.Text = template.EffectiveEndDate.ToShortDateString();
            tclTemplates.SetValues(template.IContent);
            txtHierarchy.Text = template.Hierarchy.ToString(CultureInfo.InvariantCulture);
            attAttachment.Attachments = template.IAttachments;
            ddlType.SelectedValue = template.TemplateType.Id.ToString(CultureInfo.InvariantCulture);
            ddlFromAddress.SelectedValue = template.IdEMailFromAddress.ToString();
            //chkMergeAttachs.Checked = template.MergeAttachsWithEKit;
            ddlTypeAttachsWithEkit.SelectedValue = template.TypeAttachsWithEkit.ToString();
            if (!string.IsNullOrEmpty(template.IdTemplatePDF.ToString()))
                ddlTemplatePDF.SelectedValue = template.IdTemplatePDF.ToString();            
        }

        private void LoadControls()
        {
            ddlType.DataSource = TemplateTypeHome.FindAll();
            ddlType.DataBind();

            ddlTemplatePDF.DataSource = TemplateHome.FindAllList();
            ddlTemplatePDF.DataBind();

            ddlTypeAttachsWithEkit.Items.Clear();
            foreach (var item in Enum.GetNames(typeof(EMailAdmin.BackEnd.Domain.Template.eTypeAttachsWithEkit)))
            {
                ddlTypeAttachsWithEkit.Items.Add(new ListItem() { Value = item.ToString(), Text = GetLocalResourceObject(item.ToString()).ToString() });
            }
        }

        private bool ValidateDates()
        {
            if (txtFromDate.Text.Length > 0 && txtToDate.Text.Length > 0)
            {
                DateTime dateFrom = Convert.ToDateTime(txtFromDate.Text, CultureInfo.CurrentCulture);
                DateTime dateTo = Convert.ToDateTime(txtToDate.Text, CultureInfo.CurrentCulture);

                return dateTo.CompareTo(dateFrom) >= 0;
            }
            return true;
        }

        private bool ValidateName()
        {
            if (txtName.Text.Length == 0)
            {
                return true;
            }

            IList<BackEnd.Domain.Template> templates = TemplateHome.FindByName(txtName.Text.Trim());
            if (SessionManager.GetTemplate(Session).Id != 0)
            {
                //MOD
                foreach (var template in templates)
                {
                    if (template.Id != SessionManager.GetTemplate(Session).Id)
                    {
                        return false;
                    }
                }
            }
            else
            {
                //NEW
                return templates.Count <= 0;
            }
            return true;
        }

        #endregion Private Methods


        protected void ContentAttachmentClosed(object sender, EventArgs e)
        {
            mpeContentAttachments.Hide();
        }

        protected void ContentAttachmentOpened(object sender, EventArgs e)
        {
            mpeContentAttachments.Show();
        }
        protected void AttachmentGroupClosed(object sender, EventArgs e)
        {
            mpeAttachmentGroup.Hide();
            attAttachment.Refresh();
            mpeAttachment.Show();
        }

        protected void AttachmentGroupOpened(object sender, EventArgs e)
        {
            mpeAttachmentGroup.Show();
        }

        protected void AttachmentTemplatesOpened(object sender, EventArgs e)
        {
            mpeAttachmentTemplatesSelector.Show();
        }

        protected void AttachmentTemplatesClosed(object sender, EventArgs e)
        {
            mpeAttachmentTemplatesSelector.Hide();
            attAttachment.Refresh();
            mpeAttachment.Show();
        }
    }
}