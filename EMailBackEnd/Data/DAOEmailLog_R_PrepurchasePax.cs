using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkDAC.Dato;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Parametro;
using EMailAdmin.BackEnd.Data.Interfaces;
using CapaNegocioDatos.CapaDatos;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOEmailLog_R_PrepurchasePax : DAOObjetoNegocio<EmailLog_R_PrepurchasePax>, IDAOEmailLog_R_PrepurchasePax
    {
        bool bSinIds = false;
        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override Parametros ParametrosCrear(EmailLog_R_PrepurchasePax objetoNegocio)
        {
             var parameters = new Parametros();
             parameters.AgregarParametro("IdEmailLog", objetoNegocio.EmailLog.Id);
             parameters.AgregarParametro("CodigoPaxBox", objetoNegocio.CodigoPaxBox);
             parameters.AgregarParametro("CodigoVerif", objetoNegocio.CodigoVerif);
             parameters.AgregarParametro("CountryCode", objetoNegocio.Pais.Codigo);
             if (objetoNegocio.VoucherGroup != null) parameters.AgregarParametro("VoucherGroup", objetoNegocio.VoucherGroup);
             return parameters;
        }

        protected override Parametros ParametrosModificar(EmailLog_R_PrepurchasePax objetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override Parametros ParametrosEliminar(EmailLog_R_PrepurchasePax ObjetoNegocio)
        {
            throw new NotImplementedException();
        }

        protected override void Completar(EmailLog_R_PrepurchasePax ObjetoPersistido, System.Data.SqlClient.SqlDataReader dr)
        {
            if (!bSinIds) ObjetoPersistido.Id = Convert.ToInt32(dr["IdEmailLog_R_PrepurchasePax"]);
            ObjetoPersistido.CodigoPaxBox = Convert.ToInt32(dr["CodigoPaxBox"]);
            ObjetoPersistido.CodigoVerif = dr["CodigoVerif"].ToString();
            ObjetoPersistido.VoucherGroup = dr["VoucherGroup"].ToString();
            ObjetoPersistido.Pais = DAOPais.Instancia().ObtenerPorCodigo(dr["CountryCode"].ToString());
            if (!bSinIds && dr["IdEmailLog"] != DBNull.Value)
            {
                ObjetoPersistido.EmailLog = new EMailLog();
                DAOLocator.Instance().GetDaoEMailLog().CompletarLazy(ObjetoPersistido.EmailLog, dr);
            }
        }        

        public EmailLog_R_PrepurchasePax Find(int CodigoPaxBox, string codigoVerif, int countryCode, string voucherGroup)
        {
            bSinIds = true;
            var parameters = new Parametros();
            if (CodigoPaxBox > 0)  parameters.AgregarParametro("CodigoPaxBox", CodigoPaxBox);
            parameters.AgregarParametro("CodigoVerif", codigoVerif);
            parameters.AgregarParametro("VoucherGroup", voucherGroup);
            parameters.AgregarParametro("CountryCode", countryCode);
            return Obtener(new Filtro(parameters, "dbo.EmailLog_R_PrepurchasePax_Tx_CodigoVerif"));
        }

        public IList<EmailLog_R_PrepurchasePax> Find(int CodigoPaxBox, string CodigoVerif, string Group, int CountryCode)
        {            
            var parameters = new Parametros();
            parameters.AgregarParametro("CodigoPaxBox", CodigoPaxBox);
            parameters.AgregarParametro("CodigoVerif", CodigoVerif);
            parameters.AgregarParametro("VoucherGroup", Group);
            parameters.AgregarParametro("CountryCode", CountryCode);
            return Buscar(new Filtro(parameters, "dbo.EmailLog_R_PrepurchasePax_Tx_Filters"));
        }

    }
}
