using System.Collections.Generic;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Home
{
    public class AssociationHome
    {
        public static IList<Group_R_Template> GetTemplateAssociations(int idTemplate)
        {
            return DAOLocator.Instance().GetDaoGroup_R_Template().FindByTemplateId(idTemplate);
        }

        public static IList<Attachment_R_Group> GetAttachmentAssociations(int idAttachment)
        {
            return DAOLocator.Instance().GetDaoAttachment_R_Group().FindByAttachmentId(idAttachment);
        }
    }
}