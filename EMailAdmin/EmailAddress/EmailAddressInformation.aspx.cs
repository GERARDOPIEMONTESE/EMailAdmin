using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;
using ControlMenu;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Exceptions;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.Utils;
using MenuPortalLibrary.CapaDTO;

namespace EMailAdmin.EmailAddress
{
    public partial class EmailAddressInformation : CustomPage
    {
        #region Constructor

        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["IdEmailAddress"] == null)
                {
                    SessionManager.SetEmailAddress(new EMailAddress(), Session);
                }
                else
                {
                    SessionManager.SetEmailAddress(EMailAddressHome.Get(Convert.ToInt32(Request.QueryString["IdEmailAddress"])),
                                                Session);
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
            Redirect();
        }

        protected void BtnAcceptOnClick(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    GetEmailAddress().Persistir();
                    Redirect();
                }
                catch (NonSavedObjectException ex)
                {
                    //ACA SE AVISA DE DATOS INCOMPLETOS.
                }
            }
        }

        protected void CtmNameValidator(object sender, ServerValidateEventArgs validation)
        {
            validation.IsValid = ValidateName();
            return;
        }

        #endregion Methods

        #region Private Methods

        private void Redirect()
        {
            Response.Redirect("EmailAddressList.aspx");
        }

        private EMailAddress GetEmailAddress()
        {
            EMailAddress emailAddress = SessionManager.GetEmailAddress(Session);
            emailAddress.Name = txtName.Text.Trim();
            emailAddress.Address = txtEmail.Text.Trim();
            emailAddress.IdUsuario = SessionManager.GetLoguedUser(Session).Id;
            return emailAddress;
        }

        private void CompleteControls()
        {
            var emailAddress = SessionManager.GetEmailAddress(Session);
            txtName.Text = emailAddress.Name;
            txtEmail.Text = emailAddress.Address;
        }

        private bool ValidateName()
        {
            IList<EMailAddress> emails = EMailAddressHome.FindByFilters(txtName.Text, "");
            if (SessionManager.GetEmailAddress(Session).Id != 0)
            {
                //MOD
                foreach (var mailAddress in emails)
                {
                    if (mailAddress.Id != SessionManager.GetEmailAddress(Session).Id)
                    {
                        return false;
                    }
                }
            }
            else
            {
                //NEW
                return emails.Count <= 0;
            }
            return true;
        }

        #endregion Private Methods
    }
}