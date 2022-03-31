using EMailAdmin.BackEnd.DTO;
using System;
using System.Collections.Generic;
namespace EMailAdmin.BackEnd.Service.Interfaces
{
    public interface ISendMailService
    {
        void SendMailEkit(string xml);

        void SendMailEkit(AbstractEMailDTO dto);

        void SendMailNiceTrip(AbstractEMailDTO dto);

        void SendMailWelcomeBack(AbstractEMailDTO dto);

        void SendMailPrepurchase(AbstractEMailDTO dto);
        
        void SendMailHappyBirthday(AbstractEMailDTO dto);

        void SendMailACCOMQuote(AbstractEMailDTO dto);

        void SendMailQuoteExchange(AbstractEMailDTO dto);

        void SendMailPrepurchasePax(AbstractEMailDTO dto);

        void SendMailPoints(AbstractEMailDTO dto);

        void SendMailConditionAlert(AbstractEMailDTO dto);

        void SendMailCapita(AbstractEMailDTO dto);

        void SendMailAlertChats(AbstractEMailDTO dto);

        void SendMailXAMCases(AbstractEMailDTO dto);

        void SendMailPoliza(AbstractEMailDTO dto);

        void SendMailEndoso(AbstractEMailDTO dto);

        void SendMailACCOMNotIssue(AbstractEMailDTO dto);

        void ProcessEMails();

        void ProcessEMails(int id);

        void ProcessNotSentEMails(int countryCode, DateTime fromDate, DateTime toDate, string module);

        byte[] FindAttachmentMailEkit(AbstractEMailDTO dto, out string attachName, Nullable<bool> attachMerge = null);

        byte[] FindAttachmentMailEkit(AbstractEMailDTO dto, FiltersAttachsDTO filtersAttach);

        byte[] FindAttachmentDynamicMail(AbstractEMailDTO dto, out string attachName, Nullable<bool> attachMerge = null);

        byte[] GetAttachments(AbstractEMailDTO dto);

        byte[] GetVoucherReportAttach(AbstractEMailDTO dto);

        void SendMailQuote(AbstractEMailDTO dto);

        void SendMailBotonPago(AbstractEMailDTO dto);

        void SendMailContinuaCompra(AbstractEMailDTO dto);

        void SendMailBalanceRequest(AbstractEMailDTO dto);

        void SendMailExternalPaymentFinish(AbstractEMailDTO dto);

        void SendMailDynamic(AbstractEMailDTO dto);

        byte[] GetPDFMail(AbstractEMailDTO dto, bool IsTag);

    }
}
