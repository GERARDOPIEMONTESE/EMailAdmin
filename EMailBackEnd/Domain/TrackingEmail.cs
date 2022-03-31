using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Negocio;
using EMailAdmin.BackEnd.Data;
using EMailAdmin.BackEnd.DTO;

namespace EMailAdmin.BackEnd.Domain
{
    public class TrackEmailSearch : TrackEmail
    {
        public Nullable<DateTime> CreateFrom { get; set; }
        public Nullable<DateTime> CreateTo { get; set; }
        public Nullable<DateTime> OpenFrom { get; set; }
        public Nullable<DateTime> OpenTo { get; set; }
        public int StatusOpen { get; set; }

        public TrackEmailSearch()
        {
            StatusOpen = -1;
        }
    }

    public class TrackEmail : ObjetoNegocio
    {
        #region Constants
        private const string NAME = "TrackEmail";
        #endregion

        #region Properties
        public int IdEmailLog { get; set; }
        public string Campania { get; set; }
        public string FuenteCampania { get; set; }
        public string MedioCampania { get; set; }
        public int IdTemplate { get; set; }
        public Nullable<int> IdClienteUnico { get; set; }
        public int CountryCode { get; set; }
        public int VoucherCode { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public string Evento { get; set; }
        public string jsonContenido { get; set; }

        public int Count { get; set; }
        public Nullable<DateTime> LastOpen { get; set; }
        public Nullable<Boolean> bOpen
        {
            get
            {
                return (LastOpen.HasValue && LastOpen.Value.Year > 1);
            }
        }

        public DateTime FechaCorta
        {
            get
            {
                return new DateTime(Fecha.Year, Fecha.Month, Fecha.Day);
            }
        }

        public string FechaCreacion
        {
            get
            {
                return FechaCorta.ToShortDateString();
            }
        }

        public string DateLastOpen
        {
            get
            {
                return (LastOpen.HasValue ? LastOpen.Value.ToString() : "");
            }
        }

        public string ShortDateLastOpen
        {
            get
            {
                return (LastOpen.HasValue ? LastOpen.Value.ToShortDateString() : "");
            }
        }
        
        #endregion
        
        public override FrameworkDAC.Dato.IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoTrackEmail();
        }

        public override string ObtenerNombre()
        {
            return NAME;
        }

        internal void CastInfoPax(AbstractEMailDTO dto)
        {
            Email = dto.To;
            CountryCode = dto.CountryCode;
            Documento = dto.RecipientDocumentNumber;
        }
    }

    public class TrackEmailEvent : ObjetoNegocio
    {
        #region Constants
        private const string NAME = "TrackEmailEvent";
        #endregion

        public int IdTrackEmail { get; set; }

        public override string ObtenerNombre()
        {
            return NAME;
        }

        public override FrameworkDAC.Dato.IDAOObjetoNegocio ObtenerDAO()
        {
            return DAOLocator.Instance().GetDaoTrackEmailEvent();
        }
    }
}
