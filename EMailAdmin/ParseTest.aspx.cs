using System;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.AssistCardService;
using EMailAdmin.EmailSender;

namespace EMailAdmin
{
    public partial class ParseTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //EMailSenderService service = new EMailSenderService();
            ////service.SendMailEkit(540, "10332536", "ACNET", "acnet@acnet.com", "123456");
            //service.SendMailEkit(540, "10333591", "ACNET", "acnet@acnet.com", "123456");
            //

            //////service.SendMailEkit(540, "10332211", "ACNET", "acnet@acnet.com", "123456");

            //string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><EMailEkitVouchersDTO xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><VoucherCodes><string>10332211</string><string>10332148</string></VoucherCodes></EMailEkitVouchersDTO>";
            ////string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><EMailEkitVouchersDTO xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><VoucherCodes><string>10332211</string></VoucherCodes></EMailEkitVouchersDTO>";

            //string response = service.SendMultipleMailEkit(540, xml, "ACNET", "acnet@acnet.com", "123456");
            
            ////string response = service.SendMailEkit(540, "10332211", "ACNET", "acnet@acnet.com", "123456");
            //Response.Write(response);
            //Response.End();

            //Argentina
            //EMailEkitDTO dto = new EMailEkitDTO();
            //dto.CountryCode = 540;
            //dto.VoucherCode = "10332211";
            //dto.ModuleCode = "ACNET";

            //Continental
            //998 7114872
            //EMailEkitDTO dto = new EMailEkitDTO();
            //dto.CountryCode = 998;
            //dto.VoucherCode = "7114872";
            //dto.ModuleCode = "ACNET";

            //Corte Ingles
            //560 8001633
            //EMailEkitDTO dto = new EMailEkitDTO();
            //dto.CountryCode = 560;
            //dto.VoucherCode = "8001633";
            //dto.ModuleCode = "ACNET";

            //Gral Upgrade
            //540 540 10332536
            //EMailEkitDTO dto = new EMailEkitDTO();
            //dto.CountryCode = 540;
            //dto.VoucherCode = "10332536";
            //dto.ModuleCode = "ACNET";

            //Gral Upgrade con Upgrades
            //540 10332546
            //EMailEkitDTO dto = new EMailEkitDTO();
            //dto.CountryCode = 540;
            //dto.VoucherCode = "10332546";
            //dto.ModuleCode = "ACNET";

            //Home Products
            //540 10332538
            //EMailEkitDTO dto = new EMailEkitDTO();
            //dto.CountryCode = 540;
            //dto.VoucherCode = "10332538";
            //dto.ModuleCode = "ACNET";

            //RCCL
            //103 7011129
            //EMailEkitDTO dto = new EMailEkitDTO();
            //dto.CountryCode = 103;
            //dto.VoucherCode = "7011129";
            //dto.ModuleCode = "ACNET";

            //RCI
            //998 7114873
            //EMailEkitDTO dto = new EMailEkitDTO();
            //dto.CountryCode = 998;
            //dto.VoucherCode = "7114873";
            //dto.ModuleCode = "ACNET";

            //ServiceLocator.Instance().GetSendMailService().SendMailEkit(dto);


            //DefaultEMailDTO defaultDTO = new DefaultEMailDTO();
            //defaultDTO.IdLanguage = 1;
            //defaultDTO.To = "lcominotti@assist-card.com.ar";
            //defaultDTO.CountryCode = 540;
            //defaultDTO.VoucherCode = "10332546";
            //defaultDTO.ModuleCode = "ACNET";            
            //ServiceLocator.Instance().GetSendMailService().SendMailNiceTrip(defaultDTO);
            //ServiceLocator.Instance().GetSendMailService().SendMailWelcomeBack(defaultDTO);


            //EMailSenderInformationService serviceInfo = new EMailSenderInformationService();
            //serviceInfo.SetPaxPassedAway(540, "1234567", "1112", true, "xam@xam.com", "123456");
        }

        protected void BtnAceptarOnClick(object sender, EventArgs e)
        {
            var service = new EMailSenderService();
            string response = service.SendMailEkit(Convert.ToInt32(txtPais.Text), txtVoucher.Text, "ACNET", "mailservice@assist-card.com", "123456");
        }

        protected void BtnTestACNetOnClick(object sender, EventArgs e)
        {
            //string xml = service.findVoucherAllWithTarjeta(Convert.ToInt32(txtPais.Text), Convert.ToInt32(txtVoucher.Text.Trim()), "ACNET", "ACNET");
            ServicioAssistCard service = new ServicioAssistCard();
            string xml = service.getVoucherInfo(Convert.ToInt32(txtPais.Text), Convert.ToInt32(txtVoucher.Text.Trim()));

            Response.Write(xml);
            Response.End();
        }
        protected void BtnReprocessClick(object sender, EventArgs e)
        {
            ServiceLocator.Instance().GetSendMailService().ProcessEMails();
            //ServiceLocator.Instance().GetSendMailService().ProcessEMails();
            //var service = new EMailSenderService();
            //service.InitEMailProcess("mailservice@assist-card.com", "123456");
            //var service = new EMailSenderService();
            //string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><EMailEkitVouchersDTO xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><VoucherCodes><string>10736061</string><string>10736062</string><string>10736063</string><string>10736064</string><string>10736065</string><string>10736066</string><string>10736067</string></VoucherCodes></EMailEkitVouchersDTO>";
            //service.SendMultipleMailEkit(Convert.ToInt32(txtPais.Text), xml, "ACNET", "mailservice@assist-card.com", "123456");
        }
    }
}