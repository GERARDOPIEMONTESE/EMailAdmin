using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain.External;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOBIPaxCumpleanos
    {
        IList<PaxCumpleanos> Find();
    }

    public interface IDAOBIPaxContinuaCompra
    {
        IList<PaxContinuaCompra> Find();

        int? GetIdLote();
    }
}
