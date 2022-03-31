using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ControlMenu;
using EMailAdmin.Utils;
using EMailAdmin.BackEnd.Home;
using System.Configuration;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.BackEnd.Exceptions;
using MenuPortalLibrary.CapaDTO;
using EMailAdmin.BackEnd.Domain;
using CapaNegocioDatos.CapaNegocio;

namespace EMailAdmin.ConditionVariableText
{
    public partial class ConditionVariableTextInformation : CustomPage
    {
        #region Constructor

        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            ClientScript.RegisterOnSubmitStatement(GetType(), String.Concat(ClientID, "_OnSubmit"), "javascript: return OvrdSubmit();");
            if (!IsPostBack)
            {
                ddlVariableText.CleanAndBind();
                if (Request.QueryString["IdConditionVariableText"] == null)
                {
                    SessionManager.SetConditionVariableText(new BackEnd.Domain.ConditionVariableText(), Session);
                    btnDelete.Visible = false;
                }
                else
                {
                    SessionManager.SetConditionVariableText(ConditionVariableTextHome.Get(Convert.ToInt32(Request.QueryString["IdConditionVariableText"])),
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
            Response.Redirect("ConditionVariableTextList.aspx");
        }

        protected void BtnAcceptDeleteOnClick(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    ServiceLocator.Instance().GetConditionVariableTextService().Delete(SessionManager.GetConditionVariableText(Session));
                }
                catch (NonEliminatedObjectException ex)
                {
                    //ACA SE AVISA QUE NO PUDO ELIMINAR.
                }
                finally
                {
                    Response.Redirect("ConditionVariableTextList.aspx");
                }
            }
        }

        protected void BtnAcceptOnClick(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    ServiceLocator.Instance().GetConditionVariableTextService().Save(GetConditionVariableText());
                    Response.Redirect("ConditionVariableTextList.aspx");
                }
                catch (NonSavedObjectException ex)
                {
                    //ACA SE AVISA DE DATOS INCOMPLETOS.
                }
            }
        }

        protected void ddlVariableTextGridLoadCompleted(object sender, EventArgs e)
        {
            CargarVariableTextGuardada();
        }

        protected void ddlVariableTextGridCancelCompleted(object sender, EventArgs e)
        {
            CargarVariableTextGuardada();
        }

        private void CargarVariableTextGuardada()
        {
            BackEnd.Domain.ConditionVariableText conditionVariableText = SessionManager.GetConditionVariableText(Session);
            if (conditionVariableText != null && conditionVariableText.Name != null)
            {
                ddlVariableText.SelectItems(conditionVariableText.VariablesText);
            }
        }

        protected void ctmVariableTextValidatorComplete(object sender, ServerValidateEventArgs validation)
        {
            validation.IsValid = ddlVariableText.GetSelectedItems().Count > 0;
            return;
        }
        
        protected void CtmNameValidator(object source, ServerValidateEventArgs validation)
        {            
            validation.IsValid = ValidateName();
            return;
        }

        protected void CtmTabsValidator(object source, ServerValidateEventArgs validation)
        {
            validation.IsValid = trtDescription.IsValid();
            return;
        }

        #endregion Methods

        #region Private Methods

        private BackEnd.Domain.ConditionVariableText GetConditionVariableText()
        {
            BackEnd.Domain.ConditionVariableText ConditionVariableTextSession = SessionManager.GetConditionVariableText(Session);
            ConditionVariableTextSession.Name = txtName.Text.Trim();
            ConditionVariableTextSession.VariablesText = ddlVariableText.GetSelectedItems();
            ConditionVariableTextSession.Contents = GetContents();
            ConditionVariableTextSession.IdUsuario = SessionManager.GetLoguedUser(Session).IdUsuario;
            return ConditionVariableTextSession;
        }

        private IList<ConditionVariableTextContent> GetContents()
        {
            var list = new List<ConditionVariableTextContent>();

            foreach (var value in trtDescription.GetValuesHtml())
            {
                var ConditionVariableTextContent = new ConditionVariableTextContent { Content = value.Value, Language = new Idioma { Id = value.Key }, IdUsuario = SessionManager.GetLoguedUser(Session).Id };
                list.Add(ConditionVariableTextContent);
            }

            return list;
        }

        private void CompleteControls()
        {
            BackEnd.Domain.ConditionVariableText ConditionVariableText = SessionManager.GetConditionVariableText(Session);
            txtName.Text = ConditionVariableText.Name;
                        
            ddlVariableText.SelectItems(ConditionVariableText.VariablesText);
            
            var ConditionVariableTexts = new Dictionary<int, string>();
            foreach (ConditionVariableTextContent ConditionVariableTextContent in ConditionVariableText.Contents)
            {
                ConditionVariableTexts.Add(ConditionVariableTextContent.Language.Id, ConditionVariableTextContent.Content);
            }
            trtDescription.SetValues(ConditionVariableTexts);

            try
            {
                
            }
            catch { }
        }

        private bool ValidateName()
        {
            BackEnd.Domain.ConditionVariableText ConditionVariableText = ConditionVariableTextHome.FindByName(txtName.Text.Trim(), true);
            return (SessionManager.GetConditionVariableText(Session).Id != 0 && ConditionVariableText.Id == 0) || (ConditionVariableText.Id == SessionManager.GetConditionVariableText(Session).Id);
        }

        #endregion Private Methods

        
    }
}