using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DynamicWsSetup;
using EMailAdmin.BackEnd.AssistCardService;
using System.Net;

namespace EMailAdmin.BackEnd.Service
{
    public class ServicioAssistCard
    {
        public const string wsCode = "AssistCardService";
        public const string wsEnviron = "DEFAULT";

        private AssistCardServiceService ws;

        public WsSetup config { get; set; }

        public ServicioAssistCard()
        {
            ws = new AssistCardServiceService();
            ws.Timeout = -1;
            ConfigureEnvironment();
        }

        private void ConfigureEnvironment()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            config = DynamicWsSetup.WsSetupHome.GetWebserviceURL(wsCode, wsEnviron);
            if (config != null && config.Id > 0)
            {
                ws.Url = config.Url;                
            }
        }
        
        public string findVoucherAllWithTarjeta(int countryCode, int voucher)
        {
            return ws.findVoucherAllWithTarjeta(countryCode, voucher, config.User, config.Password);
        }

        public string getVoucherInfo(int countryCode, int voucher, string modulo = "ACNET")
        {
            return ws.getVoucherInfo(countryCode, voucher, modulo, config.User, config.Password);
        }
    }

    public class ServicioCondiciones
    {
        public const string wsCode = "ServicioClausulasWS";
        public const string wsEnviron = "DEFAULT";

        private EMailAdmin.BackEnd.Serviciocondiciones.ServicioClausulasWS ws;

        public WsSetup config { get; set; }

        public ServicioCondiciones()
        {
            ws = new Serviciocondiciones.ServicioClausulasWS();
            ConfigureEnvironment();
        }

        private void ConfigureEnvironment()
        {
            config = DynamicWsSetup.WsSetupHome.GetWebserviceURL(wsCode, wsEnviron);
            if (config != null && config.Id > 0)
            {
                ws.Url = config.Url;                
            }
        }

        public string ObtenerCondicionesXml(string filtro)
        {
            return ws.ObtenerCondicionesXml(filtro, config.User, config.Password);
        }

    }

    public class ServicioXAM
    {
        public const string wsCode = "XAMServicio";
        public const string wsEnviron = "DEFAULT";

        private wsXAM.MailService ws;

        public WsSetup config { get; set; }

        public ServicioXAM()
        {
            ws = new wsXAM.MailService();
            ConfigureEnvironment();
        }

        private void ConfigureEnvironment()
        {
            config = DynamicWsSetup.WsSetupHome.GetWebserviceURL(wsCode, wsEnviron);
            if (config != null && config.Id > 0)
            {
                ws.Url = config.Url;
            }
        }
        
        internal wsXAM.ServiceCase[] GetCasesForSendingMail()
        {
            return ws.GetCasesForSendingMail();
        }
    }

    public class AssistCardDaysAcquisitionService
    {
         public const string wsCode = "XAMServicio";
        public const string wsEnviron = "DEFAULT";

        private EMailAdmin.BackEnd.ServiceBoxPax.AssistCardDaysAcquisitionServiceService ws;

        public WsSetup config { get; set; }

        public AssistCardDaysAcquisitionService()
        {
            ws = new EMailAdmin.BackEnd.ServiceBoxPax.AssistCardDaysAcquisitionServiceService();
            ConfigureEnvironment();
        }

        private void ConfigureEnvironment()
        {
            config = DynamicWsSetup.WsSetupHome.GetWebserviceURL(wsCode, wsEnviron);
            if (config != null && config.Id > 0)
            {
                ws.Url = config.Url;
            }
        }

        internal string getDaysAcquisition(int codeBoxPax)
        {
            return ws.getDaysAcquisition(codeBoxPax, config.User, config.Password);
        }

        internal string sendEmisionPreCompraMail(int codeBoxPax, string group, int countryCode)
        {
            return ws.sendEmisionPreCompraMail(codeBoxPax, group, countryCode, config.User, config.Password);
        }
    }
}
