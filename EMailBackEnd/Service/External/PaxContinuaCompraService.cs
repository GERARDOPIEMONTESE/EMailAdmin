using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain.External;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Service.Interfaces;

namespace EMailAdmin.BackEnd.Service.External
{
    public class PaxContinuaCompraService : IPaxContinuaCompraService
    {
        public IDAOBIPaxContinuaCompra DaoBIPaxContinuaCompra { get; set; }
        public IDAOEMailLogCheck DaoEMailLogCheck { get; set; }

        public IList<PaxContinuaCompra> GetAll()
        {
            return DaoBIPaxContinuaCompra.Find();
        }

        public bool CheckSendEmail(PaxContinuaCompra pax, int IdTemplateType)
        {
            EMailLog log = DaoEMailLogCheck.CheckSendEmailContinuaCompra(pax, IdTemplateType);
            return (log.Id == 0 || (log.Id > 0 && log.ProcessStatus != EMailLog.OK));
        }
        
        public int? GetIdLote()
        {
            return DaoBIPaxContinuaCompra.GetIdLote();
        }
    }
}
