using System.Collections.Generic;
using System.Transactions;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOEMailContactLocation : IDAOObjetoNegocio
    {
        void DeleteAll(int idEMailContact, TransactionScope ts);

        IList<EMailContactLocation> FindAll();

        IList<EMailContactLocation> FindByIdEMailContact(int idEMailContact);
    }
}