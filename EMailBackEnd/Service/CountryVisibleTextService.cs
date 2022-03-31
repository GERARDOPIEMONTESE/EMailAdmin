using EMailAdmin.BackEnd.Exceptions;
using EMailAdmin.BackEnd.Service.Interfaces;
using EMailAdmin.BackEnd.Data.Interfaces;

namespace EMailAdmin.BackEnd.Service
{
    public class CountryVisibleTextService : ICountryVisibleTextService
    {
        public IDAOCountryVisibleText DaoCountryVisibleText { get; set; }

        #region ICountryVisibleTextService Members

        public void Save(Domain.CountryVisibleText countryVisibleText)
        {
            if (countryVisibleText.Name != "" && countryVisibleText.Countries.Count > 0 && countryVisibleText.Content.Count > 0 && countryVisibleText.CountryVisibleTextTypes.Count > 0)
            {
                DaoCountryVisibleText.Persistir(countryVisibleText);
            }
            else
            {
                throw new NonSavedObjectException("CountryVisibleText not saved");
            }
        }

        public void Delete(Domain.CountryVisibleText countryVisibleText)
        {
            if (countryVisibleText.Id != 0)
            {
                DaoCountryVisibleText.Eliminar(countryVisibleText);
            }
            else
            {
                throw new NonEliminatedObjectException("CountryVisibleText not deleted");
            }
        }

        #endregion
    }
}
