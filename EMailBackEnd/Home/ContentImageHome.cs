using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Home
{
    public class ContentImageHome
    {
        public static ContentImage Get(int idContent, int idLanguage, string name)
        {
            return DAOLocator.Instance().GetDaoContentImage().Get(idContent, idLanguage, name);
        }

        public static ContentImage GetOrDefault(int idContent, int idLanguage, string name)
        {
            ContentImage contentImage = DAOLocator.Instance().GetDaoContentImage().Get(idContent, idLanguage, name);
            return contentImage.Id != 0 ? contentImage : 
                DAOLocator.Instance().GetDaoContentImage().Get(idContent, 0, name);
        }

        public static ContentImage Get(int idContentImage)
        {
            return DAOLocator.Instance().GetDaoContentImage().Get(idContentImage);
        }
    }
}
