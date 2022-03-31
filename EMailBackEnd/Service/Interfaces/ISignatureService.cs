using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Service.Interfaces
{
    public interface ISignatureService
    {
        void Save(Signature signature);
        void Delete(Signature signature);
    }
}
