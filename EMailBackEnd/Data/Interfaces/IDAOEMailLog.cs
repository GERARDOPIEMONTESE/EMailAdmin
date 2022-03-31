using System.Collections.Generic;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Domain.External;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOEMailLog : IDAOObjetoNegocio
    {
        IList<EMailLog> FindAllPendings();

        IList<EMailLog> FindAllPendings(string voucherCode, int idLocation, int idStatus, string fromDate, string toDate);

        IList<EMailLog> Find(int countryCode, string voucherCode, int idTemplateType);

        IList<EMailLog> FindLog(int countryCode, string voucherCode, int idTemplateType);

        IList<EMailLog> Find(int id);
        
        EMailLog GetLastByProcessStatus(int processStatus);

        bool IsVoucherValid(string countryCode, string voucherCode);

        IList<EMailLog> FindZipPending();

        EMailLog Obtener(int id);

        void CompletarLazy(EMailLog eMailLog, System.Data.SqlClient.SqlDataReader dr);
    }

    public interface IDAOEMailLogCheck
    {
        EMailLog CheckSendEmailHappyBirth(PaxCumpleanos pax, int IdTemplateType);

        EMailLog CheckSendEmailContinuaCompra(PaxContinuaCompra pax, int IdTemplateType);
    }
}
