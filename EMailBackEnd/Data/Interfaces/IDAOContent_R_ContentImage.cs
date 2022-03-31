using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;
using System.Collections.Generic;
using System.Transactions;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOContent_R_ContentImage : IDAOObjetoNegocio
    {
        IList<Content_R_ContentImage> Find(int idContent, int order);

        void DeleteByIdContent(int idContent, int idUser);

        void DeleteByIdContent(int idContent, int idUser, TransactionScope ts);
    }
}
