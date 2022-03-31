using System.Collections.Generic;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOEMailAddress : IDAOObjetoNegocio
    {
       EMailAddress Get(int id);

       IList<EMailAddress> FindAll();
       
       IList<EMailAddress> FindByFilters(string name, string address);
    }
}
