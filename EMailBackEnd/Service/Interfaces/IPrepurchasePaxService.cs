using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.DTO;

namespace EMailAdmin.BackEnd.Service.Interfaces
{
    public interface IPrepurchasePaxService
    {
        void CompleteInformation(AbstractEMailDTO dto);
    }
}
