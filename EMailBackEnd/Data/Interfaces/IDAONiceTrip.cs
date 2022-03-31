using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAONiceTrip : IDAOObjetoNegocio
    {
        IList<BaseEnvio> Find();
    }
}
