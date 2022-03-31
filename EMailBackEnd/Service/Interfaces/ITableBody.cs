using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Service.Interfaces
{
    public interface ITableBody
    {
        string ParseBody(string bodyName);
        string[] ParseBodyArray(string bodyName);
        string ParseHeader(string bodyName);
    }
}
