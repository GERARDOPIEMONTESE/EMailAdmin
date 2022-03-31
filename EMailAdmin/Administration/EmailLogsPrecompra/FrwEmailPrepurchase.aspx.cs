using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ControlMenu;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Service;
using CapaNegocioDatos.Servicios;
using CapaNegocioDatos.CapaHome;
using CapaNegocioDatos.CapaNegocio;

namespace EMailAdmin.Administration.EmailLogsPrecompra
{
    public partial class FrwEmailPrepurchase : CustomPage
    {
        #region Constants

        private const string ModuleCode = "ACNET";
        private const string UserACNETService = "mailservice@assist-card.com";
        private const string PassACNETService = "123456";

        #endregion Constants

        #region Constructor

        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDropDown();
            }
        }      

        #endregion

        #region Methods

        protected void BtnAcceptOnClick(object sender, EventArgs e)
        {
            try
            {
                if (ctmMailTo.IsValid && Validar())
                {
                    ForwardEmail(GetLogPrepurchasePax());
                    lblPopUp.Text = "Email reenviado";
                    Limpiar();
                }
            }
            catch (Exception ex)
            {
                object sMsg = GetLocalResourceObject(ex.Message);
                lblPopUp.Text = (sMsg!=null? sMsg.ToString() :  ex.Message);
            }
            mpePopUpMail.Show();
        }

        private void Limpiar()
        {
            txtCodigoPaxBox.Text = "";
            txtCodigoVerif.Text = "";
            txtVoucherGroup.Text = "";
            ddlPais.ClearSelection();
        }

        private EmailLog_R_PrepurchasePax GetLogPrepurchasePax()
        {
            EmailLog_R_PrepurchasePax log = new EmailLog_R_PrepurchasePax();

            int codigoPais = -1;
            int.TryParse(ddlPais.SelectedValue, out codigoPais);

            int codigoPaxBox = -1;
            if (txtCodigoPaxBox.Text != "")
            {
                if (!int.TryParse(txtCodigoPaxBox.Text, out codigoPaxBox))
                    throw new Exception(string.Format(GetLocalResourceObject("ERRORFORMATNUMBER").ToString(), lblCodePaxBox.Text));

                log = new EmailLog_R_PrepurchasePax()
                {
                    CodigoPaxBox = codigoPaxBox,
                    Pais = (codigoPais > 0 ? PaisHome.ObtenerPorCodigo(codigoPais.ToString()) : new Pais() { Codigo = "0" }),
                    VoucherGroup = txtVoucherGroup.Text,
                    CodigoVerif = txtCodigoVerif.Text
                };
            }
            else            
                log = EmailLog_R_PrepurchasePaxHome.GetByCodigoVerif(codigoPaxBox, txtCodigoVerif.Text, codigoPais, txtVoucherGroup.Text);
            
            if (log.CodigoPaxBox<=0)
                throw new Exception(string.Format(GetLocalResourceObject("NOTFOUND").ToString(), lblCodePaxBox.Text));

            return log;
        }

        private void ForwardEmail(EmailLog_R_PrepurchasePax LogPrepurchasePax)
        {
           
            EMailPrepurchasePaxDTO dto = new EMailPrepurchasePaxDTO()
            {
                BoxPaxCode = LogPrepurchasePax.CodigoPaxBox,
                VoucherCode = LogPrepurchasePax.CodigoPaxBox.ToString(), //si da error que guarde el id en el log
                TemplateType = TemplateTypeHome.Get(ddlTipoEmailPrepurchase.SelectedValue),                
                ModuleCode = ModuleCode,
                IdLanguage = 1,
                groupVoucher = LogPrepurchasePax.VoucherGroup,
                CountryCode = (LogPrepurchasePax.Pais!=null ? int.Parse(LogPrepurchasePax.Pais.Codigo) : 0),
                PaxEMail = (cbMailTo.Checked? txtMailTo.Text : "")
            };
            
            ServicioBroker.Instancia().ObtenerServicioUsuario().ChequearUsuario(UserACNETService, PassACNETService);
            ServiceLocator.Instance().GetSendMailService().SendMailPrepurchasePax(dto);

        }

        private void LoadDropDown()
        {
            ddlTipoEmailPrepurchase.DataSource = TemplateTypeHome.GetPrepurchasePax();
            ddlTipoEmailPrepurchase.DataBind();

            ddlPais.DataSource = CapaNegocioDatos.CapaHome.PaisHome.Buscar();
            ddlPais.DataBind();
            ddlPais.Items.Insert(0, new ListItem("", "-1"));
        }
        protected void CtmMailToOnValidate(object sender, ServerValidateEventArgs validation)
        {
            validation.IsValid = !cbMailTo.Checked || (cbMailTo.Checked && txtMailTo.Text.Length > 0);
            return;
        }
        #endregion Methods

        protected bool Validar()
        {
            bool bValid = true;
            
            if (txtCodigoPaxBox.Text == "")
            {
                if (ddlTipoEmailPrepurchase.SelectedValue == TemplateType.PrepurchaseType.BoxPaxBuy.ToString())
                {
                    bValid = (txtCodigoVerif.Text != "");
                    if (!bValid) throw new Exception(GetLocalResourceObject("DATABOXPAXBUYSEARCH").ToString());
                }
                else
                {
                    bValid = ((txtCodigoPaxBox.Text != "" || txtCodigoVerif.Text != "") && txtVoucherGroup.Text != "");
                    if (!bValid) throw new Exception(GetLocalResourceObject("DATABOXPAXSEARCH").ToString());
                }
            }
            return bValid;     
        }
    }
}