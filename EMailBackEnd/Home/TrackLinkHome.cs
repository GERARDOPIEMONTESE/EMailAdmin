using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.BackEnd.Home
{
    public class TrackEmailHome
    {
        public static IList<TrackEmail> FindAll(TrackEmailSearch filtros)
        {
            return DAOLocator.Instance().GetDaoTrackEmail().Find(filtros);
        }

        public static IList<TrackEmailEvent> FindFechas(int IdTrackEmail)
        {
            return DAOLocator.Instance().GetDaoTrackEmailEvent().FindFechas(IdTrackEmail);
        }
    }

    public class TrackLinkHome
    {
        public static IList<TrackLink> FindAll(TrackLinkSearch filtros)
        {
            return DAOLocator.Instance().GetDaoTrackLink().Find(filtros);
        }

        public static IList<TrackLinkEvent> FindFechas(int IdTrackEmailLink)
        {
            return DAOLocator.Instance().GetDaoTrackLinkEvent().FindFechas(IdTrackEmailLink);
        }
    }
}
