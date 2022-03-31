using DTOMapper;
using EMailAdmin.BackEnd.Strategies.Attachment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMailAdmin.ExternalServices.Service.Interface
{
    public interface IExternalInformationStrategyService
    {
        string GetInformation(string url, string jsonParams);
    }
}
