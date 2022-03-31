using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOAttachment : IDAOObjetoNegocio
    {
        IList<Attachment> FindAll();

        IList<Attachment> FindByFilters(int idType, int idEstrategy, string name);

        IList<Attachment> FindByFilters(int idType, int idEstrategy, string name, bool lazy);

        IList<Attachment> FindByName(string name);

        IList<Attachment> FindByType(int type);

        IList<Attachment> FindByType(int type, bool lazy);

        IList<Attachment> FindByNameType(string name, int type);

        IList<Attachment> FindByTemplateId(int idTemplate);

        Attachment Get(int id);

        IList<Attachment> FindWithAssociations(int idType, int idEstrategy, string name);

        IList<Attachment> Find(int idTemplate, int idModule, int idGroupType,
            int idLocation, int idProduct, int idRate, int IdAccount);

        IList<Attachment> FindByTypeWithoutAssociations(int idAttachmentType);

        AttachmentItem FindAttachItemByNameAndLang(string name, int IdLanguage);
    }
}
