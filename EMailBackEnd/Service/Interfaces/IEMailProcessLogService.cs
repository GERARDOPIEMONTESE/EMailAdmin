using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Service.Interfaces
{
    public interface IEMailProcessLogService
    {
        void Save(Domain.EMailProcessLog log);

        EMailProcessLog InitLog(string key);

        void FinishLog(EMailProcessLog log);
    }
}
