using EMailAdmin.BackEnd.Exceptions;
using EMailAdmin.BackEnd.Service.Interfaces;
using EMailAdmin.BackEnd.Data.Interfaces;

namespace EMailAdmin.BackEnd.Service
{
    public class UpgradeVariableTextService : IUpgradeVariableTextService
    {
        public IDAOUpgradeVariableText DaoUpgradeVariableText { get; set; }

        #region IUpgradeVariableTextService Members

        public void Save(Domain.UpgradeVariableText upgradeVariableText)
        {
            if (upgradeVariableText.Name != "" && upgradeVariableText.Upgrades.Count > 0 && upgradeVariableText.Content.Count > 0 && upgradeVariableText.UpgradeVariableTextTypes.Count > 0)
            {
                DaoUpgradeVariableText.Persistir(upgradeVariableText);
            }
            else
            {
                throw new NonSavedObjectException("UpgradeVariableText not saved");
            }
        }

        public void Delete(Domain.UpgradeVariableText upgradeVariableText)
        {
            if (upgradeVariableText.Id != 0)
            {
                DaoUpgradeVariableText.Eliminar(upgradeVariableText);
            }
            else
            {
                throw new NonEliminatedObjectException("UpgradeVariableText not deleted");
            }
        }

        #endregion
    }
}
