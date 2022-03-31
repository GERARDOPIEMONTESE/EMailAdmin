using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOUpgradeVariableTextType : IDAOObjetoNegocio
    {
        UpgradeVariableTextType Get(int id);

        UpgradeVariableTextType GetByCode(string code);

        IList<UpgradeVariableTextType> FindAll();

        IList<UpgradeVariableTextType> FindByFilters(string description);
    }
}
