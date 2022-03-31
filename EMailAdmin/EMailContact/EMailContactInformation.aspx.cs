using System;
using System.Globalization;
using System.Web.UI.WebControls;
using ControlMenu;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Exceptions;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.Utils;

namespace EMailAdmin.EMailContact
{
    public partial class EMailContactInformation : CustomPage
    {
        #region Constructor

        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ParseId();
                LoadInformation();
            }
        }

        #endregion Constructor

        #region Methods

        protected void BtnCancelOnClick(object sender, EventArgs e)
        {
            Redirect();
        }

        protected void BtnDeleteOnClick(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    ServiceLocator.Instance().GetEMailContactService().Delete(GetEMailContactDTO());
                    Redirect();
                }
                catch (EMailAdminException ex)
                {
                }
            }
        }

        protected void BtnAcceptOnClick(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    ServiceLocator.Instance().GetEMailContactService().Save(GetEMailContactDTO());
                    Redirect();
                }
                catch (EMailAdminException ex)
                {
                }
            }
        }

        protected void CsrCountryLoadCompleted(object sender, EventArgs e)
        {
            var contact = SessionManager.GetEMailContact(Session);
            ctrCountry.SelectItems(contact.CountryIds);
        }

        protected void CtmCountryValidatorComplete(object sender, ServerValidateEventArgs validation)
        {
            validation.IsValid = ctrCountry.GetSelectedItems().Count > 0;
            return;
        }

        protected void CtmNameValidator(object sender, ServerValidateEventArgs validation)
        {
            validation.IsValid = ValidateName();
            return;
        }

        protected void CtmTabsValidator(object sender, ServerValidateEventArgs validation)
        {
            validation.IsValid = trtcDescription.IsValid();
            return;
        }

        #endregion

        #region Private methods

        private void Redirect()
        {
            Response.Redirect("EMailContactList.aspx");
        }

        private EMailContactDTO GetEMailContactDTO()
        {
            var dto = new EMailContactDTO
                          {
                              Id = SessionManager.GetIdEMailContact(Session),
                              Name = txtName.Text.Trim(),
                              Description = trtcDescription.GetValuesHtml(),
                              EMail = txtEMail.Text,
                              IdEMailContactType = Convert.ToInt32(ddlEMailContactType.SelectedValue),
                              Countries = ctrCountry.GetSelectedItems(),
                              IdUser = SessionManager.GetLoguedUser(Session).Id
                          };


            return dto;
        }

        private void ParseId()
        {
            int id;
            string value = Request.QueryString["IdEMailContact"];

            Int32.TryParse(value ?? "0", out id);
            SessionManager.SetIdEMailContact(id, Session);
            SessionManager.SetEMailContact(EMailContactHome.GetDTO(id), Session);
        }

        private void LoadInformation()
        {
            btnDelete.Visible = false;
            if (SessionManager.GetIdEMailContact(Session) != 0)
            {
                var contact = SessionManager.GetEMailContact(Session);

                btnDelete.Visible = true;
                txtName.Text = contact.Name;
                txtEMail.Text = contact.EMail;
                ddlEMailContactType.SelectedValue = contact.IdEMailContactType.ToString(CultureInfo.InvariantCulture);
                trtcDescription.SetValues(contact.Description);
            }
        }

        private bool ValidateName()
        {

            var contacts = EMailContactHome.Find(txtName.Text.Trim());
            if (SessionManager.GetEMailContact(Session).Id != 0)
            {
                //MOD
                foreach (var contact in contacts)
                {
                    if (contact.Id != SessionManager.GetEMailContact(Session).Id)
                    {
                        return false;
                    }
                }
            }
            else
            {
                //NEW
                return contacts.Count <= 0;
            }
            return true;
        }

        #endregion
    }
}