using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;

namespace EMailAdmin.WindowsService
{
    partial class EMailAdminService : ServiceBase
    {
        private Timer timer = new Timer();

        public EMailAdminService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);

            //ad 2: set interval to 1 minute (= 60,000 milliseconds)
            timer.Interval = 60000;

            //ad 3: enabling the timer
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
            timer.Enabled = false;
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {

        }
    }
}
