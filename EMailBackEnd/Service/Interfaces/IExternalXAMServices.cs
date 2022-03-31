using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain.External;
using EMailAdmin.BackEnd.DTO;

namespace EMailAdmin.BackEnd.Service.Interfaces
{
    public interface IExternalXAMServices
    {
        EmailAlertXamCases[] GetAll();

        EmailAlertXamCases[] GetAllExtended(); //Nuevo servicio de XAM ACI1693.
    }
}
