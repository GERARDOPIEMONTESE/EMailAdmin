using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMailAdminAssemblyV2
{
    public class StoredProcedures
    {
        [Microsoft.SqlServer.Server.SqlProcedure]
        public static void SPEMailProcess()
        {
            var service = new EMailSenderService.EMailSenderService { Timeout = -1 };
            service.InitEMailProcess("mailservice@assist-card.com", "123456");
        }
    };
}