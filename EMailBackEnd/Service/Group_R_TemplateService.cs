using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Service.Interfaces;

namespace EMailAdmin.BackEnd.Service
{
    public class Group_R_TemplateService : IGroup_R_TemplateService
    {
        public IDAOGroup_R_Template DaoGroup_R_Template { get; set; }

        #region IGroup_R_TemplateService Members

        public void Save(Group_R_Template groupRTemplate)
        {
            Group group = groupRTemplate.Group;
            group.IdUsuario = groupRTemplate.IdUsuario;
            ServiceLocator.Instance().GetGroupService().Save(group);
            groupRTemplate.IdTemplate = groupRTemplate.Template.Id;
            groupRTemplate.IdGroup = groupRTemplate.Group.Id;
            ServiceLocator.Instance().GetTemplateService().SaveAssociations(groupRTemplate, groupRTemplate.IdUsuario);
        }

        public void Delete(Group_R_Template groupRTemplate)
        {
            
        }

        public void Delete(int idGroupRTemplate)
        {
            
        }

        #endregion
    }
}