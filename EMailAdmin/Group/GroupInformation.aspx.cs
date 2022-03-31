using System;
using System.Collections.Generic;
using System.Configuration;
using ControlMenu;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Exceptions;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.Utils;
using MenuPortalLibrary.CapaDTO;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.Group
{
    public partial class GroupInformation : CustomPage
    {
        #region Constructor

        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["IdGroup"] != null)
                    SessionManager.SetGroupIdToEdit(int.Parse(Request.QueryString["IdGroup"]), Session);
                else
                    Limpiar();
            }
        }

        private void Limpiar()
        {
            SessionManager.SetGroupConditions(new List<GroupCondition>(), Session);
            SessionManager.DeleteGroupIdToEdit(Session);
        }

        #endregion Constructor

        #region Propiedades

        protected override void ChequearSession()
        {
            UsuarioDTO usuario = base.UsuarioLogueadoDTO();
            if (usuario == null)
                Response.Redirect(ConfigurationManager.AppSettings["ExpireSessionRedirect"]);
            
        }

        #endregion Propiedades

        #region Methods

        protected void BtnAcceptOnClick(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (grpGroup.CanSave())
                {
                    try
                    {
                        BackEnd.Domain.Group group = grpGroup.Group;
                        /*editando*/
                        int idGroup = SessionManager.GetGroupIdToEdit(Session);
                        if (idGroup != -1) group.Id = idGroup;
                        /*****/
                        group.IdUsuario = SessionManager.GetLoguedUser(Session).Id;
                        ServiceLocator.Instance().GetGroupService().Save(group);
                        SessionManager.SetGroupConditions(new List<GroupCondition>(), Session);
                        Response.Redirect("GroupList.aspx");
                    }
                    catch (NonSavedObjectException ex)
                    {
                        //ACA SE AVISA DE DATOS INCOMPLETOS.
                    }
                }
                else
                {
                    mpeMissing.Show();
                }
            }
        }

        private void RedirectToList()
        {

        }

        protected void BtnCancelOnClick(object sender, EventArgs e)
        {
            Limpiar();
            Response.Redirect("GroupList.aspx");
        }

        protected void AccAccountSelectorOnSearch(object sender, EventArgs e)
        {
            mpeAccount.Show();
        }

        protected void AccAccountSelectorOnClose(object sender, EventArgs e)
        {
            grpGroup.SetAccountText(accAccount.GetSelectedItemsText());
        }

        protected void CtrCountryOnClose(object sender, EventArgs e)
        {
            grpGroup.SetCountryText(ctrCountry.GetSelectedItemsText());
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
            grpGroup.SetProductText(proProduct.GetSelectedItemsText());
            
        }

        protected void RteRateSelectorOnSearch(object sender, EventArgs e)
        {
            mpeRate.Show();
        }

        protected void RteRateSelectorOnClose(object sender, EventArgs e)
        {
            grpGroup.SetRateText(rteRate.GetSelectedItemsText());
        }      

        protected void RteRateSelectorOnCountryChanged(object sender, EventArgs e)
        {
            mpeRate.Show();
        }

        protected void RteRateOnChkPressed(object sender, EventArgs e)
        {
            mpeRate.Show();
        }

        protected void DynamicValueOnClose(object sender, EventArgs e)
        {
            grpGroup.SetDynamicValue(dvcDynamicValueConfig.GetSelectedItemsText());
        }

        protected void DynamicValueOnSearch(object sender, EventArgs e)
        {
            mpeDynamicValue.Show();
        }

        protected void DynamicValueOnChkPressed(object sender, EventArgs e)
        {
            mpeDynamicValue.Show();
        }
        #endregion
    }
}