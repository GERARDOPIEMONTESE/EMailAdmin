using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using System.Transactions;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOTemplate_R_Attachment : IDAOObjetoNegocio
    {
        IList<Template_R_Attachment> FindByTemplateId(int idTemplate);
        void DeleteByIdTemplate(int idTemplate, int idUser);
        void DeleteByIdTemplate(int idTemplate, int idUser, TransactionScope ts);
        Template_R_Attachment FindByTemplateAttach(int p1, int p2);
    }
}
