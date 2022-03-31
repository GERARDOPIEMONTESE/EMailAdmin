using EMailAdmin.BackEnd.Service.Interfaces;
using EMailAdmin.BackEnd.Strategies.EMailSender;
using EMailAdmin.BackEnd.DTO;
using System.Collections.Generic;
using EMailAdmin.BackEnd.Strategies.EMailProcess;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;
using System;
using EMailAdmin.BackEnd.Domain.External;
using EMailAdmin.ExternalServices.Data;
using System.Threading;
using EMailAdmin.BackEnd.Strategies.Execution;

namespace EMailAdmin.BackEnd.Service
{
    public class SendMailService : ISendMailService
    {
        public void SendMailEkit(string xml)
        {
            EMailSenderStrategyLocator.Instance().GetEMailSenderEKit().SendEMail(xml);
        }

        public void SendMailEkit(AbstractEMailDTO dto)
        {
            EMailSenderStrategyLocator.Instance().GetEMailSenderEKit().SendEMail(dto);
        }

        public void SendMailPoints(AbstractEMailDTO dto)
        {
            EMailSenderStrategyLocator.Instance().GetEMailPoints().SendEMail(dto);
        }

        public void SendMailConditionAlert(AbstractEMailDTO dto)
        {
            EMailSenderStrategyLocator.Instance().GetEMailConditionAlert().SendEMail(dto);
        }

        public byte[] FindAttachmentMailEkit(AbstractEMailDTO dto, out string attachName, Nullable<bool> attachMerge = null)
        {
            //dto.IdAttachmentType = AttachmentTypeHome.GetByCode(AttachmentType.STRATEGY).Id;
            return EMailSenderStrategyLocator.Instance().GetEMailSenderEKit().FindAttachment(dto, out attachName, attachMerge);
        }

        public byte[] FindAttachmentMailEkit(AbstractEMailDTO dto, FiltersAttachsDTO filtersAttach)
        {
            //dto.IdAttachmentType = AttachmentTypeHome.GetByCode(AttachmentType.STRATEGY).Id;
            return EMailSenderStrategyLocator.Instance().GetEMailSenderEKit().FindAttachment(dto, filtersAttach);
        }

        public byte[] FindAttachmentDynamicMail(AbstractEMailDTO dto, out string attachName, Nullable<bool> attachMerge = null)
        {
            attachName = "";
            return EMailSenderStrategyLocator.Instance().GetEMailDynamicSender().GenerateAttachmentByTemplate(dto);
        }

        public byte[] GetAttachments(AbstractEMailDTO dto)
        {
            dto.IdAttachmentType = AttachmentTypeHome.GetByCode(AttachmentType.STRATEGY).Id;
            return EMailSenderStrategyLocator.Instance().GetEMailSenderEKit().GetAttachments(dto);
        }

        public byte[] GetVoucherReportAttach(AbstractEMailDTO dto)
        {
            dto.IdAttachmentType = AttachmentTypeHome.GetByCode(AttachmentType.STRATEGY).Id;
            return EMailSenderStrategyLocator.Instance().GetEMailPoints().GetAttachments(dto);
        }

        public void SendMailNiceTrip(AbstractEMailDTO dto)
        {
            EMailSenderStrategyLocator.Instance().GetEMailSenderNiceTrip().SendEMail(dto);
        }

        public void SendMailWelcomeBack(AbstractEMailDTO dto)
        {
            EMailSenderStrategyLocator.Instance().GetEMailSenderWelcomeBack().SendEMail(dto);
        }

        public void SendMailPrepurchase(AbstractEMailDTO dto)
        {
            EMailSenderStrategyLocator.Instance().GetEMailPrepurchase().SendEMail(dto);
        }

        public void SendMailHappyBirthday(AbstractEMailDTO dto)
        {
            EMailSenderStrategyLocator.Instance().GetEMailHappyBirthday().SendEMail(dto);
        }

        public void SendMailCapita(AbstractEMailDTO dto)
        {
            EMailSenderStrategyLocator.Instance().GetEMailCapita().SendEMail(dto);
        }

        //public void SendAlertaChat(AbstractEMailDTO dto)
        //{
        //    EMailSenderStrategyLocator.Instance().GetEmailAlertaChat().SendEMail(dto);
        //}

        public void SendMailACCOMQuote(AbstractEMailDTO dto)
        {
            ACCOMQuoteDTO AccomQuoteDto = (ACCOMQuoteDTO)dto;
            //Mails Step Quote
            foreach (QuoteMailACCOM pendingQuoteMail in QuoteMailACCOMHome.GetPendingQuotes())
            {
                AccomQuoteDto.IdPendingQuoteMail = pendingQuoteMail.IdQuoteLog;
                AccomQuoteDto.PurchaseProcessCode = pendingQuoteMail.PurchaseProcessCode;
                EMailSenderStrategyLocator.Instance().GetEmailACCOMQuote().SendEMail(dto);
            }
            //Mails Step PrePurchase
            foreach (QuoteMailACCOM prePurchaseQuotes in QuoteMailACCOMHome.GetPrePurchaseQuotes())
            {
                AccomQuoteDto.IdPendingQuoteMail = prePurchaseQuotes.IdQuoteLog;
                AccomQuoteDto.PurchaseProcessCode = prePurchaseQuotes.PurchaseProcessCode;
                EMailSenderStrategyLocator.Instance().GetEmailACCOMQuote().SendEMail(dto);
            }
        }

        public void SendMailQuoteExchange(AbstractEMailDTO dto)
        {
            QuoteExchangeDTO quoteExchangeDTO = (QuoteExchangeDTO)dto;
            EMailSenderStrategyLocator.Instance().GetEmailQuoteExchange().SendEMail(dto);
        }

        public void SendMailPrepurchasePax(AbstractEMailDTO dto)
        {
            EMailPrepurchasePaxDTO prepurchasePaxDTO = (EMailPrepurchasePaxDTO)dto;     
            EMailSenderStrategyLocator.Instance().GetEMailPrepurchasePax().SendEMail(dto);
            
        }

        public void SendMailAlertChats(AbstractEMailDTO dto)
        {
            //EmailAlertChatsDTO alertChatsDTO = (EmailAlertChatsDTO)dto;
            //EMailSenderStrategyLocator.Instance().GetEmailAlertaChat().SendEMail(dto);
            EMailSenderStrategyLocator.Instance().GetEmailAlertaChat().SendEMail(dto);
        }

        public void ProcessEMails()
        {
            ICollection<string> keys = EMailProcessStrategyContainer.Instance().GetProcessKeys();

            foreach (string key in keys)
            {
                if (EMailProcessStrategyContainer.Instance().GetProcess(key).ShouldRun())
                {
                    EMailProcessLog log = ServiceLocator.Instance().GetDaoEMailProcessLog().InitLog(key);

                    var proceso = EMailProcessStrategyContainer.Instance().GetProcess(key);

                    proceso.Run();

                    log.IdLote = proceso.IdLote;

                    ServiceLocator.Instance().GetDaoEMailProcessLog().FinishLog(log);
                }
            }
        }

        public void ProcessEMails(int id)
        {
            ICollection<string> keys = EMailProcessStrategyContainer.Instance().GetProcessKeys();

            foreach (string key in keys)
            {
                EMailProcessStrategyContainer.Instance().GetProcess(key).Run(id);
            }
        }

        public void ProcessNotSentEMails(int countryCode, DateTime fromDate, DateTime toDate, string module)
        {
            NotSentEMailMultipleExecution execution = new NotSentEMailMultipleExecution();

            execution.CountryCode = countryCode;
            execution.FromDate = fromDate;
            execution.ToDate = toDate;
            execution.ModuleCode = module;

            Thread oThread = new Thread(new ThreadStart(execution.ExecuteSendMails));
            oThread.Start();
        }

        public void SendMailXAMCases(AbstractEMailDTO dto)
        {
            EMailSenderStrategyLocator.Instance().GetEmailXAMCases().SendEMail(dto);
        }

        public void SendMailPoliza(AbstractEMailDTO dto)
        {
            EMailSenderStrategyLocator.Instance().GetEMailSenderPoliza().SendEMail(dto);
        }

        public void SendMailEndoso(AbstractEMailDTO dto)
        {
            EMailSenderStrategyLocator.Instance().GetEMailSenderEndoso().SendEMail(dto);
        }

        public void SendMailACCOMNotIssue(AbstractEMailDTO dto)
        {
            EMailSenderStrategyLocator.Instance().GetEMailSenderACCOMNotIssue().SendEMail(dto);
        }

        public void SendMailQuote(AbstractEMailDTO dto)
        {
            EMailSenderStrategyLocator.Instance().GetEMailQuote().SendEMail(dto);
        }

        public void SendMailBotonPago(AbstractEMailDTO dto)
        {
            EMailSenderStrategyLocator.Instance().GetEMailSenderBotonPago().SendEMail(dto);
        }
        
        public void SendMailContinuaCompra(AbstractEMailDTO dto)
        {
            EMailSenderStrategyLocator.Instance().GetEMailContinuaCompra().SendEMail(dto);
        }

        public void SendMailBalanceRequest(AbstractEMailDTO dto)
        {
            EMailSenderStrategyLocator.Instance().GetEMailBalanceRequest().SendEMail(dto);
        }

        public void SendMailExternalPaymentFinish(AbstractEMailDTO dto)
        {
            EMailSenderStrategyLocator.Instance().GetEMailSenderExternalPaymentFinish().SendEMail(dto);
        }
        
        public void SendMailDynamic(AbstractEMailDTO dto)
        {
           EMailSenderStrategyLocator.Instance().GetEMailDynamicSender().SendEMail(dto);
        }

        public AbstractEMailDTO GetTemplateDynamic(AbstractEMailDTO dto)
        {
            return EMailSenderStrategyLocator.Instance().GetEMailDynamicSender().GetDynamicTemplate(dto);
        }

        public byte[] GetPDFMail(AbstractEMailDTO dto, bool IsTag)
        {
            return EMailSenderStrategyLocator.Instance().GetEMailDynamicSender().getDynamicPDF(dto, IsTag);
        }

        public AbstractEMailDTO TemplateDynamic(AbstractEMailDTO dto)
        {
            return EMailSenderStrategyLocator.Instance().GetEMailDynamicSender().GetDynamicTemplate(dto);
        }
    }
}
