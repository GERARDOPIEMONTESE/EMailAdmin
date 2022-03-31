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
    public class DAOTrackLink : DAOObjetoNegocio<TrackLink>, IDAOTrackLink
    {
        protected override string NombreConnectionString()
        {
            return "EmailAdmin";
        }

        protected override Parametros ParametrosCrear(TrackLink objetoNegocio)
        {
            var parametros = new Parametros();

            parametros.AgregarParametro("IdEmailLog", objetoNegocio.IdEmailLog);
            parametros.AgregarParametro("IdLink", objetoNegocio.IdLink);
            parametros.AgregarParametro("UrlDestino", objetoNegocio.UrlDestino);
            parametros.AgregarParametro("CountryCode", objetoNegocio.CountryCode);
            if (objetoNegocio.VoucherCode > 0)
                parametros.AgregarParametro("VoucherCode", objetoNegocio.VoucherCode);
            parametros.AgregarParametro("IdTemplate", objetoNegocio.IdTemplate);
            parametros.AgregarParametro("IdClienteUnico", objetoNegocio.IdClienteUnico);

            return parametros;
        }

        protected override Parametros ParametrosModificar(TrackLink objetoNegocio)
        {
            var parametros = new Parametros();

            parametros.AgregarParametro("IdEmailLog", objetoNegocio.IdEmailLog);
            parametros.AgregarParametro("IdLink", objetoNegocio.IdLink);
            parametros.AgregarParametro("UrlDestino", objetoNegocio.UrlDestino);

            return parametros;
        }

        public IList<TrackLink> Find(TrackLinkSearch filter)
        {
            Parametros parametros = new Parametros();
            int IdLink = -1;
            if (filter.IdEmailLog>0) parametros.AgregarParametro("IdEmailLog", filter.IdEmailLog);
            if (!string.IsNullOrEmpty(filter.LinkName)) parametros.AgregarParametro("LinkName", filter.LinkName);
            if (filter.IdLink > 0) IdLink =  filter.IdLink;            
            if (IdLink >0 ) parametros.AgregarParametro("IdLink", IdLink);
            if (filter.Open.HasValue) parametros.AgregarParametro("Open", filter.Open);
            if (filter.CreateFrom.HasValue) parametros.AgregarParametro("CreateFrom", Convert.ToDateTime(filter.CreateFrom.Value.ToShortDateString() + " 00:00:00"));
            if (filter.CreateTo.HasValue) parametros.AgregarParametro("CreateTo", Convert.ToDateTime(filter.CreateTo.Value.ToShortDateString() + " 23:59:59"));
            if (filter.OpenFrom.HasValue) parametros.AgregarParametro("OpenFrom", Convert.ToDateTime(filter.OpenFrom.Value.ToShortDateString() + " 00:00:00"));
            if (filter.OpenTo.HasValue) parametros.AgregarParametro("OpenTo", Convert.ToDateTime(filter.OpenTo.Value.ToShortDateString() + " 23:59:59"));            
            if (filter.Count>0) parametros.AgregarParametro("Count", filter.Count);
            if (filter.VoucherCode > 0) parametros.AgregarParametro("VoucherCode", filter.VoucherCode);
            if (filter.CountryCode > 0) parametros.AgregarParametro("CountryCode", filter.CountryCode);
            if (filter.IdTemplate > 0) parametros.AgregarParametro("IdTemplate", filter.IdTemplate);
            if (filter.IdClienteUnico.HasValue) parametros.AgregarParametro("IdClienteUnico", filter.IdClienteUnico.Value);

            return Buscar(new Filtro(parametros, "dbo.TrackLink_Tx_filtros"));
        }

        protected override Parametros ParametrosEliminar(TrackLink ObjetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override void Completar(TrackLink ObjetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            ObjetoPersistido.Id = Convert.ToInt32(dr["IdTrackEmailLink"]);
            ObjetoPersistido.IdEmailLog = Convert.ToInt32(dr["IdEmailLog"]);
            ObjetoPersistido.IdLink = Convert.ToInt32(dr["IdLink"]);
            ObjetoPersistido.Link=new Link(){
                Id = ObjetoPersistido.IdLink,
                Name = dr["LinkName"].ToString()
            };
            ObjetoPersistido.Template = new Template();
            if (dr["IdTemplate"] != DBNull.Value)
            {
                ObjetoPersistido.IdTemplate = Convert.ToInt32(dr["IdTemplate"]);
                ObjetoPersistido.Template.Id = ObjetoPersistido.IdTemplate;
                ObjetoPersistido.Template.Name = dr["TemplateName"].ToString();
            };
            ObjetoPersistido.UrlDestino=	dr["UrlDestino"].ToString();
            ObjetoPersistido.Fecha = Convert.ToDateTime( dr["CreationDate"]);
            ObjetoPersistido.Count = Convert.ToInt32(dr["Count"]);

            if (dr["IdClienteUnico"] != DBNull.Value)
                ObjetoPersistido.IdClienteUnico = Convert.ToInt32(dr["IdClienteUnico"]);
            if (dr["VoucherCode"] != DBNull.Value)
                ObjetoPersistido.VoucherCode = Convert.ToInt32(dr["VoucherCode"]);
            if (dr["CountryCode"] != DBNull.Value)
                ObjetoPersistido.CountryCode = Convert.ToInt32(dr["CountryCode"]);
            if (dr["ModificationDate"] != DBNull.Value)
                ObjetoPersistido.LastOpen = Convert.ToDateTime(dr["ModificationDate"]);

        }
    }

    public class DAOTrackLinkEvent : DAOObjetoNegocio<TrackLinkEvent>, IDAOTrackLinkEvent
    {
        protected override string NombreConnectionString()
        {
            return "EmailAdmin";
        }

        protected override void Completar(TrackLinkEvent ObjetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            ObjetoPersistido.Id = Convert.ToInt32(dr["IdTrackEmailLinkEvent"]);
            ObjetoPersistido.IdTrackEmailLink = Convert.ToInt32(dr["IdTrackEmailLink"]);
            ObjetoPersistido.Fecha = Convert.ToDateTime(dr["CreationDate"]);
        }

        public IList<TrackLinkEvent> FindFechas(int IdTrackLink)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdTrackEmailLink", IdTrackLink);

            return Buscar(new Filtro(parameters, "dbo.TrackEmailLinkEvent_Tx_IdTrackEmailLink"));
        }
        
        protected override Parametros ParametrosCrear(TrackLinkEvent objetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override Parametros ParametrosModificar(TrackLinkEvent objetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override Parametros ParametrosEliminar(TrackLinkEvent ObjetoNegocio)
        {
            throw new NotImplementedException();
        }

      
    }
}
