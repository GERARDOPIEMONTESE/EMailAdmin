using System.Collections.Generic;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.DTO;

namespace EMailAdmin.BackEnd.Home
{
    public class TemplateHome 
    {
        public static IList<Template> FindAll()
        {
            return DAOLocator.Instance().GetDaoTemplate().FindAll();
        }

        public static IList<Template> FindAllList()
        {
            var lst = DAOLocator.Instance().GetDaoTemplate().FindAllList();
            lst.Insert(0, new Template(){ Id=0, Name=""});
            return lst;
        }

        public static IList<Template> FindByName(string name)
        {
            return DAOLocator.Instance().GetDaoTemplate().Find(name);
        }

        public static IList<Template> Find(int idType, string name, bool lazy = true)
        {
            return DAOLocator.Instance().GetDaoTemplate().Find(idType, name, lazy);
        }

        public static IList<TemplateDTO> FindDTO(int idType, string name)
        {
            return DAOLocator.Instance().GetDaoTemplateDto().Find(idType, name);
        }        

        public static IList<Template> FindByType(int idType, bool lazy)
        {
            return DAOLocator.Instance().GetDaoTemplate().Find(idType, lazy);
        }

        public static Template Get(int idTemplate)
        {
            return DAOLocator.Instance().GetDaoTemplate().Get(idTemplate);
        }
    }
}
