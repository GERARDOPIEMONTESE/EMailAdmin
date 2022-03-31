using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;
using CapaNegocioDatos.CapaHome;
using CapaNegocioDatos.CapaNegocio;
using ControlMenu;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Exceptions;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.Utils;
using MenuPortalLibrary.CapaDTO;

namespace EMailAdmin.UpgradeVariableText
{
    public partial class UpgradeVariableTextInformation : CustomPage
    {
        #region Constructor

        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            ClientScript.RegisterOnSubmitStatement(GetType(), String.Concat(ClientID, "_OnSubmit"), "javascript: return OvrdSubmit();");
            if (!IsPostBack)
            {
                LoadCombos();
                if (Request.QueryString["IdUpgradeVariableText"] == null)
                {
                    SessionManager.SetUpgradeVariableText(new BackEnd.Domain.UpgradeVariableText(), Session);
                    btnDelete.Visible = false;
                }
                else
                {
                    SessionManager.SetUpgradeVariableText(UpgradeVariableTextHome.Get(Convert.ToInt32(Request.QueryString["IdUpgradeVariableText"])),
                                                Session);
                    btnDelete.Visible = true;
                    CompleteControls();
                }
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

        protected void BtnCancelOnClick(object sender, EventArgs e)
        {
            Response.Redirect("UpgradeVariableTextList.aspx");
        }

        protected void BtnAcceptDeleteOnClick(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    ServiceLocator.Instance().GetUpgradeVariableTextService().Delete(SessionManager.GetUpgradeVariableText(Session));
                }
                catch (NonEliminatedObjectException ex)
                {
                    //ACA SE AVISA QUE NO PUDO ELIMINAR.
                }
                finally
                {
                    Response.Redirect("UpgradeVariableTextList.aspx");
                }
            }
        }

        protected void BtnAcceptOnClick(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    ServiceLocator.Instance().GetUpgradeVariableTextService().Save(GetUpgradeVariableText());
                    Response.Redirect("UpgradeVariableTextList.aspx");
                }
                catch (NonSavedObjectException ex)
                {
                    //ACA SE AVISA DE DATOS INCOMPLETOS.
                }
            }
        }

        protected void SntUpgradeVariableTextTypeLoadCompleted(object sender, EventArgs e)
        {
            BackEnd.Domain.UpgradeVariableText upgradeVariableText = SessionManager.GetUpgradeVariableText(Session);
            if (upgradeVariableText != null)
            {
                sntUpgradeVariableTextType.SelectItems(upgradeVariableText.UpgradeVariableTextTypes);
            }
        }

        protected void UvtUpgradesLoadCompleted(object sender, EventArgs e)
        {
            BackEnd.Domain.UpgradeVariableText upgradeVariableText = SessionManager.GetUpgradeVariableText(Session);
            if (upgradeVariableText != null)
            {
                uvtUpgrades.SelectItems(upgradeVariableText.Upgrades);
            }
        }

        protected void CtmProductValidatorComplete(object sender, ServerValidateEventArgs validation)
        {
            validation.IsValid = uvtUpgrades.GetSelectedItems().Count > 0;
            return;
        }

        protected void CtmUpgradeVariableTextTypeValidatorComplete(object sender, ServerValidateEventArgs validation)
        {
            validation.IsValid = sntUpgradeVariableTextType.GetSelectedItems().Count > 0;
            return;
        }

        protected void CtmNameValidator(object sender, ServerValidateEventArgs validation)
        {
            validation.IsValid = ValidateName();
            return;
        }

        protected void CtmTabsValidator(object sender, ServerValidateEventArgs validation)
        {
            validation.IsValid = trtDescription.IsValid();
            return;
        }

        protected void DdlCountrySelectedIndexChanged(object sender, EventArgs e)
        {
            SessionManager.SetUpgradeCountryCode(ddlCountry.SelectedValue, Session);
            uvtUpgrades.CleanAndBind();
        }

        #endregion Methods

        #region Private Methods

        private BackEnd.Domain.UpgradeVariableText GetUpgradeVariableText()
        {
            BackEnd.Domain.UpgradeVariableText upgradeVariableTextSession = SessionManager.GetUpgradeVariableText(Session);
            upgradeVariableTextSession.Name = txtName.Text.Trim();
            upgradeVariableTextSession.Upgrades = uvtUpgrades.GetSelectedItems();
            upgradeVariableTextSession.UpgradeVariableTextTypes = sntUpgradeVariableTextType.GetSelectedItems();
            upgradeVariableTextSession.Content = GetContents();
            upgradeVariableTextSession.IdUsuario = SessionManager.GetLoguedUser(Session).Id;
            return upgradeVariableTextSession;
        }

        private IList<UpgradeVariableTextContent> GetContents()
        {
            var list = new List<UpgradeVariableTextContent>();

            foreach (var value in trtDescription.GetValuesHtml())
            {
                var upgradeVariableTextContent = new UpgradeVariableTextContent
                                           {Content = value.Value, Language = new Idioma {Id = value.Key}, IdUsuario = SessionManager.GetLoguedUser(Session).Id};
                list.Add(upgradeVariableTextContent);
            }

            return list;
        }

        private void CompleteControls()
        {
            BackEnd.Domain.UpgradeVariableText upgradeVariableText = SessionManager.GetUpgradeVariableText(Session);
            txtName.Text = upgradeVariableText.Name;
            var upgradeVariableTexts = new Dictionary<int, string>();
            foreach (UpgradeVariableTextContent upgradeVariableTextContent in upgradeVariableText.Content)
            {
                upgradeVariableTexts.Add(upgradeVariableTextContent.Language.Id, upgradeVariableTextContent.Content);
            }
            trtDescription.SetValues(upgradeVariableTexts);
            
            try
            {
                ddlCountry.SelectedItem.Selected = false;
                ddlCountry.Items.FindByValue(upgradeVariableText.Upgrades.Count > 0 ? upgradeVariableText.Upgrades[0].CountryCode : "").Selected = true;
                SessionManager.SetUpgradeCountryCode(ddlCountry.SelectedValue, Session);
                uvtUpgrades.CleanAndBind();
                uvtUpgrades.SelectItems(upgradeVariableText.Upgrades);
            }
            catch{}
        }

        private bool ValidateName()
        {
            IList<BackEnd.Domain.UpgradeVariableText> upgradeVariableTexts = UpgradeVariableTextHome.FindByName(txtName.Text.Trim());
            if (SessionManager.GetUpgradeVariableText(Session).Id != 0)
            {
                //MOD
                foreach (var upgradeVariableText in upgradeVariableTexts)
                {
                    if (upgradeVariableText.Id != SessionManager.GetUpgradeVariableText(Session).Id)
                    {
                        return false;
                    }
                }
            }
            else
            {
                //NEW
                return upgradeVariableTexts.Count <= 0;
            }
            return true;
        }

        private void LoadCombos()
        {
            ddlCountry.Items.Clear();
            ddlCountry.DataSource = PaisHome.BuscarLazy();
            ddlCountry.DataBind();
        }

        #endregion Private Methods   
    }
}