using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Home
{
    public class Clausula_R_EstrategyHome
    {
        public static IList<Clausula_R_Estrategy> FindByEstrategy(int CodigoPais ,int IdEstrategy)
        {
            return DAOLocator.Instance().GetDaoClausula_R_Estrategy().FindByEstrategy(CodigoPais, IdEstrategy);
        }
    }
}
