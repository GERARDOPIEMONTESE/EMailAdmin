using System;
using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOTemplate : IDAOObjetoNegocio
    {
        IList<Template> FindAll();

        IList<Template> Find(int idTemplateType);

        IList<Template> Find(int idTemplateType, bool lazy);

        IList<Template> FindByTypeWithoutAssociations(int idTemplateType);

        IList<Template> Find(string name);

        IList<Template> Find(int idTemplateType, string name, bool lazy);

        IList<Template> Find(int idTemplateType, string name, DateTime date);

        IList<Template> Find(int idTemplateType, int country, string accountCode, string productCode,
                             DateTime effectiveDate);

        IList<Template> FindWithAssociations(int idTemplateType, string name);

        Template Get(int id);

        Template Get(int id, bool lazy);

        Template Get(int idTemplateType, string name, DateTime date);

        Template Get(int idTemplateType, string name);

        IList<Template> FindAllList();

        Template GetHierarchy(int IdTemplate);
    }
}