using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.ExternalServices.Domain;
using EMailAdmin.ExternalServices.Data;
using EMailAdmin.ExternalServices.Data.Interfaces;
using EMailAdmin.BackEnd.Domain.External;
using CapaNegocioDatos.CapaHome;
using CapaNegocioDatos.CapaNegocio;
using EMailAdmin.BackEnd.DTO;

namespace EMailAdmin.BackEnd.Home.External
{
    public class PrepurchaseInformationHome
    {

        public static List<PrepurchaseCountryInformation> GetPrepurchaseCountry()
        {
            IList<PrepurchaseInformation> prepursInfo = FindMinimunBalance();
            int[] paises = GetCountrys(prepursInfo);

            List<PrepurchaseCountryInformation> lstppiCountry = new List<PrepurchaseCountryInformation>();
            foreach (var pais in paises)
            {
                lstppiCountry.Add(getAccountsByCountry(prepursInfo, pais));
            }
            return lstppiCountry;
        }

        private static int[] GetCountrys(IList<PrepurchaseInformation> ppi)
        {
            var paises = (from p in ppi select p.CountryCode).Distinct();
            return paises.ToArray<int>();
        }

        private static Idioma[] GetIdioma(PrepurchaseCountryInformation ppci)
        {
            var idiomasByUsus = (from p in ppci.EMailListUsuario select p.Idioma).Distinct();
            return idiomasByUsus.ToArray<Idioma>();
        }
        private static PrepurchaseCountryInformation getAccountsByCountry(IList<PrepurchaseInformation> ppi, int idPais)
        {            
            var prepurAccounts = from p in ppi
                                 where p.CountryCode == idPais 
                                 select p;

            PrepurchaseCountryInformation prepurC = new PrepurchaseCountryInformation();
            prepurC.Country = PaisHome.ObtenerPorCodigo(idPais.ToString());
            prepurC.prepurchaseAccount = new List<PrepurchaseInformation>();
            prepurC.prepurchaseAccount.AddRange(prepurAccounts);
            prepurC.EMailListUsuario = EMailListHome.FindForPrepurchace(idPais);

            return prepurC;
        }


        public static IList<PrepurchaseInformation> FindMinimunBalance()
        {
            //int saldoMinDias = CodigoActivadorHome.ObtenerAlarmaPrepurchaseMinDias();
            //int saldoMinTarjetas = CodigoActivadorHome.ObtenerAlarmaPrepurchaseMinTarjetas();
            //return ExternalDAOLocator.Instance().GetDaoPrepurchase().FindMinimunBalance(saldoMinDias, saldoMinTarjetas);            
            return ExternalDAOLocator.Instance().GetDaoPrepurchase().FindMinimunBalance();
        }     

    }
}
