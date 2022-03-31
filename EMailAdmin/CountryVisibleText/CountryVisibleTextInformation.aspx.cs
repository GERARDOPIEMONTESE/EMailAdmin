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

namespace EMailAdmin.CountryVisibleText
{
    public partial class CountryVisibleTextInformation : CustomPage
    {
        #region Constructor

        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["IdCountryVisibleText"] == null)
                {
                    SessionManager.SetCountryVisibleText(new BackEnd.Domain.CountryVisibleText(), Session);
                    btnDelete.Visible = false;
                }
                else
                {
                    SessionManager.SetCountryVisibleText(CountryVisibleTextHome.Get(Convert.ToInt32(Request.QueryString["IdCountryVisibleText"])),
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
            Response.Redirect("CountryVisibleTextList.aspx");
        }

        protected void BtnAcceptDeleteOnClick(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    ServiceLocator.Instance().GetCountryVisibleTextService().Delete(SessionManager.GetCountryVisibleText(Session));
                }
                catch (NonEliminatedObjectException ex)
                {
                    //ACA SE AVISA QUE NO PUDO ELIMINAR.
                }
                finally
                {
                    Response.Redirect("CountryVisibleTextList.aspx");
                }
            }
        }

        protected void BtnAcceptOnClick(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    ServiceLocator.Instance().GetCountryVisibleTextService().Save(GetCountryVisibleText());
                    Response.Redirect("CountryVisibleTextList.aspx");
                }
                catch (NonSavedObjectException ex)
                {
                    //ACA SE AVISA DE DATOS INCOMPLETOS.
                }
            }
        }

        protected void SntCountryVisibleTextTypeLoadCompleted(object sender, EventArgs e)
        {
            BackEnd.Domain.CountryVisibleText CountryVisibleText = SessionManager.GetCountryVisibleText(Session);
            if (CountryVisibleText != null)
            {
                sntCountryVisibleTextType.SelectItems(CountryVisibleText.CountryVisibleTextTypes);
            }
        }

        protected void CsrCountryLoadCompleted(object sender, EventArgs e)
        {
            BackEnd.Domain.CountryVisibleText CountryVisibleText = SessionManager.GetCountryVisibleText(Session);
            if (CountryVisibleText != null)
            {
                ctrCountry.SelectItems(CountryVisibleText.Countries);
            }
        }

        protected void CtmCountryValidatorComplete(object sender, ServerValidateEventArgs validation)
        {
            validation.IsValid = ctrCountry.GetSelectedItems().Count > 0;
            return;
        }

        protected void CtmCountryVisibleTextTypeValidatorComplete(object sender, ServerValidateEventArgs validation)
        {
            validation.IsValid = sntCountryVisibleTextType.GetSelectedItems().Count > 0;
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

        private BackEnd.Domain.CountryVisibleText GetCountryVisibleText()
        {
            BackEnd.Domain.CountryVisibleText CountryVisibleTextSession = SessionManager.GetCountryVisibleText(Session);
            CountryVisibleTextSession.Name = txtName.Text.Trim();
            CountryVisibleTextSession.Countries = ctrCountry.GetSelectedItems();
            CountryVisibleTextSession.CountryVisibleTextTypes = sntCountryVisibleTextType.GetSelectedItems();
            CountryVisibleTextSession.Content = GetContents();
            CountryVisibleTextSession.IdUsuario = SessionManager.GetLoguedUser(Session).Id;
            return CountryVisibleTextSession;
        }

        private IList<CountryVisibleTextContent> GetContents()
        {
            var list = new List<CountryVisibleTextContent>();

            foreach (var value in trtDescription.GetValuesHtml())
            {
                var CountryVisibleTextContent = new CountryVisibleTextContent
                                           {Content = value.Value, Language = new Idioma {Id = value.Key}, IdUsuario = SessionManager.GetLoguedUser(Session).Id};
                list.Add(CountryVisibleTextContent);
            }

            return list;
        }

        private void CompleteControls()
        {
            BackEnd.Domain.CountryVisibleText CountryVisibleText = SessionManager.GetCountryVisibleText(Session);
            txtName.Text = CountryVisibleText.Name;
            var CountryVisibleTexts = new Dictionary<int, string>();
            foreach (CountryVisibleTextContent CountryVisibleTextContent in CountryVisibleText.Content)
            {
                CountryVisibleTexts.Add(CountryVisibleTextContent.Language.Id, CountryVisibleTextContent.Content);
            }
            trtDescription.SetValues(CountryVisibleTexts);
        }

        private bool ValidateName()
        {
            IList<BackEnd.Domain.CountryVisibleText> CountryVisibleTexts = CountryVisibleTextHome.FindByName(txtName.Text.Trim());
            if (SessionManager.GetCountryVisibleText(Session).Id != 0)
            {
                //MOD
                foreach (var CountryVisibleText in CountryVisibleTexts)
                {
                    if (CountryVisibleText.Id != SessionManager.GetCountryVisibleText(Session).Id)
                    {
                        return false;
                    }
                }
            }
            else
            {
                //NEW
                return CountryVisibleTexts.Count <= 0;
            }
            return true;
        }

        #endregion Private Methods   
    }
}