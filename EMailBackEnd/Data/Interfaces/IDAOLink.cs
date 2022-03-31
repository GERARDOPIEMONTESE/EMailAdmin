using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using System.Collections.Generic;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOLink : IDAOObjetoNegocio
    {
        Link Get(int id);

        Link Get(string name);

        IList<Link> BuscarLinks(string nombre, string url);

        IList<Link> LinksFixed();

        IList<Link> LinksFixed(string linkType);
    }
}
