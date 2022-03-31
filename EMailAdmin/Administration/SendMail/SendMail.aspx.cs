using System;
using System.Web.UI.WebControls;
using CapaNegocioDatos.CapaHome;
using ControlMenu;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Service;

namespace EMailAdmin.Administration.SendMail
{
    public partial class SendMail : CustomPage
    {
        #region Constants

        private const string ModuleCode = "ACNET";
        private const string UserACNETService = "mailservice@assist-card.com";
        private const string PassACNETService = "123456";

        #endregion Constants

        #region Constructor

        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadDropDown();
            }
        }

        #endregion

        #region Methods

        protected void BtnAcceptOnClick(object sender, EventArgs e)
        {
            if (IsValid && ValidaEmailTo())
            {
                EMailEkitDTO dto = new EMailEkitDTO();
                dto.CountryCode = Convert.ToInt32(ddlCountry.SelectedValue);
                dto.VoucherCode = txtVoucher.Text == null || txtVoucher.Text.Length == 0 ? "0" : txtVoucher.Text.Trim();
                dto.ModuleCode = ModuleCode == null || ModuleCode.Length == 0 ? "-" : ModuleCode.Trim();
                if (cbMailTo.Checked)
                {
                    dto.GivenToAddress = cbMailTo.Checked;
                    dto.To = txtMailTo.Text;
                }

                ServiceLocator.Instance().GetSendMailService().SendMailEkit(dto);
                lblPopUp.Text = GetLocalResourceObject("VOUCHER_ENVIADO").ToString();
                mpePopUpMail.Show();
            }
        }

        protected void CtmVoucherOnValidate(object sender, ServerValidateEventArgs validation)
        {
            validation.IsValid = ValidateVoucher();
        }

        protected void CtmMailToOnValidate(object sender, ServerValidateEventArgs validation)
        {
            validation.IsValid= ValidaEmailTo();
        }

        private bool ValidaEmailTo()
        {
            bool bRst = true;
            bRst = !cbMailTo.Checked || (cbMailTo.Checked && txtMailTo.Text.Length > 0);            
            if (!bRst) ctmMailTo.ErrorMessage = GetLocalResourceObject("EMAIL_DESTINO").ToString();
            ctmMailTo.IsValid = bRst;
            return bRst;
        }
        #endregion

        #region Private Methods

        private void LoadDropDown()
        {
            ddlCountry.Items.Clear();
            ddlCountry.DataSource = PaisHome.BuscarLazy();
            ddlCountry.DataBind();
        }

        private bool ValidateVoucher()
        {
            bool bValid = true;
            int codigoVoucher = -1;
            if (int.TryParse(txtVoucher.Text, out codigoVoucher))
            {
                bValid = EMailLogHome.IsVoucherValid(ddlCountry.SelectedValue, txtVoucher.Text);
                if (!bValid) ctmVoucher.ErrorMessage = GetLocalResourceObject("VOUCHER_NOTEXIST").ToString();
            }
            else
            {
                ctmVoucher.ErrorMessage = GetLocalResourceObject("VOUCHER_CODEINVALID").ToString();
                bValid = false;
            }
                return bValid;
        }

        #endregion Private Methods
    }
}