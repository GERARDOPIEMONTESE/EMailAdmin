using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMailAdmin.BackEnd.Service.Interfaces
{
    public interface IInformationDynamicService: IInformationService
    {
        Estrategy GetStrategy(string strategyCode);
    }
}
