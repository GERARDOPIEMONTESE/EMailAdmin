using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOSignatureType : IDAOObjetoNegocio
    {
        SignatureType Get(int id);

        SignatureType GetByCode(string code);

        IList<SignatureType> FindAll();

        IList<SignatureType> FindByFilters(string description);
    }
}
