using System.Collections.Generic;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOUpgradeVariableText : IDAOObjetoNegocio
    {
        IList<UpgradeVariableText> FindAll();

        IList<UpgradeVariableText> FindByFilters(int idType, int idProduct, string name);

        IList<UpgradeVariableText> FindByFilters(int idType, int idProduct);

        IList<UpgradeVariableText> FindByName(string name);

        UpgradeVariableText Get(int id);
    }
}
