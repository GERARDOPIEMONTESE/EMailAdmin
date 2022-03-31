using System.Collections.Generic;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;
using System.Transactions;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOUpgradeVariableTextContent : IDAOObjetoNegocio
    {
        IList<UpgradeVariableTextContent> GetByIdUpgradeVariableText(int idUpgradeVariableText);

        UpgradeVariableTextContent Get(int id);

        void DeleteByIdUpgradeVariableText(int idUpgradeVariableText);

        void DeleteByIdUpgradeVariableText(int idUpgradeVariableText, TransactionScope ts);
    }
}
