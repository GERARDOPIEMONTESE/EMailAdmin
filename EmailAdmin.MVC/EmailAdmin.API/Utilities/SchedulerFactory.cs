using DTOMapper.Helpers;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Service;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace EmailAdmin.Api.Utilities
{
    #region SPEMailProcess
    public class SPEMailProcess : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            if (SchedulerFactory.IsServerJob)
            {
                ServiceLocator.Instance().GetSendMailService().ProcessEMails();
            }
            return Task.CompletedTask;
        }
    }
    #endregion

    public class SchedulerFactory
    {
        private static string _IPServerJob { get; set; }

        public static string myIp { get; set; }

        public static int? IdLoteMyIp { get; set; }

        private static void LoadMyIP()
        {

            string HostName = Dns.GetHostName();
            IPAddress[] ipaddress = Dns.GetHostAddresses(HostName);
            IPAddress _ipaddress = null;
            _ipaddress = ipaddress.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault();
            if (_ipaddress == null)
            {
                _ipaddress = ipaddress.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6).FirstOrDefault();
            }

            if (_ipaddress != null)
            {
                myIp = _ipaddress.ToString();
            }

            int ipLastPart = 0;
            int.TryParse(myIp.Split('.').LastOrDefault(), out ipLastPart);
            IdLoteMyIp = ipLastPart;
        }

        public static bool IsServerJob
        {
            get
            {
                if (!string.IsNullOrEmpty(myIp))
                {
                    return (myIp == IPServerJob);
                }
                return
                    false;
            }
        }

        public static string IPServerJob
        {
            get
            {
                string ipServerJob = ConfigurationValueHome.Obtener("IPServerJob");
                if (_IPServerJob != ipServerJob)
                {
                    _IPServerJob = ipServerJob;
                    if (myIp == _IPServerJob)
                    {
                        //indica que es el server que atiende ahora
                        EMailProcessLog log = ServiceLocator.Instance().GetDaoEMailProcessLog().InitLog("AlertJob");
                        log.IdLote = IdLoteMyIp;
                        ServiceLocator.Instance().GetDaoEMailProcessLog().FinishLog(log);
                    }
                }
                return _IPServerJob;
            }
        }

        private static JobStatus<SPEMailProcess> jobSPEMailProcess;

        public static void JobsInit()
        {
            LoadMyIP();

            try
            {
                string SchedulerFactoryActive = ConfigurationValueHome.Obtener("SchedulerFactoryActive");
                bool bSchedulerFactoryActive = false;
                if (Boolean.TryParse(SchedulerFactoryActive, out bSchedulerFactoryActive))
                {
                    jobSPEMailProcess = new JobStatus<SPEMailProcess>("SPEMailProcess");
                    jobSPEMailProcess.Start(DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                ApplicationEventLogHelper.LogErrorCatched(ex, null, "SchedulerFactory", "ERROR JobsInit");
            }
        }

        public static void ResetAllJobs()
        {
            jobSPEMailProcess.Shutdown();

            JobsInit();
        }

        public static bool StartJob(string name)
        {
            bool bOK = true;
            if (name == "SPEMailProcess")
            { 
                if (jobSPEMailProcess!=null) 
                    jobSPEMailProcess = new JobStatus<SPEMailProcess>("SPEMailProcess");

                jobSPEMailProcess.Start(DateTime.Now);
            }
            else
                bOK = false;

            return bOK;
        }

        public static bool ShutdownJob(string name)
        {
            bool bOK = true;
            if (name == "SPEMailProcess" && jobSPEMailProcess!=null)
                jobSPEMailProcess.Shutdown();
            else
                bOK = false;

            return bOK;
        }
    }

    public class JobStatus<T> where T : IJob
    {
        private string jobName;
        private string keyTriggerTime = "";
        public string triggerName = "";
        public string groupName = "";

        public JobStatus(string prefix)
        {
            keyTriggerTime = "TriggerTime" + prefix;
            triggerName = "Trigger" + prefix;
            groupName = "Group" + prefix;
            jobName = "Job" + prefix;
        }
        public async void Start(DateTime date)
        {
            int TriggerSchedulerMin = -1;
            string sConfigTriggerSchedulerMin = ConfigurationValueHome.Obtener(keyTriggerTime);
            int.TryParse(sConfigTriggerSchedulerMin, out TriggerSchedulerMin);

            if (TriggerSchedulerMin > 0)
            {
                IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
                await scheduler.Start();

                IJobDetail job = JobBuilder.Create<T>()
                  .WithIdentity("job", jobName)
                  .Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity(triggerName, groupName)
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInMinutes(TriggerSchedulerMin)
                        .RepeatForever())
                    .Build();

                await scheduler.ScheduleJob(job, trigger);
            }
        }

        public async void Shutdown()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Shutdown();
        }
    }
}