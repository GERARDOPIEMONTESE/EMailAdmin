using System.Collections.Generic;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using System;
using System.Transactions;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOGroup_R_Template_Association
    {
        IList<ReportAssociation> Find(int idTemplateType, string groupName, int IdLocacion, string accountCode, int idProduct,
                                string rateCode, DateTime effectiveDate, int idGroupType, int asociados);  
    }
}