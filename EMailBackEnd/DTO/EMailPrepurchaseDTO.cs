using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain.External;
using EMailAdmin.BackEnd.Service.Interfaces;

namespace EMailAdmin.BackEnd.DTO
{
    public class EMailPrepurchaseDTO : AbstractEMailDTO, ITableBody
    {
        public string CountryInfo { get; set; }
        public string PrepurchaseAccounts { get; set; }

        public static string GetInfoMail(PrepurchaseInformation ppi)
        {
            string col5 = (ppi.Type == PrepurchaseInformation.UnitType.DIAS ? ppi.Saldo.ToString() : "");
            string col6 = (ppi.Type == PrepurchaseInformation.UnitType.TARJETAS ? ppi.Saldo.ToString() : "");

            string info = "<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td></tr>";
            info = string.Format(info, ppi.AgencyCode, ppi.AgencySuc, ppi.AgencyName, ppi.Product, ppi.Tarif, col5, col6);

            return info;
        }

        public string ParseBody(string bodyName)
        {
            if (bodyName == "PrepurchaseAccounts")
                return this.PrepurchaseAccounts;
            else
                return "";
        }

        public string ParseHeader(string bodyName)
        {
            throw new NotImplementedException();
        }


        public string[] ParseBodyArray(string bodyName)
        {
            throw new NotImplementedException();
        }
    }
}
