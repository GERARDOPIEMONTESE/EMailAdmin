using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Properties;

namespace EMailAdmin.BackEnd.Strategies.EMailSender
{
    public class EMailSenderWelcomeBack : EMailSenderDefault
    {
        protected override void CompleteDto(AbstractEMailDTO dto)
        {
            dto.TemplateType = TemplateTypeHome.GetWelcomeBack();
            dto.ApplicationUrl = ConfigurationValueHome.GetApplicationUrl();
        }
    }
}
