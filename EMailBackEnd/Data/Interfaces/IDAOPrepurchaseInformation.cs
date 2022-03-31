using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.ExternalServices.Domain;
using EMailAdmin.BackEnd.Domain.External;

namespace EMailAdmin.ExternalServices.Data.Interfaces
{
    public interface IDAOPrepurchase
    {
        IList<PrepurchaseInformation> FindMinimunBalance(int saldoMinDias, int saldoMinTarjetas);
        IList<PrepurchaseInformation> FindMinimunBalance();
    }
}
