using System;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;

namespace EMailAdmin.BackEnd.Strategies.EMailProcess
{
    public abstract class AbstractEMailProcess
    {
        public Nullable<int> IdLote { get; set; }

        public abstract string GetTypeCode();

        protected abstract void SendMails();

        protected abstract void SendMails(int id);

        public virtual bool ShouldRun()
        {
            EMailProcessType type = EMailProcessTypeHome.Get(GetTypeCode());
            EMailProcessLog log = EMailProcessLogHome.GetLastLog(type.Id);
            bool bOk = false;
            if (!string.IsNullOrEmpty(type.PeriodHours))
            {
                if (log.Id == 0 || (log.Id > 0 && DateTime.Now.Subtract(log.StartDate).TotalMinutes > 10))
                {
                    string[] horas = (type.PeriodHours.Contains(",") ? type.PeriodHours.Split(',') : new string[1] { type.PeriodHours });

                    foreach (var item in horas)
                    {
                        TimeSpan tsItem;
                        if (TimeSpan.TryParse(item, out tsItem))
                        {
                            var diff = DateTime.Now.TimeOfDay.Subtract(tsItem).TotalMinutes;
                            bOk = (diff > 0 && diff <= 10);
                            if (bOk) break;
                        }
                    }
                }
            }
            else
            {
                bOk = type.Period > 0 && (log.Id == 0 ||
                    (log.StartDate != null && DateTime.Now.Subtract(log.StartDate).TotalMinutes >= type.Period));
            }

            if (bOk && type.CheckLote)
            {
                bOk = EMailProcessStrategyContainer.Instance().GetProcess(type.Codigo).CheckLote(log.IdLote);
            }

            return bOk;
        }

        public void Run()
        {
            SendMails();
        }

        public void Run(int id)
        {
            SendMails(id);
        }

        public virtual Nullable<int> GetIdLote(){
            return null;
        }

        private bool CheckLote(Nullable<int> LastIdLote)
        {
            IdLote = GetIdLote();
            return ((IdLote==null && LastIdLote==null) || (IdLote != LastIdLote));
        }
    }
}
 