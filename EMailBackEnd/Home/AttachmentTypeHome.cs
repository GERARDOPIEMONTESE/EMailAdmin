using System.Collections.Generic;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Home
{
    public class AttachmentTypeHome
    {
        #region Static methods used by front-end

        public static AttachmentType Fixed()
        {
            return GetByCode(AttachmentType.FIXED);
        }

        public static IList<AttachmentType> FindAll()
        {
            return DAOLocator.Instance().GetDaoAttachmentType().FindAll();
        }

        public static AttachmentType Get(int id)
        {
            return DAOLocator.Instance().GetDaoAttachmentType().Get(id);
        }

        public static AttachmentType GetByCode(string code)
        {
            return DAOLocator.Instance().GetDaoAttachmentType().GetByCode(code);
        }

        #endregion
    }
}
