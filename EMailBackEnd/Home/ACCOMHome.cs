using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Home
{
    public class ACCOMHome
    {
        public static IList<ACCOMNotIssue> FindAll()
        {
            return DAOLocator.Instance().GetDAOACCOMNotIssue().GetACCOMNotIssue();
        }
    }
}
