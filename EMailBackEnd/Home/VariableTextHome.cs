using System.Collections.Generic;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Home
{
    public class VariableTextHome
    {
        public static VariableText Get(int id)
        {
            return DAOLocator.Instance().GetDaoVariableText().Get(id);
        }

        public static VariableText Get(string name)
        {
            return DAOLocator.Instance().GetDaoVariableText().Get(name);
        }

        public static IList<VariableText> FindAll()
        {
            return DAOLocator.Instance().GetDaoVariableText().FindAll();
        }

        public static IList<VariableText> FindByType(int idType)
        {
            return DAOLocator.Instance().GetDaoVariableText().FindByType(idType);
        }

        public static IList<VariableText> FindByType(string code)
        {
            return DAOLocator.Instance().GetDaoVariableText().FindByType(code);
        } 
    }
}
