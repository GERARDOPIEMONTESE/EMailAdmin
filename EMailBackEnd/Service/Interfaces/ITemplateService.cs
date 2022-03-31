using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.DTO;
using System.Collections.Generic;

namespace EMailAdmin.BackEnd.Service.Interfaces
{
    public interface ITemplateService
    {
        void Save(Template template);

        void SaveAssociations(IList<Group_R_Template> items, int idUser);

        void SaveAssociations(Group_R_Template item, int idUser);

        void Delete(Template template);

        string ParseBody(AbstractEMailDTO dto, Template template);

        //string ParseBody(int idLanguage, int idLocation, Template template);

        string ParseBody(int idLanguage, int idLocation, Template template, bool isPreview);

        IList<string> GetVariableTags(string body);

        Template Get(TemplateType type, AssociationGroupDTO associationGroupDto);

        Template Copy(Template template);

        Template Copy(int idTemplate);
    }
}
