using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Domain.Information;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOWelcomeBackInformation
    {
        IList<WelcomeBackInformation> FindEffectiveEndDate();
    }
}
