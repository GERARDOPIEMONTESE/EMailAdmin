using CapaNegocioDatos.CapaDatos;
using EMailAdmin.BackEnd.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailAdmin.Dto;
using EMailAdmin.BackEnd.Utils;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.BackEnd.Utils
{
    public class EmailRecipientsHelper
    {
        //Trae la lista de destinatarios en string, para un pais y tipo de lista en el EmailAdmin
        public static IList<string> GetEmailListTypeRecipients(int countryCode, string emailListTypeCode)
        {
            IList<EMailListUsuarioDTO> listUsers = EMailListHome.FindUsersMailList(countryCode, emailListTypeCode);
            return listUsers.Select(item => item.CorreoElectronico).ToList();
        }
    }
}
