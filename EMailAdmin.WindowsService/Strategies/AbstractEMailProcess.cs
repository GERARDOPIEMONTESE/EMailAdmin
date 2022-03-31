using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMailAdmin.WindowsService.Strategies
{
    public abstract class AbstractEMailProcess
    {
        protected abstract string GetTypeCode();

        protected abstract void SendMails();

        protected virtual bool ShouldRun()
        {
            return true;
        }

        public void Run()
        {
            if (ShouldRun())
            {
                SendMails();
            }
        }
    }
}
