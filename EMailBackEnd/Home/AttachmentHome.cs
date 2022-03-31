using System.Collections.Generic;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Domain;
using System.Linq;

namespace EMailAdmin.BackEnd.Home
{
    public class AttachmentHome
    {
        #region Static methods used by front-end

        public static IList<Attachment> FindAll()
        {
            return DAOLocator.Instance().GetDaoAttachment().FindAll();
        }

        public static IList<Attachment> FindByFilters(int idType, int idEstrategy, string name)
        {
            return DAOLocator.Instance().GetDaoAttachment().FindByFilters(idType, idEstrategy, name);
        }

        public static IList<Attachment> FindByFilters(int idType, int idEstrategy, string name, bool lazy)
        {
            return DAOLocator.Instance().GetDaoAttachment().FindByFilters(idType, idEstrategy, name);
        }

        public static IList<Attachment> FindByName(string name)
        {
            return DAOLocator.Instance().GetDaoAttachment().FindByName(name);
        }

        public static AttachmentItem FindAttachItemByNameAndLang(string name, int IdLanguage)
        {
            return DAOLocator.Instance().GetDaoAttachment().FindAttachItemByNameAndLang(name, IdLanguage);
        }

        public static IList<Attachment> FindByType(int type, bool lazy)
        {
            return DAOLocator.Instance().GetDaoAttachment().FindByType(type, lazy);
        }

        public static IList<Attachment> FindByNameType(string name, int type)
        {
            return DAOLocator.Instance().GetDaoAttachment().FindByNameType(name, type);
        }

        public static Attachment Get(int id)
        {
            return DAOLocator.Instance().GetDaoAttachment().Get(id);
        }

        #endregion

        internal static IList<ContentAttachment> FindAttachmentContentPDF(int IdTemplate, int IdAttachment, int IdLanguage)
        {
            return DAOLocator.Instance().GetDaoContentAttachment().Find(IdTemplate, IdAttachment, IdLanguage);
        }
    }
}
