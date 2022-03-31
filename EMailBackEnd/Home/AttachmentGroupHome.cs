using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMailAdmin.BackEnd.Home
{
    public class AttachmentGroupHome
    {
        public static IList<GroupAttachment> Buscar(GroupAttachment datasearch= null)
        {
            return DAOLocator.Instance().GetDaoGroupAttachment().Find(datasearch);
        }

        public static GroupAttachment Get(int Id)
        {
            return DAOLocator.Instance().GetDaoGroupAttachment().Obtener(Id);
        }
    }
}
