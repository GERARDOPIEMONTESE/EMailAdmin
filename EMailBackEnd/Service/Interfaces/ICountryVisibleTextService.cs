using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Service.Interfaces
{
    public interface ICountryVisibleTextService
    {
        void Save(CountryVisibleText countryVisibleText);
        void Delete(CountryVisibleText countryVisibleText);
    }
}
