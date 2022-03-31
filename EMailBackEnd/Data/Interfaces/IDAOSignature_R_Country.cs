using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using System.Transactions;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOSignature_R_Country : IDAOObjetoNegocio
    {
        IList<Signature_R_Country> FindBySignatureId(int idSignature);
        
        void DeleteByIdSignature(int idSignature);
        
        void DeleteByIdSignature(int idSignature, TransactionScope ts);
    }
}
