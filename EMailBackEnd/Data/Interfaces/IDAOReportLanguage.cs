using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOReportLanguage
    {
        IList<ReportLanguage> Find(int id);
        IList<ReportLanguage> FindByIdLanguage(int idLanguage, int idStrategy);
        IList<ReportLanguage> Find(int idLanguage, int idStrategy, string key);
        IDictionary<string, string> FindByLanguage(int idLanguage, int idStrategy);
    }
}
