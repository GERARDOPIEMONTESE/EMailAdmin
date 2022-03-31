using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.DTO;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Service.Interfaces
{
    public interface IEMailLogService
    {
        EMailLog InitLog(AbstractEMailDTO dto);

        void UpdateLog(AbstractEMailDTO dto, EMailLog log);

        void SaveLog(EMailLog log);

        void FinishLog(Template template, EMailLog log);

        void RegisterEMailReception(int countryCode, string voucherCode, int idTemplateType);

        void SaveLog(AbstractEMailDTO dto, EMailLog log, int processStatus);
             
        void ZipContextInformation();

        int ViewZipContextInformation();

        string GetZipContextInformation(int countryCode, string vouchercode, string templateCode);

        void Tracking(AbstractEMailDTO dto, EMailLog log, int IdTemplate);
    }
}
