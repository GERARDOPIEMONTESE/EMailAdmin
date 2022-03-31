using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Data.Interfaces;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOTrackEmail : DAOObjetoNegocio<TrackEmail>, IDAOTrackEmail
    {
        protected override string NombreConnectionString()
        {
            return "EmailAdmin";
        }

        protected override Parametros ParametrosCrear(TrackEmail objetoNegocio)
        {
            var parametros = new Parametros();

            parametros.AgregarParametro("IdEmailLog", objetoNegocio.IdEmailLog);
            parametros.AgregarParametro("Campania", objetoNegocio.Campania);
            parametros.AgregarParametro("FuenteCampania", objetoNegocio.FuenteCampania);
            parametros.AgregarParametro("MedioCampania", objetoNegocio.MedioCampania);
            parametros.AgregarParametro("Evento", objetoNegocio.Evento);
            parametros.AgregarParametro("IdTemplate", objetoNegocio.IdTemplate);
            parametros.AgregarParametro("CountryCode", objetoNegocio.CountryCode);
            if (objetoNegocio.VoucherCode > 0)
                parametros.AgregarParametro("VoucherCode", objetoNegocio.VoucherCode);
            parametros.AgregarParametro("Documento", objetoNegocio.Documento);
            parametros.AgregarParametro("Email", objetoNegocio.Email);
            parametros.AgregarParametro("IdClienteUnico", objetoNegocio.IdClienteUnico);

            return parametros;
        }

        public IList<TrackEmail> Find(TrackEmailSearch filter)
        {
            Parametros parametros = new Parametros();
            if (filter.IdEmailLog > 0) parametros.AgregarParametro("IdEmailLog", filter.IdEmailLog);
            if (!string.IsNullOrEmpty(filter.Email)) parametros.AgregarParametro("Email", filter.Email);
            if (!string.IsNullOrEmpty(filter.Documento)) parametros.AgregarParametro("Documento", filter.Documento);
            if (!string.IsNullOrEmpty(filter.Campania)) parametros.AgregarParametro("Campania", filter.Campania);
            if (filter.CreateFrom.HasValue) parametros.AgregarParametro("CreateFrom", Convert.ToDateTime(filter.CreateFrom.Value.ToShortDateString() + " 00:00:00"));
            if (filter.CreateTo.HasValue) parametros.AgregarParametro("CreateTo", Convert.ToDateTime(filter.CreateTo.Value.ToShortDateString() + " 23:59:59"));
            if (filter.OpenFrom.HasValue) parametros.AgregarParametro("OpenFrom", Convert.ToDateTime(filter.OpenFrom.Value.ToShortDateString() + " 00:00:00"));
            if (filter.OpenTo.HasValue) parametros.AgregarParametro("OpenTo",  Convert.ToDateTime(filter.OpenTo.Value.ToShortDateString() + " 23:59:59"));
            if (filter.LastOpen.HasValue) parametros.AgregarParametro("Open", Convert.ToDateTime(filter.LastOpen.Value.ToShortDateString() + " 00:00:00"));
            if (filter.Count > 0) parametros.AgregarParametro("Count", filter.Count);
            if (filter.CountryCode > 0) parametros.AgregarParametro("CountryCode", filter.CountryCode);
            if (filter.VoucherCode > 0) parametros.AgregarParametro("VoucherCode", filter.VoucherCode);
            if (filter.IdTemplate > 0) parametros.AgregarParametro("IdTemplate", filter.IdTemplate);
            if (filter.IdClienteUnico.HasValue) parametros.AgregarParametro("IdClienteUnico", filter.IdClienteUnico.Value);
            if (filter.StatusOpen != -1) parametros.AgregarParametro("Open", ( filter.StatusOpen==0 ?false:true));

            return Buscar(new Filtro(parametros, "dbo.TrackEmail_Tx_filtros"));
        }

        protected override Parametros ParametrosModificar(TrackEmail objetoNegocio)
        {
            var parametros = new Parametros();

            parametros.AgregarParametro("IdEmailLog", objetoNegocio.IdEmailLog);
            parametros.AgregarParametro("Evento", objetoNegocio.Evento);

            return parametros;
        }

        protected override Parametros ParametrosEliminar(TrackEmail ObjetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override void Completar(TrackEmail ObjetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            ObjetoPersistido.Id = Convert.ToInt32(dr["IdTrackEmail"]);
            ObjetoPersistido.Campania = dr["Campania"].ToString();
            ObjetoPersistido.FuenteCampania = dr["FuenteCampania"].ToString();
            ObjetoPersistido.MedioCampania = dr["MedioCampania"].ToString();
            ObjetoPersistido.Evento = dr["Evento"].ToString();
            ObjetoPersistido.IdTemplate = Convert.ToInt32(dr["IdTemplate"]);
            ObjetoPersistido.Email = dr["Email"].ToString();
            ObjetoPersistido.Fecha = Convert.ToDateTime(dr["CreationDate"]);
            ObjetoPersistido.Count = Convert.ToInt32(dr["Count"]);

            if (dr["IdClienteUnico"] != DBNull.Value)
                ObjetoPersistido.IdClienteUnico = Convert.ToInt32(dr["IdClienteUnico"]);
            if (dr["CountryCode"] != DBNull.Value)
                ObjetoPersistido.CountryCode = Convert.ToInt32(dr["CountryCode"]);
            if (dr["VoucherCode"] != DBNull.Value)
                ObjetoPersistido.VoucherCode = Convert.ToInt32(dr["VoucherCode"]);
            if (dr["Documento"] != DBNull.Value)
                ObjetoPersistido.Documento = dr["Documento"].ToString();
            if (dr["ModificationDate"] != DBNull.Value)
                ObjetoPersistido.LastOpen = Convert.ToDateTime(dr["ModificationDate"]);
        }

    }

    public class DAOTrackEmailEvent : DAOObjetoNegocio<TrackEmailEvent>, IDAOTrackEmailEvent
    {
        protected override string NombreConnectionString()
        {
            return "EmailAdmin";
        }

        protected override void Completar(TrackEmailEvent ObjetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            ObjetoPersistido.Id = Convert.ToInt32(dr["IdTrackEmailEvent"]);
            ObjetoPersistido.IdTrackEmail = Convert.ToInt32(dr["IdTrackEmail"]);
            ObjetoPersistido.Fecha = Convert.ToDateTime(dr["CreationDate"]);
        }

        public IList<TrackEmailEvent> FindFechas(int IdTrackEmail)
        {
            Parametros parametros = new Parametros();
            parametros.AgregarParametro("IdTrackEmail", IdTrackEmail);

            return Buscar(new Filtro(parametros, "dbo.TrackEmailEvent_Tx_IdTrackEmail"));
        }

        protected override Parametros ParametrosCrear(TrackEmailEvent objetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override Parametros ParametrosModificar(TrackEmailEvent objetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override Parametros ParametrosEliminar(TrackEmailEvent ObjetoNegocio)
        {
            throw new NotImplementedException();
        }
    }
}
