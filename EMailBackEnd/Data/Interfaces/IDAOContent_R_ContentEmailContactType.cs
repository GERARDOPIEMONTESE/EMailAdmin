using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using System.Transactions;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOContent_R_ContentEmailContactType : IDAOObjetoNegocio
    {
        IList<Content_R_ContentEmailContactType> Find(int idContent);

        void DeleteByIdContent(int idContent, int idUser);

        void DeleteByIdContent(int idContent, int idUser, TransactionScope ts);
    }
}