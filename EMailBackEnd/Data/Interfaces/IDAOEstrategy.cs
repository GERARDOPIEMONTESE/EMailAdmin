using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOEstrategy
    {
        Estrategy Get(int id);

        Estrategy GetByCode(string code);

        IList<Estrategy> FindAll();

        Estrategy GetByClass(string className);
    }
}
