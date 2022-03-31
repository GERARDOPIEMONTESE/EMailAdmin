using System;
using System.Configuration;
using System.Web.UI.WebControls;
using ControlMenu;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Exceptions;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.Utils;
using EMailAdmin.BackEnd.Service;

namespace EMailAdmin.Type.EMailList
{
    public partial class EMailListTypeInformation : CustomPage
    {
        #region Constructor

        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["IdEmailListType"] == null)
                {
                    SessionManager.SetEmailListType(new BackEnd.Domain.EMailListType(), Session);
                    btnDelete.Visible = false;
                    txtCode.Enabled = true;
                    divMensaje.Visible = false;                    
                }
                else
                {
                    SessionManager.SetEmailListType(EMailListTypeHome.Get(Convert.ToInt32(Request.QueryString["IdEmailListType"])),
                                                   Session);
                    CompleteControls();
                }
            }
        }

        #endregion Constructor

        #region Propiedades

        protected override void ChequearSession()
        {
            var usuario = base.UsuarioLogueadoDTO();
            if (usuario == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings["ExpireSessionRedirect"]);
            }
        }

        #endregion Propiedades

        #region Methods

        private void CompleteControls()
        {
            EMailListType emailListType = SessionManager.GetEMailListType(Session);
            txtDescription.Text = emailListType.Description;
            txtCode.Text = emailListType.Code;
            litMensaje.Text = string.Format(litMensaje.Text, emailListType.UsuariosAsignados);
            ConfigPrepurchase(emailListType);
        }

        private void ConfigPrepurchase(EMailListType elt)
        {

            bool bUsusAsig = (elt.UsuariosAsignados > 0 ? true : false);
            btnDelete.Visible = !bUsusAsig;
            divMensaje.Visible = bUsusAsig;
            txtCode.Enabled = !bUsusAsig;

            if (elt.Code.Equals(EMailListType.CODEPREPURCHASELIST))
            {
                //no se puede borrar el tipo de lista precompra. Ni editarle el code.
                btnDelete.Visible = false;
                txtCode.Enabled = false;
            }
        }

        protected void BtnCancelOnClick(object sender, EventArgs e)
        {
            Redirect();
        }

        protected void BtnAcceptDeleteOnClick(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    GetEMailListType().Eliminar();
                    Redirect();
                }
                catch (NonEliminatedObjectException ex)
                {
                    //ACA SE AVISA QUE NO PUDO ELIMINAR.
                }
                finally
                {
                    Response.Redirect("EMailListTypeList.aspx");
                }
            }
        }

        protected void BtnAcceptOnClick(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    GetEMailListType().Persistir();
                    Redirect();
                }
                catch (NonSavedObjectException ex)
                {
                    
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
            Response.Redirect("EMailListTypeList.aspx");
        }


        private EMailListType GetEMailListType()
        {
            EMailListType eMailListType = SessionManager.GetEMailListType(Session);
            eMailListType.Code = txtCode.Text.Trim().ToUpper();
            eMailListType.Description = txtDescription.Text.Trim();
            eMailListType.IdUsuario = UsuarioLogueadoDTO().IdUsuario;
            return eMailListType;
        }

        private bool ValidateCode()
        {
            if (txtCode.Text.Trim().Length > 0)
            {
                var types = EMailListTypeHome.Find(txtCode.Text.Trim().ToUpper());
                if (types.Count > 0)
                {
                    if (types.Count == 1 && types[0].Id == Convert.ToInt32(Request.QueryString["IdEmailListType"]))
                        return true; //esta editando un type que no esta usado (si esta usado no habilita el txtCode)
                    else
                        return false;
                }
                return true;
            }
            else 
                return false;
        }

        #endregion Private Methods
    }
}