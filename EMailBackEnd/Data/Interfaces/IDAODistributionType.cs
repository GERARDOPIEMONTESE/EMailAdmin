using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAODistributionType
    {
        DistributionType Get(int idDistributionType);

        DistributionType Get(string code);

        IList<DistributionType> FindAll();
    }
}
