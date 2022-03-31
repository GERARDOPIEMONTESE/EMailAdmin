using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;
using EMailAdmin.BackEnd.Data;

namespace EMailAdmin.BackEnd.Domain
{
    public class TrackLinkSearch: TrackLink
    {
        public Nullable<DateTime> CreateFrom { get; set; }
        public Nullable<DateTime> CreateTo { get; set; }
        public Nullable<DateTime> OpenFrom { get; set; }
        public Nullable<DateTime> OpenTo { get; set; }
        public Nullable<Boolean> Open { get; set; }
        public int StatusOpen { get; set; }
        public string LinkName { get; set; }
        public bool Graficar { get; set; }

        public TrackLinkSearch()
        {
            Link = new Link();
            StatusOpen = -1;
            Graficar = false;
        }
    }

    public class TrackLink : ObjetoNegocio
    {
        #region Constants
        private const string NAME = "TrackEmailLink";
        #endregion

        #region Properties
        public int IdEmailLog { get; set; }        
        public int IdLink { get; set; }
        public int IdTemplate { get; set; }
        public Nullable<int> IdClienteUnico { get; set; }
        public int CountryCode { get; set; }
        public int VoucherCode { get; set; }
        public string UrlDestino { get; set; }
        public int Count { get; set; }

        public Link Link { get; set; }
        public Template Template { get; set; } 

        public Nullable<DateTime> LastOpen { get; set; }
        public Nullable<Boolean> bOpen
        {
            get
            {
                return (LastOpen.HasValue && LastOpen.Value.Year > 1);
            }
        }

        public string Link_Name
        {
            get
            {
                return (Link != null ? Link.Name : "");
            }
        }

        public string TemplateName
        {
            get
            {
                return (Template != null ? Template.Name : "");
            }
        }

        public string DateLastOpen
        {
            get
            {
                return (LastOpen.HasValue ? LastOpen.Value.ToString() : "");
            }
        }

        #endregion

        public override FrameworkDAC.Dato.IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoTrackLink();
        }

        public override string ObtenerNombre()
        {
            return NAME;
        }
       
    }

    public class TrackLinkEvent : ObjetoNegocio
    {
        #region Constants
        private const string NAME = "TrackLinkEvent";
        #endregion

        public int IdTrackEmailLink { get; set; }
        public DateTime Fecha { get; set; }

        public override string ObtenerNombre()
        {
            return NAME;
        }


        public override FrameworkDAC.Dato.IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoTrackLinkEvent();
        }
    }
}
