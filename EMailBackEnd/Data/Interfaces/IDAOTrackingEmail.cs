using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOTrackEmail : IDAOObjetoNegocio
    {
        IList<TrackEmail> Find(TrackEmailSearch filter);
    }

    public interface IDAOTrackEmailEvent : IDAOObjetoNegocio
    {   
        IList<TrackEmailEvent> FindFechas(int IdTrackEmail);
    }
}
