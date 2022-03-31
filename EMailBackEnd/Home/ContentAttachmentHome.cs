using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Home
{
    public class ContentAttachmentHome
    {
        #region Static methods used by front-end

        public static IList<ContentAttachment> Find(int IdTemplate, int IdAttachment, string type)
        {
            return DAOLocator.Instance().GetDaoContentAttachment().Find(IdTemplate, IdAttachment, type);
        }

        #endregion
    }
}
