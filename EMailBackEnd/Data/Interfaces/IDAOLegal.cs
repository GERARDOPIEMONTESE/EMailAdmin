using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOLegal 
    {
        IList<Legal> Find(int countryCode, string voucherCode, string email, string templateName);
    }
}
