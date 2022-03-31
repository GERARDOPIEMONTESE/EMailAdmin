using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.DTO;

namespace EMailAdmin.BackEnd.Service.Interfaces
{
    public interface IEMailContactService
    {
        void Save(EMailContactDTO dto);

        void Delete(EMailContactDTO dto);

        void Delete(int IdEMailContact);
    }
}
