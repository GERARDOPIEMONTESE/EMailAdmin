using System.Collections.Generic;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOEMailContact : IDAOObjetoNegocio
    {
        IList<EMailContact> FindAll();

        IList<EMailContact> Find(string name);

        IList<EMailContact> Find(string name, int idLocation);

        IList<EMailContact> Find(string name, int idLocation, int idEMailContactType);

        IList<EMailContact> Find(int idEMailContactType, int idLocation);

        EMailContact Get(int id);
    }
}
