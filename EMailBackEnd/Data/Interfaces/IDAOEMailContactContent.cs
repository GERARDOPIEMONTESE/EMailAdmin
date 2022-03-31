using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;
using System.Transactions;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOEMailContactContent : IDAOObjetoNegocio
    {
        void DeleteAll(int IdEMailContact, TransactionScope ts);

        IList<EMailContactContent> FindAll();

        IList<EMailContactContent> FindByIdEMailContact(int idEMailContact);

        EMailContactContent Get(int id);
    }
}
