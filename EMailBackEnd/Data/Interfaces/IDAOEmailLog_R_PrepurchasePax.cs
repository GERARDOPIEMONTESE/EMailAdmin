using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOEmailLog_R_PrepurchasePax : IDAOObjetoNegocio
    {
        IList<EmailLog_R_PrepurchasePax> Find(int CodigoPaxBox, string CodigoVerif, string Group, int CountryCode);
        EmailLog_R_PrepurchasePax Find(int CodigoPaxBox, string codigoVerif, int countryCode, string voucherGroup);
    }
}
