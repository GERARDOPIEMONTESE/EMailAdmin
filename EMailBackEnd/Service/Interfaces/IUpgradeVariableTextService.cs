using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Service.Interfaces
{
    public interface IUpgradeVariableTextService
    {
        void Save(UpgradeVariableText upgradeVariableText);
        void Delete(UpgradeVariableText upgradeVariableText);
    }
}
