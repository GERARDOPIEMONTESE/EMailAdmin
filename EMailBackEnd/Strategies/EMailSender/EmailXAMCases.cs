using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Service;
using System.Configuration;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.BackEnd.Strategies.EMailSender
{
    public class EmailXAMCases : EMailSenderDefault
    {
        protected override void CompleteDto(DTO.AbstractEMailDTO dto)
        {
            dto.TemplateType = TemplateTypeHome.Get("XAM");
        }
    }
}
