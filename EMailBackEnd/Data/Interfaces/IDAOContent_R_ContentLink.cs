using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using System.Transactions;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOContent_R_ContentLink : IDAOObjetoNegocio
    {
        IList<Content_R_ContentLink> Find(int idContent, int order);

        void DeleteByIdContent(int idContent, int idUser);

        void DeleteByIdContent(int idContent, int idUser, TransactionScope ts);
    }
}
