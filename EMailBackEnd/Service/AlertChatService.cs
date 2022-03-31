using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Service.Interfaces;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.BackEnd.Service
{
    public class AlertChatService : IAlertChatService
    {
        public void CompleteInformation(DTO.AbstractEMailDTO dto)
        {
            dto.TemplateType = TemplateTypeHome.GetAlertChats();
        }
    }
}
