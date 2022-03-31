public class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void SPEMailProcess()
    {
        var service = new EMailAdminAssembly.EMailSenderService.EMailSenderService { Timeout = 1000000 };
        service.InitEMailProcess("mailservice@assist-card.com", "123456");
    }
};
