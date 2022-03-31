using EMailAdmin.BackEnd.Exceptions;
using EMailAdmin.BackEnd.Service.Interfaces;
using EMailAdmin.BackEnd.Data.Interfaces;

namespace EMailAdmin.BackEnd.Service
{
    public class SignatureService : ISignatureService
    {
        public IDAOSignature DaoSignature { get; set; }

        #region ISignatureService Members

        public void Save(Domain.Signature signature)
        {
            if (signature.Name != "" && signature.Countries.Count > 0 && signature.Content.Count > 0 && signature.SignatureTypes.Count > 0)
            {
                DaoSignature.Persistir(signature);
            }
            else
            {
                throw new NonSavedObjectException("Signature not saved");
            }
        }

        public void Delete(Domain.Signature signature)
        {
            if (signature.Id != 0)
            {
                DaoSignature.Eliminar(signature);
            }
            else
            {
                throw new NonEliminatedObjectException("Signature not deleted");
            }
        }

        #endregion
    }
}
