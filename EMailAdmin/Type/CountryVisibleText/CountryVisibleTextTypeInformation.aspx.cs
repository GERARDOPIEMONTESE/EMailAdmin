using System;
using System.Configuration;
using System.Web.UI.WebControls;
using ControlMenu;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Exceptions;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.Utils;
using MenuPortalLibrary.CapaDTO;

namespace EMailAdmin.Type.CountryVisibleText
{
    public partial class CountryVisibleTextTypeInformation : CustomPage
    {
        #region Constructor

        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //
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
                    GetCountryVisibleTextType().Persistir();
                    Redirect();
                }
                catch (NonSavedObjectException ex)
                {
                    //ACA SE AVISA DE DATOS INCOMPLETOS.
                }
            }
        }

        protected void CtmCodeValidator(object sender, ServerValidateEventArgs validation)
        {
            validation.IsValid = ValidateCode();
            return;
        }

        #endregion Methods

        #region Private Methods

        private void Redirect()
        {
            Response.Redirect("CountryVisibleTextTypeList.aspx");
        }

        private CountryVisibleTextType GetCountryVisibleTextType()
        {
            var CountryVisibleTextType = new CountryVisibleTextType
                                    {
                                        Code = txtCode.Text.Trim().ToUpper(),
                                        Description = txtDescription.Text.Trim(),
                                        IdUsuario = SessionManager.GetLoguedUser(Session).Id
                                    };
            return CountryVisibleTextType;
        }

        private bool ValidateCode()
        {
            var type = CountryVisibleTextTypeHome.GetByCode(txtCode.Text.Trim().ToUpper());
            if (type.Id != 0)
            {
                return false;
            }
            return true;
        }

        #endregion Private Methods
    }
}