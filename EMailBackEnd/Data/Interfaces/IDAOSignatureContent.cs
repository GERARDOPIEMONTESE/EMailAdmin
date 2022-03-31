using System.Collections.Generic;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;
using System.Transactions;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOSignatureContent : IDAOObjetoNegocio
    {
        IList<SignatureContent> GetByIdSignature(int idSignature);

        SignatureContent Get(int id);

        void DeleteByIdSignature(int idSignature);

        void DeleteByIdSignature(int idSignature, TransactionScope ts);
    }
}
