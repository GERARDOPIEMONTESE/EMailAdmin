using System;
using System.Collections.Generic;
using ControlMenu;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Exceptions;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.Utils;

namespace EMailAdmin.Associations
{
    public partial class AttachmentAssociationsInformation : CustomPage
    {
        #region Constructor

        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SessionManager.SetAttachment(
                    Request.QueryString["IdAttachment"] == null
                        ? new BackEnd.Domain.Attachment()
                        : AttachmentHome.Get(Convert.ToInt32(Request.QueryString["IdAttachment"])), Session);
            }
        }

        #endregion Constructor

        #region Methods

        protected void BtnAcceptGroupViewerClick(object sender, EventArgs e)
        {
            mpeGroup.Hide();
            ascAssociation.Bind();
            grpGroup.ClearControls();
        }

        protected void BtnAcceptGroupClick(object sender, EventArgs e)
        {
            if (IsValid && grcGroup.CanSave())
            {
                ServiceLocator.Instance().GetGroupService().Save(grcGroup.Group);
                SessionManager.SetGroupConditions(new List<GroupCondition>(), Session);
                grpGroup.ReloadGroups();
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

        protected void BtnAcceptOnClick(object sender, EventArgs e)
        {
            var toSave = ascAssociation.AttachmentGroups;
            if (toSave.Count > 0)
            {
                try
                {
                    ServiceLocator.Instance().GetAttachmentService().SaveAssociations(toSave);
                    Response.Redirect("AttachmentAssociationsList.aspx");
                }
                catch (NonSavedObjectException ex)
                {
                    //ACA SE AVISA QUE NO PUDO GUARDAR.
                }
            }
        }

        protected void BtnCancelOnClick(object sender, EventArgs e)
        {
            Response.Redirect("AttachmentAssociationsList.aspx");
        }

        protected void ShowGroupControl(object sender, EventArgs e)
        {
            mpeGroupControl.Show();
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

        protected void BtnCancelGroupViewerClick(object sender, EventArgs e)
        {
            mpeGroup.Hide();
        }

        protected void AscAssociationDeletePressed(object sender, EventArgs e)
        {
            mpeDelete.Show();
        }

        protected void AscAssociationAddPressed(object sender, EventArgs e)
        {
            mpeGroup.Show();
        }

        protected void BtnAcceptDeleteOnClick(object sender, EventArgs e)
        {
            ascAssociation.DeleteAssociation();
        }

        protected void AddAttachmentAssociations(object sender, Attachment_R_Group grt)
        {
            var templates = SessionManager.GetAttachmentAssociations(Session);
            templates.Add(grt);
            SessionManager.SetAttachmentAssociations(templates, Session);
            mpeGroup.Show();
        }

        protected void DelAttachmentAssociations(object sender, Attachment_R_Group grt)
        {
            var templates = SessionManager.GetAttachmentAssociations(Session);
            templates.Remove(grt);
            SessionManager.SetAttachmentAssociations(templates, Session);
            mpeGroup.Show();
        }

        #endregion Methods
    }
}