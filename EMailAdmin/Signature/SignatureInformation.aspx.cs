using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;
using CapaNegocioDatos.CapaNegocio;
using ControlMenu;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Exceptions;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.Utils;
using MenuPortalLibrary.CapaDTO;

namespace EMailAdmin.Signature
{
    public partial class SignatureInformation : CustomPage
    {
        #region Constructor

        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["IdSignature"] == null)
                {
                    SessionManager.SetSignature(new BackEnd.Domain.Signature(), Session);
                    btnDelete.Visible = false;
                }
                else
                {
                    SessionManager.SetSignature(SignatureHome.Get(Convert.ToInt32(Request.QueryString["IdSignature"])),
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
            Response.Redirect("SignatureList.aspx");
        }

        protected void BtnAcceptDeleteOnClick(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    ServiceLocator.Instance().GetSignatureService().Delete(SessionManager.GetSignature(Session));
                }
                catch (NonEliminatedObjectException ex)
                {
                    //ACA SE AVISA QUE NO PUDO ELIMINAR.
                }
                finally
                {
                    Response.Redirect("SignatureList.aspx");
                }
            }
        }

        protected void BtnAcceptOnClick(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    ServiceLocator.Instance().GetSignatureService().Save(GetSignature());
                    Response.Redirect("SignatureList.aspx");
                }
                catch (NonSavedObjectException ex)
                {
                    //ACA SE AVISA DE DATOS INCOMPLETOS.
                }
            }
        }

        protected void SntSignatureTypeLoadCompleted(object sender, EventArgs e)
        {
            BackEnd.Domain.Signature signature = SessionManager.GetSignature(Session);
            if (signature != null)
            {
                sntSignatureType.SelectItems(signature.SignatureTypes);
            }
        }

        protected void CsrCountryLoadCompleted(object sender, EventArgs e)
        {
            BackEnd.Domain.Signature signature = SessionManager.GetSignature(Session);
            if (signature != null)
            {
                ctrCountry.SelectItems(signature.Countries);
            }
        }

        protected void CtmCountryValidatorComplete(object sender, ServerValidateEventArgs validation)
        {
            validation.IsValid = ctrCountry.GetSelectedItems().Count > 0;
            return;
        }

        protected void CtmSignatureTypeValidatorComplete(object sender, ServerValidateEventArgs validation)
        {
            validation.IsValid = sntSignatureType.GetSelectedItems().Count > 0;
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

        #endregion Methods

        #region Private Methods

        private BackEnd.Domain.Signature GetSignature()
        {
            BackEnd.Domain.Signature signatureSession = SessionManager.GetSignature(Session);
            signatureSession.Name = txtName.Text.Trim();
            signatureSession.Countries = ctrCountry.GetSelectedItems();
            signatureSession.SignatureTypes = sntSignatureType.GetSelectedItems();
            signatureSession.Content = GetContents();
            signatureSession.IdUsuario = SessionManager.GetLoguedUser(Session).Id;
            return signatureSession;
        }

        private IList<SignatureContent> GetContents()
        {
            var list = new List<SignatureContent>();

            foreach (var value in trtDescription.GetValuesHtml())
            {
                var signatureContent = new SignatureContent
                                           {Content = value.Value, Language = new Idioma {Id = value.Key}, IdUsuario = SessionManager.GetLoguedUser(Session).Id};
                list.Add(signatureContent);
            }

            return list;
        }

        private void CompleteControls()
        {
            BackEnd.Domain.Signature signature = SessionManager.GetSignature(Session);
            txtName.Text = signature.Name;
            var signatures = new Dictionary<int, string>();
            foreach (SignatureContent signatureContent in signature.Content)
            {
                signatures.Add(signatureContent.Language.Id, signatureContent.Content);
            }
            trtDescription.SetValues(signatures);
        }

        private bool ValidateName()
        {
            IList<BackEnd.Domain.Signature> signatures = SignatureHome.FindByName(txtName.Text.Trim());
            if (SessionManager.GetSignature(Session).Id != 0)
            {
                //MOD
                foreach (var signature in signatures)
                {
                    if (signature.Id != SessionManager.GetSignature(Session).Id)
                    {
                        return false;
                    }
                }
            }
            else
            {
                //NEW
                return signatures.Count <= 0;
            }
            return true;
        }

        #endregion Private Methods   
    }
}