using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOLinkType
    {
        LinkType Get(int id);

        LinkType Get(string code);

        IList<LinkType> FindAll();
    }
}
