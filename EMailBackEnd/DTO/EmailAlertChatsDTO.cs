using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;
using EMailAdmin.BackEnd.Service.Interfaces;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.DTO
{
    public class EmailAlertChatsDTO : AbstractEMailDTO, ITableBody
    {
        private const string NAME = "EmailAlertChats";

        public string ItemsAlerta { get; set; }
        public int Total { get; set; }

        public static string GetInfoMail(EmailAlertChat item)
        {
            string info = "<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td></tr>";
           
            info = string.Format(info, item.Negocio , item.OperatorType ,item.LatitudeLocation ,item.LongitudeLocation ,item.IsFallBack ,item.Cantidad);
            return info;            
        }

        public string ParseBody(string bodyName)
        {
            return this.ItemsAlerta;
        }

        public string[] ParseBodyArray(string bodyName)
        {
            throw new NotImplementedException();
        }

        public string ParseHeader(string bodyName)
        {
            throw new NotImplementedException();
        }
    }
}
