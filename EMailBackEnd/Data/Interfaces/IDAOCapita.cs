using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOCapita
    {
        IList<Capita> FindAll(int countryCode, string descripcion, string plan);
        Dictionary<int, string> GetCondicionesTipoDocumento();
    }
}
