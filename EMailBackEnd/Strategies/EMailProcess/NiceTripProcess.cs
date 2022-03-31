using System.Collections.Generic;
using EMailAdmin.ExternalServices.Domain;
using EMailAdmin.BackEnd.Home.External;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Strategies.EMailProcess
{
    public class NiceTripProcess : AbstractEMailProcess
    {
        public override string GetTypeCode()
        {
            return "Trip";
        }

        private IList<BaseEnvio> GetEffectiveStartDateIssuances()
        {
            return IssuanceInformationHome.Find();
            //return IssuanceInformationHome.FindEffectiveStartDate(540, 1);
        }

        protected override void SendMails()
        {
            if (CapaNegocioDatos.CapaHome.CodigoActivadorHome.HabilitaNiceTrip())
            {
                IList<BaseEnvio> issuances = GetEffectiveStartDateIssuances();

                foreach (BaseEnvio issuance in issuances)
                {
                    int IdIdioma = CapaNegocioDatos.CapaHome.SucursalHome.ObtenerIdIdioma(issuance.CountryCode, issuance.AgencyCode, issuance.BranchNumber);

                    var dto = new DefaultEMailDTO();
                    dto.IdLanguage = IdIdioma;
                    dto.To = issuance.PaxEMail;
                    dto.CountryCode = issuance.CountryCode;
                    dto.VoucherCode = issuance.VoucherCode;
                    dto.RecipientName = issuance.PaxName;
                    dto.RecipientSurname = issuance.PaxSurname;
                    dto.RecipientFullName = issuance.PaxName + " " + issuance.PaxSurname;
                    dto.RecipientDocumentNumber = issuance.PaxPassport;                 
                    dto.ModuleCode = "ACNET";
                    dto.CompleteVoucherCode = issuance.CountryCode.ToString() + issuance.VoucherCode;
                    ServiceLocator.Instance().GetSendMailService().SendMailNiceTrip(dto);

                    if (issuance.bEliminar)
                        issuance.Eliminar();
                }
            }
        }

        protected override void SendMails(int id)
        {
        }
    }
}
