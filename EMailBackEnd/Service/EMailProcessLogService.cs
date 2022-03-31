using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Service.Interfaces;
using EMailAdmin.BackEnd.Data.Interfaces;
using FrameworkDAC.Negocio;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Service
{
    public class EMailProcessLogService : IEMailProcessLogService
    {
        public IDAOEMailProcessLog DaoEMailProcessLog { get; set; }

        #region IEMailProcessLogService Members

        public void Save(Domain.EMailProcessLog log)
        {
            log.IdEstado = ObjetoNegocio.Creado();
            DaoEMailProcessLog.Persistir(log);
        }
     
        public Domain.EMailProcessLog InitLog(string key)
        {
            EMailProcessLog log = new EMailProcessLog();
            EMailProcessType type = EMailProcessTypeHome.Get(key);
            if (type.Id > 0)
            {
                log.IdEMailProcessType = type.Id;
                log.StartDate = DateTime.Now;
                log.EndDate = DateTime.MinValue.AddYears(1900);

                DaoEMailProcessLog.Persistir(log);
            }
            return log;
        }

        public void FinishLog(Domain.EMailProcessLog log)
        {
            if (log.Id > 0)
            {
                log.EndDate = DateTime.Now;
                DaoEMailProcessLog.Persistir(log);
            }
        }

        #endregion
    }
}
