using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain.External;

namespace EMailAdmin.BackEnd.Service.Interfaces
{
    public interface IPaxContinuaCompraService
    {
        IList<PaxContinuaCompra> GetAll();

        bool CheckSendEmail(PaxContinuaCompra pax, int IdTemplateType);

        Nullable<int> GetIdLote();
    }
}
