using System;
using System.Configuration;
using System.Web.UI.WebControls;
using ControlMenu;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Exceptions;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.Utils;
using MenuPortalLibrary.CapaDTO;
using System.Globalization;

namespace EMailAdmin.Type.Signature
{
    public partial class SignatureTypeInformation : CustomPage
    {
        #region Constructor

        protected override void CustomPageInit(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture =
                CultureInfo.CreateSpecificCulture("ja-JP");
            base.CustomPageInit(sender, e);
        }

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
                    GetSignatureType().Persistir();
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
            Response.Redirect("SignatureTypeList.aspx");
        }

        private SignatureType GetSignatureType()
        {
            var signatureType = new SignatureType
                                    {
                                        Code = txtCode.Text.Trim().ToUpper(),
                                        Description = txtDescription.Text.Trim(),
                                        IdUsuario = SessionManager.GetLoguedUser(Session).Id
                                    };
            return signatureType;
        }

        private bool ValidateCode()
        {
            var type = SignatureTypeHome.GetByCode(txtCode.Text.Trim().ToUpper());
            if (type.Id != 0)
            {
                return false;
            }
            return true;
        }

        #endregion Private Methods
    }
}