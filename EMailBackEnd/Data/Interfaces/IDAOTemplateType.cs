using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOTemplateType
    {
        IList<TemplateType> FindAll();

        TemplateType GetByDescription(string description);

        TemplateType Get(string code);

        TemplateType Get(int id);

        IList<TemplateType> Find(string Prefijo);
    }
}
