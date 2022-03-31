using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Domain.External;

namespace EMailAdmin.BackEnd.Service.Interfaces
{
    public interface IPaxCumpleanosService
    {
        IList<PaxCumpleanos> GetAll();

        bool CheckSendEmail(PaxCumpleanos pax, int IdTemplateType);
    }
}
