using System.Collections.Generic;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOSignature : IDAOObjetoNegocio
    {
        IList<Signature> FindAll();

        IList<Signature> FindByFilters(int idType, int idCountry, string name);

        IList<Signature> FindByFilters(int idType, int idCountry);

        IList<Signature> FindByName(string name);

        Signature Get(int id);
    }
}
