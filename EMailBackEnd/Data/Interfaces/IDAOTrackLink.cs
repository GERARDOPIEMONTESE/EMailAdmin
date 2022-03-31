using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOTrackLink : IDAOObjetoNegocio
    {
        IList<TrackLink> Find(TrackLinkSearch filter);
    }

    public interface IDAOTrackLinkEvent : IDAOObjetoNegocio
    {
        IList<TrackLinkEvent> FindFechas(int IdTrackLink);
    }
}

