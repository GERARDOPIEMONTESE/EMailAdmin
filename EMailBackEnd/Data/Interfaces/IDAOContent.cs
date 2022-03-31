using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;
using System.Transactions;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOContent : IDAOObjetoNegocio
    {
        IList<Content> Find(int idTemplate);

        Content Get(int id);

        void DeleteAll(int idTemplate, TransactionScope ts);
    }
}
