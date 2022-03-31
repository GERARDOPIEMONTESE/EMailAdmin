using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Web.UI.WebControls;
using CapaNegocioDatos.CapaHome;
using ControlMenu;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Exceptions;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.Utils;
using MenuPortalLibrary.CapaDTO;

namespace EMailAdmin.Attachment
{
    public partial class AttachmentInformation : CustomPage
    {
        #region Constructor

        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                switch (InternacionalUtils.GetCulture(UsuarioLogueadoDTO().Ididioma))
                {
                    case "es":
                        ddlLanguage.DataTextField = "Descripcion";
                        grvItems.Columns[2].Visible = true;
                        grvItems.Columns[3].Visible = false;
                        grvItems.Columns[4].Visible = false;
                        break;
                    case "en":
                        ddlLanguage.DataTextField = "DescripcionIngles";
                        grvItems.Columns[2].Visible = false;
                        grvItems.Columns[3].Visible = true;
                        grvItems.Columns[4].Visible = false;
                        break;
                    case "pt":
                        ddlLanguage.DataTextField = "DescripcionPortugues";
                        grvItems.Columns[2].Visible = false;
                        grvItems.Columns[3].Visible = false;
                        grvItems.Columns[4].Visible = true;
                        break;
                }
                LoadDropDown();

                if (Request.QueryString["IdAttachment"] == null)
                {
                    btnDelete.Visible = false;
                    SessionManager.SetAttachment(new BackEnd.Domain.Attachment(), Session);
                    SessionManager.RemoveAttachmentItems(Session);
                    lblEstrategy.Visible = (ddlType.SelectedValue != "1");
                    ddlEstrategy.Visible = (ddlType.SelectedValue != "1");
                }
                else
                {
                    BackEnd.Domain.Attachment attachment =
                        AttachmentHome.Get(Convert.ToInt32(Request.QueryString["IdAttachment"]));
                    SessionManager.SetAttachment(attachment, Session);
                    SessionManager.SetAttachmentItems(attachment.AttachmentItems, Session);
                    btnDelete.Visible = true;
                    CompleteControls();
                }
                SessionManager.SetIsDeleteFromPopUp(false, Session);
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

        protected void BtnDeleteOnClick(object sender, EventArgs e)
        {
            mpeDelete.Show();
        }

        protected void BtnCancelOnClick(object sender, EventArgs e)
        {
            SessionManager.SetAttachmentAssociations(new List<Attachment_R_Group>(), Session);
            Response.Redirect("AttachmentList.aspx");
        }

        protected void BtnAcceptDeleteOnClick(object sender, EventArgs e)
        {
            if(!SessionManager.GetIsDeleteFromPopUp(Session))
            {
                if (IsValid)
                {
                    try
                    {
                        ServiceLocator.Instance().GetAttachmentService().Delete(SessionManager.GetAttachment(Session));
                        Response.Redirect("AttachmentList.aspx");
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
                    ServiceLocator.Instance().GetAttachmentService().Save(GetAttachment());
                    SessionManager.SetAttachmentItems(new List<AttachmentItem>(), Session);
                    SessionManager.SetAttachmentAssociations(new List<Attachment_R_Group>(), Session);
                    Response.Redirect("AttachmentList.aspx");
                }
                catch (NonSavedObjectException ex)
                {
                    //ACA SE AVISA DE DATOS INCOMPLETOS.
                }
            }
        }

        protected void DdlTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            btnItems.Visible = (ddlType.SelectedValue == "1");
            lblEstrategy.Visible = (ddlType.SelectedValue != "1");
            ddlEstrategy.Visible = (ddlType.SelectedValue != "1");
        }

        protected void CtmNameValidator(object sender, ServerValidateEventArgs validation)
        {
            validation.IsValid = ValidateName();
            return;
        }

        protected void BtnAssociationsOnClick(object sender, EventArgs e)
        {
            mpeAssociation.Show();
        }

        protected void BtnAcceptGroupViewerClick(object sender, EventArgs e)
        {
            mpeGroup.Hide();
            IList<Attachment_R_Group> attachmentList = new List<Attachment_R_Group>();
            foreach (var group in grpGroup.AttachmentGroups)
            {
                attachmentList.Add(group);                
            }
            SessionManager.SetAttachmentAssociations(attachmentList, Session);
            ascAssociation.Bind();
            grpGroup.ClearControls();
            mpeAssociation.Show();
        }

        protected void BtnCancelGroupViewerClick(object sender, EventArgs e)
        {
            mpeGroup.Hide();
            mpeAssociation.Show();
        }

        protected void GrpGroupAddPressed(object sender, Attachment_R_Group art)
        {
            var templates = SessionManager.GetAttachmentAssociations(Session);
            templates.Add(art);
            SessionManager.SetAttachmentAssociations(templates, Session);
            mpeGroup.Show();
        }

        protected void GrpGroupDelPressed(object sender, Attachment_R_Group art)
        {
            var templates = SessionManager.GetAttachmentAssociations(Session);
            templates.Remove(art);
            SessionManager.SetAttachmentAssociations(templates, Session);
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

        protected void ProProductSelectorOnSearch(object sender, EventArgs e)
        {
            mpeProduct.Show();
        }

        protected void ProProductSelectorOnClose(object sender, EventArgs e)
        {
            grcGroup.SetProductText(proProduct.GetSelectedItemsText());
            mpeGroupControl.Show();
        }

        protected void ProProductOnChkPressed(object sender, EventArgs e)
        {
            mpeProduct.Show();
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

        private BackEnd.Domain.Attachment GetAttachment()
        {
            BackEnd.Domain.Attachment attachmentSession = SessionManager.GetAttachment(Session);

            attachmentSession.Name = txtName.Text.Trim();
            attachmentSession.AttachmentType = AttachmentTypeHome.Get(Convert.ToInt32(ddlType.SelectedValue));
            attachmentSession.AttachmentItems = btnItems.Visible ? SessionManager.GetAttachmentItems(Session) : new List<AttachmentItem>();
            attachmentSession.Estrategy = EstrategyHome.Get(Convert.ToInt32(lblEstrategy.Visible ? ddlEstrategy.SelectedValue : "-1"));
            attachmentSession.IdUsuario = SessionManager.GetLoguedUser(Session).Id;
            attachmentSession.IGroups = ascAssociation.AttachmentGroups;

            return attachmentSession;
        }

        private void CompleteControls()
        {
            BackEnd.Domain.Attachment attachment = SessionManager.GetAttachment(Session);
            txtName.Text = attachment.Name;
            try
            {
                ddlType.Items.FindByValue(attachment.AttachmentType.Id.ToString(CultureInfo.InvariantCulture)).Selected = true;
                btnItems.Visible = (ddlType.SelectedValue == "1");
                lblEstrategy.Visible = (ddlType.SelectedValue != "1");
                ddlEstrategy.Visible = (ddlType.SelectedValue != "1");
            }
            catch
            {
            }

            try
            {
                ddlEstrategy.Items.FindByValue(attachment.Estrategy.Id.ToString(CultureInfo.InvariantCulture)).Selected = true;
            }
            catch
            {
            }
            Bind();
        }

        private void LoadDropDown()
        {
            ddlType.Items.Clear();
            ddlType.DataSource = AttachmentTypeHome.FindAll();
            ddlType.DataBind();

            ddlEstrategy.Items.Clear();
            ddlEstrategy.DataSource = EstrategyHome.FindAll();
            ddlEstrategy.DataBind();

            ddlLanguage.Items.Clear();
            ddlLanguage.DataSource = IdiomaHome.Buscar();
            ddlLanguage.DataBind();
        }

        #endregion Private Methods

        #region Items

        protected void BtnAddOnClick(object sender, EventArgs e)
        {
            IList<AttachmentItem> items = SessionManager.GetAttachmentItems(Session) ?? new List<AttachmentItem>();
            if (fupFile.FileBytes.Length != 0 && txtDescription.Text != "")
            {
                string[] splited = fupFile.FileName.Split(new[] {"."}, StringSplitOptions.None);
                var item = new AttachmentItem
                               {
                                   Content = fupFile.FileBytes,
                                   Name = fupFile.FileName,
                                   Description = txtDescription.Text,
                                   Language = IdiomaHome.Obtener(Convert.ToInt32(ddlLanguage.SelectedValue)),
                                   Type = splited[splited.Length - 1],
                                   Dimenssion = fupFile.FileBytes.Length,
                                   IdUsuario = SessionManager.GetLoguedUser(Session).Id
                               };
                items.Add(item);
                SessionManager.SetAttachmentItems(items, Session);
                Bind();
                txtDescription.Text = "";
                ddlLanguage.SelectedIndex = 0;
            }
            mpeItems.Show();
        }

        protected void IbnDelete_Onclick(object sender, EventArgs e)
        {
            var bt = (ImageButton) sender;
            IList<AttachmentItem> items = SessionManager.GetAttachmentItems(Session) ?? new List<AttachmentItem>();
            items.Remove(items[((GridViewRow) bt.Parent.Parent).RowIndex]);
            SessionManager.SetAttachmentItems(items, Session);
            Bind();
        }

        protected void CtmFileValidator(object sender, ServerValidateEventArgs validation)
        {
            validation.IsValid = ValidateFile();
            return;
        }

        private void Bind()
        {
            grvItems.DataSource = SessionManager.GetAttachmentItems(Session);
            grvItems.DataBind();
        }

        private bool ValidateName()
        {
            IList<BackEnd.Domain.Attachment> attachments = AttachmentHome.FindByName(txtName.Text.Trim());
            if (SessionManager.GetAttachment(Session).Id != 0)
            {
                //MOD
                foreach (var attachment in attachments)
                {
                    if (attachment.Id != SessionManager.GetAttachment(Session).Id)
                    {
                        return false;
                    }
                }
            }
            else
            {
                //NEW
                return attachments.Count <= 0;
            }
            return true;
        }

        private bool ValidateFile()
        {
            return fupFile.FileBytes.Length != 0;
        }

        #endregion
    }
}