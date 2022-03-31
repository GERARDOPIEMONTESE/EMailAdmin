using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using System.Transactions;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOUpgradeVariableText_R_Upgrade : IDAOObjetoNegocio
    {
        IList<UpgradeVariableText_R_Upgrade> FindByUpgradeVariableTextId(int idUpgradeVariableText);
        
        void DeleteByIdUpgradeVariableText(int idUpgradeVariableText);
        
        void DeleteByIdUpgradeVariableText(int idUpgradeVariableText, TransactionScope ts);
    }
}
