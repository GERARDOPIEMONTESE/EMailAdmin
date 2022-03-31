using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Home;
using EMailAdmin.BackEnd.Service.Interfaces;
using CapaNegocioDatos.CapaNegocio;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain.External;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Service.External
{
    public class PaxCumpleanosService : IPaxCumpleanosService
    {
        public IDAOBIPaxCumpleanos DaoBIPaxCumpleanos { get; set; }
        public IDAOEMailLogCheck DaoEMailLogCheck { get; set; }

        public IList<PaxCumpleanos> GetAll()
        {
            return DaoBIPaxCumpleanos.Find();
        }

        public bool CheckSendEmail(PaxCumpleanos pax, int IdTemplateType)
        {
            EMailLog log = DaoEMailLogCheck.CheckSendEmailHappyBirth(pax, IdTemplateType);
            return (log.Id == 0 || (log.Id>0 && log.ProcessStatus != EMailLog.OK));
        }
    }
}
