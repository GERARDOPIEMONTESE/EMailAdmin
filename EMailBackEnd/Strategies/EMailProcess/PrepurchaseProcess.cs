using System.Collections.Generic;
using EMailAdmin.ExternalServices.Domain;
using EMailAdmin.BackEnd.Home.External;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Service;
using EMailAdmin.BackEnd.Domain.External;
using System.Linq;
using System;
using System.IO;
using CapaNegocioDatos.CapaNegocio;
using System.Configuration;


namespace EMailAdmin.BackEnd.Strategies.EMailProcess
{
    public class PrepurchaseProcess : AbstractEMailProcess
    {
        private string _EMAILDEFAULTPREPURCHASE = "";
        private string EMAILDEFAULTPREPURCHASE 
        {
            get
            {
                if (_EMAILDEFAULTPREPURCHASE == "")
                    _EMAILDEFAULTPREPURCHASE = ConfigurationManager.AppSettings["PrepurchaseEmailToDefault"].ToString();
                return _EMAILDEFAULTPREPURCHASE;
            }
        }

        public override string GetTypeCode()
        {
            return "PreBalance";
        }
        private List<PrepurchaseCountryInformation> GetAccountsMinimumBalance()
        {
            //busca todas la cuentas que esten bajo el umbral definido de precompra
            //agrupadas por pais
            return PrepurchaseInformationHome.GetPrepurchaseCountry();
        }

        protected override void SendMails()
        {
            IList<PrepurchaseCountryInformation> prepurCountrys = GetAccountsMinimumBalance();

            foreach (var pci in prepurCountrys)
            {
                string accountsInfo = GetAccountsInfo(pci);
                string ToEMails = GetEMails(pci);

                var dto = new EMailPrepurchaseDTO();
                dto.IdLanguage = 1;
                dto.CountryCode = int.Parse( pci.Country.Codigo);
                dto.ModuleCode = "ACNET";
                dto.To = ToEMails;
                dto.PrepurchaseAccounts = accountsInfo;
                dto.CountryInfo = pci.CountryDesc();
                ServiceLocator.Instance().GetSendMailService().SendMailPrepurchase(dto);
            }
        }        

        private string GetEMails(PrepurchaseCountryInformation ppci)
        {
            string emails = "";
            foreach (var emailUsu in ppci.EMailListUsuario)
            {
                if (emails != "") emails += ",";
                emails += emailUsu.CorreoElectronico;
            }            
            if (emails == "") emails = EMAILDEFAULTPREPURCHASE;
            return emails;
        }

        private string GetAccountsInfo(PrepurchaseCountryInformation ppci)
        {
            string Accountsdata = "";
            foreach (var pciaccount in ppci.prepurchaseAccount)
            {
                Accountsdata += EMailPrepurchaseDTO.GetInfoMail(pciaccount);
            }
            return Accountsdata;
        }        

        protected override void SendMails(int id)
        {
        }    
    }
}
