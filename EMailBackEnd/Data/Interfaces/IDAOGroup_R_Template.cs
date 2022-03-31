using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using System;
using System.Transactions;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOGroup_R_Template : IDAOObjetoNegocio
    {
        void DeleteByIdTemplate(int idTemplate, int idUser);
        void DeleteByIdTemplate(int idTemplate, int idUser, TransactionScope ts);
        IList<Group_R_Template> FindByGroupId(int idGroup);
        IList<Group_R_Template> FindByTemplateId(int idTemplate);
        IList<Group_R_Template> Find(int idTemplateType, int idLocation, int idAccount, int idProduct, 
                                int idRate, int idDistributionType, DateTime effectiveDate, int idModule, int idGroupType);
        IList<Group_R_Template> Find(int idTemplateType, int idLocation, int idAccount, int idProduct,
                                        int idRate, int idDistributionType, DateTime effectiveDate, int idModule, int idGroupType, bool lazy);
        IList<Group_R_Template> Find(int idTemplateType, string countryCode, string accountCode, int idProduct,
                                string rateCode, DateTime effectiveDate, int idGroupType, bool lazy);
        IList<Group_R_Template> Find(int idTemplateType, string countryCode, string accountCode, int idProduct,
                                string rateCode, DateTime effectiveDate, int idGroupType);
        IList<Group_R_Template> Find(int idTemplate, int idAttachment, int idLocation, int idAccount, int idProduct,
                                        int idRate, int idDistributionType, DateTime effectiveDate, int idModule, int idGroupType, bool lazy);
        }
}