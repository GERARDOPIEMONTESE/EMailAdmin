using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMailAdmin.BackEnd.Strategies.EMailProcess
{
    public class DummyProcess : AbstractEMailProcess
    {
        public override string GetTypeCode()
        {
            return "";
        }

        protected override void SendMails()
        {
        }

        protected override void SendMails(int id)
        {
        }
    }
}
