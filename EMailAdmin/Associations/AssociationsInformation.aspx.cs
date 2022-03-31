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
    public partial class AssociationsInformation : CustomPage
    {
        #region Constructor

        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                SessionManager.SetTemplate(
                    Request.QueryString["IdTemplate"] == null
                        ? new BackEnd.Domain.Template()
                        : TemplateHome.Get(Convert.ToInt32(Request.QueryString["IdTemplate"])), Session);
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

        protected void BtnCancelGroupClick(object sender, EventArgs e)
        {
            mpeGroupControl.Hide();
            grcGroup.ClearControls();
            mpeGroup.Show();
        }

        protected void BtnAcceptOnClick(object sender, EventArgs e)
        {
            var toSave = ascAssociation.TemplateGroups;
            if (toSave.Count > 0)
            {
                try
                {
                    ServiceLocator.Instance().GetTemplateService().SaveAssociations(toSave, 
                        SessionManager.GetLoguedUser(Session).Id);
                    Response.Redirect("AssociationsList.aspx");
                }
                catch (NonSavedObjectException ex)
                {
                    //ACA SE AVISA QUE NO PUDO GUARDAR.
                }
            }
        }

        protected void BtnCancelOnClick(object sender, EventArgs e)
        {
            Response.Redirect("AssociationsList.aspx");
        }

        protected void BtnCancelGroupViewerClick(object sender, EventArgs e)
        {
            mpeGroup.Hide();
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

        protected void AscAssociationDeletePressed(object sender, EventArgs e)
        {
            mpeDelete.Show();
        }

        protected void ShowGroupAssociations(object sender, EventArgs e)
        {
            grpGroup.BindGridGrupos();
            mpeGroup.Show();
        }

        protected void BtnAcceptDeleteOnClick(object sender, EventArgs e)
        {
            ascAssociation.DeleteAssociation();
        }

        #endregion Methods
    }
}