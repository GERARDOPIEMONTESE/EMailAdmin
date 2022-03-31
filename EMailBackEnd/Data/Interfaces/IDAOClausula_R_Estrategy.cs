using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOClausula_R_Estrategy
    {
        IList<Clausula_R_Estrategy> FindByEstrategy(int CodigoPais, int IdEstrategy);
    }
}
