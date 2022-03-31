using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Home
{
    public class TableVariableTextHome
    {
        public static TableVariableText Get(string name)
        {
            return DAOLocator.Instance().GetDaoTableVariableText().Get(name);
        }

        public static TableVariableText Get(int id)
        {
            return DAOLocator.Instance().GetDaoTableVariableText().Get(id);
        }

        public static IList<TableVariableText> FindAll()
        {
            return DAOLocator.Instance().GetDaoTableVariableText().FindAll();
        }
    }
}
